using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementNodeClass : MonoBehaviour {

    public GameObject blueSettlement;
    public GameObject currentBuilding;
    public bool hasBuilding;
    public bool placed;
    public Dictionary<int, GameObject> ColorCode = new Dictionary<int, GameObject>();
    // Use this for initialization
    void Start ()
    {
        ColorCode.Add(1, blueSettlement);
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnMouseOver()
    {
        if (GameManager.instance.currentPhase == "inPlacement")
            BuildingPlacement(GameManager.instance.activePlayer, GameManager.instance.currentPhase);
    }

    private void OnMouseExit()
    {
        if (!placed)
        {
            Destroy(currentBuilding);
            hasBuilding = false;
        }
    }

    void BuildingPlacement(int activePlayer, string phase)
    {
        if (phase == "inPlacement" && !GameManager.instance.Players[activePlayer - 1].GetComponent<PlayerClass>().initialSettlement)
        {
            if (!hasBuilding)
            {
                currentBuilding = Transform.Instantiate(ColorCode[activePlayer], this.transform);
                hasBuilding = true;
            }
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("clicked");
                placed = true;
                GameManager.instance.Players[activePlayer - 1].GetComponent<PlayerClass>().initialSettlement = true;
            }
        }
    }
}
