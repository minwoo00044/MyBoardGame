using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBaseMB<GameManager>
{
    NetworkManager networkManager;   
    // Start is called before the first frame update
    void Start()
    {
        networkManager = GetComponent<NetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {

    }
}
