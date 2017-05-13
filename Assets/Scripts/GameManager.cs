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
    public int activePlayer;
    public string currentPhase;

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

        DontDestroyOnLoad(gameObject);
        BuildMap();

        Populate(4);
        Place();
        RefreshPlayerObjects();
        //TODO: Probability Tiles
        currentPhase = "startPlacement";
        activePlayer = 1;
    }

    void Update()
    {
        //Placement Phase
        if (currentPhase == "startPlacement")
        {
            PlacementPhase();
            currentPhase = "inPlacement";
        }
    }

    void PlacementPhase()
    {
        StartCoroutine("Placement");
    }

    public IEnumerator Placement()
    {
        //code to determine who goes first, for now just 1-4
        activePlayer = 1;
        while (!Players[activePlayer-1].GetComponent<PlayerClass>().placementPhaseCompleted)
        {
            Debug.Log("p1Turn");
            yield return 0;
        }
        activePlayer = 2;
        while (!Players[activePlayer - 1].GetComponent<PlayerClass>().placementPhaseCompleted)
        {
            Debug.Log("p2Turn");
            yield return 0;
        }
        activePlayer = 3;
        while (!Players[activePlayer - 1].GetComponent<PlayerClass>().placementPhaseCompleted)
        {
            Debug.Log("p3Turn");
            yield return 0;
        }
        activePlayer = 4;
        while (!Players[activePlayer - 1].GetComponent<PlayerClass>().placementPhaseCompleted)
        {
            Debug.Log("p4Turn");
            yield return 0;
        }
    }

    void Place()
    {
        List<Vector3> positions = new List<Vector3>() { new Vector3(0, 0, 6), new Vector3(6, 0, 0), new Vector3(0, 0, -6), new Vector3(-6, 0, 0) };
        int id = 0;
        for (int i = Players.Count; i > 0; i--)
        {         
            int tempInt = Random.Range(0, i - 1);
            Players[tempInt].GetComponent<PlayerClass>().playerID = id+1;
            Transform.Instantiate(Players[tempInt], positions[tempInt], Quaternion.identity);
            positions.RemoveAt(tempInt);
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
        //    int tempInt = Random.Range(0, i);
        //    Players[tempInt].GetComponent<PlayerClass>().playerID = i + 1;
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
