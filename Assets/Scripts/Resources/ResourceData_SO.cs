using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameObjects/ResourceData")]
public class ResourceData_SO: ScriptableObject
{
    public string resourceName;

    public Sprite icon;

    public Sprite icon_selected;

    [TextArea]
    public string discription;
}

