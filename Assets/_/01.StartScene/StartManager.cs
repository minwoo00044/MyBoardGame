using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
public class StartManager : MonoBehaviourPunCallbacks
{
    public Button startBtn;
    public TMP_InputField nameIF;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        startBtn.onClick.AddListener(LogIn);
    }
    // Update is called once per frame
    public override void OnConnected()
    {
        base.OnConnected();
        startBtn.interactable = true;
    }
    private void LogIn()
    {
        if (nameIF.text == string.Empty)
            return;
        PhotonNetwork.NickName = nameIF.text;
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Util.SceneChange(SceneNum.Lobby);
    }
}
