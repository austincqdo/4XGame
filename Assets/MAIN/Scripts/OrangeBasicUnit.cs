using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OrangeBasicUnit : Unit
{

    protected override void Awake()
    {
        base.Awake();
        this.type = "Basic Unit";
        this.maxHealth = 100f;
    }
}
