using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BimbelBee
{
    class Koneksi
    {
        public string connectionString() // buat membangun dan mengembalikan string koneksi ke database
        {
            string connectStr = "";
            try
            {
                //string serverku = "DESKTOP-7QP727C\\HUSNAKAMILA";
                string localIP = GetLocalIPAddress(); //mendeklarasikan ipaddress
                connectStr = $"Server={localIP};Initial Catalog=BIMBELBEE;" +
                    $"user id = sa; password = 111139121Kmilaaa;trustservercertificate=true;";

                return connectStr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        public static string GetLocalIPAddress() //untuk mengambil IP Adress pada PC yang menjalankan aplikasi
        {
            //mengambil informasi tentang local host
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork) //mengambil IPv4
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Tidak ada alamat IP yang ditemukan.");
        }
    }
}
