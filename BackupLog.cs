using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackUpFiles
{
    public class BackupLog
    {
        BackupConfig config;
        public BackupLog(BackupConfig _config)
        {
            config = _config;
        }

        public void WriteLog(string text)
        {
            string time = DateTime.Now.ToString();
            string textLog = $"{time} | {text}";
            Console.WriteLine(textLog);

            WriteFile(textLog);
        }

        public void ConsoleLog(string text)
        {
            string time = DateTime.Now.ToString();
            string textLog = $"{time} | {text}";
            Console.WriteLine(textLog);
        }

        private void WriteFile(string log)
        {
            File.AppendAllText(config.LogPatch + $"LogFileBackup_{DateTime.Now.ToShortDateString()}.txt", log + "\n");
        }
    }
}
