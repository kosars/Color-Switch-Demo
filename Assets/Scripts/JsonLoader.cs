using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class JsonLoader
{
    private const string FILE_EXTENSION = @".json";
    /// <summary>
    /// Removes the default file extension from path.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static string RemoveFileExtension(string path)
    {
        if (path.Length >= FILE_EXTENSION.Length)
        {
            //If file extension exist, remove it.
            if (path.ToLower().Substring(path.Length - FILE_EXTENSION.Length, FILE_EXTENSION.Length) == FILE_EXTENSION.ToLower())
                return path.Substring(0, path.Length - FILE_EXTENSION.Length);
            //File extension doesn't exist.
            else
                return path;
        }
        //Path isn't long enough to contain file extension.
        else
        {
            return path;
        }
    }

    /// <summary>
    /// Removes the directory separator if at the begining of path.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static string RemoveLeadingDirectorySeparator(string path)
    {
        //Remove directory separate character if it exist on the first character.
        if (char.Parse(path.Substring(0, 1)) == Path.DirectorySeparatorChar || char.Parse(path.Substring(0, 1)) == Path.AltDirectorySeparatorChar)
            return path.Substring(1);
        else
            return path;
    }

    /// <summary>
    /// Returns string result of a text file from Resources.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static string ReturnFileResource(string path)
    {
        //Remove default file extension and format the path to the platform.
        path = RemoveFileExtension(path);
        path = RemoveLeadingDirectorySeparator(path);

        if (path == string.Empty)
        {
            Debug.LogError("ReturnFileResource -> path is empty.");
            return string.Empty;
        }

        //Try to load text from file path.
        TextAsset textAsset = Resources.Load(path) as TextAsset;

        if (textAsset != null)
            return textAsset.text;
        else
            return string.Empty;
    }

    /// <summary>
    /// Returns a database at the file path.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public static List<T> ReturnDatabase<T>(string path)
    {
        string result = ReturnFileResource(path);

        if (result.Length != 0)
        {
            return JsonConvert.DeserializeObject<List<T>>(result);
        }
        else
        {
            Debug.LogWarning("ReturnDatabase -> result text is empty.");
            return new List<T>();
        }
    }
}
