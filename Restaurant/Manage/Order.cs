using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public partial class Order : MonoBehaviour
{
    public SuperController superController;
    public float ordertime;//生成的间隔
    public float duration = 30f;//总时间
    public List<float> move_time = new List<float> { 8.0f, 6.0f, 4.0f };//移动时间

    public GameObject customer;//指向顾客的引用
    public GameObject uiElement; // 指向UI元素的引用

    public CallGenerate callGenerater;//召唤的Button
    public TMP_Text show_time;//时间
    public TMP_Text money;//金钱

    public List<Vector3> spawnposes = new List<Vector3>(); // 指定生成位置
    public List<bool> spawned = new List<bool>();//生成位置当前是否有顾客
    private float timer;//生成计时器

    private float today_income;
    private float timecalculater = 0f;

    // Start is called before the first frame update
    void Start()
    {
        superController = SuperController.Instance;
        //初始化
        DeactivateUI();
        timer = 0;
   
        //总计时器
        InvokeRepeating("UpdateTimer", 0f, 1f);

        //加载菜单，原料
        LoadData();
        
        //初始化位置状态
        for (int j = 0; j < 3; j++)
        {
            spawned.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= ordertime)
        {
            bool hasGenerated = GenerateCustomer();
            if (hasGenerated)
            {
                timer = 0f;
            }
        }
        show_time.text = FormatTime(duration - timecalculater);
        money.text = "收入:" + today_income;
    }
    public string FormatTime(float seconds)
    {
        int minutes = (int)(seconds / 60);
        int remainingSeconds = (int)(seconds % 60);

        return string.Format("{0:D2}:{1:D2}", minutes, remainingSeconds);
    }
    //生成顾客
    bool GenerateCustomer()
    {
        bool generated = false;
        for (int i = 0; i < spawnposes.Count; i++)
        {
            if (spawned[i] == false)
            {
                generated = true;
                spawned[i] = true;

                GameObject current_custom = Instantiate(customer, spawnposes[i], Quaternion.identity);
                Customer current_one = current_custom.GetComponent<Customer>();

                //设置customer
                if (current_one != null)
                {
                    current_one.pos = i; // 设置参数

                    current_one.order = this;
                    current_one.SetMoveTime(move_time[i]);
                    current_one.SetPos(spawnposes[i]);

                    string cur_product = GenerateOrder();
                    current_one.required_product.Add(cur_product, false);
                    current_one.ingredients = today_menu[cur_product];
                    current_one.price = price[cur_product];
                }
                break;
            }
        }
        return generated;
    }

    //生成订单
    public string GenerateOrder()
    {
        if (today_menu == null || today_menu.Count == 0)
        {
            Debug.LogError("Today's menu is empty or not set.");
            return "";
        }

        // 获取今天菜单的所有键
        List<string> keys = new List<string>(today_menu.Keys);

        // 随机选择一个键
        int randomIndex = Random.Range(0, keys.Count);
        string selectedKey = keys[randomIndex];

        return selectedKey;
    }

    //顾客更新状态    
    public void hasServed(int pos, int cup_pos, float money)
    {
        Debug.Log("earn" + money);
        callGenerater.CupDestroyed(cup_pos);
        today_income += money;
        spawned[pos] = false;
    }
    public void hasServed(int pos)
    {
        Debug.Log("顾客逃跑了！！！");
        spawned[pos] = false;
    }

    // 调用这个方法来激活UI
    public void ActivateUI()
    {
        Time.timeScale = 0;
        uiElement.SetActive(true);
    }

    // 调用这个方法来禁用UI
    public void DeactivateUI()
    {
        uiElement.SetActive(false);
    }

    //invoke总计时器
    private void UpdateTimer()
    {
        timecalculater += 1f;
        if (timecalculater >= duration)
        {
            Debug.Log("time is up");
            ActivateUI();

            /*******************
            *仅供测试
            *销毁心态条
            */
            HealthControl[] spirits = FindObjectsOfType<HealthControl>();

            // 遍历并输出每个 Builder 对象的信息
            foreach (HealthControl health in spirits)
            {
                health.Die();
            }


            uiElement.GetComponent<FinalResult>().showResult(today_income);
            timecalculater = 0f; // Reset timer if you want to reuse it
        }
    }

    public List<string> GetIngredients(string target_product)
    {
        return today_menu[target_product];
    }
    public void OnDestroy()
    {
        CancelInvoke("UpdateTimer");
    }
}


