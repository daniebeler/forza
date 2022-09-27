using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using Object = System.Object;

public class SaveData : MonoBehaviour
{
    private static String GetPaths()
    {
        //Test Path
        //return Application.dataPath + Path.AltDirectorySeparatorChar + "SaveUserData.json";
        //Persistent Path
        return Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveUserData.json";
    }

    public static void SaveDataVoid(UserData saveObject)
    {
        Debug.Log("save data");
        string savePath = GetPaths();
        
        string json = JsonUtility.ToJson(saveObject);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.WriteLine(json);
    }

    [CanBeNull]
    public static UserData LoadUserData()
    {
        Debug.Log(GetPaths());
        if (File.Exists(GetPaths()))
        {
            using StreamReader reader = new StreamReader(GetPaths());
            string json = reader.ReadToEnd();
            UserData data = JsonUtility.FromJson<UserData>(json);
            return data;
        }

        return null;
    }
}