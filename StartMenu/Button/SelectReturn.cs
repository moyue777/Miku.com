using UnityEngine;

public class SelectReturn : MonoBehaviour
{
    public void Onclick()
    {
        SuperController superController = SuperController.Instance;
        if (superController != null)
        {
            Debug.Log("Save");
            superController.CallSystem(true);
        }
    }
}
