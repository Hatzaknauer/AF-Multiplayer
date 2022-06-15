using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerUnity;

public class Shot : MonoBehaviour
{
    public Player player;
    public GameObject vfx;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<Player>().damaged)
            {
                vfx = Instantiate(vfx, collision.contacts[0].point, Quaternion.identity);
                Destroy(vfx, 3f);
                Destroy(this.gameObject, 0);
                collision.gameObject.GetComponent<Player>().SetHealth(2, player);

                //collision.gameObject.GetComponent<Player>().Morri();
            }
        }
        else if (collision.gameObject.tag == "Explodable")
        {
            vfx = Instantiate(vfx, collision.contacts[0].point, Quaternion.identity);
            Destroy(vfx, 3f);
            Destroy(this.gameObject, 0);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "NonExplodable")
        {
            vfx = Instantiate(vfx, collision.contacts[0].point, Quaternion.identity);
            Destroy(vfx, 3f);
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
