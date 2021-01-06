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

        public  TcpClient client;

        public  StreamReader reader;
        public  StreamWriter writer;

        int intSubnetOder = 1;
        string strMaskbits;

        bool Handle_reset = false;
        bool Handle_combobox = false;
        bool Handle_wrongWrite = false;
        bool Handle_Empty = false;
        bool Handle_Empty_subnetName = true;
        bool Handle_allowToSend = false;
        bool Handle_allowCalculate = true;
        bool Handle_needHost = false;

        int maxhost = 0;

        string IpAdressNext;

        List<SubnetClass> liSubnet = new List<SubnetClass>();


        Stack<UnRedo> undoStack;
        Stack<UnRedo> redoStack;

        private void btnReset_Click(object sender, EventArgs e)
        {
            maxhost = 0;
            Handle_reset = true;

            intSubnetOder = 1;
            strMaskbits = "";

            btnSubnet.Text = "Subnet 2";

            tbIPAddress.Text = "";
            tbHostRange.Text = "";
            tbSubnetID.Text = "";
            tbBroadCast.Text = "";
            tbSubnetName.Text = "";
            tbNeededHost.Text = "";
            tbFirstOctet.Text = "";
            tbMaskNumber.Text = "";

            cbHostsPerSubnet.DataSource = null;
            cbSubnetBits.DataSource = null;
            cbMaskBits.DataSource = null;
            cbSubnetMasks.DataSource = null;
            cbMaximumSubnets.DataSource = null;

            tbIPAddress.Enabled = true;
            tbMaskNumber.Enabled = true;
            btnSubnet.Enabled = true;

            rbClassA.Checked = false;
            rbClassB.Checked = false;
            rbClassC.Checked = false;
            rbClassD.Checked = false;
            rbClassE.Checked = false;

             Handle_reset = false;
             Handle_combobox = false;
             Handle_wrongWrite = false;
             Handle_Empty = false;
             Handle_Empty_subnetName = true;
             Handle_allowToSend = false;
             Handle_allowCalculate = true;
             Handle_needHost = false;
        
            IpAdressNext = "";

            Check_validate(enum_global.kindOfCheckValidate.Empty);

            liSubnet.Clear();
             
            table.Rows.Clear();

        }
        public uc_ChiaSubnet()
        {
            InitializeComponent();

            Check_validate(enum_global.kindOfCheckValidate.Empty);


        }
        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void reset_Value(Package pg)
        {
            maxhost = 0;
            tbIPAddress.Text = pg.IpAddress;
            tbSubnetID.Text = pg.SubnetID;
            tbBroadCast.Text = pg.BroadcastAddress;


            cbSubnetMasks.BindingContext = this.BindingContext;

            cbSubnetMasks.DataSource = pg.LiSubnetMask;
            cbSubnetBits.DataSource = pg.LiSubnetBits;
            cbMaskBits.DataSource = pg.LiMaskBits;
            cbHostsPerSubnet.DataSource = pg.LiHostPerSubnets;
            cbMaximumSubnets.DataSource = pg.LiMaximumSubnets;

            tbHostRange.Text = pg.Host_Address_Range;

            IpAdressNext = pg.IpAddressNext;
            strMaskbits = pg.MaskBits.ToString();


            foreach (int item in pg.LiHostPerSubnets)
            {
                if (item > maxhost)
                {
                    maxhost = item;
                }
            }
        }

        private void uc_ChiaSubnet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Package pg_send, pg_receive;
                pg_receive = new Package();

                pg_send = new Package(Package.PackageType.CTS_PackageToCalculate, tbIPAddress.Text, int.Parse(tbMaskNumber.Text), cbSubnetMasks.Text);

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

                pg_send = new Package(Package.PackageType.CTS_PackageToCalculate, tbIPAddress.Text, int.Parse(tbMaskNumber.Text), cbSubnetMasks.Text);

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

            IpAdressNext = pg.IpAddressNext;
            strMaskbits = pg.MaskBits.ToString();
        }

        private void tbIPAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Handle_needHost = true;
            //tbNeededHost.Text = cbHostsPerSubnet.Text;
            //Handle_needHost = false;
            if (!Handle_combobox || Handle_reset)
                return;

            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;

            int index = comboBox.SelectedIndex;


            cbHostsPerSubnet.SelectedIndex = cbMaskBits.SelectedIndex = cbSubnetBits.SelectedIndex = cbSubnetMasks.SelectedIndex = cbMaximumSubnets.SelectedIndex = index;

            Package pg_send, pg_receive;
            pg_receive = new Package();
            pg_send = new Package(Package.PackageType.CTS_PackageToCalculate, tbIPAddress.Text, int.Parse(tbMaskNumber.Text), cbSubnetMasks.Text);

            pg_send.SerializeSurrealists(client, client.GetStream());
            pg_receive = IpHelpers.getPackageDeserialized(client.GetStream());

            showSTC_calculatePackage(pg_receive);
            Handle_combobox = false;
        }

        private void tbMaskNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void tbMaskNumber_TextChanged(object sender, EventArgs e)
        {

            if (tbMaskNumber.Text == string.Empty || Handle_reset || !Handle_allowCalculate)
            {
                return;
            }
            int iMaskNumber;
            int.TryParse(tbMaskNumber.Text, out iMaskNumber);
            if (iMaskNumber > 30)
            {
                tbMaskNumber.Text = "30";
            }
            else if (iMaskNumber < 1)
            {
                tbMaskNumber.Text = "1";
            }
            Check_validate(enum_global.kindOfCheckValidate.Empty);
            if (!Handle_Empty && !Handle_wrongWrite)
            {
                Package pg_send, pg_receive;
                pg_receive = new Package();
                pg_send = new Package(Package.PackageType.CTS_SubnetOne, tbIPAddress.Text, int.Parse(tbMaskNumber.Text), -1);

                pg_send.SerializeSurrealists(client, client.GetStream());
                pg_receive = IpHelpers.getPackageDeserialized(client.GetStream());

                reset_Value(pg_receive);
            }
        }

        private void Combobox_DropDown(object sender, EventArgs e)
        {
            Handle_combobox = true;

        }
        private void Combobox_DropDownClose(object sender, EventArgs e)
        {

        }

        private void tbIPAddress_TextChanged(object sender, EventArgs e)
        {
            if (!Handle_allowCalculate || Handle_reset)
            {
                return;
            }
            Check_validate(enum_global.kindOfCheckValidate.wrongWrite);
            if (!Handle_Empty && !Handle_wrongWrite)
            {
                // send
                Package pg_send, pg_receive;
                pg_receive = new Package();
                pg_send = new Package(Package.PackageType.CTS_SubnetOne, tbIPAddress.Text, int.Parse(tbMaskNumber.Text), -1);

                pg_send.SerializeSurrealists(client, client.GetStream());
                pg_receive = IpHelpers.getPackageDeserialized(client.GetStream());

                reset_Value(pg_receive);
            }


        }
        private void tbIPAddress_TextChanged_1(object sender, EventArgs e)
        {
            if ((!Handle_Empty && !Handle_wrongWrite) || Handle_reset || !Handle_allowCalculate)
            {
                Package pg_send, pg_receive;
                pg_receive = new Package();
                pg_send = new Package(Package.PackageType.CTS_SubnetOne, tbIPAddress.Text, int.Parse(tbMaskNumber.Text), -1);

                pg_send.SerializeSurrealists(client, client.GetStream());
                pg_receive = IpHelpers.getPackageDeserialized(client.GetStream());

                reset_Value(pg_receive);
            }
        }

        private void tbIPAddress_Leave(object sender, EventArgs e)
        {
            //Check_validate(enum_global.kindOfCheckValidate.wrongWrite);
            //if (!Handle_wrongWrite)
            //{
            //    Check_validate(enum_global.kindOfCheckValidate.Empty);
            //}
        }


        private void Check_validate(enum_global.kindOfCheckValidate e)
        {
            string regex_IPaddress = @"^[\d]+\.[\d]+\.[\d]+\.[\d]+$";

            switch (e)
            {
                case enum_global.kindOfCheckValidate.Empty:
                    if (Handle_wrongWrite)
                    {
                        return;
                    }
                    if (tbIPAddress.Text == string.Empty)
                    {
                        lbMessage.Text = @"Mời bạn nhập IPAddress theo mẫu: {N.N.N.N} (N là number) ";
                        gbIpaddress.BackColor = Color.Coral;
                        Handle_Empty = true;
                        return;
                    }

                    if (tbMaskNumber.Text == string.Empty)
                    {
                        lbMessage.Text = @"Mời bạn nhập Mask Present Number";
                        gbMaskPreNumber.BackColor = Color.Coral;
                        Handle_Empty = true;
                        return;
                    }
                    gbIpaddress.BackColor = Color.White;
                    gbMaskPreNumber.BackColor = Color.White;
                    Handle_Empty = false;
                    break;
                case enum_global.kindOfCheckValidate.wrongWrite:
                    if (!Regex.IsMatch(tbIPAddress.Text, regex_IPaddress))
                    {
                        gbIpaddress.BackColor = Color.Coral;
                        lbMessage.Text = @"Mời bạn nhập IPAddress theo mẫu: {N.N.N.N} (N là number)";
                        Handle_wrongWrite = true;
                    }
                    else
                    {
                        List<string> liNumber = new List<string>();
                        var splitted = tbIPAddress.Text.Split('.');
                        foreach (var i in splitted)
                        {
                            int number = int.Parse(i);
                            if (number > 255)
                            {
                                number = 255;
                            }
                            liNumber.Add(number.ToString());
                        }
                        tbIPAddress.Text = String.Join(".", liNumber);
                        tbIPAddress.SelectionStart = tbIPAddress.Text.Length;
                        //clarify class
                        string regex = @"^[\d]+";
                        int strFirstNumber;

                        strFirstNumber = int.Parse(Regex.Match(tbIPAddress.Text, regex).Value);
                        if (strFirstNumber >= 1 && strFirstNumber <= 127)
                        {
                            rbClassA.Checked = true;
                            tbFirstOctet.Text = "1-127";
                        }
                        else if (strFirstNumber >= 128 && strFirstNumber <= 191)
                        {
                            rbClassB.Checked = true;
                            tbFirstOctet.Text = "128-191";
                        }
                        else if (strFirstNumber >= 192 && strFirstNumber <= 223)
                        {
                            rbClassC.Checked = true;
                            tbFirstOctet.Text = "192-223";
                        }
                        else if (strFirstNumber >= 224 && strFirstNumber <= 239)
                        {
                            rbClassD.Checked = true;
                            tbFirstOctet.Text = "224-239";
                        }
                        else if (strFirstNumber >= 240 && strFirstNumber <= 254)
                        {
                            rbClassE.Checked = true;
                            tbFirstOctet.Text = "240-254";
                        }

                     

                        lbMessage.Text = string.Empty;
                        gbIpaddress.BackColor = Color.White;
                        Handle_wrongWrite = false;
                        Check_validate(enum_global.kindOfCheckValidate.Empty);
                    }
                    break;
                default:
                    break;
            }
        }

        private void tbMaskNumber_Leave(object sender, EventArgs e)
        {
            Check_validate(enum_global.kindOfCheckValidate.wrongWrite);
            if (!Handle_wrongWrite)
            {
                Check_validate(enum_global.kindOfCheckValidate.Empty);
            }
            if (tbMaskNumber.Text == string.Empty)
            {
                tbMaskNumber.Text = "0";
            }
        }

        private void tbMaskNumber_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbNeededHost_TextChanged(object sender, EventArgs e)
        {
            if(tbNeededHost.Text == string.Empty || Handle_needHost)
            {
                return;
            }
            if (Handle_reset || !Handle_allowCalculate)
            {
                return;
            }
            if (cbHostsPerSubnet.Items.Count != 0)
            {
                int neededHost = int.Parse(tbNeededHost.Text);
                int index = 0;

                var itemInCb = cbHostsPerSubnet.Items;
                foreach (var item in itemInCb)
                {
                    if (neededHost > (int)item)
                    {
                        Handle_combobox = true;
                        if (index == 0)
                        {
                            tbNeededHost.Text = item.ToString();
                            cbHostsPerSubnet.SelectedIndex = index;
                            return;
                        }
                        cbHostsPerSubnet.SelectedIndex = index - 1;
                        return;
                    }
                    index++;
                }
                cbHostsPerSubnet.SelectedIndex = index - 1;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (lbMessage.Text == string.Empty && tbSubnetName.Text == string.Empty)
            {
                lbMessage.Text = "Mời bạn nhập SubnetName";
                gbSubnetName.BackColor = Color.Coral;
                Handle_Empty_subnetName = false;
                return;
            }
            Handle_allowCalculate = false;
            string strNeededHost = "";
            if (tbNeededHost.Text == string.Empty)
            {
                strNeededHost = cbHostsPerSubnet.Text;
            }
            SubnetClass subnetItem = new SubnetClass(intSubnetOder.ToString(), tbIPAddress.Text,tbMaskNumber.Text, tbSubnetName.Text, tbNeededHost.Text, cbHostsPerSubnet.Text, tbSubnetID.Text, cbMaskBits.Text, cbSubnetMasks.Text, tbHostRange.Text, tbBroadCast.Text);

            liSubnet.Add(subnetItem);
            //liSubnet.Add(subnetItem);
            //DataGridViewRow dtRow = new DataGridViewRow();
            //dtRow.Cells[0].Value = tbIPAddress.Text;
            //dtRow.Cells[1].Value = intSubnetOder.ToString();
            //dtRow.Cells[2].Value = strNeededHost;
            //dtRow.Cells[0].Value = cbHostsPerSubnet.Text;
            //dtRow.Cells[0].Value = cbMaskBits.Text;
            //dtRow.Cells[0].Value = tbSubnetName.Text;
            //dtRow.Cells[0].Value = tbSubnetID.Text;
            //dtRow.Cells[0].Value = cbSubnetMasks.Text;
            //dtRow.Cells[0].Value = tbHostRange.Text;
            //dtRow.Cells[0].Value = tbBroadCast.Text;

            //table.Rows.Add(intSubnetOder.ToString(), tbIPAddress.Text, tbSubnetName.Text, tbNeededHost.Text, cbHostsPerSubnet.Text, tbSubnetID.Text, cbMaskBits.Text, cbSubnetMasks.Text, tbHostRange.Text, tbBroadCast.Text);

            

            tbIPAddress.Text = IpAdressNext;
            tbMaskNumber.Text = cbMaskBits.Text;
            Handle_allowCalculate = true;
            


            Package pg_send, pg_receive;
            pg_receive = new Package();

            pg_send = new Package(Package.PackageType.CTS_SubnetOne, tbIPAddress.Text, int.Parse(tbMaskNumber.Text), maxhost - int.Parse(cbHostsPerSubnet.Text) - 2);

            pg_send.SerializeSurrealists(client, client.GetStream());
            pg_receive = IpHelpers.getPackageDeserialized(client.GetStream());

            BindingSubnetToTable();
            if (pg_receive.IsEmptySubnet)
            {
                btnSubnet.Text = "Hết";
                btnSubnet.Enabled = false;
                return;
            }

            reset_Value(pg_receive);


            //table.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);

            table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            table.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            

            intSubnetOder++;
            tbIPAddress.Enabled = false;
            tbMaskNumber.Enabled = false;

            btnSubnet.Text = "Subnet " + (intSubnetOder + 1).ToString() ;
            
        }

        private void BindingSubnetToTable()
        {
            table.Rows.Clear();
            foreach (var item in liSubnet)
            {
                table.Rows.Add(item.SubnetOrder, item.MajorNetwork, item.SubnetName, item.NeededSize, item.AllocatedSize, item.SubnetID, item.MaskBit, item.SubnetMask, item.HostAddressRange, item.Broadcast);
            }
        }


        private void tbSubnetName_TextChanged(object sender, EventArgs e)
        {
            if (!Handle_Empty_subnetName || Handle_reset || !Handle_allowCalculate )
            {
                lbMessage.Text = string.Empty;
                gbSubnetName.BackColor = Color.White;
                Handle_Empty_subnetName = true;
            }
        }

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       

        private void tbNeededHost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbNeededHost_Leave(object sender, EventArgs e)
        {
            if (tbNeededHost.Text == "0")
            {
                cbHostsPerSubnet.SelectedIndex = 0;
                tbNeededHost.Text = cbHostsPerSubnet.Text;
            }
        }

        private void rbClass_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;


        }

        private void tableLayoutPanel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tbIPAddress_Validated(object sender, EventArgs e)
        {

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {

        }
    }
}
