using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SystemUI
{
    public class SystemUIButtonManage : MonoBehaviour
    {
        public List<Button> buttons= new List<Button>();
        public GameObject SavePanel;
        // Start is called before the first frame update
        void Start()
        {   
            SavePanel.SetActive(false);
            buttons[0].onClick.AddListener(SaveActive);
            buttons[1].onClick.AddListener(LoadActive);
            buttons[2].onClick.AddListener(ToTittle);   
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("now escaping");
                Quit();
            }
        }

        private void Quit()
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }

        private void SaveActive()
        {
            Debug.Log("now Saving");
            transform.parent.gameObject.SetActive(false);
            SavePanel.SetActive(true);
        }

        private void LoadActive()
        {
            Debug.Log("now Loading");
        }

        private void ToTittle()
        {
            Debug.Log("now to Tittle");
        }

    }

}