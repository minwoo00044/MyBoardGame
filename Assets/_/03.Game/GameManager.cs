using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBaseMB<GameManager>
{
    NetworkManager networkManager;
    TurnManager turnManager;
    public GameObject waitPanel;
    // Start is called before the first frame update
    void Start()
    {
        networkManager = GetComponent<NetworkManager>();
        turnManager = GetComponent<TurnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        waitPanel.SetActive(false);
    }
}
