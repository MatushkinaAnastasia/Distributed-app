using Client.Clients;
using Common.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace Client
{
	class Program
	{
		const string DBPath = @"C:\Users\nasya\Desktop\ТРРП\лр2\SocketMQ\Client\graduates";

		static void Main(string[] args)
		{
			var graduatesService = new GraduatesServiceProvider(DBPath);

			Socket(graduatesService);
			RabbitMQ(graduatesService);

			Console.WriteLine("Для завершения нажмите любую кнопку");
			Console.ReadKey();
		}

        private static void Socket(GraduatesServiceProvider graduatesService)
        {
            //var ip = new IPAddress(new byte[] { 192, 168, 1, 43 });
            var ip = Dns.GetHostEntry("localhost").AddressList[0];
            var port = 11000;
            var socketClient = new SocketClient(ip, port);

            Console.WriteLine("----------------------Сокеты----------------------");
            int slicesCount = 0;
            foreach (List<Graduate> data in graduatesService.GetAll(2))
            {
                var jsonData = JsonConvert.SerializeObject(data);

                socketClient.Send(jsonData);

                Console.WriteLine($"-----Данные отправлены ({slicesCount++}):");
                Console.WriteLine(jsonData);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void RabbitMQ(GraduatesServiceProvider graduatesService)
        {
            Console.WriteLine("---------------------RabbitMQ---------------------");
            var producer = new Producer("localhost"); //"192.168.1.43"
            int slicesCount = 0;

            foreach (List<Graduate> data in graduatesService.GetAll(2))
            {
                Console.WriteLine($"----------Отправляем данные ({slicesCount++})----------");
                for (int i = 0; i < data.Count; i++)
                {
                    var json = JsonConvert.SerializeObject(new List<Graduate> { data[i] });
                    Console.WriteLine($"-----Отправка {i + 1} из {data.Count}...");
                    Console.WriteLine(json);
                    producer.Send(json);
                }
            }

            Console.WriteLine();
        }
    }
}
