using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UcanDotNET;
using Joystik;
using System.Xml.Serialization;
using System.IO;

namespace KINZHAL
{
    public partial class frmMain : Form
    {
        Device devSUO,
            //devPKP_PU2,
            //devPU_K,
            //devPK_K,
            //devPU_NR,
            //devPK_NR,
            //devPU_RD,
            //devPK_RD,
            //devBU,
            //devUSO,
            devPPK,
            devBroadCast;
        //MTTD;

        Message
            comCaptureDevice,   //Захват устройства
            comReleaseDevice,   //Освобождение устройства

            a5_1,   //запрос абонента ППК
            a5_2,   //Установить оптический фильтр
            a5_3,   //Изменить режим работы
            a5_4,   //Перейти в стабилизацию
            a5_5,   //Сопровождение по углам и скоростям_1
            a5_6,   //Сопровождение по углам и скоростям_2
            a5_7,   //Сопровождение по углам и скоростям_3
            a5_8,   //Сопровождение по углам и скоростям_4
            a5_9,   //Сопровождение по скоростям_1
            a5_10,  //Сопровождение по скоростям_2
            a5_11,  //Сопровождение по скоростям_3
            a5_12,  //Переброс по заданным углам_1
            a5_13,  //Переброс по заданным углам_2
            a5_14,  //Переброс по заданным углам_3
            a5_15,  //Запрос диагностики
            a5_16,  //Сменить поле зрения прицела
            a5_17,  //Измерить дальность
            a5_18,  //Включить обогрев стекла
            a5_19,  //Запросить наработку ЛД
            a5_20,  //Запросить состояние прицела
            a5_21,  //Провести калибровку ТПВ матрицы
            a5_22,  //Перейти в транспортное положение
            a5_23,  //Установить параметр
            a5_24,  //Запросить параметр
            a5_25,  //Включить ТПВ матрицу
            a5_26,  //Отключить ТПВ матрицу
            a5_27,  //Запросить координаты визирной оси


            a6_1,   //Причина старта абонента ППК
            a6_2_0, //Команда принята к исполнению
            a6_2_1, //Команда не принята к исполнению
            a6_2_2, //Команда не поддерживается абонентом
            a6_2_3, //Команда не может быть выполнена прямо сейчас (абонент временно занят)
            a6_2_4, //Команда не может быть выполнена в данном режиме работы абонента
            a6_2_5, //Ошибка в параметрах команды

            a6_3,   //Команда не может быть выполнена (абонент захвачен)
            a6_4,   //Отклик абонента ППК
            a6_5,   //Состояние прицела_1
            a6_6,   //Состояние прицела_2
            a6_7,   //Состояние прицела_3
            a6_8,   //Команда исполнена
            a6_9,   //Код ошибки
            a6_10,  //Наработка ЛД
            a6_11,  //Измеренная дальность
            a6_12,  //Время измерения ЛД
            a6_13,  //Диагностика
            a6_14,  //Значение параметра
            a6_15,  //Координаты визирной оси_1
            a6_16,  //Координаты визирной оси_2
            a6_17,  //Координаты визирной оси_3
            a6_18;  //Координаты визирной оси_4


        List<Device> Devices = new List<Device>();  //список устройств, что мы имитируем
        List<Message> lstMessages = new List<Message>();
        List<string> lstErr = new List<string>();
        USBcanServer CanSrv = new USBcanServer();
        MyJoystick myJoystick;
        Message lastMsg; //храним ссылку на последнюю отправленную команду

        string[] status_mas = { "/", "-", "\\", "|" };
        bool[] lastJoystickStateButtons = { false, false, false, false, false };
        byte index_status_mas = 0;
        public byte Index_status_mas
        {
            get
            {
                index_status_mas++;
                if (index_status_mas > status_mas.Length - 1) index_status_mas = 0;
                return index_status_mas;
            }
        }


        public frmLog _frmLog;
        public frmPU _frmPU;
        public frmDebug _frmDebug;
        public frmSettings _frmSettings;
        public frmCoord _frmCoord;

        byte bRet;

        double kHor = 0, kVert = 0; //коэффициенты масштабирования для использования джойстика
        Values valuesScrolls = new Values(); //в структуре храним значения нум-полей

        int selectedIndex = 0;
        byte cntOfAnglesRate = 0;

        public int iCnt1280 = 0, iCnt1280_ = 0, iCnt1280__ = 0;
        int cntFPSrate = 0;
        Multimedia.Timer tmrSendA5_9_11 = new Multimedia.Timer();
        bool flag_a5_3;

        private SettingXML sets;

        public SDI_Capture sdi;


        //Bitmap bitmap;
        //Graphics imgr;
        public frmMain()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            DeviceInit();
            MessageInit();
            sdi = new SDI_Capture(this);

            _frmLog = new frmLog();
            _frmPU = new frmPU();
            _frmDebug = new frmDebug();
            _frmSettings = new frmSettings(this);
            _frmCoord = new frmCoord();
            //_frmVideoSet = new frmVideoSettings();
            _frmLog._frmMain =  _frmPU._frmMain = _frmDebug._frmMain = _frmCoord._frmMain = this;

            //pict1.Image = sdi.bitmap;
            sets = new SettingXML();

            sdi.eventFPS += sdi_eventFPS;
            sdi.eventFPS_FrameReceived += sdi_eventFPS_FrameReceived;
            sdi.updateBitmap += sdi_updateBitmap;

            LoadSettings();

            //cmbA5_5_MarkColor.SelectedIndex = 0;
            //cmbA5_5_MarkColor.SelectedItem = cmbA5_5_MarkColor.Items[0];

            tmrSendA5_1.Enabled = true; //стартуем таймер посылки запроса от выбранного абонента в ППК

            tmrSendA5_9_11.Period = 20; //стартуем таймер посылки команды 
            tmrSendA5_9_11.Tick += TmrSendA5_9_11_Tick;
            tmrSendA5_9_11.Resolution = 1;
            tmrSendA5_9_11.Mode = Multimedia.TimerMode.Periodic;
            //пока не врубаем таймер. Может потом пригодиццо, поэтому не удаляем
            //tmrSend1540.Start();   


            numSpeedAz.Tag = 0;
            numSpeedUM.Tag = 0;
            numAngleAZ.Tag = 0;
            numAngleUM.Tag = 0;

            lstErr.Add("готовность БУ");
            lstErr.Add("готовность ТВ");
            lstErr.Add("готовность ТПВ");
            lstErr.Add("резерв");
            lstErr.Add("готовность ЛД");
            lstErr.Add("резерв");
            lstErr.Add("резерв");
            lstErr.Add("готовность привода");
            lstErr.Add("неисправность БУ");
            lstErr.Add("неисправность ТВ");
            lstErr.Add("неисправность ТПВ");
            lstErr.Add("резерв");
            lstErr.Add("неисправность ЛД");
            lstErr.Add("резерв");
            lstErr.Add("резерв");
            lstErr.Add("неисправность приводов");

            //txtErrorCode.Text = String.Join(Environment.NewLine, lstErr);
        }

        private void TmrSendA5_9_11_Tick(object sender, EventArgs e)
        {
            A5_9_11();
        }

        void sdi_eventFPS_FrameReceived(double fps)
        {
            Invoke((MethodInvoker)delegate()
            {
                _frmSettings.lblFPS_Received.Text = "FPS_FrameReceived: " + String.Format("{0}", Math.Round(fps, 1));
            });
        }

       
        void sdi_updateBitmap(Bitmap bmp)
        {
            Invoke((MethodInvoker)delegate()
            {
                //imgr.DrawImage(bmp, 0, 0, 1920, 1080);
                if (pict1 != null)
                    pict1.Image = bmp;
            });
        }


        void sdi_eventFPS(double fps)
        {
            // cntFPSrate++;
            //if (cntFPSrate >= 60)
            //{
                Invoke((MethodInvoker)delegate()
                {
                    _frmSettings.lblFPSTotal.Text = "FPS_Total: " + String.Format("{0}", Math.Round(fps, 1));
                });
                cntFPSrate = 0;
            //}
        }


