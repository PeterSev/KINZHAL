using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KINZHAL
{
    public class Message
    {
        typeMessage type;

        public typeMessage Type
        {
            get { return type; }
        }

        string name;

        public string Name
        {
            get { return name; }
        }
        byte priority;

        public byte Priority
        {
            get { return priority; }
        }
        ushort descriptor;

        public ushort Descriptor
        {
            get { return descriptor; }
        }
        ushort time_periodic;

        public ushort Time_periodic
        {
            get { return time_periodic; }
        }
        byte[] data;

        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// Отправляемое сообщение. Тип и имя сообщения присваиваются автоматически.
        /// </summary>
        /// <param name="_priority">Приоритет сообщения {0..31}</param>
        /// <param name="_descriptor">Дескриптор {0..255 - Служебные, 256..1279 - Команды, 1280..1295 - Подтверждения}</param>
        /// <param name="_time_periodic">Время периодичности сообщения, мс. 0 - спорадическое сообщение</param>
        /// <param name="_dataNum">Количество байт данных. 0 - при отсутствии</param>
        public Message(byte _priority, ushort _descriptor, ushort _time_periodic, byte _dataNum)
        {
            if (_descriptor > 0 && _descriptor <= 255)
                type = typeMessage.SERVICE;
            else if (_descriptor > 255 && _descriptor <= 1279)
                type = typeMessage.COMMAND;
            else if (_descriptor > 1279 && _descriptor <= 1295)
                type = typeMessage.ACK;
            else if (_descriptor > 1295 && _descriptor <= 2047)
                type = typeMessage.BROADCAST;
            else if (_descriptor > 2047 && _descriptor <= 4095)
                type = typeMessage.OTHER;


            name = ((DESCRIPTORS)_descriptor).ToString();
            priority = _priority;

            descriptor = _descriptor;
            switch (type) //проверка и установка дескриптора на соответствующий типу сообщения диапазон
            {
                case typeMessage.SERVICE:
                    if (_descriptor > 255) descriptor = 255; break; //знак проверяется типом переменной
                case typeMessage.COMMAND:
                    if (_descriptor < 256 || _descriptor > 1279) descriptor = 1279; break;
                case typeMessage.ACK:
                    if (_descriptor < 1280 || _descriptor > 1295) descriptor = 1295; break;
            }
            time_periodic = _time_periodic;

            //data = new byte[0];
            data = new byte[_dataNum];
        }
    }
}
