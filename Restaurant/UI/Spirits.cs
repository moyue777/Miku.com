using UnityEngine.UI;
using UnityEngine;

public class Spirits : MonoBehaviour
{
    private Slider slider; // 血条Slider组件
    public HealthControl customer; 
    
    void Start()
    {
        slider = gameObject.GetComponentInChildren <Slider>(); 
   
    }
    void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (slider != null)
        {            // 更新血条的值
            slider.value = customer.currentHealth / customer.maxHealth;
        }
    }
}

