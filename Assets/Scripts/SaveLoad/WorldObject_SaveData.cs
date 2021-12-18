﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class WorldObject_SaveData
{
    public string worldObjectType;

    public Vector3Int worldPosition;

    public Component_SaveData[] component_SaveDatas;
}
