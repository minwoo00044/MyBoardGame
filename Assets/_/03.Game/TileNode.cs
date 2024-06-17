using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TileNode : MonoBehaviour
{
    public bool isTouchble = false;
    Transform player;
    Player playerMovement;
    PhotonView view;

    private void Start()
    {
        foreach(var item in FindObjectsByType<PhotonView>(sortMode: FindObjectsSortMode.None))
        {
            if(item.IsMine)
                view = item;
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<Player>();
    }

    private void OnMouseDown()
    {
        if (!isTouchble) return;
        playerMovement.StartMovement(transform.position);
    }
}
