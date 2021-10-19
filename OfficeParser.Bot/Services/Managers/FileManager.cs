using OfficeParser.Bot.Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace OfficeParser.Bot.Services.Managers
{
    public class FileManager : IFileManager
    {
        private static string baseDocumentFilePath = 
            AppDomain.CurrentDomain.BaseDirectory + "Documents";

        public FileManager()
        {
            CreateFolder(baseDocumentFilePath);
        }

        public bool CreateFolder(string folderPath)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                return true;
            }
            catch { 
                return false;
            }
        }

        public bool CheckFolder(string folderPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            return directoryInfo.Exists;
        }

        public bool CreateFile(string filePath)
        {
            try
            {
                File.Open(filePath, FileMode.Create, FileAccess.ReadWrite);
                return true;
            }
            catch { 
                return false;
            }
        }

        public bool CheckFile(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            return fi.Exists;
        }

        public void WriteToFile(string filePath, 
            List<string> text)
        {
            try
            {
                File.WriteAllLines(filePath, text);
            }
            catch{}
        }

        public string CreateFileDocx()
        {
            if (!CheckFolder(baseDocumentFilePath)) CreateFolder(baseDocumentFilePath);
            var docxFilePath = baseDocumentFilePath + "/document.docx";
            CreateFile(docxFilePath);
            return docxFilePath;
        }

        public string CreateFileXls()
        {
            if (!CheckFolder(baseDocumentFilePath)) CreateFolder(baseDocumentFilePath);
            string excelFilePath = baseDocumentFilePath + "/document.xlsx";
            CreateFile(excelFilePath);
            return excelFilePath;
        }

        public string CreateFilePdf()
        {
            if (!CheckFolder(baseDocumentFilePath)) CreateFolder(baseDocumentFilePath);
            string pdfFilePath = baseDocumentFilePath + "/document.pdf";
            return pdfFilePath;
        }
    }
}
