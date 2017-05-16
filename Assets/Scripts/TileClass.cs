using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClass : MonoBehaviour {

    public GameObject LeftTile;
    public GameObject RightTile;
    public GameObject LeftInsideTile;
    public GameObject RightInsideTile;
    public List<GameObject> SettlementNodes = new List<GameObject>();

    //Nodes are placed in the List as such:
    //
    //                 0
    //                 +
    //   5       +           +       1
    //     +                       +
    //     +                       +
    //     +                       +
    //     +                       +
    //     +                       +
    //     +                       +
    //     +                       +
    //   4       +           +       2 
    //                 +
    //                 3
    //

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Only necessary while tiles lack set nodes
		if (SettlementNodes.Count > 0)
        {
            if (SettlementNodes[0].GetComponent<SettlementNodeClass>().placed == true
                || SettlementNodes[2].GetComponent<SettlementNodeClass>().placed == true
                || SettlementNodes[4].GetComponent<SettlementNodeClass>().placed == true)
            {
                SettlementNodes[1].SetActive(false);
                SettlementNodes[3].SetActive(false);
                SettlementNodes[5].SetActive(false);
            }
            else if (SettlementNodes[1].GetComponent<SettlementNodeClass>().placed == true
                        || SettlementNodes[3].GetComponent<SettlementNodeClass>().placed == true
                        || SettlementNodes[5].GetComponent<SettlementNodeClass>().placed == true)
            {
                SettlementNodes[0].SetActive(false);
                SettlementNodes[2].SetActive(false);
                SettlementNodes[4].SetActive(false);
            }
        }
	}
}
