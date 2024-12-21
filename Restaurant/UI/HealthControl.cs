using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthControl : MonoBehaviour
{
    public float maxHealth = 100f;
    public float diffHealth = 0;
    public float diff;//距离customer的距离，向上为正    
    public float currentHealth;
    public GameObject healthBarPrefab; // 血条Prefab引用
    private Coroutine damageCoroutine; // 用于存储协程的引用
    private GameObject healthBarObject;
    private Canvas targetCanvas;
    public string targetCanvasName = "CustomerStatus"; // 指定Canvas的名称

    void Start()
    {
        currentHealth = maxHealth;
        targetCanvas = null;
    }
    void Update()
    {
        //Debug.Log(currentHealth);
    }
    public void beginWaiting()
    {
        InstantiateHealthBar();
        // 启动每秒扣血的协程
        damageCoroutine = StartCoroutine(TakeDamageEverySecond());
    }

    private void InstantiateHealthBar()
    {
        if (healthBarPrefab != null)
        {
            // 自动获取指定名称的Canvas
            targetCanvas = FindCanvasByName(targetCanvasName);
            if (targetCanvas != null)
            {
                healthBarObject = Instantiate(healthBarPrefab, targetCanvas.transform);
                healthBarObject.GetComponent<Spirits>().customer = this;
                // 获取健康条对象的 RectTransform
                RectTransform healthBarRectTransform = healthBarObject.GetComponent<RectTransform>();

                // 计算健康条在 Canvas 上的具体位置
                Vector2 canvasPosition = CalculateCanvasPosition(gameObject, targetCanvas);

                // 设置健康条的位置
                healthBarRectTransform.anchoredPosition = canvasPosition;        
            }
            else
            {
                Debug.LogError("Canvas not found: " + targetCanvasName);                
            }
            
            }
        }

        private Canvas FindCanvasByName(string canvasName)
        {
            Canvas[] canvases = FindObjectsOfType<Canvas>();
            foreach (Canvas canvas in canvases)
            {
                if (canvas.name == canvasName)
                {
                    return canvas;
                }
            }
            return null;
        }

    Vector2 CalculateCanvasPosition(GameObject currentObject, Canvas targetCanvas)
    {
        // 将当前物体的世界坐标转换为屏幕坐标
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(currentObject.transform.position);

        // 将屏幕坐标转换为 Canvas 的局部坐标
        RectTransform canvasRectTransform = targetCanvas.GetComponent<RectTransform>();
        Vector2 canvasLocalPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPosition, Camera.main, out canvasLocalPosition);

        // 调整位置使其位于物体下方
        canvasLocalPosition.y += diff; // 调整 y 坐标的偏移量

        return canvasLocalPosition;
    }
    private IEnumerator TakeDamageEverySecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // 等待一秒钟
            TakeDamage(diffHealth);
        }
    }

    private void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die(true);
        }
    }

    public void Die(bool needQuit = false)
    {
        // 停止扣血协程
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }
        // 销毁血条对象

        if (healthBarObject != null)
        {
            Destroy(healthBarObject);
        }
        
        if (needQuit)
        {
            Customer custom = gameObject.GetComponent<Customer>();
            custom.Quit();  
        }
    }

    public float getHealth()
    { return currentHealth; }
}