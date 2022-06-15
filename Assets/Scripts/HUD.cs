using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text txtPoints;
    public TMP_Text txtNick;
    public TMP_Text txtCount;
    public Timer death;

    private void Start()
    {

    }

    private void Update()
    {
        txtCount.text = death.stgTimer;
    }

    public void UpdateScore()
    {
        txtPoints.text = GameManager.GM.thisPlayer.data.score.ToString();
        txtNick.text = GameManager.GM.thisPlayer.view.Owner.NickName;
    }

    public void DeathCountdown(int playerNum)
    {
        if (playerNum == 1)
        {
            death.player = playerNum;
            death.gameObject.SetActive(true);
        }
        if(playerNum == 2)
        {
            death.player = playerNum;
            death.gameObject.SetActive(true);
        }

    }
}
