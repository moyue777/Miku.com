using UnityEngine;
using TMPro;
public class FinalResult : MonoBehaviour
{
    public void showResult(float money) 
    {
        TMP_Text result= transform.Find("finalincome").gameObject.GetComponent<TMP_Text>();
        result.text = "You have earned <color=yellow>" + money + "</color>";
    }
}
