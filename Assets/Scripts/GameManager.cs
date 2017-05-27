using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private GameObject[] Tiles;
    private Material Desert, Fields, Forest, Hills, Mountains, Pasture;
    private List<Material> TileOptions;
    private List<int> Probabilities;
    public List<GameObject> Players = new List<GameObject>();
    public int activePlayer;
    public string currentPhase;
    public GameObject ProbabilityTile;
    public List<int> PlayerOrder = new List<int>();

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
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

        Probabilities = new List<int>(new int[18] { 5, 2, 6, 3, 8, 10, 9, 12, 11, 4, 8, 10, 9, 4, 5, 6, 3, 11 });

        DontDestroyOnLoad(gameObject);
        BuildMap();

        Populate(4);
        Place();
        RefreshPlayerObjects();
        ProbabilityTiles();
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

    IEnumerator PlayerPlacement(int activePlayerID, int placementPhase)
    {
        activePlayer = activePlayerID;
        if (placementPhase == 1)
        {
            while (!Players[activePlayer - 1].GetComponent<PlayerClass>().placementPhase1Completed)
            {
                Debug.Log("p1Turn");
                yield return 0;
            }
        }
        else if (placementPhase == 2)
        {
            while (!Players[activePlayer - 1].GetComponent<PlayerClass>().placementPhase2Completed)
            {
                Debug.Log("p1Turn");
                yield return 0;
            }
        }
    }

    public IEnumerator Placement()
    {
        //TODO: code to determine who goes first, for now just 1-4
        yield return StartCoroutine(PlayerPlacement(1, 1));
        yield return StartCoroutine(PlayerPlacement(2, 1));
        yield return StartCoroutine(PlayerPlacement(3, 1));
        yield return StartCoroutine(PlayerPlacement(4, 1));
        yield return StartCoroutine(PlayerPlacement(4, 2));
        yield return StartCoroutine(PlayerPlacement(3, 2));
        yield return StartCoroutine(PlayerPlacement(2, 2));
        yield return StartCoroutine(PlayerPlacement(1, 2));
        //activePlayer = 1;
        //while (!Players[activePlayer - 1].GetComponent<PlayerClass>().placementPhase1Completed)
        //{
        //    Debug.Log("p1Turn");
        //    yield return 0;
        //}
    }

    void ProbabilityTiles()
    {
        List<string> StartTiles = new List<string>() { "Tile1", "Tile3", "Tile8", "Tile12", "Tile17", "Tile19" };
        int tempInt = Random.Range(0, 5);
        string StartTileName = StartTiles[tempInt];
        int StartTileIndex = 0;
        for (int i = 0; i < Tiles.Length; i++)
        {
            if (Tiles[i].name == StartTileName)
            {
                //come look at this later, if starting corner is desert need to do something other than changing index by one, might be fixed by allowing
                //players to choose.
                if (Tiles[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.name == "Desert (Instance)")
                {
                    StartTileIndex = i - 1;
                    break;
                }
                else
                {
                    StartTileIndex = i;
                    break;
                }
            }
        }
        List<GameObject> Visited = new List<GameObject>();
        GameObject CurrentTile = Tiles[StartTileIndex];
        Visited.Add(CurrentTile);
        for (int i = 0; i < 18; i++)
        {
            if (CurrentTile.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.name != "Desert (Instance)")
            {
                GameObject ProbTile = Instantiate(ProbabilityTile, CurrentTile.transform);
                ProbTile.transform.Find("Canvas").Find("Text").gameObject.GetComponent<Text>().text = Probabilities[i].ToString();
                if (!Visited.Contains(CurrentTile.GetComponent<TileClass>().RightTile))
                {
                    CurrentTile = CurrentTile.GetComponent<TileClass>().RightTile;
                    Visited.Add(CurrentTile);
                }
                else
                {
                    CurrentTile = CurrentTile.GetComponent<TileClass>().RightInsideTile;
                    Visited.Add(CurrentTile);
                }
            }
            else
            {
                Visited.Add(CurrentTile);
                if (Visited.Contains(CurrentTile.GetComponent<TileClass>().RightTile))
                    CurrentTile = CurrentTile.GetComponent<TileClass>().RightInsideTile;
                else
                    CurrentTile = CurrentTile.GetComponent<TileClass>().RightTile;
                GameObject ProbTile = Instantiate(ProbabilityTile, CurrentTile.transform);
                ProbTile.transform.Find("Canvas").Find("Text").gameObject.GetComponent<Text>().text = Probabilities[i].ToString();
                if (!Visited.Contains(CurrentTile.GetComponent<TileClass>().RightTile))
                {
                    CurrentTile = CurrentTile.GetComponent<TileClass>().RightTile;
                    Visited.Add(CurrentTile);
                }
                else
                {
                    CurrentTile = CurrentTile.GetComponent<TileClass>().RightInsideTile;
                    Visited.Add(CurrentTile);
                }
            }
        }


    }
    void Place()
    {
        List<Vector3> positions = new List<Vector3>() { new Vector3(0, 0, 6), new Vector3(6, 0, 0), new Vector3(0, 0, -6), new Vector3(-6, 0, 0) };
        List<int> playerids = new List<int>() { 1, 2, 3, 4 };
        for (int i = Players.Count; i > 0; i--)
        {
            int tempInt = Random.Range(0, i - 1);
            Players[tempInt].GetComponent<PlayerClass>().playerID = playerids[tempInt];
            PlayerOrder.Add(playerids[tempInt]);
            Transform.Instantiate(Players[tempInt], positions[0], Quaternion.identity);
            positions.RemoveAt(0);
            playerids.RemoveAt(tempInt);
            Players.RemoveAt(tempInt);
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
    }
    void BuildMap()
    {
        foreach (GameObject Tile in Tiles)
        {
            int tempInt = Random.Range(0, TileOptions.Count - 1);
            if (TileOptions[tempInt].name != "Forest" && TileOptions[tempInt].name != "Fields")
            {
                Tile.transform.Find("default").gameObject.GetComponent<Renderer>().material = TileOptions[tempInt];
                Tile.GetComponent<TileClass>().Type = TileOptions[tempInt].name;
            }
            //Example of how tiles will be made when we have meshes for them all
            else if (TileOptions[tempInt].name == "Forest")
            {
                Destroy(Tile.transform.Find("default").gameObject);
                GameObject ForestTile = Instantiate((GameObject)Resources.Load("ForestTile", typeof(GameObject)), Tile.transform);
                ForestTile.transform.parent = Tile.transform;
                Tile.GetComponent<TileClass>().Type = "Forest";
            }
            else if (TileOptions[tempInt].name == "Fields")
            {
                Destroy(Tile.transform.Find("default").gameObject);
                GameObject ForestTile = Instantiate((GameObject)Resources.Load("FieldsTile", typeof(GameObject)), Tile.transform);
                ForestTile.transform.parent = Tile.transform;
                Tile.GetComponent<TileClass>().Type = "Fields";
            }
            TileOptions.RemoveAt(tempInt);
        }
    }
}
