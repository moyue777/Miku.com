using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

public class Store : MonoBehaviour
{
    private List<Vector2> store_positions = new List<Vector2>();
    private List<bool> store_available = new List<bool>();
    public int max_store_count;
    void Start()
    {
        store_available.Clear();
        store_positions.Clear();
        
        store_positions.Add(new Vector2(3.64f,-2.01f));
        store_positions.Add(new Vector2(4.39f,-1.88f));
        store_positions.Add(new Vector2(5.36f,-2.1f));
        
        store_available.Add(true);
        store_available.Add(true);
        store_available.Add(true);
        
        max_store_count = store_available.Count;
    }
    public int HaveEmptyPosition()
    {
        for (int i = 0; i < store_available.Count; i++)
        {
            if (store_available[i])
            {
                return i;
            }
        }
       return -1;
    }

    public int GetDefaultPosition()
    {
        for (int i = 0; i < store_available.Count; i++)
        {
            if (store_available[i])
            {
                store_available[i] = false;
                return i;
            }
        }
        return -1;
    }
    
    public int onlyGet()
    {
        for (int i = 0; i < store_available.Count; i++)
        {
            if (store_available[i])
            {
                return i;
            }
        }
        return -1;
    }
    public Vector2 GetPosVector2(int cur_cup)
    {
        return store_positions[cur_cup];
    }

    public void ReleasePos(int cup_pos)
    {
        store_available[cup_pos] = true;
    }
}