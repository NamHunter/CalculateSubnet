using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcualateSubNetForm.Model
{
    class SubnetClass
    {
        string subnetOrder;
        string neededSize;
        string allocatedSize;
        string maskBit;

        string iPaddress;
        string subnetName;
        string subnetID;
        string subnetMask;
        string hostAddressRange;
        string broadcast;
        string majorNetwork;


        public SubnetClass( string SubnetOrder, string strIPaddress, string presentMark,string SubnetName, string NeededSize, string AllocatedSize, string SubnetID, string MaskBit, string SubnetMask, string HostAddressRange, string Broadcast )
        {
            this.majorNetwork = strIPaddress + "/" + presentMark;
            this.iPaddress = strIPaddress;
            this.SubnetOrder = SubnetOrder;
            this.NeededSize = NeededSize;
            this.AllocatedSize = AllocatedSize;
            this.MaskBit = MaskBit;
            this.SubnetName = SubnetName;
            this.SubnetID = SubnetID;
            this.SubnetMask = SubnetMask;
            this.HostAddressRange = HostAddressRange;
            this.Broadcast = Broadcast;
        }

        public string SubnetOrder { get => subnetOrder; set => subnetOrder = value; }
        public string NeededSize { get => neededSize; set => neededSize = value; }
        public string AllocatedSize { get => allocatedSize; set => allocatedSize = value; }
        public string MaskBit { get => maskBit; set => maskBit = value; }
        public string IPaddress { get => iPaddress; set => iPaddress = value; }
        public string SubnetName { get => subnetName; set => subnetName = value; }
        public string SubnetID { get => subnetID; set => subnetID = value; }
        public string SubnetMask { get => subnetMask; set => subnetMask = value; }
        public string HostAddressRange { get => hostAddressRange; set => hostAddressRange = value; }
        public string Broadcast { get => broadcast; set => broadcast = value; }
        public string MajorNetwork { get => majorNetwork; set => majorNetwork = value; }
    }
}
