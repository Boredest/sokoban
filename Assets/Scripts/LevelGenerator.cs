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

    [Range(10, 55)]
    public int randomFillPercent;


    public int width;
    public int height;
    
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
            //Old tiles need to be cleared if width and height are changed.
            tilemap.ClearAllTiles();
            GenerateLevel();
        }

        //Debugging Grid
        if (Input.GetMouseButtonDown(1))
        {

            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int location = tilemap.WorldToCell(mp);

            if (tilemap.GetTile(location))
            {
                Debug.Log("Tile: " + tilemap.GetTile(location));
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

        FillLevel();
        
        
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
      
    }//RandomFillMap

    public void FillLevel()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int pos = new Vector3Int(x - width / 2, y - height / 2, 0);

                //Place walls, floors
                if (map[x, y] == 1)
                {
                    tilemap.SetTile(pos, wallTile);
                }
                else
                {
                    tilemap.SetTile(pos, floorTile);
                   
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
                
                if (neighbourWalls >= 7)
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
