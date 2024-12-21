using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class StaticSaveManager
{
    public const string SaveFolder = "Saves";
    public const string SaveFileExtension = ".save";
    public const string SaveList = ".savelist";

    // 返回存档表的路径
    private static string SaveSlotsPath
    {
        get { return Path.Combine(Application.persistentDataPath, "saveSlots.dat"); }
    }
    
    // 返回文件保存的路径
    private static string GetSavePath(int cursaveSlot)
    {
        return Path.Combine(Application.persistentDataPath, SaveFolder, cursaveSlot + SaveFileExtension);
    }
    
    // 保存当前数据
    public static void SavePlayerData(PlayerData playerData, int saveSlot)
    {
        if (IsSaveSlotAvailable(saveSlot))
        {
            string path = GetSavePath(saveSlot);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(path);
            bf.Serialize(file, playerData);
            file.Close();
        }
        else
        {
            Debug.LogError("Save slot is already in use or save slots are full.");
        }
    }

    // 读取存档
    public static PlayerData LoadPlayerData(int saveSlot)
    {
        string path = GetSavePath(saveSlot);
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            PlayerData playerData = (PlayerData)bf.Deserialize(file);
            file.Close();
            return playerData;
        }
        return null;
    }

    // 删除存档
    public static void DeleteSave(int saveSlot)
    {
        string path = GetSavePath(saveSlot);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    // 检查存档是否存在
    public static bool IsSaveSlotAvailable(int saveSlot)
    {
        if ( File.Exists(GetSavePath(saveSlot)) )
        {
            return true;
        }else{
            return false;
        }
    }
}

