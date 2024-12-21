[System.Serializable]
public class PlayerData
{
    public int PlotStage;
    public int day;
    public int money;

    public PlayerData(int cur_plotStage = 0, int cur_day = 0, int cur_money = 0)
    {
        day = cur_day;
        money = cur_money;
        PlotStage = cur_plotStage;
    }
}