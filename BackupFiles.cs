using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackUpFiles
{
    public  class BackupFiles
    {
        private BackupConfig backupConfig;
        private BackupLog backupLog;
        public BackupFiles(BackupConfig _config, BackupLog _log)
        {
            backupConfig = _config;
            backupLog = _log;
        }

        public void BackupStart()
        {
            try
            {
                DeleteFiles(backupConfig.SoursePath, backupConfig.DestinationPath);
                DeleteFolder(backupConfig.SoursePath, backupConfig.DestinationPath);
                BackupsFiles(backupConfig.SoursePath, backupConfig.DestinationPath);
            }
            catch (DirectoryNotFoundException dirEx)
            {
                backupLog.WriteLog($"Error: {dirEx.Message}");
                //Console.WriteLine(dirEx.Message);
            }
            catch (Exception ex)
            {
                backupLog.WriteLog($"Error: {ex.Message}");
                //Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Удаление файлов, если в родительской папке этих файлов не стало
        /// </summary>
        /// <param name="soursePath"></param>
        /// <param name="destinationPath"></param>
        void DeleteFiles(string soursePath, string destinationPath)
        {
            foreach (string dirPath in Directory.GetFiles(destinationPath, "*", SearchOption.AllDirectories))
            {
                string replasePath = dirPath.Replace(destinationPath, soursePath);
                string replasePath1 = dirPath.Replace(soursePath, destinationPath);

                if (File.Exists(replasePath) == false)
                {
                    File.Delete(replasePath1);
                    backupLog.WriteLog($"Delete File: {replasePath1}");
                    //Console.WriteLine($"Delete File: {replasePath1}");
                }

            }
        }

        /// <summary>
        /// Удаление папок, если в родительской папке этих каталогов не стало
        /// </summary>
        /// <param name="soursePath"></param>
        /// <param name="destinationPath"></param>
        void DeleteFolder(string soursePath, string destinationPath)
        {
            List<string> patchDeleteList = new List<string>();

            foreach (string dirPath in Directory.GetDirectories(destinationPath, "*", SearchOption.AllDirectories))
            {
                string replasePath = dirPath.Replace(destinationPath, soursePath);
                string replasePath1 = dirPath.Replace(soursePath, destinationPath);

                if (Directory.Exists(replasePath) == false)
                {
                    if (Directory.Exists(replasePath1))
                    {
                        patchDeleteList.Add(replasePath1);
                        //Console.WriteLine($"Delete Catalog: {replasePath1}");
                    }
                }
            }

            //Развернем массив для удаления папок
            patchDeleteList.Reverse();

            foreach (string dirPath in patchDeleteList)
            {
                if (Directory.Exists(dirPath))
                {
                    Directory.Delete(dirPath);
                    backupLog.WriteLog($"Delete Catalog: {dirPath}");
                    //Console.WriteLine($"Delete Catalog: {dirPath}");
                }

            }
        }

        /// <summary>
        /// Копирует файлы без замены
        /// </summary>
        /// <param name="soursePath"></param>
        /// <param name="destinationPath"></param>
        void BackupsFiles(string soursePath, string destinationPath)
        {
            //Создадим все каталоги
            foreach (string dirPath in Directory.GetDirectories(soursePath, "*", SearchOption.AllDirectories))
            {
                string replasePatch = dirPath.Replace(soursePath, destinationPath);

                if (Directory.Exists(replasePatch) == false)
                {
                    Directory.CreateDirectory(replasePatch);
                    backupLog.WriteLog($"Create Catalog: {replasePatch}");
                    //Console.WriteLine($"Create Catalog: {replasePatch}");
                }
            }

            // Копируем все файлы и заменяем все файлы с тем же именем
            foreach (string newPatch in Directory.GetFiles(soursePath, "*.*", SearchOption.AllDirectories))
            {
                string replasePath = newPatch.Replace(soursePath, destinationPath);

                //если файл существует в каталоге, то пропустим его
                if (File.Exists(replasePath) == false)
                {
                    File.Copy(newPatch, replasePath);
                    backupLog.WriteLog($"Copy File: {replasePath}");
                    //Console.WriteLine($"Copy File: {replasePath}");
                }
            }
        }

    }
}
