using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNode : MonoBehaviour
{
    public bool isTouchble = false;
    Transform player;
    Player playerMovement;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<Player>();
    }

    private void OnMouseDown()
    {
        if (!isTouchble) return;
        playerMovement.StartMovement(transform.position);
    }
}
