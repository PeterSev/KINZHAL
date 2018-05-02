namespace KINZHAL
{
    public class Device
    {

        public string Name { get; }

        public byte Address { get; }
        
        public string Description { get; }

        /// <summary>
        /// Абонент. Устройство, осуществляющее обмен в сети CAN
        /// </summary>
        /// <param name="_address">Сетевой адрес устройства в канале CAN</param>
        /// <param name="_description">Описание устройства</param>
        public Device(byte _address, string _description)
        {
            //name = _name;
            Name = ((DEVICE_ADDR)_address).ToString();
            Address = _address;
            Description = _description;
        }
    }
}
