using MBusWMSim.mbus;
using MBusWMSim.spx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MBusWMSim.mbus.mbreqrep;
using static System.Net.Mime.MediaTypeNames;

namespace MBusWMSim
{
    public partial class Form1 : Form
    {
        #region Fields - Serial port 
        public sp sp1; // baud 9600, parity for MBUS always is even
        sputil su1;
        public bool is_sp_open = false;
        int sp_idx = 0;
        #endregion

        #region Fields - Water meter
        double volume = 0; //M3 
        double flow_rate = 3600; //M3 per hour
        double temprature = 25.0; //M3 per hour
        #endregion

        #region Fields - MBUS
        mbreqrep _mbrr;
        byte _req_process_status;
        WMparam _wmparam;
        #endregion

        #region Methods Init and constructs
        public Form1()
        {
            InitializeComponent();
            Init_UI();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Init_SP();
            su1 = new sputil();
            sp_update_combobox();
            this.ActiveControl = btn_sp_con;          
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sp1 != null)
            {
                sp1.Close();
                is_sp_open = false;
            }
        }
        void Init_UI()
        {
            cbx_binary_err_code.SelectedIndex = 0;
            cbx_status.SelectedIndex = 0;

        }
        #endregion

        #region Methods - Serial Port
        public void Init_SP()
        {
            sp1 = new sp(true);
            sp1.onEv += Sp1_onEv;
            sp1.onRec += Sp1_onRec;
        }
        public void Open_sp()
        {
            if (sp1 != null)
            {
                sp1.Close();
                is_sp_open = false;
            }

            sp_idx = cbx_spx.SelectedIndex;
            if (sp_idx > -1)
            {
                sp1.Open(sp_idx);
                if (sp1.Is_Port_Open)
                {
                    is_sp_open = true;
                    Init_Mbus();
                }
            }
        }
        public void sp_update_combobox()
        {
            cbx_spx.Items.Clear();
            sp1.get_avalable_sp();
            if (sp1.Avalable_SP_Count > 0)
            {
                for (int i = 0; i < sp1.Avalable_SP_Count; i++)
                {
                    cbx_spx.Items.Add(sp1.Avalable_SP_Names[i]);
                }
                cbx_spx.SelectedIndex = 0;
                //print_status(sp1.Print_Avalable_Sp_Info(false), Color.Lime);
            }
            else
            {
               // print_status("No Serial Port Avalable!", Color.Red);
            }
        }
        private void Sp1_onRec(object sender, spRecEv e)
        {
            su1.SetRichText(txtr_main,"Master Request: " + BitConverter.ToString(e.data_byte), Color.Pink);
            _req_process_status =  _mbrr.process_req(e.data_byte);
        }
        private void Sp1_onEv(object sender, spEvents e)
        {
            Color c = Color.White;
            switch (e.ev_type)
            {
                case SpEventType.Error:
                    c = Color.Red;
                    break;

                case SpEventType.Info:
                    c = Color.Lime;
                    break;

                case SpEventType.Warning:
                    c = Color.Orange;
                    break;

                case SpEventType.Success:
                    c = Color.Lime;
                    break;
            }
            su1.SetRichText(txtr_main, e.ev_msg, c);
        }
        #endregion

        #region Method - Serial Port clicks
        private void btn_find_sp_Click(object sender, EventArgs e)
        {
            sp_update_combobox();
        }

        private void btn_sp_con_Click(object sender, EventArgs e)
        {
            Open_sp();
        }

        private void btn_sp_discon_Click(object sender, EventArgs e)
        {
            if (sp1 != null)
            {
                sp1.Close();
                is_sp_open = false;
            }
        }
        #endregion

        #region Method - Status textbox 
        void print_status(string text, Color c)
        {
            txtr_main.SelectionColor = Color.Yellow;
            txtr_main.AppendText("[" + DateTime.Now.ToString("HH:mm:ss-fff") + "] ");
            txtr_main.SelectionColor = c;
            txtr_main.AppendText(text);
            txtr_main.AppendText("\n");
        }
        private void txtr_main_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            txtr_main.SelectionStart = txtr_main.Text.Length;
            // scroll it automatically
            txtr_main.ScrollToCaret();
        }

        #endregion

        #region Methods - Timer
        private void btn_start_wm_Click(object sender, EventArgs e)
        {
            tim_wm.Enabled = true;
            tim_wm.Start();
        }

        private void btn_stop_wm_Click(object sender, EventArgs e)
        {
            tim_wm.Stop();
            tim_wm.Enabled = false;         
        }
        private void tim_wm_Tick(object sender, EventArgs e)
        {
            update_wm_param();
        }
        #endregion

        #region Methods - Water Meter
        void update_wm_param()
        {
            volume += flow_rate/3600;

            lbl_volume.Text = String.Format("{0:0.00}", volume);
            lbl_temp.Text = temprature.ToString();
            lbl_flowrate.Text = flow_rate.ToString();
        }
        void update_wm_data_record_params()
        {
            _wmparam.Temprature = (float)temprature;
            _wmparam.Volume = (UInt32) volume;
            _wmparam.ErrorCode = (UInt32)cbx_binary_err_code.SelectedIndex;
            _wmparam.Flowrate = (float)flow_rate;
            _wmparam.OnTime = 1000;
        }
        private void btn_update_wm_settings_Click(object sender, EventArgs e)
        {
            volume = (double) nud_volume.Value;
            flow_rate = (double)nud_flowrate.Value;
            temprature = (double)nud_temp.Value;

            lbl_volume.Text = String.Format("{0:0.00}", volume);
            lbl_temp.Text = temprature.ToString();
            lbl_flowrate.Text = flow_rate.ToString();
        }
        #endregion

        #region Methods - MBUS
        public void Init_Mbus()
        {
            if(sp1 != null && sp1.Is_Port_Open)
            {
                _mbrr = new mbreqrep(sp1, txtr_main);
                print_status("M-Bud Initialized successfully.", Color.Green);
            }
            else
            {
                print_status("Cant Init Mbus, CON port err.", Color.Red);
            }
        }
        #endregion
    }
}
