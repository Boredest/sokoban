using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject boxPrefab;
    [SerializeField]
    private GameObject markerPrefab;
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject gameManagerPrefab;

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

    public void GenerateLevel()
    {
        map = new int[width, height];
        RandomFillMap();
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
                if(x == 0 || x == width-1 || y==0 || y== height - 1)
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
                Vector3 pos = new Vector3(-width / 2 + x + .5f, -height / 2 + y + .5f, 0);
                if (map[x, y] == 1)
                {
                    Instantiate(wallPrefab, pos, Quaternion.identity);
                }
                else
                {
                    Instantiate(boxPrefab, pos, Quaternion.identity);
                }

            }
        }
    }//FillLevel

    private void SmoothMap()
    {

    }//Smooth map

    
}
