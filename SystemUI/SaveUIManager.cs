using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SystemPanel;
    public Button returnButton;
    void Start()
    {
        returnButton.onClick.AddListener(() =>
        {
            //SuperController.Instance.PauseStop();
            gameObject.SetActive(false);
            SystemPanel.SetActive(true);
        }
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
