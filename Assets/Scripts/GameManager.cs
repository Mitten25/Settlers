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
    public List<GameObject> Players = new List<GameObject>();

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

        TileOptions = new List<Material>(new Material[19] { Desert, Fields, Fields, Fields, Fields, Forest, Forest,
            Forest, Forest, Hills, Hills, Hills, Mountains, Mountains, Mountains, Pasture, Pasture, Pasture, Pasture });

        Tiles = GameObject.FindGameObjectsWithTag("Tile");
        //TileOptions = 

        DontDestroyOnLoad(gameObject);
        BuildMap();

        Populate(4);
        Place();
        RefreshPlayerObjects();
    }

    void Update()
    {

    }

    void Place()
    {
        List<Vector3> positions = new List<Vector3>() { new Vector3(5, 0, -4), new Vector3(-5, 0, -4), new Vector3(5, 0, 4), new Vector3(-5, 0, 4) };
        int id = 1;
        for (int i = Players.Count; i > 0; i--)
        {         
            int tempInt = Random.Range(0, i - 1);
            Players[tempInt].GetComponent<PlayerClass>().playerID = id;
            Transform.Instantiate(Players[tempInt], positions[i-1], Quaternion.identity);
            Players.RemoveAt(tempInt);
            id++;
        }
    }

    //put clones in empty players list
    void RefreshPlayerObjects()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        Players = new List<GameObject>(temp) { };
    }

    void Populate(int total)
    {
        for (int i = total; i > 0; i--)
        {
            GameObject newPlayer = (GameObject)Resources.Load("Player", typeof(GameObject));
            Players.Add(newPlayer);
        }
        //for (int i = 0; i < total; i++)
        //{
        //    Players[i].GetComponent<PlayerClass>().playerID = i + 1;
        //}
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
