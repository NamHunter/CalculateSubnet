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
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using CalcualateSubNetForm.global;

using CalcualateSubNetForm.Model;

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

        TcpClient client = uc_ChiaSubnet.Instance.client;
        StreamReader streamReader = uc_ChiaSubnet.Instance.reader;
        StreamWriter StreamWriter = uc_ChiaSubnet.Instance.writer;


        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress ip_adress = IPAddress.Parse(tbIPaddress.Text);

            try
            {
                tb1.Text = string.Empty;
                tb1.Text = tb1.Text  + "- Đang kết nối tới server........ \r\n";
                client = new TcpClient(ip_adress.ToString(), int.Parse(tbPort.Text));
                tb1.Text = tb1.Text + "\n" + "- Kết nối đến server thành công";

                //Console.WriteLine("Connection Successful!");
                streamReader = new StreamReader(client.GetStream());
                StreamWriter = new StreamWriter(client.GetStream());
            }
            catch (Exception ex)
            {
                tb1.Text = tb1.Text + "\n" + "- Error: " + ex.Message;
            }
            uc_ChiaSubnet.Instance.client = client;
            uc_ChiaSubnet.Instance.reader = streamReader;
            uc_ChiaSubnet.Instance.writer = StreamWriter;


        }
    }
}
