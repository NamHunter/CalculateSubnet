﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CalcualateSubNetForm
{
    [Serializable]
    public class Package
    {
        public enum PackageType
        {
            CTS_SubnetOne,
            STC_PackageReset,
            STC_Result,
            CTS_PackageToCalculate,
        }
        public enum Class
        {
            A, B, C
        }

        PackageType packageType;
        Class class_subnet;
        string subnetMask;
        string ipAddress;
        string host_Address_Range;
        string subnetID;
        string broadcastAddress;
        string ipAddressNext;
        bool isEmptySubnet = false;

        int subnetBits;
        int maskBits;
        int maximumSubnets;
        int hostPerSubnets;
        int smallestFirstOctel;
        int biggestFirstOctel;
        int maskNumber;
        int maskNumberNext;
        int maxHost = -1;

        List<string> liSubnetMask;
        List<int> liSubnetBits;
        List<int> liMaskBits;
        List<int> liMaximumSubnets;
        List<int> liHostPerSubnets;


        public PackageType Package_Type { get { return packageType; } set { packageType = value; } }
        public Class Class_subnet { get { return class_subnet; } set { class_subnet = value; } }
        public string SubnetMask { get { return subnetMask; } set { subnetMask = value; } }
        public string IpAddress { get => ipAddress; set => ipAddress = value; }
        public string IpAddressNext { get => ipAddressNext; set => ipAddressNext = value; }
        public string Host_Address_Range { get => host_Address_Range; set => host_Address_Range = value; }
        public string BroadcastAddress { get => broadcastAddress; set => broadcastAddress = value; }
        public string SubnetID { get => subnetID; set => subnetID = value; }
        public int SubnetBits { get { return subnetBits; } set { subnetBits = value; } }
        public int MaskBits { get { return maskBits; } set { maskBits = value; } }
        public int MaximumSubnets { get { return maximumSubnets; } set { maximumSubnets = value; } }
        public int HostperSubnets { get { return hostPerSubnets; } set { hostPerSubnets = value; } }
        public int SmallestFirstOctel { get => smallestFirstOctel; set => smallestFirstOctel = value; }
        public int BiggestFirstOctel { get => biggestFirstOctel; set => biggestFirstOctel = value; }
        public int Masknumber { get => maskNumber; set => maskNumber = value; }
        public int MaskNumberNext { get => maskNumberNext; set => maskNumberNext = value; }
        public int MaxHost { get => maxHost; set => maxHost = value; }

        public List<string> LiSubnetMask { get => liSubnetMask; set => liSubnetMask = value; }
        public List<int> LiSubnetBits { get => liSubnetBits; set => liSubnetBits = value; }
        public List<int> LiMaskBits { get => liMaskBits; set => liMaskBits = value; }
        public List<int> LiMaximumSubnets { get => liMaximumSubnets; set => liMaximumSubnets = value; }
        public List<int> LiHostPerSubnets { get => liHostPerSubnets; set => liHostPerSubnets = value; }

        public bool IsEmptySubnet { get => isEmptySubnet; set => isEmptySubnet = value; }

        public Package()
        {
            Initial_value();
        }
        public Package(PackageType packageType, string strIpAddress, int iMaskNumber, int maxHost)
        {
            // gởi CTS_SubnetOne,
            Initial_value();
            this.packageType = packageType;
            this.ipAddress = strIpAddress;
            this.maskNumber = iMaskNumber;
            this.maxHost = maxHost;
        }
        public Package(PackageType packageType, String Ip_Address, int maskNumber, string Subnet_Mask)
        {
            // gởi CTS_PackgeToCalculate
            this.packageType = packageType;
            this.ipAddress = Ip_Address;
            this.subnetMask = Subnet_Mask;
            this.maskNumber = maskNumber;
        }
        private void Initial_value()
        {
            liSubnetMask = new List<string>();
            liSubnetBits = new List<int>();
            liMaskBits = new List<int>();
            liMaximumSubnets = new List<int>();
            liHostPerSubnets = new List<int>();
        }
    }
}
