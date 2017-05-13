using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClass : MonoBehaviour {

    private List<Vector3> positions;
    public GameObject centerTile;
	// Use this for initialization
	void Start ()
    {
        positions = new List<Vector3>() { new Vector3(0, 9, 6), new Vector3(6, 9, 0), new Vector3(0, 9, -6), new Vector3(-6, 9, 0) };
    }
	
	// Update is called once per frame
	void Update ()
    {
            this.transform.position = new Vector3(GameManager.instance.Players[GameManager.instance.activePlayer-1].transform.position.x, 9, GameManager.instance.Players[GameManager.instance.activePlayer - 1].transform.position.z);
            this.transform.LookAt(centerTile.transform);
	}
}
