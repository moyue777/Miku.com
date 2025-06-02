using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;

public static class StaticSaveManager
{
    public const string SaveFolder = "Saves";
    public const string SaveFileExtension = ".save";
    public const string SaveList = ".savelist";
    /// <summary>
    /// 返回文件保存的路径
    /// </summary>
    /// <param name="cursaveSlot"></param>
    /// <returns></returns>
    private static string GetSavePath(int cursaveSlot)
    {
        Debug.Log(Application.persistentDataPath);
        return Path.Combine(Application.persistentDataPath, SaveFolder, cursaveSlot + SaveFileExtension);
    }

    /// <summary>
    /// 获取所有存档
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, DateTime> GetFile_Times()
{
    string saveDir = Path.Combine(Application.persistentDataPath, SaveFolder);
    Dictionary<string, DateTime> saveDict = new Dictionary<string, DateTime>();
    Debug.Log(GetSavePath(0));

    if (!Directory.Exists(saveDir))
    {
        Directory.CreateDirectory(saveDir); // 如果文件夹不存在，则创建
    }

    Debug.Log("getting all files");
    string[] saveFiles = Directory.GetFiles(saveDir, "*" + SaveFileExtension);

    foreach (var filePath in saveFiles)
    {
        string fileName = Path.GetFileName(filePath); // 获取带扩展名的文件名
        DateTime lastWriteTime = File.GetLastWriteTime(filePath);
        saveDict[fileName] = lastWriteTime;
    }

    return saveDict;
}

    /// <summary>
    /// 保存当前数据
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="saveSlot"></param>
    public static void SavePlayerData(PlayerData playerData, int saveSlot)
    {
        string path = GetSavePath(saveSlot);

        try
        {
            // 确保目录存在
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            string dataToSave = JsonUtility.ToJson(playerData, true); // true 表示格式化输出
            File.WriteAllText(path, dataToSave);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"保存失败: {e.Message}");
        }
    }

    /// <summary>
    /// 读取存档
    /// </summary>
    /// <param name="saveSlot"></param>
    /// <returns></returns>
    public static PlayerData LoadPlayerData(int saveSlot)
    {
        string path = GetSavePath(saveSlot);

        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                return JsonUtility.FromJson<PlayerData>(json);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"加载失败: {e.Message}");
            }
        }
        return null;
    }

    /// <summary>
    /// 删除存档
    /// </summary>
    /// <param name="saveSlot"></param>
    public static void DeleteSave(int saveSlot)
    {
        string path = GetSavePath(saveSlot);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

}

