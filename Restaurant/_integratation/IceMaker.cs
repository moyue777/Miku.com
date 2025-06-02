using UnityEngine;
using System.Collections;

public class IceMaker : PressFacility
{
    private int iceCount = 0;
    private int iceCount_max = 10;
    private float incrementTime = 1f; // 每次增加的时间间隔
    private int epochIceAdd_max = 5;
    private Coroutine currentCoroutine = null; // 用于存储当前协程

    public override void Handle()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(IncrementValueCoroutine());
        }
        else
        {
            Debug.Log("A coroutine is already running.");
        }
    }

    IEnumerator IncrementValueCoroutine()
    {
        for (int i = 0; (iceCount < iceCount_max) && (i < epochIceAdd_max); i++)
        {
            iceCount++;
            Debug.Log("Current iceCount: " + iceCount);
            yield return new WaitForSeconds(incrementTime);
        }
        Debug.Log("Target value reached: " + iceCount);
        currentCoroutine = null; // 协程结束后重置标志变量
    }
}