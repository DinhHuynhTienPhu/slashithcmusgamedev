using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using JetBrains.Annotations;
using System.ComponentModel.Design;
using LitJson;

 [Serializable] 
public class PyramidData {
    public int unlockedMaze; //so maze đã unlock
    public List<int> stars=new List<int>(100); //ket qua star cac man choi
    
}
[System.Serializable]
public class DataManager
{
    private static DataManager _instance;

    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Init();
            }

            return _instance;
        }
    }



    #region PLAYER INFO & STAT
    public bool firtsTime = true;

    public int levelPlay = 1;
    public int stagePlay = 1;
    public int soundvolume=70;
    public int musicvolume = 70;

    public DateTime lastTimeWatchAd=DateTime.MinValue;
    #endregion




    static void Init()
    {
        Load();
        if (Instance.firtsTime)
        {
            
            
            Instance.firtsTime = false;
        }

        // Add any code here
        // Maybe check if data is not valid

    }

    public static void ResetToDefault()
    {
        _instance = new DataManager();
        Save();
    }

    #region SAVE/LOAD/RESET

    private const string dataKey = "Data";

    public static void Save()
    {
        //        string data = JsonUtility.ToJson(_instance);
        string data = JsonFieldOnlyMapper.ToJson(_instance);
        PlayerPrefs.SetString(dataKey, data);
    }

    static void Load()
    {
        if (PlayerPrefs.HasKey(dataKey))
        {
            string data = PlayerPrefs.GetString(dataKey, "{}");
            _instance = JsonFieldOnlyMapper.ToObject<DataManager>(data);
            //            _instance = JsonUtility.FromJson<DataManager>(data);
        }
        else
        {
            ResetToDefault();
        }
    }

    internal class hero
    {
    }

    #endregion
}