using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName ="GameObjects/UnitData")]
public class UnitData_SO : ScriptableObject
{
    public string unitName;

    [TextArea]
    public string unitDiscription;

    public Sprite unitIcon;
}
