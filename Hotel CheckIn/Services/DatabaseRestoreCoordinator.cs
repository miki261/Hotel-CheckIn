using Hotel_CheckIn.Data;
using System;
using System.IO;

namespace Hotel_CheckIn.Services
{
    public class DatabaseRestoreCoordinator
    {
        private readonly string _databasePath;
        private readonly string _walPath;
        private readonly string _shmPath;
        private readonly string _pendingRestoreFile;

        public DatabaseRestoreCoordinator()
        {
            _databasePath = DatabasePaths.MainDatabasePath;
            _walPath = _databasePath + "-wal";
            _shmPath = _databasePath + "-shm";
            _pendingRestoreFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pending_restore.txt");
        }

        public void SavePendingRestore(string backupFilePath)
        {
            File.WriteAllText(_pendingRestoreFile, backupFilePath);
        }

        public bool HasPendingRestore()
        {
            return File.Exists(_pendingRestoreFile);
        }

        public void ExecutePendingRestore()
        {
            if (!File.Exists(_pendingRestoreFile))
                return;

            string backupPath = File.ReadAllText(_pendingRestoreFile).Trim();

            if (!File.Exists(backupPath))
            {
                File.Delete(_pendingRestoreFile);
                return;
            }

            DeleteIfExists(_walPath);
            DeleteIfExists(_shmPath);
            DeleteIfExists(_databasePath);

            File.Copy(backupPath, _databasePath, true);

            DeleteIfExists(_walPath);
            DeleteIfExists(_shmPath);
            DeleteIfExists(_pendingRestoreFile);
        }

        private void DeleteIfExists(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}