using UnityEngine;
using UnityEngine.UI;
public class NextButton : MonoBehaviour
{
    public PlotManager plotManager;
    public Button jumpButton;
    void Start()
    {
        plotManager = FindObjectOfType<PlotManager>();
        jumpButton.onClick.AddListener(()=>{
            plotManager.jump();
        });
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && plotManager.superController.isTalking == true)
        {
            Onclick();
        }
        
    }
    public void Onclick()
    {
        plotManager.ClickNext();
    }

    
}