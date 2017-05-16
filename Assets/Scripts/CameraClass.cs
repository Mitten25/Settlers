using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClass : MonoBehaviour {
    
    public GameObject centerTile;
	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = new Vector3(GameManager.instance.Players[GameManager.instance.activePlayer-1].transform.position.x, 9, GameManager.instance.Players[GameManager.instance.activePlayer - 1].transform.position.z);
        this.transform.LookAt(centerTile.transform);
	}
}
