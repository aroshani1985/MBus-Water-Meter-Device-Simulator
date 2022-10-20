using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBusWMSim.mbus
{
    public class wmdatarec
    {
        #region Fields
        UInt32 _volume;
        float _flowrate;
        float _temprature;
        UInt32 _err_code;
        UInt32 _on_time;
        public struct Datarecord
        {

            public byte dif;
            public bool is_dif_ex;
            public byte[] dife;
            public byte vif;
            public byte[] vife;
            public bool is_vif_ex;
            public byte[] data;
            public byte[] Record;
            public byte len;
        }
        #endregion

        #region Constructor
        public wmdatarec()
        {

        }
        public wmdatarec(UInt32 Volume, float Flowrate, float Temprature, UInt32 OnTime, UInt32 ErrorCode)
        {
            _volume = Volume;
            _flowrate = Flowrate;
            _temprature = Temprature;
            _err_code = ErrorCode;
            _on_time = OnTime;
        }
        #endregion

        #region 

        #endregion

        #region 

        #endregion

        public byte[] DateTime_Record()
        {
            Datarecord date_time_record = new Datarecord();
            date_time_record.dif = 0x04;
            date_time_record.vif = 0x6d;
            date_time_record.is_dif_ex = false;
            date_time_record.is_vif_ex = false;

            byte[] record_data = new byte[4];
            record_data[0] = (byte)(DateTime.Now.Minute & 0x3F);
            record_data[1] = (byte)(DateTime.Now.Hour & 0x1F);
            record_data[1] |= 0x40;
            record_data[2] = (byte)((DateTime.Now.Year - 2000) & 0x07);
            record_data[2] <<= 5;
            record_data[2] |= (byte)(DateTime.Now.Day & 0x1F);
            record_data[3] = (byte)(((DateTime.Now.Year - 2000) >> 3) & 0x0F);
            record_data[3] <<= 4;
            record_data[3] |= (byte)(DateTime.Now.Month & 0x0F);
            date_time_record.len = 6;
            date_time_record.Record = new byte[6];
            date_time_record.Record[0] = date_time_record.dif;
            date_time_record.Record[1] = date_time_record.vif;
            date_time_record.Record[2] = record_data[0];
            date_time_record.Record[3] = record_data[1];
            date_time_record.Record[4] = record_data[2];
            date_time_record.Record[5] = record_data[3];

            return date_time_record.Record;

        }
        public byte[] Volume_Record(UInt32 Volume)
        {
            Datarecord date_time_record = new Datarecord();
            date_time_record.dif = 0x04;
            date_time_record.vif = 0x13;
            date_time_record.is_dif_ex = false;
            date_time_record.is_vif_ex = false;

            byte[] record_data = new byte[4];
            record_data[0] = (byte)((Volume >> 0) & 0xFF);
            record_data[1] = (byte)((Volume >> 8) & 0xFF);
            record_data[2] = (byte)((Volume >> 16) & 0xFF);
            record_data[3] = (byte)((Volume >> 24) & 0xFF);
            date_time_record.len = 6;
            date_time_record.Record = new byte[6];
            date_time_record.Record[0] = date_time_record.dif;
            date_time_record.Record[1] = date_time_record.vif;
            date_time_record.Record[2] = record_data[0];
            date_time_record.Record[3] = record_data[1];
            date_time_record.Record[4] = record_data[2];
            date_time_record.Record[5] = record_data[3];

            return date_time_record.Record;
        }

        public byte[] Volume_Reverse_Record(UInt32 Volume_Reverce)
        {
            Datarecord date_time_record = new Datarecord();
            date_time_record.dif = 0x04;
            date_time_record.vif = 0x93;
            date_time_record.is_dif_ex = false;
            date_time_record.is_vif_ex = true;
            date_time_record.vife = new byte[1];
            date_time_record.vife[0] = 0x3c;

            byte[] record_data = new byte[4];
            record_data[0] = (byte)((Volume_Reverce >> 0) & 0xFF);
            record_data[1] = (byte)((Volume_Reverce >> 8) & 0xFF);
            record_data[2] = (byte)((Volume_Reverce >> 16) & 0xFF);
            record_data[3] = (byte)((Volume_Reverce >> 24) & 0xFF);
            date_time_record.len = 7;
            date_time_record.Record = new byte[7];
            date_time_record.Record[0] = date_time_record.dif;
            date_time_record.Record[1] = date_time_record.vif;
            date_time_record.Record[2] = date_time_record.vife[0];
            date_time_record.Record[3] = record_data[0];
            date_time_record.Record[4] = record_data[1];
            date_time_record.Record[5] = record_data[2];
            date_time_record.Record[6] = record_data[3];

            return date_time_record.Record;
        }

        public byte[] Serial_Number_Record(UInt32 SerialNumber)
        {
            Datarecord date_time_record = new Datarecord();
            date_time_record.dif = 0x0C;
            date_time_record.vif = 0x78;
            date_time_record.is_dif_ex = false;
            date_time_record.is_vif_ex = false;
            byte[] record_data = new byte[4];
            for (int i = 3; i >= 0; i--)
            {
                byte digit = (byte)(SerialNumber % 10);
                SerialNumber /= 10;
                record_data[i] = digit;
                record_data[i] <<= 4;
                digit = (byte)(SerialNumber % 10);
                SerialNumber /= 10;
                record_data[i] |= digit;
            }
            date_time_record.len = 6;
            date_time_record.Record = new byte[6];
            date_time_record.Record[0] = date_time_record.dif;
            date_time_record.Record[1] = date_time_record.vif;
            date_time_record.Record[2] = record_data[0];
            date_time_record.Record[3] = record_data[1];
            date_time_record.Record[4] = record_data[2];
            date_time_record.Record[5] = record_data[3];

            return date_time_record.Record;
        }

        public byte[] FlowRate_Record(float Flowrate)
        {
            Datarecord date_time_record = new Datarecord();
            date_time_record.dif = 0x05;
            date_time_record.vif = 0x3B;
            date_time_record.is_dif_ex = false;
            date_time_record.is_vif_ex = false;
            byte[] record_data = new byte[4];

            //0x4149999a   12.6
            record_data[0] = 0x9a;
            record_data[1] = 0x99;
            record_data[2] = 0x49;
            record_data[3] = 0x41;

            byte[] single_percision_float = BitConverter.GetBytes(Flowrate);
            record_data[0] = single_percision_float[0];
            record_data[1] = single_percision_float[1];
            record_data[2] = single_percision_float[2];
            record_data[3] = single_percision_float[3];
            date_time_record.len = 6;
            date_time_record.Record = new byte[6];
            date_time_record.Record[0] = date_time_record.dif;
            date_time_record.Record[1] = date_time_record.vif;
            date_time_record.Record[2] = record_data[0];
            date_time_record.Record[3] = record_data[1];
            date_time_record.Record[4] = record_data[2];
            date_time_record.Record[5] = record_data[3];

            return date_time_record.Record;
        }

        public byte[] Temperature_Record(float Temperature)
        {
            Datarecord date_time_record = new Datarecord();
            date_time_record.dif = 0x05;
            date_time_record.vif = 0x5B;
            date_time_record.is_dif_ex = false;
            date_time_record.is_vif_ex = false;
            byte[] record_data = new byte[4];

            byte[] single_percision_float = BitConverter.GetBytes(Temperature);
            record_data[0] = single_percision_float[0];
            record_data[1] = single_percision_float[1];
            record_data[2] = single_percision_float[2];
            record_data[3] = single_percision_float[3];
            date_time_record.len = 6;
            date_time_record.Record = new byte[6];
            date_time_record.Record[0] = date_time_record.dif;
            date_time_record.Record[1] = date_time_record.vif;
            date_time_record.Record[2] = record_data[0];
            date_time_record.Record[3] = record_data[1];
            date_time_record.Record[4] = record_data[2];
            date_time_record.Record[5] = record_data[3];

            return date_time_record.Record;
        }

        //On time
        public byte[] Battery_Operation_Time_Record(UInt32 BatteryOperationTime)
        {
            Datarecord date_time_record = new Datarecord();
            date_time_record.dif = 0x04;
            date_time_record.vif = 0x20;
            date_time_record.is_dif_ex = false;
            date_time_record.is_vif_ex = false;
            byte[] record_data = new byte[4];

            record_data[0] = (byte)((BatteryOperationTime >> 0) & 0xFF);
            record_data[1] = (byte)((BatteryOperationTime >> 8) & 0xFF);
            record_data[2] = (byte)((BatteryOperationTime >> 16) & 0xFF);
            record_data[3] = (byte)((BatteryOperationTime >> 24) & 0xFF);

            date_time_record.len = 6;
            date_time_record.Record = new byte[6];
            date_time_record.Record[0] = date_time_record.dif;
            date_time_record.Record[1] = date_time_record.vif;
            date_time_record.Record[2] = record_data[0];
            date_time_record.Record[3] = record_data[1];
            date_time_record.Record[4] = record_data[2];
            date_time_record.Record[5] = record_data[3];

            return date_time_record.Record;
        }


        public byte[] Error_Flag_Binary_Record(UInt16 ErrorCode)
        {
            Datarecord date_time_record = new Datarecord();
            date_time_record.dif = 0x32;
            date_time_record.vif = 0xFD;
            date_time_record.is_dif_ex = false;
            date_time_record.is_vif_ex = true;
            date_time_record.vife = new byte[1];
            date_time_record.vife[0] = 0x17;

            byte[] record_data = new byte[2];
            record_data[0] = (byte)((ErrorCode >> 0) & 0xFF);
            record_data[1] = (byte)((ErrorCode >> 8) & 0xFF);

            date_time_record.len = 5;
            date_time_record.Record = new byte[5];
            date_time_record.Record[0] = date_time_record.dif;
            date_time_record.Record[1] = date_time_record.vif;
            date_time_record.Record[2] = date_time_record.vife[0];
            date_time_record.Record[3] = record_data[0];
            date_time_record.Record[4] = record_data[1];


            return date_time_record.Record;
        }

        public byte[] Error_Code_Record(UInt32 ErrorCode)
        {
            Datarecord date_time_record = new Datarecord();
            date_time_record.dif = 0x34;
            date_time_record.vif = 0xFD;
            date_time_record.is_dif_ex = false;
            date_time_record.is_vif_ex = true;
            date_time_record.vife = new byte[1];
            date_time_record.vife[0] = 0x17;

            byte[] record_data = new byte[4];
            record_data[0] = (byte)((ErrorCode >> 0) & 0xFF);
            record_data[1] = (byte)((ErrorCode >> 8) & 0xFF);
            record_data[2] = (byte)((ErrorCode >> 16) & 0xFF);
            record_data[3] = (byte)((ErrorCode >> 24) & 0xFF);

            date_time_record.len = 7;
            date_time_record.Record = new byte[7];
            date_time_record.Record[0] = date_time_record.dif;
            date_time_record.Record[1] = date_time_record.vif;
            date_time_record.Record[2] = date_time_record.vife[0];
            date_time_record.Record[3] = record_data[0];
            date_time_record.Record[4] = record_data[1];
            date_time_record.Record[5] = record_data[2];
            date_time_record.Record[6] = record_data[3];

            return date_time_record.Record;
        }

        public byte[] WaterMeter_All_Records()
        {
            // 6 + 6 + 7 + 6 + 6 + 6 + 6 + 5 + 7
            byte[] data = new byte[55];

            byte[] buff = DateTime_Record();
            Array.Copy(buff, 0, data, 0, buff.Length);

            buff = Volume_Record(_volume);
            Array.Copy(buff, 0, data, 6, buff.Length);

            buff = Volume_Reverse_Record(0);
            Array.Copy(buff, 0, data, 12, buff.Length);

            buff = Serial_Number_Record(12345678);
            Array.Copy(buff, 0, data, 19, buff.Length);

            buff = FlowRate_Record(_flowrate);
            Array.Copy(buff, 0, data, 25, buff.Length);

            buff = Temperature_Record(_temprature);
            Array.Copy(buff, 0, data, 31, buff.Length);

            buff = Battery_Operation_Time_Record(_on_time);
            Array.Copy(buff, 0, data, 37, buff.Length);

            buff = Error_Flag_Binary_Record(0x0002);
            Array.Copy(buff, 0, data, 43, buff.Length);

            buff = Error_Code_Record(0x0002);
            Array.Copy(buff, 0, data, 48, buff.Length);

            return data;
        }
        #region Methods

        #endregion

        #region 
        #endregion

        #region 
        #endregion
    }
}
