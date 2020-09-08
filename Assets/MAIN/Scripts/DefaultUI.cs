using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DefaultUI : MonoBehaviour
{
    public static DefaultUI instance;

    private GameObject GameCanvas;
    public TextMeshProUGUI TurnDisplay;

    void Awake()
    {
        instance = this;
        GameCanvas = gameObject;
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }
}
