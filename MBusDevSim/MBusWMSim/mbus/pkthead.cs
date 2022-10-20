using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBusWMSim.mbus
{
    public class pkthead
    {
        #region consts
        const UInt32 DEFAULI_ID_NO = 0x78563412;
        const UInt16 DEFAULI_MANUFACTURE_ID = 0x0907; // AB AxisIndustries manufacturer code „AXI“
        const UInt16 DEFAULI_SIGNATURE = 0x0000;
        const byte DEFAULI_DEVICE_TYPE = 0x07;  //Water meter
        const byte DEFAULI_VERSION = 0x07;  //
        const byte DEFAULI_STATUS = 0x00;
        #endregion

        #region Fields
        UInt32 _id_no;
        UInt16 _manufacture_id;
        byte _version;
        byte _dev_type;
        byte _telegram_counter;
        byte _status;
        UInt16 _signature;

        byte[] _header_packet;
        #endregion

        #region Constructors
        public pkthead()
        {
            /*
                   68 1F 1F 68 header of RSP_UD telegram (length 1Fh = 31d bytes)
                   08 02 72 C field = 08 (RSP), address 2, CI field 72H (var.,LSByte first)
                   78 56 34 12 identification number = 12345678
                   24 40 01 07 manufacturer ID = 4024h (PAD in EN 61107), generation 1, water
                   55 00 00 00 TC = 55h = 85d, Status = 00h, Signature = 0000h
                   03 13 15 31 00 Data block 1: unit 0, storage No 0, no tariff, instantaneous volume, 12565 l (24 bit integer)
                   DA 02 3B 13 01 Data block 2: unit 0, storage No 5, no tariff, maximum volume flow, 113 l/h (4 digit BCD)
                   8B 60 04 37 18 02 Data block 3: unit 1, storage No 0, tariff 2, instantaneous energy,
                   44 6 Application Layer 218,37 kWh (6 digit BCD)
                   18 16 checksum and stopsign
            */
            //byte[] header = { 0x78, 0x56, 0x34, 0x12, 0x24, 0x40, 0x01, 0x07, 0x55, 0x00, 0x00, 0x00 };
            _id_no = DEFAULI_ID_NO;
            _manufacture_id = DEFAULI_MANUFACTURE_ID;
            _version = DEFAULI_VERSION;
            _dev_type = DEFAULI_DEVICE_TYPE;
            _telegram_counter = 0;
            _status = DEFAULI_STATUS;
            _signature = DEFAULI_SIGNATURE;

            _header_packet = new byte[12];
            make_header();
        }
        public pkthead(UInt32 IdentificationNo, UInt16 ManufactureID, byte DeviceType, byte TelegramCounter, byte Status)
        {
            _id_no = IdentificationNo;
            _manufacture_id = ManufactureID;
            _version = 0x07;
            _dev_type = DeviceType;
            _telegram_counter = TelegramCounter;
            _status = Status;
            _signature = DEFAULI_SIGNATURE;

            _header_packet = new byte[12];
            make_header();
        }        
        #endregion

        #region methods
        void make_header()
        {
            _header_packet[0] = (byte)((_id_no >> 24) & 0xFF);
            _header_packet[1] = (byte)((_id_no >> 16) & 0xFF);
            _header_packet[2] = (byte)((_id_no >> 8) & 0xFF);
            _header_packet[3] = (byte)((_id_no >> 0) & 0xFF);

            _header_packet[4] = (byte)((_manufacture_id >> 0) & 0xFF);
            _header_packet[5] = (byte)((_manufacture_id >> 8) & 0xFF);

            _header_packet[6] = _dev_type;
            _header_packet[7] = _version;
            _header_packet[8] = _telegram_counter;
            _header_packet[9] = _status;
            _header_packet[10] = (byte)((_signature >> 8) & 0xFF); ;
            _header_packet[11] = (byte)((_signature >> 0) & 0xFF); ;

        }
        #endregion

        #region Properties
        public byte[] HeaderPacket
        {
            get
            {
                return _header_packet;
            }
        }
        #endregion

        #region 
        #endregion
    }
}
