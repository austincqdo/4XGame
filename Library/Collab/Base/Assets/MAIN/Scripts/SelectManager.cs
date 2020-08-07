using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectManager : MonoBehaviour
{
    private PlayerInfo playerInfo;
    
    // Start is called before the first frame update
    void Start()
    {
        playerInfo = GameObject.Find("PlayerInfo").GetComponent < PlayerInfo > ();
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
                if (!playerInfo.GetSelectedUnits()[unitHit.GetComponent<BasicUnit>().id])
                {
                    SelectUnit(unitHit);
                }
            }
            else //no hit, so deselect all
            {
                if (playerInfo.GetSelectedUnits().Any(s => s))
                {
                    int idx = playerInfo.GetSelectedUnits().IndexOf(true);
                    DeselectUnit(playerInfo.GetUnits()[idx]);
                }
            }
        }
    }

    public void SelectUnit(GameObject o)
    {
        Color tmp = o.GetComponent<SpriteRenderer>().color;
        tmp.a = .75f;
        o.GetComponent<SpriteRenderer>().color = tmp;
        playerInfo.ChangeSelectedUnitStatus(o.GetComponent<BasicUnit>().id, true);
    }

    public void DeselectUnit(GameObject o)
    {
        Color tmp = o.GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        o.GetComponent<SpriteRenderer>().color = tmp;
        playerInfo.ChangeSelectedUnitStatus(o.GetComponent<BasicUnit>().id, false);
    }
}
