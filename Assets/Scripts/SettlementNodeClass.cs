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
        if (GameManager.instance.currentPhase == GameManager.state.PLACEMENT && !placed)
            BuildingPlacement(GameManager.instance.activePlayer, GameManager.state.PLACEMENT);
    }

    private void OnMouseExit()
    {
        if (!placed)
        {
            Destroy(currentBuilding);
            hasBuilding = false;
        }
    }

    void BuildingPlacement(int activePlayer, GameManager.state phase)
    {
        if ((phase == GameManager.state.PLACEMENT && GameManager.instance.Players[activePlayer - 1].GetComponent<PlayerClass>().initialSettlement == 0 && GameManager.instance.Players[activePlayer - 1].GetComponent<PlayerClass>().initialRoad <= 1)
            || (phase == GameManager.state.PLACEMENT && GameManager.instance.Players[activePlayer - 1].GetComponent<PlayerClass>().initialSettlement == 1 && GameManager.instance.Players[activePlayer - 1].GetComponent<PlayerClass>().initialRoad >= 1))
        {
            if (!hasBuilding)
            {
                currentBuilding = Transform.Instantiate(ColorCode[activePlayer], this.transform);
                hasBuilding = true;
            }
            if (Input.GetMouseButtonDown(0))
            {
                placed = true;
                GameManager.instance.Players[activePlayer - 1].GetComponent<PlayerClass>().initialSettlement ++;
            }
        }
    }
}
