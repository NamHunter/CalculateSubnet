using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CalcualateSubNetForm
{
    public static class IpHelpers
    {
        public static string ToIpString(this UInt32 value)
        {
            var bitmask = 0xff000000;
            var parts = new string[4];
            for (var i = 0; i < 4; i++)
            {
                var masked = (value & bitmask) >> ((3 - i) * 8);
                bitmask >>= 8;
                //parts[i] = masked.ToString(CultureInfo.InvariantCulture);
            }
            return String.Join(".", parts);
        }

        public static UInt32 ParseIp(this string ipAddress)
        {
            var splitted = ipAddress.Split('.');
            UInt32 ip = 0;
            for (var i = 0; i < 4; i++)
            {
                ip = (ip << 8) + UInt32.Parse(splitted[i]);
            }
            return ip;
        }
        public static void SerializeSurrealists(this Package pg, TcpClient client, NetworkStream stream)
        {
            string result = JsonConvert.SerializeObject(pg);
            Console.WriteLine(result);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, result);
        }
        public static Package getPackageDeserialized(NetworkStream stream)
        { 
            return JsonConvert.DeserializeObject<Package>(DeserializeSurrealists(stream)); 
        }
        public static string DeserializeSurrealists(NetworkStream stream)
        {
            BinaryFormatter bf = new BinaryFormatter();
            
            return (string)bf.Deserialize(stream);
        }
        public static string ConvertToHexIP(this string ipAddress)
        {
            var splitted = ipAddress.Split('.');

            List<string> parts = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                parts.Add(int.Parse(splitted[i]).ToString("X").PadLeft(2, '0'));
            }
            return string.Join(".", parts);
        }
    }
}
