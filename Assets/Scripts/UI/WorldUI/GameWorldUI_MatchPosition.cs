using System;
using System.Collections.Generic;
using UnityEngine;
public class GameWorldUI_MatchPosition: MonoBehaviour
{
    Transform owner;

    public void SetOwner(Transform owner)
    {
        this.owner = owner;
    }

    public void FixedUpdate()
    {
        if(owner != null)
        this.transform.position = owner.position;
    }
}

