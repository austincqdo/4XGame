using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColliderTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
        	Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        	RaycastHit hit;
        	if (Physics.Raycast(ray, out hit))
        	{
        		print("COLLIDER TEST PASSES");
        	}
        }
    }
}
