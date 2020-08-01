using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private Vector3 spawnPosition;

    // testing getcomponent instead of accessors
    private List<GameObject> units;
    private List<bool> selectedUnits;
    // end testing

    [SerializeField]
    [Tooltip("The Basic Unit prefab.")]
    private GameObject basicUnit;

    private Dictionary<string, GameObject> unitStringToGameObject;
    

    void Awake()
    {
        GameObject playerInfoObject = GameObject.Find("PlayerInfo");
        playerInfo = playerInfoObject.GetComponent<PlayerInfo>();
        spawnPosition = new Vector3(0f, 0f, 0f);

        unitStringToGameObject = new Dictionary<string, GameObject>();
        unitStringToGameObject.Add("BasicUnit", basicUnit);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            SpawnUnit();
        }
    }

    public void SpawnUnit(string unitType="BasicUnit")
    {
        GameObject newUnit = Instantiate(unitStringToGameObject[unitType], spawnPosition, Quaternion.identity);
        playerInfo.units.Add(newUnit);
        playerInfo.selectedUnits.Add(false);
        spawnPosition.y++;
    }

    public void DespawnUnit(int unitId)
    {

    }
}
