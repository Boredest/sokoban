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
    private GameObject boxes;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject GameManager;



    public bool useRandomSeed;
    public string seed;

    public int randomFillPercent;


    public int width;
    public int height;

    public float wallProbability;

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
    }

    public void GenerateLevel()
    {
        map = new int[width, height];
        RandomFillMap();

        for(int i = 0; i < 3; i++)
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
                }

            }
        }
    }//FillLevel

    private int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {

                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += map[neighbourX, neighbourY];
                    }
                    else
                    {
                        wallCount++;
                    }
                }
            }
        }
        return wallCount;

    }//GetSurroundingWallCount

    private void SmoothLevel()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWalls = GetSurroundingWallCount(x, y);
                if(neighbourWalls > 4)
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




}
