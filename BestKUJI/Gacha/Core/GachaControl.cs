using GachaSystem.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace  GachaSystem.Core
{

    public class GachaControl : MonoBehaviour
    {
        public GachaSystem gachaSystem;
        public EggManager eggManager;
        public Button gachaButton;
        void Start()
        {
            gachaSystem = gameObject.GetComponent<GachaSystem>();
            eggManager = gameObject.GetComponent<EggManager>();
            gachaButton.onClick.AddListener(() =>
            {
                DrawAction();
                var result = gachaSystem.DrawSingle();
                Debug.Log(result.id);
            });
        }


        /// <summary>
        /// 处理演出逻辑
        /// </summary>
        void DrawAction()
        {
            eggManager.Draw();
        }
    }
}