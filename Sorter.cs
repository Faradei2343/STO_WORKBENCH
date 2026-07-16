using System;

public class Class1
{
    string path = null;

    static List<string> GetDetFilesList(DirectoryInfo directoryInfo)
    {
        List<string> detList = new List<string>();
        foreach (var filename in directoryInfo.GetFiles("det_*.xls"))
        {
            detList.Add(filename.ToString());
        }

        if (detList.Count > 0)
        {
            return detList;
        }
        else
        {
            Console.WriteLine("!!det_.xls files does not exists!!");
            return null;
        }
    }

    static string GetFileName(string filePath)
    {
        string fileName = Path.GetFileName(filePath);
        return fileName;
    }

    static string GetDirectoryName(string fileName)
    {
        string[] words = fileName.Split('_');
        string directoryName = words[2];
        return directoryName;
    }


    static int SortDetFiles(string directoryName, string fileName, DirectoryInfo directoryInfo, string path)
    {
        Console.WriteLine("**Creation directory " + directoryName + "**");
        directoryInfo.CreateSubdirectory(directoryName);
        string subDirectoryPath = path + @"\" + directoryName + @"\" + fileName;
        string sourceFilePath = path + @"\" + fileName;
        File.Move(sourceFilePath, subDirectoryPath);
        return 0;
    }


    static string GetOdFile(string directoryName, DirectoryInfo directoryInfo)
    {
        List<string> odPair = new List<string>();
        foreach (var filename in directoryInfo.GetFiles("od_*_" + directoryName + "_*.xls"))
        {
            odPair.Add(filename.ToString());
        }

        if (odPair.Count > 0)
        {
            return odPair[0];
        }
        else
        {
            return null;
        }
    }

    static int SortOdFiles(string directoryName, DirectoryInfo directoryInfo, string path)
    {
        string subDirectoryPath = path + @"\" + directoryName + @"\" + GetFileName(GetOdFile(directoryName, directoryInfo));
        File.Move(GetOdFile(directoryName, directoryInfo), subDirectoryPath);
        return 0;
    }


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

if (GetDetFilesList(directoryInfo) != null)
{

    foreach (var item in GetDetFilesList(directoryInfo))
    {
        string fileName = GetFileName(item);
        string directoryName = GetDirectoryName(fileName);
        SortDetFiles(directoryName, fileName, directoryInfo, path);
        SortOdFiles(directoryName, directoryInfo, path);
    }
}
Console.WriteLine("---------------------------------------------------------------------------");
Console.WriteLine("**Sorting complete**");

public Class1()
	{

	}
}
