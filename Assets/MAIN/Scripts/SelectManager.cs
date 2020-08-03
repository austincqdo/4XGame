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
                GameObject unitHit = hit.collider.gameObject;
                if (!unitHit.GetComponent<Unit>().selected)
                {
                    SelectUnit(unitHit);
                }
            }
            else //no hit, so deselect all
            {
                GameObject selectedUnit = player.GetUnits().Find(s => s.GetComponent<Unit>().selected);
                if (selectedUnit)
                {
                    DeselectUnit(selectedUnit);
                }
            }
        }
    }

    public void SelectUnit(GameObject o)
    {
        Color tmp = o.GetComponent<SpriteRenderer>().color;
        tmp.a = .75f;
        o.GetComponent<SpriteRenderer>().color = tmp;
        o.GetComponent<Unit>().selected = true;
    }

    public void DeselectUnit(GameObject o)
    {
        Color tmp = o.GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        o.GetComponent<SpriteRenderer>().color = tmp;
        o.GetComponent<Unit>().selected = false;
    }
}
