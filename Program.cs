using System;
using System.Collections.Generic;
using System.IO;

namespace BackUpFiles
{
    class Program
    {
        /// <summary>
        /// Требования:
        /// 1)Сихронизация должна быть односторонней: после завершения процесса
        ///     синхронизации содержимое каталога-реплики должно в точности соответствовать
        ///     содержимому каталогу-источника;
        /// 2)Синхронизация должна производиться периодически;
        /// 3)Операции создания/копирования/удаления объектов должны логироваться в файле
        ///     и выводиться в консоль;
        /// 4)Пути к каталогам, интервал синхронизации и путь к файлу логирования должны
        ///     задаваться параметрами командной строки при запуске программы.
        /// </summary>
        /// <param name="args"></param>

        static void Main(string[] args)
        {
            Console.WriteLine(args.Length);
            foreach (var item in args)
            {
                Console.WriteLine(item);
            }

            if (args.Length == 0) //0
            {
                string output = "Запуск невозможен, нет аргументов командной строки \n" +
                                "Аргумент 1 - папка ресурса\n" +
                                "Аргумент 2 - папка резервной копии\n" +
                                "Аргумент 3 - папка для логов\n" +
                                "Аргумент 4 - интервал запуска в формате {hh:mm:ss}";
                
                Console.WriteLine(output);
            }
            else if (args.Length == 4)
            {
                BackupConfig config = new BackupConfig();
                config.SoursePath = args[0];
                config.DestinationPath = args[1];
                config.LogPatch = args[2];
                config.IntervalTime = TimeSpan.Parse(args[3]);

                BackupTimer backupTimer = new BackupTimer(config);
            }
            else if (args.Length == 0) //4
            {
                BackupConfig config = new BackupConfig();
                config.IntervalTime = new TimeSpan(0,0,10);
                config.SoursePath = @"D:\sourse\";
                config.DestinationPath = @"D:\destination\";
                config.LogPatch = @"D:\logFiles\";

                BackupTimer backupTimer = new BackupTimer(config);
            }

            Console.ReadLine();
        }

       
    }
}
