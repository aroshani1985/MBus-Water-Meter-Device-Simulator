using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MBusWMSim.spx
{
    public class sputil
    {
        delegate void RTB_CT(RichTextBox tb, string str, Color c);
        delegate void TB_CT(TextBox tb, string str);
        delegate void LBL_CT(Label tb, string str, Color c);

        public void SetRichText(RichTextBox rtb, string text, Color c)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (rtb.InvokeRequired)
            {
               
                RTB_CT d = new RTB_CT(SetRichText);
                rtb.Invoke(d, new object[] { rtb, text, c });
            }
            else
            {
                rtb.SelectionColor = Color.Yellow;
                rtb.AppendText("[" + DateTime.Now.ToString("HH:mm:ss-fff") + "] ");
                rtb.SelectionColor = c;
                rtb.AppendText(text);
                rtb.AppendText("\n");
            }
        }
        public void SetText(TextBox tb, string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (tb.InvokeRequired)
            {
                TB_CT d = new TB_CT(SetText);
                tb.Invoke(d, new object[] { tb, text });
            }
            else
            {
                tb.AppendText(text);
                tb.AppendText("\r\n");
            }
        }

        public void SetLbl(Label lbl, string text, Color c)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (lbl.InvokeRequired)
            {
                LBL_CT d = new LBL_CT(SetLbl);
                lbl.Invoke(d, new object[] { lbl, text, c });
            }
            else
            {
                lbl.Text = text;
                lbl.ForeColor = c;
            }
        }
    }
}
