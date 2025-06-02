using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SystemUI
{
    /// <summary>
    /// 管理MenuUI
    /// </summary>
    public class SystemUIButtonManage : MonoBehaviour
    {
        [Header("JumpButton in order")]
        public List<CustomButton> buttons = new List<CustomButton>();
        [Header("TargetPanel in order")]
        public List<GameObject> panels = new List<GameObject>();
        public GameObject SystemUI;
        public Text text;
        private List<string> button_texts;
        // Start is called before the first frame update
        void Start()
        {
            // 加载 button 的文本
            TextAsset textAsset = Resources.Load<TextAsset>("UI/systemUIText");
            SystemUIText systemUIText;
            if (textAsset != null)
            {
                systemUIText = JsonUtility.FromJson<SystemUIText>(textAsset.text);
                button_texts = systemUIText.buttonText;
            }

            // 初始化每个 panel
            foreach (GameObject panel in panels)
            {
                panel.SetActive(false);
                panel.GetComponent<UIButtonManage>().MenuCanva = gameObject;
            }

            // 添加 Listener
            for (int i = 0; i < buttons.Count; i++)
            {
                int index = i; // 捕获当前索引
                buttons[i].onClick.AddListener(() =>
                {
                    gameObject.SetActive(false);
                    // 确保索引在有效范围内
                    if (index >= 0 && index < panels.Count)
                    {
                        foreach (GameObject panel in panels)
                        { panel.SetActive(false); }
                        panels[index].SetActive(true);
                    }
                });

                buttons[i].Setbutton_num(i);
                buttons[i].SetSystemUIButtonManage(this);
            }
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

        public void LoadText(int button_num)
        {
            if (button_num >= 0 && button_num < button_texts.Count)
            { text.text = button_texts[button_num]; }
            else
            { text.text = ""; }

        }
        private void Quit()
        {
            FindObjectOfType<SuperController>().CloseSystem();
        }
    }
}