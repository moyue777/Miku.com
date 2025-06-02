using UnityEngine;

public class CustomController : MonoBehaviour
{
    public enum CustomerState { Entering, Waiting, Leaving }
    public CustomerState currentState = CustomerState.Entering;
    public Transform targetPosition;
    public float moveSpeed = 2f;
    public float waitTime = 5f; // 等待时间
    private float waitTimer;

    void Update()
    {
        switch (currentState)
        {
            case CustomerState.Entering:
                MoveToTarget();
                break;
            case CustomerState.Waiting:
                Wait();
                break;
            case CustomerState.Leaving:
                Leave();
                break;
        }
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
        if (transform.position == targetPosition.position)
        {
            currentState = CustomerState.Waiting;
            waitTimer = Time.time + waitTime;
        }
    }

    void Wait()
    {
        if (Time.time >= waitTimer)
        {
            currentState = CustomerState.Leaving;
        }
    }

    void Leave()
    {
        Destroy(gameObject);
    }

    public void SetTargetPosition(Transform position)
    {
        targetPosition = position;
    }
}