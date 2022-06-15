using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerUnity;
public class Timer : MonoBehaviour
{
    public int player;
    public float timeToReset;
    float time;
    public string stgTimer;

    private void Start()
    {
        time = timeToReset;
    }

    private void Reset()
    {
        this.gameObject.SetActive(false);
        time = 3;
        stgTimer = string.Format("{0:00}", time);
    }

    private void FixedUpdate()
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            if(player == 1)
            {
                GameManager.GM.player1.Reset();
            }
            if (player == 2)
            {
                GameManager.GM.player2.Reset();
            }
            Reset();
        }
        stgTimer = string.Format("{0:00}", time);
    }
}
