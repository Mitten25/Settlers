using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour {

    public int playerID = 0;
	// Use this for initialization
	void Start ()
    {
		if(playerID == 1)
        {
            this.GetComponent<Renderer>().material = (Material)Resources.Load("Fields", typeof(Material));
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playerID == 1)
        {
            this.GetComponent<Renderer>().material = (Material)Resources.Load("Fields", typeof(Material));
        }
    }
}
