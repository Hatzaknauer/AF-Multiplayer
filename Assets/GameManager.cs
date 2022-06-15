using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerUnity;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager GM;


    private void Awake()
    {
        if (GM == null)
        {
            GM = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
    #endregion

    public GameData_SO gameData;
    public HUD hud;

    public Player thisPlayer;
    public int thisPlayerPoints = 0; //guarda os pontos do player da instancia atual (player jogando nesta tela)

    GameObject[] players;
    public Player player1, player2, player3, player4;

    public Camera cameraInScene;

    public void Start()
    {
        FindObjectOfType<AudioManager>().Play("Ambience");
        gameData.OnUpdateHUD.AddListener(UpdateHUD);
        gameData.OnPlayerEnter.AddListener(Revisa);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Restart();

    }

    public void Restart()
    {
        thisPlayer.Reset();
    }

    public void UpdateHUD()
    {
        hud.UpdateScore();
    }

    public void Revisa()
    {
        hud.panel.SetActive(true);
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject gameObjects in players)
        {
            if (((int)gameObjects.GetComponent<Player>().numPlayer) == 1)
            {
                player1 = gameObjects.GetComponent<Player>();
            }
            else if (((int)gameObjects.GetComponent<Player>().numPlayer) == 2)
            {
                player2 = gameObjects.GetComponent<Player>();
            }
            else if (((int)gameObjects.GetComponent<Player>().numPlayer) == 3)
            {
                player3 = gameObjects.GetComponent<Player>();
            }
            else if (((int)gameObjects.GetComponent<Player>().numPlayer) == 4)
            {
                player4 = gameObjects.GetComponent<Player>();
            }
        }
    }
}