using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BackUpFiles
{
    public class BackupTimer
    {
        System.Timers.Timer Timer { get; set; }
        private BackupFiles backupFiles;
        private BackupLog backupLog;
        object locker = new object();
        public BackupTimer(BackupConfig config)
        {
            backupLog = new BackupLog(config);
            backupFiles = new BackupFiles(config, backupLog);

            Timer = new Timer();
            Timer.Interval = config.IntervalTime.TotalMilliseconds;
            Timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            Timer.Enabled = true;

            BackupStart();
        }

        public void TimerStop()
        {
            if (Timer != null)
            {
                Timer.Elapsed -= new ElapsedEventHandler(Timer_Elapsed);
                Timer.Close();
                Console.WriteLine("Резервное копирование остановлено");
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            BackupStart();
        }

        private void BackupStart()
        {
            lock (locker)
            {
                Console.WriteLine($"{DateTime.Now} | Timer Backup Elapsed");
                backupFiles.BackupStart();
            }
        }
    }
}
