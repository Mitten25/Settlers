using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeClass : MonoBehaviour {

    public GameObject blueSettlement;
    public GameObject currentBuilding;
    public bool hasBuilding;
    public bool placed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnMouseOver()
    {
        if (GameManager.instance.activePlayer == 1 && !hasBuilding)
        {
            currentBuilding = Transform.Instantiate(blueSettlement, this.transform);
            hasBuilding = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("clicked");
            placed = true;
        }
    }

    private void OnMouseExit()
    {
        if (!placed)
        {
            Destroy(currentBuilding);
            hasBuilding = false;
        }
    }
}
