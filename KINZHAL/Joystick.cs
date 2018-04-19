using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Multimedia;
using SharpDX.DirectInput;

namespace Joystik
{
    public delegate void MessageAppeared(string str);
    public delegate void MyJoystickStateReceived(MyJoystickState state);
    public class MyJoystick
    {
        Joystick joystik;
        Multimedia.Timer tmr = new Multimedia.Timer();
        JoystickState state = new JoystickState();
        MyJoystickState curJoyState = new MyJoystickState();

        int currentTry = 0, cntTry = 3;

        ushort periodInit = 5000;
        /// <summary>
        /// Интервал между попытками соединения при его нарушении, мс. От 100 до 65535. 10000 - по умолчанию.
        /// </summary>
        public ushort PeriodInit
        {
            get { return periodInit; }
            set 
            {
                if (periodInit < 100) periodInit = 100;
                else periodInit = value; 
            }
        }

        ushort periodWorking = 100;
        /// <summary>
        /// Интервал между запросами состояния джойстика, мс. От 10 от 65535. 100 - по умолчанию.
        /// </summary>
        public ushort PeriodWorking
        {
            get { return periodWorking; }
            set 
            {
                if (value < 10) periodWorking = 10;
                else periodWorking = value; 
            }
        }

        /// <summary>
        /// Событие иницирует возврат строкового сообщения
        /// </summary>
        public event MessageAppeared msgAppeared;
        
        /// <summary>
        /// Событие, возникающее при возврате состояния основных датчиков (оси и кнопки) джойстика
        /// </summary>
        public event MyJoystickStateReceived myJoystickStateReceived;
        public MyJoystick()
        {
            //bool b = InitJoystick();
            tmr.Period = periodWorking;
            tmr.Resolution = 0;
     
            tmr.Tick += tmr_Tick;
            //tmr.Start();
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            try
            {
                state = joystik.GetCurrentState();
                curJoyState.x = state.X;
                curJoyState.y = state.Y;
                curJoyState.buttons[0] = state.Buttons[0];
                curJoyState.buttons[1] = state.Buttons[1];
                curJoyState.buttons[2] = state.Buttons[2];
                curJoyState.buttons[3] = state.Buttons[3];
                curJoyState.buttons[4] = state.Buttons[4];

                if (myJoystickStateReceived != null) myJoystickStateReceived(curJoyState);
            }
            catch (Exception ex)
            {
                if (msgAppeared != null)
                {
                    if (ex is SharpDX.SharpDXException)
                        msgAppeared(String.Format("{0}",((SharpDX.SharpDXException)ex).Descriptor.Description, periodInit));
                    else
                        msgAppeared(ex.Message);

                    currentTry++;
                    if (!InitJoystick())
                    {
                        tmr.Period = periodInit;
                        if (currentTry >= cntTry)
                        {
                            msgAppeared(String.Format("Подключите джойстик и перезагрузите программу"));
                            tmr.Stop();
                        }
                        else
                            msgAppeared(String.Format("Попытка #{0} переподключения через {1} мс.",currentTry, periodInit));
                    }
                    else
                    {
                        tmr.Period = periodWorking;
                        currentTry = 0;
                    }
                }
            }
        }

        public void Close()
        {
            if (tmr.IsRunning)
            {
                tmr.Stop();
            }
            joystik.Unacquire();
            
        }
        public bool InitJoystick()
        {
            var directInput = new DirectInput();
            var joystikGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                joystikGuid = deviceInstance.InstanceGuid;

            if (joystikGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                    joystikGuid = deviceInstance.InstanceGuid;

            //if (msgAppeared != null && joystikGuid != Guid.Empty) msgAppeared(joystikGuid.ToString());

            if (joystikGuid != Guid.Empty)
                joystik = new Joystick(directInput, joystikGuid);
            else
            {
                msgAppeared("Устройство не найдено");
                return false;
            }

            if (msgAppeared != null) msgAppeared("Найден джойстик с GUID: " + joystikGuid);

            //joystik.Properties.BufferSize = 128;

            joystik.Acquire();
            return true;
        }

        public void StartWorking()
        {
            bool b = InitJoystick();
            tmr.Period = periodWorking;
            currentTry = 0;
            tmr.Start();
        }
    }

    public class MyJoystickState
    {
        public int x, y;
        public bool[] buttons;

        public MyJoystickState()
        {
            x = 0; y = 0;
            buttons = new bool[5];
        }
    }
}
