using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBusWMSim.spx
{
    public class spRecEv:EventArgs
    {
        #region
        public string data_str;
        public byte[] data_byte;

        public int len;
        public DateTime dt;
        public Int64 interval_usec;
        #endregion

        #region
        public spRecEv(int RecLen, string RecStr)
        {
            dt = DateTime.Now;
            
            len = RecLen;
            ///////////////////////////////////
            if (RecStr != null)
                data_str = RecStr;
            else
                data_str = "";
            ////////////////////////////////////
            data_byte = Encoding.UTF8.GetBytes(data_str);
        }
        public spRecEv(int RecLen, byte[] RecBytes)
        {
            dt = DateTime.Now;
            len = RecLen;
            ////////////////////////////////////
            data_byte = RecBytes;
            ////////////////////////////////
            data_str = System.Text.Encoding.UTF8.GetString(data_byte);
        }
        public spRecEv(int RecLen, byte[] RecBytes, Int64 IntervalTicks)
        {
            dt = DateTime.Now;
            len = RecLen;
            ////////////////////////////////////
            data_byte = RecBytes;
            interval_usec = IntervalTicks / 10;
            ////////////////////////////////
            data_str = System.Text.Encoding.UTF8.GetString(data_byte);
        }
        #endregion
    }
}
