using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 A LocalGameManager is a something that keeps track of what state the (local) game is in, manages the menu/pause systems, 
 records and stores information for various purposes (audio/video settings, control bindings). 

 Local client will use this to keep track of
- local Settings:
    - audio
    - controls
    - etc.

 From the state data of this game manager, one should be able to generate local settings files.
 
 Logic in this component is not networked in any way whatsoever.

 A LocalGameManager only exists during the main game scene, i.e. it is not created during a lobby scene.
**/


public class LocalGameManager : MonoBehaviour
{

    #region Singleton
    // Check to see if we're about to be destroyed.
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static LocalGameManager m_Instance;

    /// Access singleton instance through this propriety.
    public static LocalGameManager Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(LocalGameManager) +
                    "' already destroyed. Returning null.");
                return null;
            }

            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    // Search for existing instance.
                    m_Instance = (LocalGameManager)FindObjectOfType(typeof(LocalGameManager));

                    // Create new instance if one doesn't already exist.
                    if (m_Instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<LocalGameManager>();
                        singletonObject.name = typeof(LocalGameManager).ToString() + " (Singleton)";

                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return m_Instance;
            }
        }
    }

    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }


    private void OnDestroy()
    {


        m_ShuttingDown = true;
    }



    #endregion



    // Bootstrap Game Systems (e.g. Audio) Here
    void Awake()
    {
        Debug.Log("Initializing Settings Manager");

    }


    // Update is called once per frame
    void Update()
    {

    }

}
