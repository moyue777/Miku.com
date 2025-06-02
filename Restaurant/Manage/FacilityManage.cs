using UnityEngine;
using System.Collections.Generic;
public partial class Order : MonoBehaviour
{
    public Dictionary<string, List<string>> today_menu = new Dictionary<string, List<string>>();//本次营业菜单
    public Dictionary<string, int> price = new Dictionary<string, int>();//价格
    public List<string> today_facilities = new List<string>();//其他设施
    public List<string> today_tea = new List<string>();//茶
    public List<string> today_topping = new List<string>();//小料
    public List<Vector2> tea_pos = new List<Vector2>();//茶位置
    public List<Vector2> topping_pos = new List<Vector2>();//小料位置

    public List<GameObject> Teas = new List<GameObject>();
    public List<GameObject> Toppings = new List<GameObject>();
    private void LoadData()
    {
        LoadMenu();
        LoadIngredient();
        Generate();
    }
    private void LoadMenu()
    {
        foreach (var item in superController.playerData.cookData.menu)
        {
            today_menu.Add(item.name, item.ingredients);
        }
    }
    private void LoadIngredient()
    {
        foreach (var item in superController.playerData.cookData.facilities)
        {
            today_facilities.Add(item);
        }
        foreach (var item in superController.playerData.cookData.teas)
        {
            today_tea.Add(item);
        }
        foreach (var item in superController.playerData.cookData.toppings)
        {
            today_topping.Add(item);
        }
    }

    private void Generate()
    {
        for (int i = 0; i < tea_pos.Count; i++)
        {
            GameObject tea_prefab = Resources.Load<GameObject>("cook/Tea");
            tea_prefab.GetComponent<TeaWaitingProcess>().ingredient = today_tea[i];
            Teas.Add( Instantiate(tea_prefab, tea_pos[i], Quaternion.identity));
        }
        for (int i = 0; i < topping_pos.Count; i++)
        {
            GameObject topping_prefab = Resources.Load<GameObject>("cook/Topping");
            topping_prefab.GetComponent<Ingredient>().ingredientName = today_topping[i];
            Toppings.Add(Instantiate(topping_prefab, topping_pos[i], Quaternion.identity));
        }
    }
}