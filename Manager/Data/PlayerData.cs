using System.Collections.Generic;
[System.Serializable]
public class PlayerData
{
    public int PlotStage;
    public int day;
    public int money;
    public CookData cookData;
    public PlayerData(int cur_plotStage = 0, int cur_day = 0, int cur_money = 0)
    {
        day = cur_day;
        money = cur_money;
        PlotStage = cur_plotStage;
        cookData = new CookData();
    }
    public List<int> remaining;
}

[System.Serializable]
public class CookData
{
    public List<string> teas;
    public List<string> toppings;
    public List<string> facilities;
    public List<Product> menu;
    public CookData()
    {
        teas = new List<string>();
        toppings = new List<string>();
        facilities = new List<string>();
        menu = new List<Product>();
    }
}

[System.Serializable]
public class Product
{
    public string name;
    public List<string> ingredients;
}