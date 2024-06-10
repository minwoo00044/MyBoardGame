using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NetworkManager :MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text waitText;

    private string fiexdText = "친구들 기다리는 중"; 
    private void Start()
    {
        WaitTxtChange(PhotonNetwork.CurrentRoom.PlayerCount);
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            GameManager.instance.GameStart();
        }
        WaitTxtChange(PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        WaitTxtChange(PhotonNetwork.CurrentRoom.PlayerCount);
    }
    private void WaitTxtChange(int count)
    {
        waitText.text = $"{fiexdText} {count}/4";
    }
}
