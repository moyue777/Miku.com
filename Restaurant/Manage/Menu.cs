using System.Collections.Generic;
using System.Data;


public class Menu
{
    public static Dictionary<string, bool> menu = new Dictionary<string, bool>();
    public static Dictionary<string, int> price = new Dictionary<string, int>();
    public static Dictionary<string, List<string>> ingerdients = new Dictionary<string, List<string>>();
    
    //更新单个product
    public static void UpdateMenu(string target_product)
    {
        if (menu.ContainsKey(target_product))
        {
            menu[target_product] = true;
        }
    }

    //加载全部product
    public static void UpdateMenu(Dictionary<string, bool> loadmenu)
    {
        menu = loadmenu;
    }

    //检查单一product是否解锁
    public static bool CheckMenu(string target_product)
    {
        return menu[target_product];
    }

    //获取ingredients
    public static List<string> GetIngerdients(string target_products)
    {
        return ingerdients[target_products];
    }
}

