using OfficeParser.Bot.Services.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace OfficeParser.Bot.Logger
{
    public class Logger : ILogger
    {
        private readonly IFileManager _filemanager;

        private static string baseLogFilePath =
            AppDomain.CurrentDomain.BaseDirectory + "Log";

        private string systemLogFolderPath = baseLogFilePath + "/SystemLogs";
        private string specificLogFolderPath = baseLogFilePath + "/SpecificLogs";
        private string otherLogFolderPath = baseLogFilePath + "/OtherLogs";

        public Logger(IFileManager manager)
        {
            this._filemanager = manager;

            if (manager.CheckFolder(baseLogFilePath))
                manager.CreateFolder(baseLogFilePath);

            if (manager.CheckFolder(systemLogFolderPath))
                manager.CreateFolder(systemLogFolderPath);

            if (manager.CheckFolder(specificLogFolderPath))
                manager.CreateFolder(specificLogFolderPath);

            if (manager.CheckFolder(otherLogFolderPath))
                manager.CreateFolder(otherLogFolderPath);
        }

        public void Handle(Exception error)
        {
            var logFilePath = systemLogFolderPath + "/" + GetFileName();

            if (_filemanager.CheckFile(logFilePath))
                _filemanager.CreateFile(logFilePath);
            var data = new List<string>();
            data.Add($"[{DateTime.Now}] ");
            data.Add(error.Message);
            _filemanager.WriteToFile(logFilePath, data);

        }

        public void Handle(string log)
        {
            var logFilePath = specificLogFolderPath + "/" + GetFileName();

            if (_filemanager.CheckFile(logFilePath))
                _filemanager.CreateFile(logFilePath);
            var data = new List<string>();
            data.Add($"[{DateTime.Now}] ");
            data.Add(log);
            _filemanager.WriteToFile(logFilePath, data);
        }

        public void Handle(string log, Exception error)
        {
            var logFilePath = otherLogFolderPath + "/" + GetFileName();

            if (_filemanager.CheckFile(logFilePath))
                _filemanager.CreateFile(logFilePath);
            var data = new List<string>();
            data.Add($"[{DateTime.Now}] {log}");
            data.Add(error.Message);
            _filemanager.WriteToFile(logFilePath, data);
        }

        private string GetFileName()
        {
            var date = DateTime.Now;
            string path = $"Log_{date.Day}/{date.Month}/{date.Year}.txt";
            return path;
        }

    }
}
