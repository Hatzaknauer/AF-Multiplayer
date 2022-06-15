using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public GameObject p1, p2;

    Camera cameraP1, cameraP2;

    public enum Cameras { cameraHor, cameraVer }

    public Cameras cam;

    public void Start()
    {
        cam = Cameras.cameraHor;
        cameraP1 = p1.GetComponentInChildren<Camera>();
        cameraP2 = p2.GetComponentInChildren<Camera>();
        SinglePlayer();
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeCamera();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SinglePlayer();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MultiPlayer();
        }
    }

    private void ChangeCamera()
    {
        switch (cam)
        {
            case Cameras.cameraVer:
                cameraP1.rect = new Rect(0, 0.5f, 1f, 0.5f);
                cameraP2.rect = new Rect(0, 0, 1f, 0.5f);
                cam = Cameras.cameraHor;
                break;
            case Cameras.cameraHor:
                cameraP1.rect = new Rect(0, 0, 0.5f, 1);
                cameraP2.rect = new Rect(0.5f, 0, 0.5f, 1);
                cam = Cameras.cameraVer;
                break;
        }
    }

    void SinglePlayer()
    {
        p2.SetActive(false);
        cameraP1.rect = new Rect(0, 0, 1, 1);
    }

    void MultiPlayer()
    {
        p2.SetActive(true);
        cameraP1.rect = new Rect(0, 0, 0.5f, 1);
        cameraP2.rect = new Rect(0.5f, 0, 0.5f, 1);
    }
}
