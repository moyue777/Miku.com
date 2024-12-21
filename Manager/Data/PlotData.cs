using System.Collections.Generic;
[System.Serializable]
public class Dialogue
{
    public string character;
    public string text;
}

[System.Serializable]
public class UpdateList
{
    public Dictionary<int, string> target;
}

[System.Serializable]
public class PlotData
{
    public int plot_id;
    public int waiting_trigger;
    public UpdateList updateList;
    public List<Dialogue> dialogues;
}