        void cboDevices_DropDownClosed(object sender, EventArgs e)
        {
            tipMessage.Hide(cboDevices);
        }
        
        void cboDevices_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) { return; } // added this line thanks to Andrew's comment
            string text = cboDevices.GetItemText(cboDevices.Items[e.Index]);
            //string text = Devices[e.Index].Description;
            e.DrawBackground();
            using (SolidBrush br = new SolidBrush(e.ForeColor))
            { e.Graphics.DrawString(text, e.Font, br, e.Bounds); }
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && cboDevices.DroppedDown)
            //{ tipMessage.Show(text, cboDevices, e.Bounds.Right, e.Bounds.Bottom); }
            { tipMessage.Show(Devices[e.Index].Description, cboDevices, e.Bounds.Right, e.Bounds.Bottom); }
            e.DrawFocusRectangle();
        }

        void FillDevices()
        {
            //ППК не должно быть в списке доступных устройств
            Devices.Clear();
            Devices.Add(devSUO);

            cboDevices.DrawMode = DrawMode.OwnerDrawFixed;
            cboDevices.DropDownClosed += cboDevices_DropDownClosed;
            cboDevices.DataSource = Devices;
            cboDevices.DisplayMember = "Name";
            //cboDevices.Items.AddRange(Devices.Select(a => a.Name).ToArray());
            cboDevices.SelectedIndex = 0;
        }

        void SendMessage(Device devSource, Device devDestination, Message msg)
        {
            lastMsg = msg;

            //реализация формирования CAN-пакета и отправка
            byte channel = (byte)USBcanServer.eUcanChannel.USBCAN_CHANNEL_CH0;
            USBcanServer.tCanMsgStruct[] canMsg = new USBcanServer.tCanMsgStruct[1];
            int iCnt = 0;

            //canMsg[0].m_bData = new byte[msg.Data.Length];
            canMsg[0].m_bData = new byte[8];
            canMsg[0].m_bDLC = msg.Data == null ? (byte)0 : (byte)msg.Data.Length;

            if(msg.Data!=null)
                Array.Copy(msg.Data, canMsg[0].m_bData, msg.Data.Length);
            
            //canMsg[0].m_bData = msg.Data;
            canMsg[0].m_bFF = (byte)USBcanServer.eUcanMsgFrameFormat.USBCAN_MSG_FF_EXT;
            canMsg[0].m_dwTime = 1000;

            canMsg[0].m_dwID = devSource.Address; // &0x3f;
            canMsg[0].m_dwID |= devDestination.Address << 6;
            canMsg[0].m_dwID |= msg.Descriptor << 12;
            canMsg[0].m_dwID |= msg.Priority << 24;


            bRet = CanSrv.WriteCanMsg(channel, ref canMsg, ref iCnt);

            //if (!_frmDebug.IsHandleCreated) return;
            Invoke((MethodInvoker)delegate
            {
                _frmDebug.lblCanReturnRECEIVED.Text = ((USBcanServer.eUcanReturn)bRet).ToString();
            });


            //Запись в лог об отправке команды
            if ((msg.Descriptor == 1538 && _frmDebug.chk1538.Checked) ||
                        (msg.Descriptor == 1546 && _frmDebug.chk1546.Checked) ||
                        (msg.Descriptor == 1554 && _frmDebug.chk1554.Checked) ||
                        (msg.Descriptor == 1 && _frmDebug.chkZapros.Checked) ||
                        (msg.Descriptor == 2 && _frmDebug.chkAck.Checked) ||
                        (msg.Descriptor == 1540 && _frmDebug.chk1540.Checked))
                UpdateCountsDebug(msg.Descriptor, devSource.Address);
            else
                _frmLog.AddToList(canMsg[0], true);
        }

        void MessageInit()
        {
            comCaptureDevice =  new Message(0, 256, 0, 1);
            comReleaseDevice =  new Message(1, 257, 0, 1);

            a5_1 =              new Message(0, 1, 1500, 0);
            a5_2 =              new Message(20, 312, 0, 1);
            a5_3 =              new Message(20, 320,  0, 1);
            a5_4 =              new Message(20, 328,  0, 0);
            a5_5 =              new Message(20, 336,  0, 8);
            a5_6 =              new Message(20, 344,  0, 7);
            a5_7 =              new Message(20, 352,  0, 7);
            a5_8 =              new Message(20, 360,  0, 3);
            a5_9 =              new Message(20, 368, 0, 8);
            a5_10 =             new Message(20, 376, 0, 7);
            
            a5_11 =             new Message(20, 384, 0, 5);
            a5_12 =             new Message(20, 392, 0, 8);
            a5_13 =             new Message(20, 400, 0, 7);
            a5_14 =             new Message(20, 408, 0, 5);
            a5_15 =             new Message(20, 416, 0, 0);
            a5_16 =             new Message(20, 424, 0, 2);
            a5_17 =             new Message(20, 432, 0, 5);
            a5_18 =             new Message(20, 440, 0, 2);
            a5_19 =             new Message(20, 448, 0, 0);
            a5_20 =             new Message(20, 456, 0, 0);

            a5_21 =             new Message(20, 464, 0, 0);
            a5_22 =             new Message(20, 472, 0, 0);
            a5_23 =             new Message(20, 476, 0, 8);
            a5_24 =             new Message(20, 480, 0, 4);
            a5_25 =             new Message(20, 488, 0, 0);
            a5_26 =             new Message(20, 496, 0, 0);
            a5_27 =             new Message(20, 504, 0, 0);

            a6_1 =              new Message(0, 3, 0, 1);

            a6_2_0 =            new Message(31, 1280, 0, 2);
            a6_2_1 =            new Message(31, 1281, 0, 2);
            a6_2_2 =            new Message(31, 1282, 0, 2);
            a6_2_3 =            new Message(31, 1283, 0, 2);
            a6_2_4 =            new Message(31, 1284, 0, 2);
            a6_2_5 =            new Message(31, 1285, 0, 2);

            a6_3 =              new Message(31, 1286, 0, 2);
            a6_4 =              new Message(0, 2, 0, 0);

            a6_5 =              new Message(20, 1538, 500, 7);
            a6_6 =              new Message(20, 1546, 500, 8);
            a6_7 =              new Message(20, 1554, 500, 3);
            a6_8 =              new Message(20, 1562, 0, 2);
            a6_9 =              new Message(20, 1570, 0, 4);
            a6_10 =             new Message(20, 1578, 0, 4);
            a6_11 =             new Message(20, 1586, 0, 8);
            a6_12 =             new Message(20, 1594, 0, 8);
            a6_13 =             new Message(20, 1602, 0, 8);
            a6_14 =             new Message(20, 1610, 0, 8);
            a6_15 =             new Message(20, 1618, 0, 7);
            a6_16 =             new Message(20, 1626, 0, 7);
            a6_17 =             new Message(20, 1634, 0, 7);
            a6_18 =             new Message(20, 1642, 0, 7);


            lstMessages.Clear();
            lstMessages.Add(comCaptureDevice);
            lstMessages.Add(comReleaseDevice);
            lstMessages.Add(a5_1);
            lstMessages.Add(a5_2);
            lstMessages.Add(a5_3);
            lstMessages.Add(a5_4);
            lstMessages.Add(a5_5);
            lstMessages.Add(a5_6);
            lstMessages.Add(a5_7);
            lstMessages.Add(a5_8);
            lstMessages.Add(a5_9);
            lstMessages.Add(a5_10);
            lstMessages.Add(a5_11);
            lstMessages.Add(a5_12);
            lstMessages.Add(a5_13);
            lstMessages.Add(a5_14);
            lstMessages.Add(a5_15);
            lstMessages.Add(a5_16);
            lstMessages.Add(a5_17);
            lstMessages.Add(a5_18);
            lstMessages.Add(a5_19);
            lstMessages.Add(a5_20);
            lstMessages.Add(a5_21);
            lstMessages.Add(a5_22);
            lstMessages.Add(a5_23);
            lstMessages.Add(a5_24);
            lstMessages.Add(a5_25);
            lstMessages.Add(a5_26);
            lstMessages.Add(a5_27);

            lstMessages.Add(a6_1);
            lstMessages.Add(a6_2_0);
            lstMessages.Add(a6_2_1);
            lstMessages.Add(a6_2_2);
            lstMessages.Add(a6_2_3);
            lstMessages.Add(a6_2_4);
            lstMessages.Add(a6_2_5);
            lstMessages.Add(a6_3);
            lstMessages.Add(a6_4);
            lstMessages.Add(a6_5);
            lstMessages.Add(a6_6);
            lstMessages.Add(a6_7);
            lstMessages.Add(a6_8);
            lstMessages.Add(a6_9);
            lstMessages.Add(a6_10);
            lstMessages.Add(a6_11);
            lstMessages.Add(a6_12);
            lstMessages.Add(a6_13);

            lstMessages.Add(a6_14);
            lstMessages.Add(a6_15);
            lstMessages.Add(a6_16);
            lstMessages.Add(a6_17);
            lstMessages.Add(a6_18);
        }

        void DeviceInit()
        {
            devSUO = new Device(36, "Панель управления прицела командира панорамного 7605.00.00.000 - 1");
            devPPK = new Device(35, "Блок управления прицела командира панорамного 7605.00.00.000");
            devBroadCast = new Device(63, "Условное устройство для широковещания");

            FillDevices();
        }

        public void btnA5_11_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_11);
        }

        public void btnA5_12_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_12);
        }

        public void btnA5_14_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_14);
        }

        public void btnA5_15_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_15);
        }

        public void btnA5_16_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_16);
        }

        public void btnA5_17_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_17);
        }

        public void btnA5_18_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_18);
        }

        public void btnA5_19_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_19);
        }

        private void btnA5_23_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_23);
        }

        private void btnA5_26_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_26);
        }

        

        void A5_27_Process()
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_27);
        }

        private void numAngleAZ_ValueChanged(object sender, EventArgs e)
        {
            double d = (double)numAngleAZ.Value * CMR.SPEED_AZ_UM;
            d = Math.Round(d);
            int i = (int)d;
            numAngleAZ.Tag = i;
            trackAngleAz.Value = i;
        }

        private void numAngleUM_ValueChanged(object sender, EventArgs e)
        {
            double d = (double)numAngleUM.Value * CMR.SPEED_AZ_UM;
            d = Math.Round(d);
            int i = (int)d;
            numAngleUM.Tag = i;
            trackAngleUM.Value = i;
        }

        private void trackAngleAz_ValueChanged(object sender, EventArgs e)
        {
            lblTrackAngleAZ.Text = trackAngleAz.Value.ToString();
            if ((int)numAngleAZ.Tag == trackAngleAz.Value) return;

            numAngleAZ.Value = (decimal)(trackAngleAz.Value / CMR.SPEED_AZ_UM);
        }

        private void trackAngleUM_ValueChanged(object sender, EventArgs e)
        {
            lblTrackAngleUM.Text = trackAngleUM.Value.ToString();
            if ((int)numAngleUM.Tag == trackAngleUM.Value) return;

            numAngleUM.Value = (decimal)(trackAngleUM.Value / CMR.SPEED_AZ_UM);
        }

        private void radUPZ_TV_Click(object sender, EventArgs e)
        {
            a5_16.Data[0] = 0;
            a5_16.Data[1] = 0;
            if (radUPZ_TV.Checked) a5_16.Data[0] = 1;
            if (radUPZ2_TV.Checked) a5_16.Data[0] = 2;
            if (radUPZ_TPV.Checked) a5_16.Data[1] = 1;
            if (radUPZ2_TPV.Checked) a5_16.Data[1] = 2;

            SendMessage(Devices[selectedIndex], devPPK, a5_16);
        }

        private void btnZaprosLD_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_19);
        }

        private void btnZaprosStatePricel_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_20);
        }

        private void btnCalibrateTPVMatr_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_21);
        }

        private void btnVklObogrev_Click(object sender, EventArgs e)
        {
            a5_18.Data[0] = (byte)(int)numVklObogrev.Value;
            a5_18.Data[1] = (byte)((int)numVklObogrev.Value >> 8);
            SendMessage(Devices[selectedIndex], devPPK, a5_18);
        }

        private void btnZaprosCoord_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_27);
        }

        private void btnVklTPVMatr_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_25);
        }

        private void btnOtklTPVMatr_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_26);
        }

        private void radA5_3_Click(object sender, EventArgs e)
        {
            RadioButton rad = (RadioButton)sender;
            if (rad.Checked)
            {
                a5_3.Data[0] = 0;
                switch (rad.Name.Substring(8, rad.Name.Length - 8))
                {
                    default:
                    case "Dezh":
                        break;
                    case "Rab":
                        a5_3.Data[0] = 1;
                        break;
                    case "Teh":
                        a5_3.Data[0] = 2;
                        break;
                }

                SendMessage(Devices[selectedIndex], devPPK, a5_3); 
            }
        }

        private void btnA5_4_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_4);
        }

        private void btnA5_22_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_22);
        }

        private void btnA5_15_Click_1(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPPK, a5_15);
        }

        private void numSpeedAz_ValueChanged(object sender, EventArgs e)
        {
            double d = (double)numSpeedAz.Value * CMR.SPEED_AZ_UM;
            d = Math.Round(d);
            int i = (int)d;
            numSpeedAz.Tag = i;
            trackSpeedAz.Value = i;   
        }

        private void trackAz_ValueChanged(object sender, EventArgs e)
        {
            lblTrackAzValue.Text = trackSpeedAz.Value.ToString();
            if ((int)numSpeedAz.Tag == trackSpeedAz.Value) return;

            numSpeedAz.Value = (decimal) (trackSpeedAz.Value / CMR.SPEED_AZ_UM);
        }

        private void координатыВизирнойОсиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmCoord.Show();
        }

        private void trackUM_ValueChanged(object sender, EventArgs e)
        {
            lblTrackUM.Text = trackSpeedUM.Value.ToString();
            if ((int)numSpeedUM.Tag == trackSpeedUM.Value) return;

            numSpeedUM.Value = (decimal)(trackSpeedUM.Value / CMR.SPEED_AZ_UM);
        }

        private void numSpeedUM_ValueChanged(object sender, EventArgs e)
        {
            double d = (double)numSpeedUM.Value * CMR.SPEED_AZ_UM;
            d = Math.Round(d);
            int i = (int)d;
            numSpeedUM.Tag = i;
            trackSpeedUM.Value = i;
        }

        private void chkA5_9_11_CheckedChanged(object sender, EventArgs e)
        {
            if (chkA5_9_11.Checked)
            {
                chkA5_9_11.BackColor = Color.FromArgb(255, 128, 0);
                chkA5_9_11.ForeColor = Color.White;
                tmrSendA5_9_11.Start();
            }
            else
            {
                chkA5_9_11.BackColor = SystemColors.Control;
                chkA5_9_11.ForeColor = SystemColors.ControlText;
                tmrSendA5_9_11.Stop();
            }
        }

        void A5_9_11()
        {
            Invoke((MethodInvoker)delegate
            {
                if (radBySpeed.Checked)
                {
                    a5_9.Data[0] = 0;
                    a5_9.Data[1] = radSSK.Checked ? (byte)0 : (byte)1;
                    a5_9.Data[2] = (byte)trackSpeedAz.Value;
                    a5_9.Data[3] = (byte)(trackSpeedAz.Value >> 8);
                    a5_9.Data[4] = (byte)trackSpeedUM.Value;
                    a5_9.Data[5] = (byte)(trackSpeedUM.Value >> 8);
                    a5_9.Data[6] = (byte)(int)numFocusTV.Value;
                    a5_9.Data[7] = (byte)((int)numFocusTV.Value >> 8);
                    SendMessage(Devices[selectedIndex], devPPK, a5_9);

                    a5_10.Data[0] = 1;
                    a5_10.Data[1] = (byte)(int)numVremyaExpTV.Value;
                    a5_10.Data[2] = (byte)((int)numVremyaExpTV.Value >> 8);
                    a5_10.Data[3] = (byte)numUsilMatrTV.Value;
                    a5_10.Data[4] = (byte)numContrastTV.Value;
                    a5_10.Data[5] = (byte)(int)numFocusTPV.Value;
                    a5_10.Data[6] = (byte)((int)numFocusTPV.Value >> 8);
                    SendMessage(Devices[selectedIndex], devPPK, a5_10);

                    a5_11.Data[0] = 2;
                    a5_11.Data[1] = (byte)(int)numVremyaExpTPV.Value;
                    a5_11.Data[2] = (byte)((int)numVremyaExpTPV.Value >> 8);
                    a5_11.Data[3] = (byte)numUsilMatrTPV.Value;
                    a5_11.Data[4] = (byte)numContrastTPV.Value;
                    SendMessage(Devices[selectedIndex], devPPK, a5_11);
                }
                else if (radByAngle.Checked)
                {
                    a5_5.Data[0] = 0;
                    a5_5.Data[1] = radSSK.Checked ? (byte)0 : (byte)1;
                    a5_5.Data[2] = (byte)trackAngleAz.Value;
                    a5_5.Data[3] = (byte)(trackAngleAz.Value >> 8);
                    a5_5.Data[4] = (byte)trackAngleUM.Value;
                    a5_5.Data[5] = (byte)(trackAngleUM.Value >> 8);
                    a5_5.Data[6] = (byte)trackSpeedAz.Value;
                    a5_5.Data[7] = (byte)(trackSpeedAz.Value >> 8);
                    SendMessage(Devices[selectedIndex], devPPK, a5_5);

                    a5_6.Data[0] = 1;
                    a5_6.Data[1] = (byte)trackSpeedUM.Value;
                    a5_6.Data[2] = (byte)(trackSpeedUM.Value >> 8);
                    a5_6.Data[3] = (byte)(int)numFocusTV.Value;
                    a5_6.Data[4] = (byte)((int)numFocusTV.Value >> 8);
                    a5_6.Data[5] = (byte)(int)numVremyaExpTV.Value;
                    a5_6.Data[6] = (byte)((int)numVremyaExpTV.Value >> 8);
                    SendMessage(Devices[selectedIndex], devPPK, a5_6);

                    a5_7.Data[0] = 2;
                    a5_7.Data[1] = (byte)numUsilMatrTV.Value;
                    a5_7.Data[2] = (byte)numContrastTV.Value;
                    a5_7.Data[3] = (byte)(int)numFocusTPV.Value;
                    a5_7.Data[4] = (byte)((int)numFocusTPV.Value >> 8);
                    a5_7.Data[5] = (byte)(int)numVremyaExpTPV.Value;
                    a5_7.Data[6] = (byte)((int)numVremyaExpTPV.Value >> 8);
                    SendMessage(Devices[selectedIndex], devPPK, a5_7);

                    a5_8.Data[0] = 3;
                    a5_8.Data[1] = (byte)numUsilMatrTPV.Value;
                    a5_8.Data[2] = (byte)numContrastTPV.Value;
                    SendMessage(Devices[selectedIndex], devPPK, a5_8);
                }
                else if (radByPerebros.Checked)
                {
                    a5_12.Data[0] = 0;
                    a5_12.Data[1] = radSSK.Checked ? (byte)0 : (byte)1;
                    a5_12.Data[2] = (byte)trackAngleAz.Value;
                    a5_12.Data[3] = (byte)(trackAngleAz.Value >> 8);
                    a5_12.Data[4] = (byte)trackAngleUM.Value;
                    a5_12.Data[5] = (byte)(trackAngleUM.Value >> 8);
                    a5_12.Data[6] = (byte)(int)numFocusTV.Value;
                    a5_12.Data[7] = (byte)((int)numFocusTV.Value >> 8); ;
                    SendMessage(Devices[selectedIndex], devPPK, a5_12);

                    a5_13.Data[0] = 1;
                    a5_13.Data[1] = (byte)(int)numVremyaExpTV.Value;
                    a5_13.Data[2] = (byte)((int)numVremyaExpTV.Value >> 8);
                    a5_13.Data[3] = (byte)numUsilMatrTV.Value;
                    a5_13.Data[4] = (byte)numContrastTV.Value;
                    a5_13.Data[5] = (byte)(int)numFocusTPV.Value;
                    a5_13.Data[6] = (byte)((int)numFocusTPV.Value >> 8);
                    SendMessage(Devices[selectedIndex], devPPK, a5_13);

                    a5_14.Data[0] = 2;
                    a5_14.Data[1] = (byte)(int)numVremyaExpTPV.Value;
                    a5_14.Data[2] = (byte)((int)numVremyaExpTPV.Value >> 8);
                    a5_14.Data[3] = (byte)numUsilMatrTPV.Value;
                    a5_14.Data[4] = (byte)numContrastTPV.Value;
                    SendMessage(Devices[selectedIndex], devPPK, a5_14);

                    chkA5_9_11.Checked = false;
                }
            }); 
        }

        private void radA5_2_Click(object sender, EventArgs e)
        {
            RadioButton rad = (RadioButton)sender;
            if (rad.Checked)
            {
                a5_2.Data[0] = 0;
                switch (rad.Name.Substring(8, rad.Name.Length - 8))
                {
                    default:
                    case "NoFilter":
                        break;
                    case "1Filter":
                        a5_2.Data[0] = 1;
                        break;
                    case "2Filter":
                        a5_2.Data[0] = 2;
                        break;
                    case "3Filter":
                        a5_2.Data[0] = 3;
                        break;
                }
                SendMessage(Devices[selectedIndex], devPPK, a5_2);
            }
        }

        public void chkPU_MouseDown(object sender, MouseEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            chk.Tag = true;

            A5_20_Process();
        }

        public void chkPU_MouseUp(object sender, MouseEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            chk.Tag = false;

            A5_20_Process();
        }

        public void A5_20_MenuTehnAndInzh()
        {
            A5_20_Process();
        }

        void A5_20_Process()
        {
            a5_20.Data[0] = 0;
            a5_20.Data[1] = 0;
            a5_20.Data[2] = 0;



            if (_frmPU.chkA5_20_Menu.Tag != null && (bool)_frmPU.chkA5_20_Menu.Tag) a5_20.Data[0] |= (1 << 0);
            if (_frmPU.chkA5_20_Left.Tag != null && (bool)_frmPU.chkA5_20_Left.Tag) a5_20.Data[0] |= (1 << 1);
            if (_frmPU.chkA5_20_Right.Tag != null && (bool)_frmPU.chkA5_20_Right.Tag) a5_20.Data[0] |= (1 << 2);
            if (_frmPU.chkA5_20_Up.Tag != null && (bool)_frmPU.chkA5_20_Up.Tag) a5_20.Data[0] |= (1 << 3);
            if (_frmPU.chkA5_20_Down.Tag != null && (bool)_frmPU.chkA5_20_Down.Tag) a5_20.Data[0] |= (1 << 4);
            if (_frmPU.chkA5_20_Obogrev.Tag != null && (bool)_frmPU.chkA5_20_Obogrev.Tag) a5_20.Data[0] |= (1 << 5);
            if (_frmPU.chkA5_20_Focus.Tag != null && (bool)_frmPU.chkA5_20_Focus.Tag) a5_20.Data[0] |= (1 << 6);
            if (_frmPU.chkA5_20_Usil.Tag != null && (bool)_frmPU.chkA5_20_Usil.Tag) a5_20.Data[0] |= (1 << 7);

            if (_frmPU.chkA5_20_Svetofilter.Tag != null && (bool)_frmPU.chkA5_20_Svetofilter.Tag) a5_20.Data[1] |= (1 << 0);
            if (_frmPU.chkA5_20_Polar.Tag != null && (bool)_frmPU.chkA5_20_Polar.Tag) a5_20.Data[1] |= (1 << 1);
            if (_frmPU.chkA5_20_Day.Tag != null && (bool)_frmPU.chkA5_20_Day.Tag) a5_20.Data[1] |= (1 << 2);
            if (_frmPU.chkA5_20_Uvel.Tag != null && (bool)_frmPU.chkA5_20_Uvel.Tag) a5_20.Data[1] |= (1 << 3);
            if (_frmPU.chkA5_20_Umen.Tag != null && (bool)_frmPU.chkA5_20_Umen.Tag) a5_20.Data[1] |= (1 << 4);
            if (_frmPU.chkA5_20_Marka.Tag != null && (bool)_frmPU.chkA5_20_Marka.Tag) a5_20.Data[1] |= (1 << 5);

            if (_frmPU.radA5_20_I.Checked) a5_20.Data[1] |= (1 << 6);
            if (_frmPU.radA5_20_Dezh.Checked) a5_20.Data[1] |= (2 << 6);


            SendMessage(Devices[selectedIndex], devPPK, a5_20);
        }

        public void radA5_20_Click(object sender, EventArgs e)
        {
            A5_20_Process();
        }

        private void btnCaptureDevice_Click(object sender, EventArgs e)
        {
            comCaptureDevice.Data[0] = devPPK.Address;

            SendMessage(Devices[selectedIndex], devBroadCast, comCaptureDevice);
        }

        private void btnReleaseDevice_Click(object sender, EventArgs e)
        {
            comReleaseDevice.Data[0] = devPPK.Address;

            SendMessage(Devices[selectedIndex], devBroadCast, comReleaseDevice);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            CanSrv.CanMsgReceivedEvent += CanSrv_CanMsgReceivedEvent;
            CanSrv.StatusEvent += CanSrv_StatusEvent;
            CanSrv.FatalDisconnectEvent += CanSrv_FatalDisconnectEvent;
            USBcanServer.ConnectEvent += USBcanServer_ConnectEvent;
            USBcanServer.DisconnectEvent += USBcanServer_DisconnectEvent;
            CanSrv.InitCanEvent += CanSrv_InitCanEvent;

            bool b = CanInit();

            myJoystick = new MyJoystick();
            myJoystick.PeriodWorking = 50;
            myJoystick.msgAppeared += myJoystick_msgAppeared;
            myJoystick.myJoystickStateReceived += myJoystick_myJoystickStateReceived;
            myJoystick.StartWorking();



            panelPict.Location = new Point(0, 0);
            panelPict.Size = new Size(1920, 1080);
            videoSourcePlayer.Parent = panelPict;

            this.HorizontalScroll.Enabled = false;


            _frmLog.Show();
            //_frmShutDown.Hide();
        }

        void CanSrv_InitCanEvent(byte bDeviceNr_p, byte bChannel_p)
        {
            //throw new NotImplementedException();
        }

        void USBcanServer_DisconnectEvent()
        {
            //CanShutdown();
        }

        void USBcanServer_ConnectEvent()
        {
            bool b = CanInit();
        }

        void CanSrv_FatalDisconnectEvent(byte bDeviceNr_p)
        {
            CanShutdown();
        }

        void myJoystick_myJoystickStateReceived(MyJoystickState state)
        {
            Invoke((MethodInvoker)delegate
            {
                //обработка нажатий кнопок джойстика
                if (state.buttons[0] != lastJoystickStateButtons[0] && state.buttons[0]) A5_27_Process(); //выполняем команду Измерить дальность
                if (state.buttons[1] != lastJoystickStateButtons[1])
                    if (state.buttons[1])
                        A5_9_11();  //Включение наведения
                if (state.buttons[2] != lastJoystickStateButtons[2]) //Шагаем вправо по ШПЗ/УПЗ/УПЗ2/ОПЗ
                    if (state.buttons[2])
                    {
                        
                    }

                if (state.buttons[3] != lastJoystickStateButtons[3])   //Меняем канал ДТК или ТПВК.
                {
                    if (state.buttons[3])
                    {
                        
                    }
                }
                if (state.buttons[4] != lastJoystickStateButtons[4])   //Шагаем влево по ШПЗ/УПЗ/УПЗ2/ОПЗ
                {
                    if (state.buttons[4])
                    {
                        
                    }
                }

                state.buttons.CopyTo(lastJoystickStateButtons, 0);

                //управление ползунками "Управление наведением ПКП-МРО по скорости А5.30"
                if (chkJoy.Checked)
                {
                    int dMiddle = ushort.MaxValue / 2 + 1;
                    kHor = (Math.Abs(trackSpeedAz.Minimum) + Math.Abs(trackSpeedAz.Maximum)) / (ushort.MaxValue - 2 * (double)_frmSettings.numCoefSensHor.Value);
                    int codeH = state.x;
                    if (Math.Abs(codeH - dMiddle) > (double)_frmSettings.numCoefSensHor.Value)
                    {
                        if (codeH > dMiddle)
                            codeH -= (int)_frmSettings.numCoefSensHor.Value;
                        else
                            codeH += (int)_frmSettings.numCoefSensHor.Value;
                    }
                    else
                        codeH = dMiddle;

                    double dCodeHor = kHor * (codeH - dMiddle);
                    int valH = (int)(Math.Round(dCodeHor * (double)_frmSettings.numCoefKrutHor.Value));
                    trackSpeedAz.Value = valH;

                    


                    kVert = (Math.Abs(trackSpeedUM.Minimum) + Math.Abs(trackSpeedUM.Maximum)) / (ushort.MaxValue - 2 * (double)_frmSettings.numCoefSensVert.Value);
                    int codeV = state.y;

                    if (Math.Abs(codeV - ushort.MaxValue / 2) > (double)_frmSettings.numCoefSensVert.Value)
                    {
                        if (codeV > ushort.MaxValue / 2)
                            codeV -= (int)_frmSettings.numCoefSensVert.Value;
                        else
                            codeV += (int)_frmSettings.numCoefSensVert.Value;
                    }
                    else
                        codeV = ushort.MaxValue / 2;

                    double dCodeVert = kVert * (codeV - ushort.MaxValue / 2);
                    int valV = (int)(Math.Round(dCodeVert * (double)_frmSettings.numCoefKrutVert.Value));
                    trackSpeedUM.Value = -1*valV;
                }
            });
        }

        void myJoystick_msgAppeared(string str)
        {
            _frmLog.AddToList(str);
        }


        void CanSrv_StatusEvent(byte bDeviceNr_p, byte bChannel_p)
        {
            USBcanServer.tStatusStruct statusStruct = new USBcanServer.tStatusStruct();
            CanSrv.GetStatus((byte)USBcanServer.eUcanChannel.USBCAN_CHANNEL_CH0, ref statusStruct);
            _frmLog.AddToList("CAN STATUS UPDATED: " + ((USBcanServer.eUcanCanStatus)statusStruct.m_wCanStatus).ToString());
            Invoke((MethodInvoker)delegate
            {
                //_frmDebug.txtStatus.Text = ((DESCR_CAN_STATUS)statusStruct.m_wCanStatus).ToString();
                //DESCR_CAN_STATUS dEnum = new DESCR_CAN_STATUS();
                _frmDebug.txtStatus.Text = string.Empty;
                for (int i = 0; i < 10; i++)
                {
                    if ((statusStruct.m_wCanStatus & (1 << i)) != 0) 
                    { 
                       _frmDebug.txtStatus.Text+= ((DESCR_CAN_STATUS)(statusStruct.m_wCanStatus & (1 << i))).ToString() + "\n";
                    }
                }
                if (_frmDebug.txtStatus.Text == string.Empty) _frmDebug.txtStatus.Text = "No_Error";
                else
                    _frmDebug.txtStatus.Text = _frmDebug.txtStatus.Text.Substring(0, _frmDebug.txtStatus.Text.Length - 1);

            });
        }

        void CanSrv_CanMsgReceivedEvent(byte bDeviceNr_p, byte bChannel_p)
        {
            byte channel = (byte)USBcanServer.eUcanChannel.USBCAN_CHANNEL_CH0;
            int iCnt = 0;
            USBcanServer.tCanMsgStruct[] msg = new USBcanServer.tCanMsgStruct[1000];

            bRet = CanSrv.ReadCanMsg(ref channel, ref msg, ref iCnt);

            if (iCnt <= 0) return;

            Invoke((MethodInvoker)delegate
            {
                _frmDebug.lblCanReturnSEND.Text = ((USBcanServer.eUcanReturn)bRet).ToString();
            });

            if (bRet == (byte)USBcanServer.eUcanReturn.USBCAN_SUCCESSFUL)
            {
                for (int i = 0; i < iCnt; i++) 
                {
                    byte dAddr = (byte)((msg[i].m_dwID >> 6) & 0x3F);
                    ushort descr = (ushort)((msg[i].m_dwID >> 12) & 0xFFF);


                    if (descr == 1280) iCnt1280++;
                    if (InvokeRequired)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            _frmDebug.lblCnt1280.Text = iCnt1280.ToString();
                        });
                    }
                    else
                        _frmDebug.lblCnt1280.Text = iCnt1280.ToString();


                    if ((descr == 1538 && _frmDebug.chk1538.Checked) ||
                        (descr == 1546 && _frmDebug.chk1546.Checked) ||
                        (descr == 1554 && _frmDebug.chk1554.Checked) ||
                        (descr == 1 && _frmDebug.chkZapros.Checked) ||
                        (descr == 2 && _frmDebug.chkAck.Checked))
                        //(descr == 2 && _frmDebug.chkAckBU.Checked && dAddr == 21))
                        UpdateCountsDebug(descr, dAddr);
                    else
                    {
                        //Отображаем в логе входящую посылку
                        _frmLog.AddToList(msg[i], false);
                    }

                    if (dAddr != 63 && dAddr != Devices[selectedIndex].Address && dAddr != 21) //не обрабатываем входящее сообщение если оно не широковещательное или же не адресовано нам. Только отображаем в логе
                        return;

                    ReactPing(msg[i]); //функция реагирования в ответ на ту или иную посылку
                }
            }
        }

        void ReactPing(USBcanServer.tCanMsgStruct msg)
        {
            byte sAddr = (byte)(msg.m_dwID & 0x3F);
            byte dAddr = (byte)((msg.m_dwID >> 6) & 0x3F);
            ushort descr = (ushort)((msg.m_dwID >> 12) & 0xFFF);

            //определение входящей команды
            Message inMsg = GetMessageByDescriptor(descr);
            if (inMsg == null)
            {
                _frmLog.AddToList("Команда не найдена!");
                return;
            }

            Array.Copy(msg.m_bData, inMsg.Data, inMsg.Data.Length);
            //inMsg.Data = msg.m_bData;

            
            if (inMsg.Type == typeMessage.COMMAND && dAddr!=63)
            {
                //заполняем команду подтверждения дескриптором
                a6_2_0.Data[0] = (byte)descr;
                a6_2_0.Data[1] = (byte)(descr >> 8);

                SendMessage(Devices[selectedIndex], devPPK, a6_2_0); //шлем подтверждение принятой команды

            }

            switch (descr)
            {
                case 1538:  //Состояние прицела в 3-х пакетах
                    //cntOfAnglesRate++;
                    //if (cntOfAnglesRate >= 10) //прореживаем вывод на экран
                    //{
                        UpdateAnglesAndSpeeds0(inMsg.Data);
                        //cntOfAnglesRate = 0;
                    //}
                    break;
                case 1546:
                    UpdateAnglesAndSpeeds1(inMsg.Data);
                    break;
                case 1554:     
                    UpdateAnglesAndSpeeds2(inMsg.Data);
                    break;
                case 1280: //в зависимости от прошлой посланной команды реагируем на пришедшее подтверждение
                    switch (lastMsg.Descriptor)
                    {
                        case 256:
                            UpdateDeviceCaptured();
                            break;
                        case 257:
                            UpdateDeviceReleased();
                            break;
                    }
                    break;
                case 256: //обработка широковещалки с объявлением попытки захвата устройства ПКП_БУ
                    UpdateDeviceCapturedBroadcast(sAddr);
                    break;
                case 257:
                    UpdateDeviceReleased();
                    break;
                case 3:
                    UpdateReasonStartPPK(inMsg.Data);
                    break;
                case 1570:
                    UpdateErrorCode(inMsg.Data);
                    break;
                case 1578:
                    UpdateNarabotkaLD(inMsg.Data);
                    break;
                case 1586:
                    UpdateAims(inMsg.Data);
                    break;
                case 1594:
                    UpdateVremyaIzmerLD(inMsg.Data);
                    break;
                case 1602:
                    UpdateDiagnostic(inMsg.Data);
                    break;
                case 1618:
                    UpdateCoordinates0(inMsg.Data);
                    break;
                case 1626:
                    UpdateCoordinates1(inMsg.Data);
                    break;
                case 1634:
                    UpdateCoordinates2(inMsg.Data);
                    break;
                case 1642:
                    UpdateCoordinates3(inMsg.Data);
                    break;
                    
            }
        }

        private void UpdateAims(byte[] buf)
        {
            string sNum = "", sAim1 = "", sAim2 = "", sAim3 = "";
            UInt16 num = (UInt16)(buf[0] + buf[1] << 8);
            UInt16 aim1 = (UInt16)(buf[2] + buf[3] << 8);
            UInt16 aim2 = (UInt16)(buf[4] + buf[5] << 8);
            UInt16 aim3 = (UInt16)(buf[6] + buf[7] << 8);
            sNum = num.ToString();

            if (num == 1 && aim1 == 0) sNum = "Промах";
            else if (num == 65535) sNum = "Уголок";
            else if (num == 1 && aim1 == 65535) sNum = "Нет старта";
            Invoke((MethodInvoker)delegate
            {
                txtNumOfAims.Text = sNum;
                txtAim1.Text = aim1.ToString();
                txtAim2.Text = aim2.ToString();
                txtAim3.Text = aim3.ToString();
            });
        }

        private void UpdateCoordinates3(byte[] buf)
        {
            Invoke((MethodInvoker)delegate
            {
                _frmCoord.txtTPV_UPZ_X.Text = (buf[1] + buf[2] << 8).ToString();
                _frmCoord.txtTPV_SHPZ_Y.Text = (buf[3] + buf[4] << 8).ToString();
                _frmCoord.txtTPV_SHPZ_X.Text = (buf[5] + buf[6] << 8).ToString();
            });
        }

        private void UpdateCoordinates2(byte[] buf)
        {
            Invoke((MethodInvoker)delegate
            {
                _frmCoord.txtTPV_UPZ2_Y.Text = (buf[1] + buf[2] << 8).ToString();
                _frmCoord.txtTPV_UPZ2_X.Text = (buf[3] + buf[4] << 8).ToString();
                _frmCoord.txtTPV_UPZ_Y.Text = (buf[5] + buf[6] << 8).ToString();
            });
        }

        private void UpdateCoordinates1(byte[] buf)
        {
            Invoke((MethodInvoker)delegate
            {
                _frmCoord.txtTV_UPZ_X.Text = (buf[1] + buf[2] << 8).ToString();
                _frmCoord.txtTV_SHPZ_Y.Text = (buf[3] + buf[4] << 8).ToString();
                _frmCoord.txtTV_SHPZ_X.Text = (buf[5] + buf[6] << 8).ToString();
            });
        }

        private void UpdateCoordinates0(byte[] buf)
        {
            Invoke((MethodInvoker)delegate
            {
                _frmCoord.txtTV_UPZ2_Y.Text = (buf[1] + buf[2] << 8).ToString();
                _frmCoord.txtTV_UPZ2_X.Text = (buf[3] + buf[4] << 8).ToString();
                _frmCoord.txtTV_UPZ_Y.Text = (buf[5] + buf[6] << 8).ToString();
            });
        }

        private void UpdateDiagnostic(byte[] buf)
        {
            Invoke((MethodInvoker)delegate
            {
                txtDiagnostic.Text =
                (buf[0] + (buf[1] << 8) +
                (buf[2] << 16) + (buf[3] << 24) +
                (buf[4] << 32) + (buf[5] << 40) +
                (buf[6] << 48) + (buf[7] << 56)).ToString();
            });
        }

        private void UpdateVremyaIzmerLD(byte[] buf)
        {
            Invoke((MethodInvoker)delegate
            {
                txtVremyaIzmLD.Text =
                (buf[0] + (buf[1] << 8) +
                (buf[2] << 16) + (buf[3] << 24) +
                (buf[4] << 32) + (buf[5] << 40) +
                (buf[6] << 48) + (buf[7] << 56)).ToString();
            });
        }

        private void UpdateNarabotkaLD(byte[] buf)
        {
            Invoke((MethodInvoker)delegate
            {
                txtNarabotkaLD.Text = (buf[0] + (buf[1] << 8) + (buf[2] << 16) + (buf[3] << 24)).ToString();
            });
        }

        void UpdateErrorCode(byte[] buf)
        {
            UInt32 erCode = 0;
            List<string> lst = new List<string>();
            //txtErrorCode.Text = "";
            erCode = (uint)(buf[0] + (buf[1] << 8) + (buf[2] << 16) + (buf[3] << 24));
            
            for(int i = 0; i < lstErr.Count; i++)
            {
                if ((erCode & (1 << i)) != 0) lst.Add(lstErr[i]);
            }
            Invoke((MethodInvoker)delegate
            {
                lblErrorCode.Text = "Код ошибки (0х" + erCode.ToString("X") + ")";
                txtErrorCode.Text = String.Join(Environment.NewLine, lst);
            });
        }

        void UpdateReasonStartPPK(byte[] buf)
        {
            Invoke((MethodInvoker)delegate
            {
                string str = "";
                switch (buf[0])
                {
                    case 0: str = "Включение питания"; break;
                    case 1: str = "Сигнал аппаратного сброса"; break;
                    case 2: str = "Срабатывание защиты ВИП"; break;
                    case 3: str = "Программный рестарт"; break;
                    default: str = "Резерв"; break;
                }
                txtA6_1.Text = str;
            });
        }

        void UpdateDeviceCapturedBroadcast(byte sAddr)
        {
            Invoke((MethodInvoker)delegate
            {
                lblCapturedDevice.Text = ((DEVICE_ADDR)sAddr).ToString();
            });
        }

        void UpdateDeviceReleased()
        {
            Invoke((MethodInvoker)delegate
            {
                lblCapturedDevice.Text = "";
            });
        }

        void UpdateDeviceCaptured()
        {
            //if (buf.Length < 2) return;
            Invoke((MethodInvoker)delegate
            {
               // ushort descr = (ushort)(buf[0] + (buf[1] << 8));
                lblCapturedDevice.Text = (Devices[selectedIndex].Name).ToString();
            });
        }

        void UpdateCountsDebug(ushort descr, byte Addr)
        {
            Invoke((MethodInvoker)delegate
            {
                switch (descr)
                {
                    case 1538:
                        if (_frmDebug.chk1538.Checked) _frmDebug.lblCnt1538.Text = (_frmDebug.iCnt1538++).ToString();
                        break;
                    case 1546:
                        if (_frmDebug.chk1546.Checked) _frmDebug.lblCnt1546.Text = (_frmDebug.iCnt1546++).ToString();
                        break;
                    case 1554:
                        if (_frmDebug.chk1554.Checked) _frmDebug.lblCnt1554.Text = (_frmDebug.iCnt1554++).ToString();
                        break;
                    case 1:
                        if (Addr == 21)
                            _frmDebug.lblCntZaprosBU.Text = _frmDebug.iCntZaprosBU++.ToString();
                        else
                            _frmDebug.lblZaprosSELECTED.Text = _frmDebug.iCntZaprosSELECTED++.ToString();
                        break;
                    case 2:
                        if (Addr == 21)
                            _frmDebug.lblCntAckBU.Text = (_frmDebug.iCntAckBU++).ToString();
                        else
                            _frmDebug.lblCntAckSELECTED.Text = (_frmDebug.iCntAckSELECTED++).ToString();
                        break;
                    case 1540:
                        _frmDebug.lbl1540.Text = (_frmDebug.iCnt1540++).ToString();
                        break;
                }

            });
        }

        void UpdateAnglesAndSpeeds0(byte[] buf)
        {
            if (buf.Length < 7) return;
            Invoke((MethodInvoker)delegate
            {
                double s1 = ((short)(buf[1] + (buf[2] << 8)) / CMR.SPEED_AZ_UM);
                double s2 = ((short)(buf[3] + (buf[4] << 8)) / CMR.SPEED_AZ_UM);
                double s3 = ((short)(buf[5] + (buf[6] << 8)) / CMR.SPEED_AZ_UM);

                txtAngleAz.Text = Math.Round(s1, 3).ToString();
                txtAngleUM.Text = Math.Round(s2, 3).ToString();
                txtSpeedAz.Text = Math.Round(s3, 3).ToString();
            });
        }

        void UpdateAnglesAndSpeeds1(byte[] buf)
        {
            if (buf.Length < 8) return;
            Invoke((MethodInvoker)delegate
            {
                string str = "";
                double s1 = ((short)(buf[1] + (buf[2] << 8)) / CMR.SPEED_AZ_UM);
                txtSpeedUM.Text = Math.Round(s1, 2).ToString();
                txtStateTVpotoka.Text = buf[3] == 0 ? "Включен" : "Отключен";
                txtStateTPVpotoka.Text = buf[4] == 0 ? "Включен" : "Отключен";
                switch(buf[5] & 0x03)
                {
                    default:
                    case 0: str = "Дежурный"; break;
                    case 1: str = "Рабочий"; break;
                    case 2: str = "Технологический"; break;
                }
                txtRezhimPricela.Text = str;

                if ((buf[5] & 0x04) != 0) indPrivodaVKL.BackColor = Color.LightGreen; else indPrivodaVKL.BackColor = Color.Red;
                if ((buf[5] & 0x08) != 0) indTVMatrVKL.BackColor = Color.LightGreen; else indTVMatrVKL.BackColor = Color.Red;

                switch((buf[5]>>4) & 0x03)
                {
                    default: str = "Ошибка!"; break;
                    case 0: str = "Отключена"; break;
                    case 1: str = "Включается"; break;
                    case 2: str = "Включена"; break;
                    case 3: str = "Отключается"; break;
                }
                txtStateTPVMatr.Text = str;

                if ((buf[5] & 0x40) != 0) indLDGotov.BackColor = Color.LightGreen; else indLDGotov.BackColor = Color.Red;

                txtSystemCoord.Text = (buf[6] & 0x02) != 0 ? "ЗСК" : "ССК";
                switch ((buf[6] >> 2) & 0x03)
                {
                    default:    str = "Ошибка!"; break;
                    case 0: str = "Нет фильтра"; break;
                    case 1: str = "1-й фильтр"; break;
                    case 2: str = "2-й фильтр"; break;
                    case 3: str = "3-й фильтр"; break;
                }
                txtOpticFilterTV.Text = str;

                if ((buf[6] & 0x10) != 0) indObogrevStekla.BackColor = Color.LightGreen; else indObogrevStekla.BackColor = Color.Red;

                switch (buf[7])
                {
                    case 0: str = "Стабилизация";  break;
                    case 1: str = "Сопровождение по координатам и скоростям"; break;
                    case 2: str = "Сопровождение с заданными скоростями"; break;
                    case 3: str = "Переброс по координатам"; break;
                    case 4: str = "Транспортное положение"; break;
                    default: str = "Ошибка!"; break;
                }
                txtSposobDvizh.Text = str;
            });
        }

        void UpdateAnglesAndSpeeds2(byte[] buf)
        {
            if (buf.Length < 3) return;
            Invoke((MethodInvoker)delegate
            {
                string str = "";
                switch (buf[1])
                {
                    case 0: str = "ШПЗ"; break;
                    case 1: str = "УПЗ"; break;
                    case 2: str = "УПЗх2"; break;
                    default: str = "Ошибка!"; break;
                }
                txtPoleZrenTV.Text = str;

                switch (buf[2])
                {
                    case 0: str = "ШПЗ"; break;
                    case 1: str = "УПЗ"; break;
                    case 2: str = "УПЗх2"; break;
                    default: str = "Ошибка!"; break;
                }
                txtPoleZrenTPV.Text = str;
            });
        }

        Message GetMessageByDescriptor(ushort descr)
        {
            Message msg = lstMessages.Find(n => n.Descriptor == descr);
            return msg;
        }

        void CanShutdown()
        {
            bRet = CanSrv.Shutdown();
            if (bRet != (byte)USBcanServer.eUcanReturn.USBCAN_SUCCESSFUL)
            {
                //MessageBox.Show("Shutdown error. " + (USBcanServer.eUcanReturn)bRet);
                _frmLog.AddToList("Shutdown error. " + (USBcanServer.eUcanReturn)bRet);
            }
            else
            {
                _frmLog.AddToList("Устройство отключено. " + (USBcanServer.eUcanReturn)bRet);
            }
        }

        void ShowCanInfo()
        {
            bool b = CanSrv.IsCan0Initialized;
            bool b1 = CanSrv.IsCan1Initialized;
            bool b2 = CanSrv.IsHardwareInitialized;
            USBcanServer.tUcanHardwareInfoEx info = new USBcanServer.tUcanHardwareInfoEx();
            USBcanServer.tUcanChannelInfo ch0Inf = new USBcanServer.tUcanChannelInfo();
            USBcanServer.tUcanChannelInfo ch1Inf = new USBcanServer.tUcanChannelInfo();
            byte bb = CanSrv.GetHardwareInfo(ref info, ref ch0Inf, ref ch1Inf);
            bool b4 = USBcanServer.CheckIs_G3(info);
            bool b5 = USBcanServer.CheckIs_G4(info);
            _frmLog.AddToList("IsCan0Initialized".PadRight(30) + b);
            _frmLog.AddToList("IsCan1Initialized".PadRight(30) + b1);
            _frmLog.AddToList("IsHardwareInitialized".PadRight(30) + b2);
            _frmLog.AddToList("Is_G3".PadRight(30) + b4);
            _frmLog.AddToList("Is_G4".PadRight(30) + b5);
            _frmLog.AddToList("Device number of CANmodul".PadRight(30) + info.m_bDeviceNr);
            _frmLog.AddToList("Additional flags".PadRight(30) + info.m_dwFlags);
            _frmLog.AddToList("Version firmware".PadRight(30) + info.m_dwFwVersionEx);
            _frmLog.AddToList("Product code".PadRight(30) + (USBcanServer.eUcanProductCode)info.m_dwProductCode);//
            _frmLog.AddToList("SerialNumber".PadRight(30) + info.m_dwSerialNr);
            _frmLog.AddToList("Size structure".PadRight(30) + info.m_dwSize);
            _frmLog.AddToList("Unique ID".PadRight(30) + info.m_dwUniqueId0);
        }

        bool CanInit()
        {
            bRet = CanSrv.InitHardware();
            _frmLog.AddToList("InitHardware: " + ((USBcanServer.eUcanReturn)bRet).ToString());
            if (bRet != (byte)USBcanServer.eUcanReturn.USBCAN_SUCCESSFUL)
            {
                return false;
            }

            bRet = CanSrv.InitCan((byte)USBcanServer.eUcanChannel.USBCAN_CHANNEL_CH0,
                (short)USBcanServer.eUcanBaudrate.USBCAN_BAUD_250kBit,
                (int)USBcanServer.eUcanBaudrateEx.USBCAN_BAUDEX_USE_BTR01,
                USBcanServer.USBCAN_AMR_ALL,
                USBcanServer.USBCAN_ACR_ALL,
                (byte)USBcanServer.tUcanMode.kUcanModeNormal,
                (byte)USBcanServer.eUcanOutputControl.USBCAN_OCR_DEFAULT);

            _frmLog.AddToList("InitCan: " + ((USBcanServer.eUcanReturn)bRet).ToString());
            if (bRet != (byte)USBcanServer.eUcanReturn.USBCAN_SUCCESSFUL)
            {
                MessageBox.Show("InitCan error. " + (USBcanServer.eUcanReturn)bRet);
                return false;
            }

            ShowCanInfo();
            return true;
        }

        private void cboDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = cboDevices.SelectedIndex;
        }

        private void обнаружитьДжойстикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if(myJoystick.InitJoystick())

            myJoystick.StartWorking();
        }

        private void информацияCANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCanInfo();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseMainForm(null);
            //Environment.Exit(0);
        }

        private void логToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmLog.Show();
        }

        private void панельУправленияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmPU.Show();
        }

        private void отладкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmDebug.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseMainForm(e);
        }

        void CloseMainForm(FormClosingEventArgs e)
        {
            if (MessageBox.Show("Выйти из программы?", "KINZHAL", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification) == System.Windows.Forms.DialogResult.Cancel)
            {
                if(e!=null)
                    e.Cancel = true;
                return;
            }

            //_frmShutDown.Show();

            if (sets != null)
                SaveSettings();

            CanShutdown();

            while (tmrSendA5_9_11.IsRunning)
                tmrSendA5_9_11.Stop();


            try
            {
                sdi.DisconnectCapture();
                //CloseCurrentVideoSource();
                //Environment.Exit(0);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Environment.Exit(0);
        }

        private void CloseCurrentVideoSource()
        {
            if (videoSourcePlayer.VideoSource != null)
            {
                videoSourcePlayer.VideoSource.SignalToStop();

                videoSourcePlayer.VideoSource = null;
            }
        }


        private void tmrSendA5_1_Tick(object sender, EventArgs e)
        {
            if(chkA5_1_ON.Checked)
                SendMessage(Devices[selectedIndex], devPPK, a5_1);
        }


        #region Сохранение настроек в ХМЛ

        private void LoadSettings()
        {
            try
            {
                sets = LoadList(Application.StartupPath + "\\settings.xml");

                _frmSettings.numCoefKrutHor.Value =  Convert.ToDecimal(sets.CoeffKrutHor);
                _frmSettings.numCoefKrutVert.Value = Convert.ToDecimal(sets.CoefKrutVert);
                _frmSettings.numCoefSensHor.Value = Convert.ToDecimal(sets.CoefSensHor);
                _frmSettings.numCoefSensVert.Value = Convert.ToDecimal(sets.CoefSensVert);
            }
            catch
            {
                MessageBox.Show("Файл настроек XML не может быть загружен. Использованы параметры по умолчанию.", "Ошибка открытия файла", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sets = new SettingXML();

                _frmSettings.numCoefKrutHor.Value = 1;
                _frmSettings.numCoefKrutVert.Value = 1;
                _frmSettings.numCoefSensHor.Value = 1;
                _frmSettings.numCoefSensVert.Value = 1;
            }
        }

        private void SaveSettings()
        {
            sets.CoeffKrutHor = _frmSettings.numCoefKrutHor.Value.ToString();
            sets.CoefKrutVert = _frmSettings.numCoefKrutVert.Value.ToString();
            sets.CoefSensHor = _frmSettings.numCoefSensHor.Value.ToString();
            sets.CoefSensVert = _frmSettings.numCoefSensVert.Value.ToString();

            SaveList(Application.StartupPath + "\\settings.xml", sets);
        }

        private SettingXML LoadList(string fileName)
        {
            XmlSerializer writer = new XmlSerializer(typeof(SettingXML));
            using (TextReader tr = new StreamReader(fileName))
            {
                return (SettingXML)writer.Deserialize(tr);
            }
        }

        private void SaveList(string fileName, SettingXML obj)
        {
            try
            {
                XmlSerializer writer = new XmlSerializer(typeof(SettingXML));
                using (TextWriter tw = new StreamWriter(fileName))
                {
                    writer.Serialize(tw, obj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error occured while saving XML-file");
            }
        }

        #endregion

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmSettings.Show();
        }


        private void tmrSendA5_3_Tick(object sender, EventArgs e)
        {
            if(flag_a5_3)
                SendMessage(Devices[selectedIndex], devPPK, a5_3);
            else
                SendMessage(Devices[selectedIndex], devPPK, a5_4);
            if(tmrSendA5_3.Interval > 100)
                tmrSendA5_3.Interval -= 100;
        }


        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.CloseMainForm(null);
            }
        }
    }

    struct Values
    {
        public decimal a5_30_Hor;
        public decimal a5_30_Vert;
        public decimal a5_34_Hor;
        public decimal a5_34_Vert;
    }
}
