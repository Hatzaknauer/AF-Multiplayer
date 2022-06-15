using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerUnity;

public class Shot : MonoBehaviour
{
    public Player player;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<Player>().damaged)
            {
                Destroy(this.gameObject, 0);
                collision.gameObject.GetComponent<Player>().SetHealth(2, player);

                //collision.gameObject.GetComponent<Player>().Morri();
            }
        }
        else if (collision.gameObject.tag == "Explodable")
        {
            Destroy(this.gameObject, 0);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "NonExplodable")
        {
            Destroy(this.gameObject, 0);
        }
    }

    private void Start()
    {
        {
            //Autodestroy tiro
            Destroy(this.gameObject, 5f);
        }
    }
}
