using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicUnit : Unit
{



    protected override void SetType()
    {
        this.type = "BasicUnit";
    }

    protected override void SetHealth()
    {
        this.health = 100f;
    }

}
