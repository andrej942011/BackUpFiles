using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackUpFiles
{
    public class BackupConfig
    {
        public string SoursePath { get; set; }
        public string DestinationPath { get; set; }
        public string LogPatch { get; set; }
        public TimeSpan IntervalTime { get; set; }
        public BackupConfig()
        {
            
        }
    }
}
