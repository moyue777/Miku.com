using System.Collections.Generic;
using UnityEngine;
public static class SceneJumpData
{
    /// <summary>
    /// 新场景需要的玩家位置
    /// </summary>
    private static Dictionary<string, Vector3> jumpVector = new Dictionary<string, Vector3>(){
        {"HomeScene" , new Vector2(-4, -3.12f)},
        {"SubwayScene" , new Vector2(-1, -3.12f)},
        {"InsideRestaurant" , new Vector2(0, 0)},
        {"StreetSceneLeft" , new Vector2(-10, -2.1f)},
        {"StreetSceneRight" , new Vector2(4.3f, -2.1f)},
    };

    public static Vector3 GetJumpVector(string sceneName)
    {
        if(jumpVector.ContainsKey(sceneName))
        {
            return jumpVector[sceneName];
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }
}

public static class SceneTriggers
{
    /// <summary>
    /// 场景当中需要的trigger
    /// </summary>
    private static Dictionary<string,List<string>> scene_triggers = new Dictionary<string, List<string>>()
    {
        {"StreetScene" , new List<string>(){"02","03","04" }},
        {"SubwayScene" , new List<string>(){"01" }},
        {"HomeScene" , new List<string>(){"00" }},
    };

    public static List<string> GetSceneTriggers(string sceneName)
    {
        if (scene_triggers.ContainsKey(sceneName))
        {
            return scene_triggers[sceneName];
        }
        else
        {
            return new List<string>();
        }
    }
}
