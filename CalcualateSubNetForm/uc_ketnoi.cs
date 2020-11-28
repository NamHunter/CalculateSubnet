using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing.Drawing2D;

namespace CalcualateSubNetForm
{
    public partial class uc_ketnoi : DevExpress.XtraEditors.XtraUserControl
    {
        private static uc_ketnoi _instance;

        public static uc_ketnoi Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new uc_ketnoi();
                }
                return _instance;
            }
        }
        public uc_ketnoi()
        {
            InitializeComponent();
        }
        private void Ketnoi()
        {

        }

        private void XtraUserControl1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
    }
}
