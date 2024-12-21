using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WaitingProcess : MonoBehaviour
{
    public int waitingTime = 5;
    public List<Vector2> cupPosition = new List<Vector2>();
    public List<bool> hadCup = new List<bool>();
    protected Vector2 curEmpty;
    // Start is called before the first frame update
    public void Start()
    {
        hadCup.Add(false);
    }
    public int getWaitTime()
    {
        return waitingTime;
    }

    public Vector2 getEmptyPositon()
    {
        if(findEmpty() == false){
            Debug.Log("no empty");
        }
        return curEmpty;
    }
    public bool findEmpty()
    {
        for (int i = 0; i < cupPosition.Count; i++)
        {
            if(hadCup[i] == true)
            {
                continue;
            }else
            {
                hadCup[i] = true;
                curEmpty = cupPosition[i];
                return true;
            }
        }
        return false;
    }
}