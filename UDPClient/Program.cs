using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new UdpClientManager("127.0.0.1", 20000);

            Task.Factory.StartNew(async () => {
                while (true)
                {
                    try
                    {
                        client.Send($"Test message + {DateTime.Now.ToShortTimeString()}");
                        await Task.Delay(1000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            });
            Console.ReadLine();
        }
    }
}
