using UnityEngine;

public class MessageCanvas : MonoBehaviour
{
    public bool is_active;
    public void SetActive(bool active)
    {
        is_active = active;
        gameObject.SetActive(active);
    }
}