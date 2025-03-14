using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insidehoose : MonoBehaviour
{
    public SuperController superController;
    public PlayerData playerData;

    void Start()
    {
        playerData = new PlayerData();
        superController = SuperController.Instance; 
    }

    public void SetPlayerData()
    {
        superController.playerData = playerData;
    }


}
