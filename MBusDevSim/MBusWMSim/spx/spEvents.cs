using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBusWMSim.spx
{
    public class spEvents:EventArgs
    {
        #region Fields
        public string ev_msg;
        public int ev_id;
        public SpEventType ev_type;
        public DateTime ev_dt;
        #endregion

        #region Constructors
        public spEvents(SpEventType EventType, int EventID, string EventMsg)
        {
            ev_dt = DateTime.Now;
            ev_id = EventID;
            ev_type = EventType;
            if (EventMsg != null)
                ev_msg = EventMsg;
            else
                ev_msg = "no msg";
        }
        #endregion
    }
}
