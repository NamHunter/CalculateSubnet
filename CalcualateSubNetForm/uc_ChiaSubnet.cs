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

namespace CalcualateSubNetForm
{
    public partial class uc_ChiaSubnet : DevExpress.XtraEditors.XtraUserControl
    {
        private static uc_ChiaSubnet _instance;

        public static uc_ChiaSubnet Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new uc_ChiaSubnet();
                }
                return _instance;
            }
        }

        TcpClient client;

        StreamReader reader;
        StreamWriter writer;

        public uc_ChiaSubnet()
        {
            InitializeComponent();

            IPAddress ip_adress = IPAddress.Parse("127.0.0.1");
            int port = 8080;

            try
            {
                Console.WriteLine($"Attempting to connect to server at IP address: {ip_adress.ToString()} port: 5000");
                client = new TcpClient(ip_adress.ToString(), 8080);
                //Console.WriteLine("Connection Successful!");
                reader = new StreamReader(client.GetStream());
                writer = new StreamWriter(client.GetStream());
            }
            catch (Exception ex)
            {
     
            }

        }
        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            Package pg_send, pg_receive;
            switch (radioButton.Name)
            {
                case "rbClassA":
                    pg_send = new Package(Package.PackageType.CTS_ClassAtFirst, Package.Class.A);
                    pg_receive = new Package();
                    pg_send.SerializeSurrealists(client, client.GetStream());
                    pg_receive = IpHelpers.getPackageDeserialized(client.GetStream());

                    reset_Value(pg_receive);
                    break;
                case "rbClassB":
                    pg_send = new Package(Package.PackageType.CTS_ClassAtFirst, Package.Class.B);
                    pg_receive = new Package();
                    pg_send.SerializeSurrealists(client, client.GetStream());
                    pg_receive = IpHelpers.getPackageDeserialized(client.GetStream());

                    reset_Value(pg_receive);
                    break;
                case "rbClassC":
                    pg_send = new Package(Package.PackageType.CTS_ClassAtFirst, Package.Class.C);
                    pg_receive = new Package();
                    pg_send.SerializeSurrealists(client, client.GetStream());
                    pg_receive = IpHelpers.getPackageDeserialized(client.GetStream());

                    reset_Value(pg_receive);
                    break;
            }
        }
        private void reset_Value(Package pg)
        {
            tbIPAddress.Text = pg.IpAddress;
            tbSubnetID.Text = pg.SubnetID;
            tbBroadCast.Text = pg.BroadcastAddress;
            tbFirstOctet.Text = string.Join("-", new List<string>() { pg.SmallestFirstOctel.ToString(), pg.BiggestFirstOctel.ToString() });

            cbSubnetMasks.BindingContext = this.BindingContext;

            cbSubnetMasks.DataSource = pg.LiSubnetMask;
            cbSubnetBits.DataSource = pg.LiSubnetBits;
            cbMaskBits.DataSource = pg.LiMaskBits;
            cbHostsPerSubnet.DataSource = pg.LiHostPerSubnets;
            cbMaximumSubnets.DataSource = pg.LiMaximumSubnets;

            tbHostRange.Text = pg.Host_Address_Range;
        }

        private void uc_ChiaSubnet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Package pg_send, pg_receive;
                pg_receive = new Package();

                pg_send = new Package(Package.PackageType.CTS_PackageToCalculate, tbIPAddress.Text, cbSubnetMasks.Text);

                pg_send.SerializeSurrealists(client, client.GetStream());
                pg_receive = IpHelpers.getPackageDeserialized(client.GetStream());

                tbHostRange.Text = pg_receive.Host_Address_Range;
            }
        }

        private void tbIPAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Package pg_send, pg_receive;
                pg_receive = new Package();

                pg_send = new Package(Package.PackageType.CTS_PackageToCalculate, tbIPAddress.Text, cbSubnetMasks.Text);

                pg_send.SerializeSurrealists(client, client.GetStream());
                pg_receive = IpHelpers.getPackageDeserialized(client.GetStream());

                showSTC_calculatePackage(pg_receive);
            }
        }
        private void showSTC_calculatePackage(Package pg)
        {
            tbHostRange.Text = pg.Host_Address_Range;
            tbSubnetID.Text = pg.SubnetID;
            tbBroadCast.Text = pg.BroadcastAddress;
        }

        private void tbIPAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != '.' )
            {
                e.Handled = true;
            }
        }
    }
}
