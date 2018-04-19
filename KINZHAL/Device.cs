using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KINZHAL
{
    public class Device
    {
        string name;

        public string Name
        {
            get { return name; }
        }
        byte address;

        public byte Address
        {
            get { return address; }
        }
        string description;

        public string Description
        {
            get { return description; }
        }

        /// <summary>
        /// Абонент. Устройство, осуществляющее обмен в сети CAN
        /// </summary>
        /// <param name="_address">Сетевой адрес устройства в канале CAN</param>
        /// <param name="_description">Описание устройства</param>
        public Device(byte _address, string _description)
        {
            //name = _name;
            name = ((DEVICE_ADDR)_address).ToString();
            address = _address;
            description = _description;
        }
    }
}
