using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private Tile wallTile;
    [SerializeField]
    private Tile boxTile;
    [SerializeField]
    private Tile floorTile;
    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private GameObject box;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject GameManager;

    [Range(0, 10)]
    public int numofBoxes;

    public int boxesSpawned;



    public bool useRandomSeed;
    public string seed;

    [Range(1, 70)]
    public int randomFillPercent;


    public int width;
    public int height;
    Vector3Int testGrid = new Vector3Int(1, 3, 0);

    public int smoothingIterations;

    int[,] map;


    private void Start()
    {
        GenerateLevel();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateLevel();
        }


        if (Input.GetMouseButtonDown(1))
        {

            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int location = tilemap.WorldToCell(mp);

            if (tilemap.GetTile(location))
            {
                Debug.Log("Tile");
            }


        }


    }

    public void GenerateLevel()
    {
        map = new int[width, height];
        RandomFillMap();
        Debug.Log("Num of Boxes " + numofBoxes);

        for (int i = 0; i < smoothingIterations; i++)
        {
           SmoothLevel();
        }
    }//GenerateLevel

    public void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = Time.time.ToString();
        }
        System.Random prng = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //Ensure level will be closed in with walls
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (prng.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }

            }
        }
        FillLevel();



    }//RandomFillMap

    public void FillLevel()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int pos = new Vector3Int(x - width / 2, y - height / 2, 0);


                if (map[x, y] == 1)
                {
                    tilemap.SetTile(pos, wallTile);
                }
                else
                {
                    tilemap.SetTile(pos, floorTile);
                    if (boxesSpawned < numofBoxes)
                    {
                        Vector3 boxPos = new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0);
                        Instantiate(box, boxPos, Quaternion.identity);
                        boxesSpawned++;
                        Debug.Log("Wall Count: " + GetSurroundingWallCount(x, y));
                    }



                }

            }
        }
    }//FillLevel

    private int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        

        for (int negibourX = gridX - 1; negibourX <= gridX + 1; negibourX++)
        {
            for (int negibourY = gridY - 1; negibourY <= gridY + 1; negibourY++)
            {
                if (negibourX >= 0 && negibourX < width && negibourY >= 0 && negibourY < height)
                {
                    if (negibourX != gridX || negibourY != gridY)
                    {
                        wallCount += map[negibourX, negibourY];
                        
                    }
                }

                else
                {
                    wallCount++;
                }


            }
        }

        return wallCount;
    }



    private void SmoothLevel()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWalls = GetSurroundingWallCount(x, y);
                Debug.Log("Neighbour Walls " + neighbourWalls);
                if (neighbourWalls > 4)
                {
                    map[x, y] = 1;
                }
                else if (neighbourWalls < 3)
                {
                    map[x, y] = 0;
                }
            }
        }
    }

    private void ResetLevel()
    {
        numofBoxes = 0;
        boxesSpawned = 0;
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (GameObject box in boxes)
        {
            Destroy(box);
        }


    }//ResetLevel

}
