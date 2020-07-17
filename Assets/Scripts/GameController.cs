using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    public int playerHP = 10;
    RoomManager roomManager;
    int level = 1;
    public float levelStartDelay = 2f;
    GameObject levelImage;
    Text levelText;

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
        DontDestroyOnLoad(gameObject);

        roomManager = GetComponent<RoomManager>();

        InitGame();
    }

    void Start()
    {
        SceneManager.sceneLoaded += LevelWasLoaded;
    }

    void LevelWasLoaded(Scene s, LoadSceneMode mode)
    {
        level++;
        InitGame();
    }

    void InitGame()
    {
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Level " + level;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);

        roomManager.SetupScene(level);
    }

    void HideLevelImage()
    {
        levelImage.SetActive(false);
    }

    public void GameOver()
    {
        levelText.text = "You have reached " + level + ".";
        levelImage.SetActive(true);
        enabled = false;
    }
}
