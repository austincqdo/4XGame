using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicUnit : Unit
{

    protected override void Awake()
    {
        base.Awake();
        this.type = "BasicUnit";
        this.maxHealth = 100f;
    }
}
