using Common.Model;
using Newtonsoft.Json;
using Server.Core;
using Server.Core.Servers;
using Server.Database;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
	public class Program : IMessageHandler
    {
        private const string ConnectionString = "Host=localhost;Port=5432;Database=graduates;Username=postgres;Password=9378325;";
        private readonly GraduatesServiceProvider _graduatesService;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private object _locker;

        public Program()
        {
            _locker = new object();
            _cancellationTokenSource = new CancellationTokenSource();
            _graduatesService = new GraduatesServiceProvider(ConnectionString);
        }

        static void Main()
        {
            var program = new Program();
            Console.CancelKeyPress += (sender, e) =>
            {
                try
                {
                    program.Cancel();
                }
                finally
                {
                    e.Cancel = true;
                }
            };
            program.Run();
        }

        public void Run()
        {
            //var ip = new IPAddress(new byte[] { 192, 168, 1, 37 });
            var ip = Dns.GetHostEntry("localhost").AddressList[0];
            var port = 11000;

            var servers = new List<IServer>()
            {
                new SocketServer(ip, port),
                new Consumer("localhost")
            };

            var tasks = servers
                .Select(server => Task.Run(() => server.Run(this, _cancellationTokenSource.Token)))
                .ToArray();
			Console.WriteLine("-----------Сервер-в-режиме-ожидания------------");

            Task.WaitAll(tasks);
        }

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
        }

        public void Handle(string message)
        {
            Console.WriteLine("данные получены:");
            Console.WriteLine(message);

            var graduates = JsonConvert.DeserializeObject<List<Graduate>>(message);

            foreach (var graduate in graduates)
            {
                lock (_locker)
                {
                    var id_university = _graduatesService.InsertUniversity(graduate.Name_university);
                    var id_faculty = _graduatesService.InsertFaculty(graduate.Name_faculty, id_university);
                    var id_student = _graduatesService.InsertStudent(graduate.Fio, id_faculty);
                    var id_subject = _graduatesService.InsertSubject(graduate.Name_subject);
                    var id_grade = _graduatesService.InsertGrade(graduate.Grade, id_student, id_subject);
                }
            }

            Console.WriteLine("Данные записаны.");
        }
    }
}
