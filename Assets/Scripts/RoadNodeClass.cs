using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNodeClass : MonoBehaviour {

    public GameObject blueRoad;
    public GameObject redRoad;
    public GameObject orangeRoad;
    public GameObject whiteRoad;
    public GameObject currentBuilding;
    public bool hasBuilding;
    public bool placed;
    public Dictionary<int, GameObject> ColorCode = new Dictionary<int, GameObject>();
	// Use this for initialization
	void Start ()
    {
        ColorCode.Add(1, blueRoad);
        ColorCode.Add(2, redRoad);
        ColorCode.Add(3, orangeRoad);
        ColorCode.Add(4, whiteRoad);
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
        if ((phase == "inPlacement" && GameManager.instance.Players[activePlayer - 1].GetComponent<PlayerClass>().initialRoad == 0 && GameManager.instance.Players[activePlayer - 1].GetComponent<PlayerClass>().initialSettlement <= 1) ||
            (phase == "inPlacement" && GameManager.instance.Players[activePlayer - 1].GetComponent<PlayerClass>().initialRoad == 1 && GameManager.instance.Players[activePlayer - 1].GetComponent<PlayerClass>().initialSettlement >= 1))
        {
            if (!hasBuilding)
            {
                currentBuilding = Transform.Instantiate(ColorCode[activePlayer], this.transform);
                hasBuilding = true;
            }
            if (Input.GetMouseButtonDown(0))
            {
                placed = true;
                GameManager.instance.Players[activePlayer - 1].GetComponent<PlayerClass>().initialRoad ++;
            }
        }
    }
}
