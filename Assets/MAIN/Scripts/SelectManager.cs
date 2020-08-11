using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectManager : MonoBehaviour
{
    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero);
            if (hit.collider)
            {
                Unit unit = hit.collider.gameObject.GetComponent<Unit>();
                if (!unit.selected)
                {
                    unit.Select();
                }
            }
            else //no hit, so deselect all
            {
                GameObject selectedUnitObject = player.GetUnits().Find(s => s.GetComponent<Unit>().selected);
                if (selectedUnitObject)
                {
                    selectedUnitObject.GetComponent<Unit>().Deselect();
                }
            }
        }
    }
}
