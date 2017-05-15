using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementNodeClass : MonoBehaviour {

    public GameObject blueSettlement;
    public GameObject redSettlement;
    public GameObject orangeSettlement;
    public GameObject whiteSettlement;
    public GameObject currentBuilding;
    public bool hasBuilding;
    public bool placed;
    public Dictionary<int, GameObject> ColorCode = new Dictionary<int, GameObject>();
    // Use this for initialization
    void Start ()
    {
        //should eventually map color to color chosen by certain player
        ColorCode.Add(1, blueSettlement);
        ColorCode.Add(2, redSettlement);
        ColorCode.Add(3, orangeSettlement);
        ColorCode.Add(4, whiteSettlement);
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnMouseOver()
    {
        if (GameManager.instance.currentPhase == "inPlacement" && !placed)
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
                placed = true;
                GameManager.instance.Players[activePlayer - 1].GetComponent<PlayerClass>().initialSettlement = true;
            }
        }
    }
}
