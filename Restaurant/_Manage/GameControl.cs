using UnityEngine;

public class GameControl : Timer
{
    public float threshold;
    public GameObject customerPrefab; // 顾客预制体
    public Transform[] customerPositions; // 顾客位置数组

    protected override void Start()
    {
        base.Start();
        StartTimer();
    }

    public override void RepeatedAction()
    {
        if (customerPositions.Length == 0) return; // 如果没有顾客位置，直接返回

        // 随机选择一个位置
        int randomIndex = Random.Range(0, customerPositions.Length);
        Transform randomPosition = customerPositions[randomIndex];

        // 在随机位置生成顾客预制体
        GameObject customerObject = Instantiate(customerPrefab, randomPosition.position, randomPosition.rotation);
        CustomController customControl = customerObject.GetComponent<CustomController>();
        if (customControl != null)
        {
            customControl.SetTargetPosition(randomPosition);
        }
    }
}