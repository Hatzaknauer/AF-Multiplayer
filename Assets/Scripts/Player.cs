using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace PlayerUnity
{
public enum EnumPlayer
{
    Player1 = 1,
    Player2 = 2,
    Player3 = 3,
    Player4 = 4
}

public class Player : MonoBehaviour, IPunObservable
{
    NetworkController networkController;
    public GameData_SO gameData;
    public PlayerData_SO data;
    public EnumPlayer numPlayer;
    public PhotonView view;
        public Canvas myCanvas;

    public float force = 10;
    public float torque = 10;

    public float myHealth = 10;

    Rigidbody rb;

    float hor;
    float ver;

    Vector3 startPos;
    Quaternion startRotation;

    public GameObject fumaca, fumacaOld;
    public GameObject Shot;
    public GameObject ponta;

        public GameObject myCam,
            mainCam;

    public bool damaged,
        isMoving,
        isPlaying;

    private void Awake()
    {
        FindObjectOfType<AudioManager>().Play("Shutup");
    }

    private void Start()
    {
        networkController = FindObjectOfType<NetworkController>();
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        startRotation = transform.rotation;
        view = GetComponent<PhotonView>();
            if (view.IsMine)
            {
                GameManager.GM.thisPlayer = this;
                GameManager.GM.UpdateHUD();
            }
    }

    private void Update()
    {
            if (view.IsMine)
            {
                GameManager.GM.thisPlayerPoints = data.score;
                myCam.SetActive(true);
                GameManager.GM.cameraInScene = myCam.GetComponent<Camera>();
                Destroy(Camera.main);
                myCanvas.gameObject.SetActive(false);
            }
            else
            {
                myCanvas.transform.LookAt(GameManager.GM.cameraInScene.transform.position);
                myCam.SetActive(false);
                return;
            }

            if (!damaged)
        {
            if(Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
            {
                FindObjectOfType<AudioManager>().Stop("TankIdle");
                if(!isMoving && isPlaying)
                {
                    isPlaying = false;
                    isMoving = true;
                }
            }
            else if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
            {
                isMoving = false;
                isPlaying = false;
                FindObjectOfType<AudioManager>().Stop("TankMoving");
            }

            hor = Input.GetAxis("Horizontal");
            ver = Input.GetAxis("Vertical");

                if (Input.GetButtonDown("Fire"))
                    view.RPC("Fire", RpcTarget.All);
        }
    }

    private void FixedUpdate()
    {

        if (!damaged)
        {
            Movement();
        }
    }

    public void Reset()
    {
        myHealth = 10;
        damaged = false;
        Destroy(fumacaOld);
        transform.rotation = startRotation;
        transform.position = networkController.GetRandomSpawnPoint().transform.position;
        FindObjectOfType<AudioManager>().Stop("Fire" + (int)numPlayer);
        FindObjectOfType<AudioManager>().Play("Shutup");
    }

    public void Morri(Player player)
    {
        if (this.enabled == true)
        {
            //Instancia fumaça no modelo e não no centro do objeto que nao entendi onde está
            fumacaOld = Instantiate(fumaca, this.gameObject.GetComponentInChildren<LODGroup>().transform);
                player.AddPoints(1);
            damaged = true;
                StartCoroutine("MorreuCountdown");
            FindObjectOfType<AudioManager>().Stop("TankIdle" + (int)numPlayer);
            FindObjectOfType<AudioManager>().Stop("TankMoving" + (int)numPlayer);
            FindObjectOfType<AudioManager>().Play("Fire" + (int)numPlayer);
            FindObjectOfType<AudioManager>().Play("Shutdown");
            }
    }

    void Movement()
    {
        Vector3 dir = transform.forward * ver * force;
        rb.velocity = new Vector3(dir.x, rb.velocity.y, dir.z);

        float angle = transform.rotation.eulerAngles.y;
        rb.MoveRotation(Quaternion.Euler(0, angle + (hor * torque), 0));
        if (isMoving && !isPlaying)
        {
            isPlaying = true;
            FindObjectOfType<AudioManager>().Play("TankMoving" + (int)numPlayer);
        }
        else if (!isMoving && !isPlaying)
        {
            isPlaying = true;
            FindObjectOfType<AudioManager>().Play("TankIdle" + (int)numPlayer);
        }

    }

    [PunRPC]
    void Fire()
    {
            var instance = Instantiate(Shot, ponta.transform.position, ponta.transform.rotation);
            instance.GetComponent<Shot>().player = this;
            instance.GetComponent<Rigidbody>().AddForce(ponta.transform.forward * 6000);
            FindObjectOfType<AudioManager>().Play("TankFire" + (int)numPlayer);
    }

    public void AddPoints(int value)
    {
        data.score += value;
        gameData.OnUpdateHUD.Invoke();
    }

    public void SetHealth(int value, Player player)
    {
            myHealth -= value;
            if(myHealth < 1)
            {
                Morri(player);
            }
            Debug.Log(myHealth);
    }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            /*if (stream.IsWriting)
            {
                stream.SendNext(render.flipX);
            }
            else
            {
                render.flipX = (bool)stream.ReceiveNext();
            }*/
            
        }
        IEnumerator MorreuCountdown()
        {
            yield return new WaitForSeconds(5f);
            Reset();
        }
    }
}