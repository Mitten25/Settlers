using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private GameObject[] Tiles;
    private Material Desert, Fields, Forest, Hills, Mountains, Pasture;
    private List<Material> TileOptions;

    // Use this for initialization
    void Awake ()
    {
		if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        Desert = (Material)Resources.Load("Desert", typeof(Material));
        Fields = (Material)Resources.Load("Fields", typeof(Material));
        Forest = (Material)Resources.Load("Forest", typeof(Material));
        Hills = (Material)Resources.Load("Hills", typeof(Material));
        Mountains = (Material)Resources.Load("Mountains", typeof(Material));
        Pasture = (Material)Resources.Load("Pasture", typeof(Material));

        TileOptions = new List<Material>(new Material[19] { Desert, Fields, Fields, Fields, Fields, Forest, Forest, Forest, Forest, Hills, Hills, Hills, Mountains, Mountains, Mountains, Pasture, Pasture, Pasture, Pasture });

        Tiles = GameObject.FindGameObjectsWithTag("Tile");
        //TileOptions = 

        DontDestroyOnLoad(gameObject);
        BuildMap();
	}

    void Update()
    {
    }

    void BuildMap()
    {
        foreach(GameObject Tile in Tiles)
        {
            int tempInt = Random.Range(0, TileOptions.Count-1);
            Tile.transform.FindChild("default").gameObject.GetComponent<Renderer>().material = TileOptions[tempInt];
            TileOptions.RemoveAt(tempInt);
        }
    }
}
