using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour {

    public int playerID = 0;
    public int initialSettlement;
    public int initialRoad;
    public bool placementPhase1Completed;
    public bool placementPhase2Completed;
    // Use this for initialization
    void Start ()
    {
		if(playerID == 1)
        {
            this.GetComponent<Renderer>().material = (Material)Resources.Load("Blue", typeof(Material));
        }
        else if (playerID == 2)
        {
            this.GetComponent<Renderer>().material = (Material)Resources.Load("Red", typeof(Material));
        }
        else if (playerID == 3)
        {
            this.GetComponent<Renderer>().material = (Material)Resources.Load("Orange", typeof(Material));
        }
        else if (playerID == 4)
        {
            this.GetComponent<Renderer>().material = (Material)Resources.Load("White", typeof(Material));
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (initialSettlement == 1 && initialRoad == 1)
        {
            placementPhase1Completed = true;
        }
        if (initialSettlement == 2 && initialRoad == 2)
        {
            placementPhase2Completed = true;
        }
    }
}
