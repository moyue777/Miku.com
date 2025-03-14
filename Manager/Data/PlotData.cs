using System.Collections.Generic;
[System.Serializable]
public class Dialogue
{
    public string voice;
    public string character;
    public string text;
}

[System.Serializable]
public class UpdateList
{
    public List<string> active_triggers;
}

[System.Serializable]
public class scripts
{
    public string names;
    public List<Dialogue> dialogues;
}

[System.Serializable]
public class PlotData
{
    public int plot_id;
    public int next_id;
    public string waiting_trigger;
    public UpdateList updateList;
    public List<scripts> scripts;
}