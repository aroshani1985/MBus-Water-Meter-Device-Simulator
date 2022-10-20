using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBusWMSim.mbus
{
    public class longmsg
    {
        #region fields
        const byte LONG_MSG_START = 0x68;
        const byte LONG_MSG_STOP = 0x16;
        const byte C_FIELD_RSP_UD1 = 0x08;
        const byte C_FIELD_RSP_UD2 = 0x18;
        const byte C_FIELD_RSP_UD3 = 0x28;
        const byte C_FIELD_RSP_UD4 = 0x38;

        const byte CI_FIELD_RSP_UD_VARIABLE = 0x72;
        const byte CI_FIELD_RSP_UD_VARIABLE_1 = 0x76;


        byte _packet_len;
        byte _len;
        byte _control;
        byte _address;
        byte _control_information;
        byte _check_sum;
        byte[] _header;
        byte[] _data;
        byte[] _packet;
        #endregion

        #region constructors
        public longmsg(byte SlaveAddress, byte[] Header, byte[] Data)
        {
            _header = Header;
            _data = Data;
            _address = SlaveAddress;
            _packet_len = (byte)(9 + _header.Length + _data.Length);
            _packet = new byte[_packet_len];
            _len = (byte)(3 + _header.Length + _data.Length);
            _check_sum = 0;

            make_mbus_long_msg();
        }
        #endregion

        #region Methods
        public void make_mbus_long_msg()
        {
            _packet[0] = LONG_MSG_START;
            _packet[1] = _len;
            _packet[2] = _len;
            _packet[3] = LONG_MSG_START;

            _packet[4] = C_FIELD_RSP_UD1;
            _packet[5] = _address;
            _packet[6] = CI_FIELD_RSP_UD_VARIABLE;

            for (int i = 0; i < _header.Length; i++)
            {
                _packet[7 + i] = _header[i];
            }

            for (int i = 0; i < _data.Length; i++)
            {
                _packet[7 + _header.Length + i] = _data[i];
            }

            for (int i = 4; i < _packet.Length - 2; i++)
            {
                _check_sum += _packet[i];
            }
            _packet[_packet.Length - 2] = _check_sum;
            _packet[_packet.Length - 1] = LONG_MSG_STOP;
        }

        #endregion

        #region Properties
        public byte[] Packet
        {
            get
            {
                return _packet;
            }
        }
        #endregion

        #region 
        #endregion
    }
}
