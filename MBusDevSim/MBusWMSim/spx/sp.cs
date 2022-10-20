using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBusWMSim.spx
{
    public class sp
    {
        #region Fields - Find SP
        SerialPort spx;
        string[] _available_sp_name;
        int _available_sp_count;

        #endregion

        #region Fields - Serial Port Setting
        private int default_boudrate = 9600;
        //private int default_boudrate = 19200;
        //private int default_boudrate = 38400;
        //private int default_boudrate = 57600;
        //private int default_boudrate = 115200;
        //private int default_boudrate = 230400;
        //private Parity default_parity = Parity.None;
        private Parity default_parity = Parity.Even;
        private int default_data_bits = 8;
        private StopBits default_stop_bits = StopBits.One;
        private Encoding default_encoding = Encoding.GetEncoding("iso-8859-1");
        private int default_read_buffer_size = 4096;
        private int default_write_buffer_size = 1024;
        private int default_ReceivedBytesThreshold = 1;

        #endregion

        #region Fields - Class status and flags
        bool _is_sp_available;
        bool _is_sp_open;
        string _last_msg_str;
        bool _is_rec_byte_mode; // true receive bytes false receive string
        #endregion

        #region Fields - Receive
        Int64 _cur_ticks;
        Int64 _pre_ticks;
        Int64 _dif_ticks;
        DateTime _dt;
        #endregion


        #region Constructors
        public sp(bool IsByteMode)
        {
            _last_msg_str = "no msg!";
            _is_sp_open = false;
            _available_sp_count = 0;
            _is_sp_available = false;
            _is_rec_byte_mode = IsByteMode;
            _dt = new DateTime();
            _cur_ticks = 0;
            _pre_ticks = 0;
            _dif_ticks = 0;
        }
        #endregion

        #region Properties

        public int Avalable_SP_Count
        {
            get
            {
                return _available_sp_count;
            }
        }
        public string[] Avalable_SP_Names
        {
            get
            {
                return _available_sp_name;
            }
        }
        public string SP_Last_Msg
        {
            get
            {
                return _last_msg_str;
            }
        }

        public bool Is_Port_Open
        {
            get
            {
                return _is_sp_open;
            }
        }
        public bool Is_Byte_Mode
        {
            set
            {
                _is_rec_byte_mode = value;
            }
            get
            {
                return _is_rec_byte_mode;
            }
        }
        #endregion


        #region Methods for SP Finding
        public void get_avalable_sp()
        {
            _is_sp_available = false;
            _available_sp_name = SerialPort.GetPortNames();
            _available_sp_count = _available_sp_name.Length;
            if (_available_sp_count != 0)
            {
                _is_sp_available = true;
                _last_msg_str = _available_sp_count.ToString() + " SP is Available. ";
                for (int i = 0; i < _available_sp_count; i++)
                {
                    _last_msg_str += _available_sp_name[i] + " - ";

                }
                spEvents sp_ev = new spEvents(SpEventType.Info, 1, _last_msg_str);
                raiseEv(sp_ev);
            }
            else
            {
                _last_msg_str = "No Serial Port Avalable.";
                spEvents sp_ev = new spEvents(SpEventType.Warning, 1, _last_msg_str);
                raiseEv(sp_ev);
            }
        }
        public string Print_Avalable_Sp_Info(bool single_line)
        {
            string str = "Serial Ports Status: ";
            if (!single_line)
                str += "\r\n";
            get_avalable_sp();
            if (_is_sp_available)
            {
                str += _available_sp_count.ToString() + " SP is Available. ";
                if (!single_line)
                    str += "\r\n";
                for (int i = 0; i < _available_sp_count; i++)
                {
                    str += _available_sp_name[i];
                    if (!single_line)
                        str += "\r\n";
                }
            }
            else
            {
                str += "No SP avaliable!";
                if (!single_line)
                    str += "\r\n";
            }
            return str;
        }
        #endregion

        #region Open Close SP
        public void Close()
        {
            if (spx != null)
            {
                if (spx.IsOpen)
                {
                    spx.Close();
                    _is_sp_open = false;
                    _last_msg_str = spx.PortName + " Closed.";
                    spEvents sp_ev = new spEvents(SpEventType.Info, 1, _last_msg_str);
                    raiseEv(sp_ev);
                }
            }
        }

        public void Open(int SP_Index)
        {
            _is_sp_open = false;
            get_avalable_sp();
            //////////////////////////////////////
            if (SP_Index > _available_sp_count)
            {
                SP_Index = 0;
            }
            //////////////////////
            if (_is_sp_available)
            {
                try
                {
                    spx = new SerialPort(_available_sp_name[SP_Index], default_boudrate, default_parity, default_data_bits, default_stop_bits);
                    spx.Encoding = default_encoding;
                    spx.DataReceived += Spx_DataReceived; ;
                    spx.ReadBufferSize = default_read_buffer_size;
                    spx.WriteBufferSize = default_write_buffer_size;
                    spx.ReceivedBytesThreshold = default_ReceivedBytesThreshold;

                    if (!spx.IsOpen)
                    {
                        spx.Open();
                        spx.DiscardInBuffer();
                        spx.DiscardOutBuffer();
                        _is_sp_open = true;
                        _last_msg_str = "Open SP OK. ";
                        _last_msg_str += spx.PortName + " -B: " + spx.BaudRate.ToString() + " -DB: " +
                                         spx.DataBits.ToString() + " -SB: " + spx.StopBits.ToString() +
                                         " PB: " + spx.Parity.ToString() + "\r\n";
                        spEvents sp_ev = new spEvents(SpEventType.Success, 1, _last_msg_str);
                        raiseEv(sp_ev);
                    }


                }
                catch (Exception ex)
                {
                    _last_msg_str = "Exception->\r\n " + ex.Message;
                    spEvents sp_ev = new spEvents(SpEventType.Error, 1, _last_msg_str);
                    raiseEv(sp_ev);
                    spx.Close();   // is equal to Dispose(true) // you should make new object to connect
                }
            }
            else
            {
                _last_msg_str = "No Serial Port Avalable.";
                spEvents sp_ev = new spEvents(SpEventType.Warning, 2, _last_msg_str);
                raiseEv(sp_ev);
            }
        }
        #endregion

        #region Receive 
        private void Spx_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //A single tick represents one hundred nanoseconds or one ten-millionth of a second.
            //There are 10,000 ticks in a millisecond, or 10 million ticks in a second.
            if (e.EventType == SerialData.Eof)
                return;
            _dt = DateTime.Now;
            int rec_len = spx.BytesToRead;
            _pre_ticks = _cur_ticks;
            _cur_ticks = _dt.Ticks;
            _dif_ticks = _cur_ticks - _pre_ticks;
            ////////////////////////////////////////////////
            if (_is_rec_byte_mode)
            {
                byte[] rec_buffer = new byte[rec_len];
                spx.Read(rec_buffer, 0, rec_len);
                //spRecEv sp_rec_ev = new spRecEv(rec_len, rec_buffer);
                spRecEv sp_rec_ev = new spRecEv(rec_len, rec_buffer, _dif_ticks);
                raiseRec(sp_rec_ev);
            }
            else
            {
                string rec_str = spx.ReadExisting();
                spRecEv sp_rec_ev = new spRecEv(rec_len, rec_str);
                raiseRec(sp_rec_ev);
            }

        }
        #endregion

        #region Send
        public void SendArray(byte[] Paket, int Length)
        {
            if (spx.IsOpen)
            {
                try
                {
                    spx.Write(Paket, 0, Length);
                }
                catch (Exception ex)
                {
                    _last_msg_str = "Send Exception->\r\n " + ex.Message;
                    spEvents sp_ev = new spEvents(SpEventType.Error, 2, _last_msg_str);
                    raiseEv(sp_ev);
                }
            }
            else
            {
                _last_msg_str = "Sendig Data to a Closed Port!";
                spEvents sp_ev = new spEvents(SpEventType.Warning, 2, _last_msg_str);
                raiseEv(sp_ev);
            }
        }

        public void SendString(string atc)
        {
            if (spx.IsOpen)
            {
                try
                {
                    spx.Write(atc);
                }
                catch (Exception ex)
                {
                    _last_msg_str = "Send Exception->\r\n " + ex.Message;
                    spEvents sp_ev = new spEvents(SpEventType.Error, 2, _last_msg_str);
                    raiseEv(sp_ev);
                }
            }
            else
            {
                _last_msg_str = "Sendig Data to a Closed Port!";
                spEvents sp_ev = new spEvents(SpEventType.Warning, 2, _last_msg_str);
                raiseEv(sp_ev);
            }
        }

        public void SendString_LFCR(string atc)
        {
            if (spx.IsOpen)
            {
                try
                {
                    spx.Write(atc + "\r\n");
                }
                catch (Exception ex)
                {
                    _last_msg_str = "Send Exception->\r\n " + ex.Message;
                    spEvents sp_ev = new spEvents(SpEventType.Error, 2, _last_msg_str);
                    raiseEv(sp_ev);
                }
            }
            else
            {
                _last_msg_str = "Sendig Data to a Closed Port!";
                spEvents sp_ev = new spEvents(SpEventType.Warning, 2, _last_msg_str);
                raiseEv(sp_ev);
            }
        }

        public void SendString_LF(string atc)
        {
            if (spx.IsOpen)
            {
                try
                {
                    spx.Write(atc + "\r");
                }
                catch (Exception ex)
                {
                    _last_msg_str = "Send Exception->\r\n " + ex.Message;
                    spEvents sp_ev = new spEvents(SpEventType.Error, 2, _last_msg_str);
                    raiseEv(sp_ev);
                }
            }
            else
            {
                _last_msg_str = "Sendig Data to a Closed Port!";
                spEvents sp_ev = new spEvents(SpEventType.Warning, 2, _last_msg_str);
                raiseEv(sp_ev);
            }
        }
        #endregion

        #region Events - Serial Port
        public delegate void SpEventsHandler(object sender, spEvents e);
        public event SpEventsHandler onEv;
        private void raiseEv(spEvents e)
        {
            if (onEv != null)
                onEv(this, e);
        }
        #endregion

        #region Events - Serial Receive
        public delegate void SpRecHandler(object sender, spRecEv e);
        public event SpRecHandler onRec;
        private void raiseRec(spRecEv e)
        {
            if (onRec != null)
                onRec(this, e);
        }
        #endregion
    }
}
