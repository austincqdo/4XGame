﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBasicUnit : Unit
{
    protected override void Awake()
    {
        base.Awake();
        this.type = "Purple Unit";
        this.maxHealth = 150f;
    }
}