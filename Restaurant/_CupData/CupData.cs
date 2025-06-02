using System.Collections.Generic;
using UnityEngine;
public class CupAttribute
{
    public string Name { get; set; }
    public object Value { get; set; }

    public CupAttribute(string name, object value)
    {
        Name = name;
        Value = value;
    }
}
[CreateAssetMenu(fileName = "CookConfig", menuName = "Cook System/Config")]
public class CupData
{
    private List<CupAttribute> attributes;
    private List<CupAttribute> initialAttributes;

    public CupData()
    {
        attributes = new List<CupAttribute>
        {
            new CupAttribute("Topping1", false),
            new CupAttribute("Topping2", false),
            new CupAttribute("Milk", false),
            new CupAttribute("Tea", false),
            new CupAttribute("Ice", false),
            new CupAttribute("Packing", false),
            new CupAttribute("Sugar", 0)
        };

        // 备份初始属性值
        initialAttributes = new List<CupAttribute>(attributes);
    }

    public void SetAttribute(string name, object value)
    {
        CupAttribute attribute = attributes.Find(attr => attr.Name == name);
        if (attribute != null)
        {
            attribute.Value = value;
        }
    }

    public object GetAttribute(string name)
    {
        CupAttribute attribute = attributes.Find(attr => attr.Name == name);
        if (attribute != null)
        {
            return attribute.Value;
        }
        return null;
    }

    public void ResetAttributes()
    {
        for (int i = 0; i < attributes.Count; i++)
        {
            attributes[i].Value = initialAttributes[i].Value;
        }
    }
}