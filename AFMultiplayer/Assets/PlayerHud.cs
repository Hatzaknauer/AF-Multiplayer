using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerHud : MonoBehaviourPunCallbacks
{
    public TMP_Text textNick;
    public PhotonView view;

    private void Start()
    {
        textNick.text = view.Owner.NickName;
    }

    public void CallSetHUD()
    {
        view.RPC("SetHUD", RpcTarget.All);
    }

    [PunRPC]
    public void SetHUD() 
    {
        textNick.text = view.Owner.NickName;
    }
}
