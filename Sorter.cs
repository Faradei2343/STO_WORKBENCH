using System;
using System.Collections.Generic;
using System.IO;

internal static class Sorter
{
    static int Main()
    {
        string path = null;
        while (path == null)
        {
            Console.WriteLine("Write the path to directory");
            path = Console.ReadLine();
            if (!Directory.Exists(path))
            {
                path = null;
                Console.WriteLine("!!Directory does not exist!!");
            }
        }

        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        Console.WriteLine("**Directory exist**");
        Console.WriteLine("---------------------------------------------------------------------------");
        Console.WriteLine("**Sorting documents**");
        Console.WriteLine("---------------------------------------------------------------------------");

        List<string> detFiles = GetDetFilesList(directoryInfo);
        if (detFiles.Count == 0)
        {
            Console.WriteLine("!!det_*.xls files do not exist!!");
        }
        else
        {
            foreach (string item in detFiles)
            {
                try
                {
                    string fileName = GetFileName(item);
                    string directoryName = GetDirectoryName(fileName);
                    SortDetFiles(directoryName, fileName, directoryInfo, path);
                    SortOdFiles(directoryName, directoryInfo, path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("!!ERROR processing " + GetFileName(item) + ": " + ex.Message);
                }
            }
        }

        Console.WriteLine("---------------------------------------------------------------------------");
        Console.WriteLine("**Sorting complete**");
        return 0;
    }

    static List<string> GetDetFilesList(DirectoryInfo directoryInfo)
    {
        List<string> detList = new List<string>();
        foreach (FileInfo file in directoryInfo.GetFiles("det_*.xls"))
        {
            detList.Add(file.FullName);
        }
        return detList;
    }

    static string GetFileName(string filePath)
    {
        return Path.GetFileName(filePath);
    }

    static string GetDirectoryName(string fileName)
    {
        string[] words = Path.GetFileNameWithoutExtension(fileName).Split('_');
        if (words.Length < 3)
        {
            throw new InvalidOperationException("invalid file name format, expected det_<...>_<name>_<...>.xls");
        }
        return words[2];
    }

    static void SortDetFiles(string directoryName, string fileName, DirectoryInfo directoryInfo, string path)
    {
        Console.WriteLine("**Creation directory " + directoryName + "**");
        directoryInfo.CreateSubdirectory(directoryName);
        string sourceFilePath = Path.Combine(path, fileName);
        string subDirectoryPath = Path.Combine(path, directoryName, fileName);
        MoveFile(sourceFilePath, subDirectoryPath, fileName, directoryName);
    }

    static string GetOdFile(string directoryName, DirectoryInfo directoryInfo)
    {
        foreach (FileInfo file in directoryInfo.GetFiles("od_*_" + directoryName + "_*.xls"))
        {
            return file.FullName;
        }
        return null;
    }

    static void SortOdFiles(string directoryName, DirectoryInfo directoryInfo, string path)
    {
        string odFilePath = GetOdFile(directoryName, directoryInfo);
        if (odFilePath == null)
        {
            Console.WriteLine("!!od-file for " + directoryName + " does not exist!!");
            return;
        }

        string fileName = GetFileName(odFilePath);
        string subDirectoryPath = Path.Combine(path, directoryName, fileName);
        MoveFile(odFilePath, subDirectoryPath, fileName, directoryName);
    }

    static void MoveFile(string sourceFilePath, string targetFilePath, string fileName, string directoryName)
    {
        if (File.Exists(targetFilePath))
        {
            Console.WriteLine("!!file " + fileName + " already exists in " + directoryName + "!!");
            return;
        }
        File.Move(sourceFilePath, targetFilePath);
    }
}
