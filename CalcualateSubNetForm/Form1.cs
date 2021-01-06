using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CalcualateSubNetForm
{
    public partial class Form1 : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public Form1()
        {
            InitializeComponent();


        }

        private void fluentDesignFormContainer1_Click(object sender, EventArgs e)
        {

        }

        private void ace_Ketnoi_Click(object sender, EventArgs e)
        {
            if (!container.Controls.Contains(uc_ketnoi.Instance))
            {
                container.Controls.Add(uc_ketnoi.Instance);
                uc_ketnoi.Instance.Dock = DockStyle.Fill;
                uc_ketnoi.Instance.BringToFront();
            }
            uc_ketnoi.Instance.BringToFront();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ace_Calculate_Click(object sender, EventArgs e)
        {
            if (!container.Controls.Contains(uc_ChiaSubnet.Instance))
            {
                container.Controls.Add(uc_ChiaSubnet.Instance);
                uc_ChiaSubnet.Instance.Dock = DockStyle.Fill;
                uc_ChiaSubnet.Instance.BringToFront();
            }
            uc_ChiaSubnet.Instance.BringToFront();
        }

        private void accordionControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
