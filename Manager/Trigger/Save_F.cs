using UnityEngine;
public class Save_F : MyTrigger
{
    public SuperController super_controller;
    public MessageCanvas messageCanvas;
    protected override void OnFKeyPressed()
    { 
        
        super_controller.SavePlayerData(1);
    }
}