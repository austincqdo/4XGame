using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicUnit : Unit
{

    void Awake()
    {
        this.type = "BasicUnit";
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    // May move this code to central unit controlling class so click detection only needs to happen once.
    // Move to Player.cs? and keep bool array of isSelected to scan everytime click happens outside of any
    // sprite collider
    void Update()
    {
        //if (Mouse.current.rightButton.wasPressedThisFrame)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        //    RaycastHit hit;
        //    if ((Physics.Raycast(ray, out hit)) && (hit.collider.gameObject == gameObject))
        //    {
        //        if (!isSelected)
        //        {
        //            isSelected = true;
        //            Color tmp = GetComponent<SpriteRenderer>().color;
        //            tmp.a = .75f;
        //            GetComponent<SpriteRenderer>().color = tmp;
        //        }
        //    } else // either ray hits different collider or no ray is hit, so deselect unit
        //    {
        //        if (isSelected)
        //        {
        //            isSelected = false;
        //            Color tmp = GetComponent<SpriteRenderer>().color;
        //            tmp.a = 1f;
        //            GetComponent<SpriteRenderer>().color = tmp;
        //        }
        //    }
        //}
    }
}
