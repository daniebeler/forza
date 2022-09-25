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
        string savePath = GetPaths();
        
        Debug.Log("Saving Data at " + savePath);
        string json = JsonUtility.ToJson(saveObject);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.WriteLine(json);
    }

    [CanBeNull]
    public static UserData LoadUserData()
    {
        if (File.Exists(GetPaths()))
        {
            Debug.Log("file exists");
            using StreamReader reader = new StreamReader(GetPaths());
            string json = reader.ReadToEnd();
            UserData data = JsonUtility.FromJson<UserData>(json);
            Debug.Log(data.CurrentCarId);
            return data;
        }
        return null;
    }
}
