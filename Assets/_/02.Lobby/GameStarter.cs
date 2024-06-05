using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviourPunCallbacks
{
    Button startBtn;
    private void Start()
    {
        startBtn = GetComponent<Button>();
        startBtn.onClick.AddListener(Match);
    }
    private void Match()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinRandomOrCreateRoom(null, 0, MatchmakingMode.FillRoom, null, null, "room", roomOptions);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Util.SceneChange(SceneNum.Game);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
}
