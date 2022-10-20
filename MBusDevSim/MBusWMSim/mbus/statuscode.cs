using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBusWMSim.mbus
{
    public class statuscode
    {
        #region Consts
        const byte DEFAULT_STATUS = 0x00;
        byte[] AlarmCode = new byte[] { 0x04, 0x08, 0x10, 0x70, 0xD0, 0xB0, 0x30 };
        byte[] AlarmPriority = new byte[] { 1, 1, 5, 4, 3, 2, 1 };
        public enum AlarmType : byte
        {
            LowBattery = 0,
            PermanentErr = 1,
            DryOrTemporaryError = 2,
            BackFlow = 3,
            Manipulation = 4,
            Burst = 5,
            Leakage = 6
        }
        #endregion

        #region Fields
        byte _status;
        #endregion

        #region Constructors
        public statuscode()
        {
            _status = DEFAULT_STATUS;
        }
        public statuscode(AlarmType Alarm)
        {
            _status = AlarmCode[(int)Alarm];
        }
        #endregion

        #region Methods
        public byte get_alarm_priority(AlarmType Alarm)
        {
            return AlarmPriority[(int)Alarm];
        }
        public byte get_alarm_code(AlarmType Alarm)
        {
            return AlarmCode[(int)Alarm];
        }
        #endregion

        #region Properties
        public byte Status
        {
            get
            {
                return _status;
            }
        }
        #endregion


    }
}
