using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileClass : MonoBehaviour
{

    public GameObject LeftTile;
    public GameObject RightTile;
    public GameObject LeftInsideTile;
    public GameObject RightInsideTile;
    public List<GameObject> SettlementNodes = new List<GameObject>();
    public string Type;
    public int Probability;

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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //will be reduced to else statement when all tiles have meshes
        //if (transform.GetChild(0).name == "default")
        //{
        //    Type = transform.GetChild(0).gameObject.GetComponent<Renderer>().material.name;
        //    Type = Type.Split(' ')[0];
        //}
        //else
        //{
        //    Type = transform.GetChild(0).name;
        //    Type = Type.Split('(')[0];
        //}

        //if (transform.GetChild(1) != null)
        //{
        //        Probability = int.Parse(transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text);
        //}

        //Only necessary while tiles lack set nodes
        if (SettlementNodes.Count > 0)
        {
            if (SettlementNodes[0].GetComponent<SettlementNodeClass>().placed == true)
            {
                SettlementNodes[5].SetActive(false);
                SettlementNodes[1].SetActive(false);
            }
            else if (SettlementNodes[1].GetComponent<SettlementNodeClass>().placed == true)
            {
                SettlementNodes[0].SetActive(false);
                SettlementNodes[2].SetActive(false);
            }
            else if (SettlementNodes[2].GetComponent<SettlementNodeClass>().placed == true)
            {
                SettlementNodes[1].SetActive(false);
                SettlementNodes[3].SetActive(false);
            }
            else if (SettlementNodes[3].GetComponent<SettlementNodeClass>().placed == true)
            {
                SettlementNodes[2].SetActive(false);
                SettlementNodes[4].SetActive(false);
            }
            else if (SettlementNodes[4].GetComponent<SettlementNodeClass>().placed == true)
            {
                SettlementNodes[3].SetActive(false);
                SettlementNodes[5].SetActive(false);
            }
            else if (SettlementNodes[5].GetComponent<SettlementNodeClass>().placed == true)
            {
                SettlementNodes[0].SetActive(false);
                SettlementNodes[4].SetActive(false);
            }
        }
    }
}
