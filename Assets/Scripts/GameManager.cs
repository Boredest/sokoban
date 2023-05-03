using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    private GameObject[] markerCount;
    private bool levelOver;

    public int currentMarkerCount;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        
        levelOver = false;
        markerCount = GameObject.FindGameObjectsWithTag("Marker");
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Resetting game");
            ResetScene();
        }
       
        if (currentMarkerCount == markerCount.Length)
        {
            Debug.Log("All markers covered. Loading next level");
            LevelOver();
        }

    }
    public void ResetScene()
    {
        levelOver = false;
        currentMarkerCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        levelOver = false;
        //todo
    }

    public void LevelOver()
    {
        levelOver = true;
        currentMarkerCount = 0;
        
    }

    
}
