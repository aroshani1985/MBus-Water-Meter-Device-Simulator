using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MBusWMSim.spx;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing;
using static MBusWMSim.mbus.mbreqrep;

namespace MBusWMSim.mbus
{
    public class mbreqrep
    {
        #region Consts
        const byte MBUS_SLAVE_ADDRESS = 1;
        const byte DEFAULT_BAUD_RATE = 5;
        #endregion

        #region Fields
        byte _slave_current_address;
        byte[] _mbus_req_pkt;

        byte _err_code;
        string _err_str;

        sp _sp;
        sputil _sput;
        RichTextBox _rtb;

        public struct WMparam
        {
            public UInt32 Volume;
            public float  Flowrate;
            public float  Temprature;
            public UInt32 ErrorCode;
            public UInt32 OnTime;
        }
        WMparam _wmparam;
        #endregion

        #region Constructors
        public mbreqrep(sp spx, RichTextBox rtb)
        {
            _slave_current_address = MBUS_SLAVE_ADDRESS;
            _sp = spx;
            _sput = new sputil();
            _rtb = rtb;
        }
        public mbreqrep(byte Address, sp spx, RichTextBox rtb, WMparam WaterMeterParam)
        {
            _slave_current_address = Address;
            _sp = spx;
            _sput = new sputil();
            _rtb = rtb;
            _wmparam = WaterMeterParam;
        }
        #endregion

        #region Methods
        public byte process_req(byte[] pkt)
        {
            _err_code = 0;
            if (pkt.Length < 5)
            {
                _err_code = 1;
                return _err_code;
            }

            _mbus_req_pkt = pkt;
            // check sum validation here

            if (pkt[0] == 0x10 && pkt[pkt.Length - 1] == 0x16) // short frame
            {
                if (pkt.Length != 5)
                {
                    _err_code = 2;
                    return _err_code;
                }
                    
                // check sum validation here
                if (pkt[2] != _slave_current_address)
                {
                    _err_code = 3;
                    return _err_code;
                }

                switch (pkt[1])
                {
                    case 0x40:  //SND_NKE
                        send_confirm();
                        break;

                    case 0x7a:  //REQ_UD1
                        break;

                    case 0x7b:  //REQ_UD2
                        send_default_data();
                        break;

                    case 0x5b:  //REQ_UD2
                        send_default_data();
                        break;

                }
            }
            else if (pkt[0] == 0x68 && pkt[3] == 0x68 && pkt[pkt.Length - 1] == 0x16) // long frame
            {
                if (pkt[5] != _slave_current_address)
                {
                    _err_code = 4;
                    return _err_code;
                }
                byte ci = pkt[6];

                switch (ci)
                {
                    case 0x50:   // select responce data-type
                        //response_type(mbus_pkt[7]);
                        break;

                    case 0x51:   // send data to slave
                        //parameter_setting(mbus_pkt[7], mbus_pkt[8]);
                        break;
                }
            }

            return _err_code;
        }
        #endregion

        #region Methods Reply
        void send_confirm()
        {
            if (_sp.Is_Port_Open)
            {
                byte[] data = new byte[1];
                data[0] = 0xE5;
                _sp.SendArray(data, 1);
                _sput.SetRichText(_rtb, "Slave Reply: " + BitConverter.ToString(data), Color.Aqua);
            }
        }
        void send_default_data()
        {
            if (_sp.Is_Port_Open)
            {
                statuscode sc = new statuscode(statuscode.AlarmType.Leakage);
                pkthead head = new pkthead(0x00000001, 0xAABB, 0x07, 1, sc.Status);
                //wmdatarec wmd = new wmdatarec();
                wmdatarec wmd = new wmdatarec(_wmparam.Volume, _wmparam.Flowrate, _wmparam.Temprature, _wmparam.OnTime, _wmparam.ErrorCode);
                //byte[] header = { 0x78, 0x56, 0x34, 0x12, 0x24, 0x40, 0x01, 0x07, 0x55, 0x00, 0x00, 0x00 };
                //byte[] data = { 0x03, 0x13, 0x15, 0x31, 0x00, 0xDA, 0x02, 0x3B, 0x13, 0x01, 0x8B, 0x60, 0x04, 0x37, 0x18, 0x02 };
                longmsg lmsg1 = new longmsg(_slave_current_address, head.HeaderPacket, wmd.WaterMeter_All_Records());
                _sp.SendArray(lmsg1.Packet, lmsg1.Packet.Length);
                _sput.SetRichText(_rtb, "Slave Reply: " + BitConverter.ToString(lmsg1.Packet), Color.Aqua);
            }
        }
        #endregion
    }
}
