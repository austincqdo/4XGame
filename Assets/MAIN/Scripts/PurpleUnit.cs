using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleUnit : Unit
{

    protected override void SetType()
    {
        this.type = "PurpleUnit";
    }

    protected override void SetHealth()
    {
        this.health = 100f;
    }
}
