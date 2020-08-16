using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleUnit : Unit
{
    protected override void Awake()
    {
        base.Awake();
        this.type = "PurpleUnit";
        this.maxHealth = 150f;
    }
}