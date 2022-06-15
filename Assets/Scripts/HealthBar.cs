using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    RectTransform rectTransform;
    public GameObject HP;
    public PlayerUnity.Player player;
    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = GameManager.GM.thisPlayer;
        }
        rectTransform = HP.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.localScale = new Vector3(player.myHealth * 0.10f, rectTransform.localScale.y, rectTransform.localScale.z);
    }
}
