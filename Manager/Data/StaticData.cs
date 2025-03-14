using System.Collections.Generic;
using UnityEngine;
public static class SceneJumpData
{
    private static Dictionary<string, Vector3> jumpVector = new Dictionary<string, Vector3>(){
        {"HomeScene" , new Vector2(-4, -0.58f)},
        {"InsideRestaurant" , new Vector2(0, 0)},
        {"StreetSceneLeft" , new Vector2(-10, -2.3f)},
        {"StreetSceneRight" , new Vector2(4.3f, -2.3f)},
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