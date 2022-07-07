using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class BinarySaveSystem
{
    public static void SaveSystem<T>(T data, string path) where T : class
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
        {
            bf.Serialize(fs, data);
        }
    }

    public static T LoadSystem<T>(string path) where T : class
    {
        if (!File.Exists(path)) return default;
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            return bf.Deserialize(fs) as T;
        }
    }
}
