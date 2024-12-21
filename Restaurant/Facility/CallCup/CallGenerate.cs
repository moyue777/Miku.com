using UnityEngine;

public class CallGenerate : MonoBehaviour
{
    public GameObject target;
    public Vector3 spawnPosition; // 指定生成位置
    public Store store;
    private int max_cup_count = 0;
    private int cur_cup_count = 0;
    // Start is called before the first frame update
    void Start()
    {
        max_cup_count = store.max_store_count;
        cur_cup_count = 0;
        Debug.Log("" + max_cup_count);
        Debug.Log("" + cur_cup_count);
    }
    
    public void CupDestroyed(int cup_pos)
    {
        cur_cup_count--;
        store.ReleasePos(cup_pos);
    }

    public void OnClick()
    {
        Debug.Log("" + cur_cup_count);
        Debug.Log("" + max_cup_count);
        
        if (cur_cup_count<max_cup_count)
        {
            GameObject cur_cup= Instantiate(target);
            cur_cup.transform.position = store.GetPosVector2(store.onlyGet());
            Builder builder = cur_cup.GetComponent<Builder>();
            builder.store = store;
            builder.default_pos = store.GetDefaultPosition();
            builder.defaultPosition = store.GetPosVector2(builder.default_pos);   
            cur_cup_count++;
        }
        
    }
    // Update is called once per frame
}