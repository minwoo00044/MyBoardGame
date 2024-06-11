using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections.Generic;

public class TurnManager : MonoBehaviourPunCallbacks
{
    private const string TurnOrderKey = "TurnOrder";
    private const string CurrentTurnIndexKey = "CurrentTurnIndex";

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // ���� ���� �� �� ������ �����ϰ� ����
            InitializeTurnOrder();
        }
    }

    public void InitializeTurnOrder()
    {
        List<int> playerOrder = new List<int>();

        foreach (var player in PhotonNetwork.PlayerList)
        {
            playerOrder.Add(player.ActorNumber);
        }

        // �� ������ �����ϰ� ����
        for (int i = 0; i < playerOrder.Count; i++)
        {
            int temp = playerOrder[i];
            int randomIndex = Random.Range(i, playerOrder.Count);
            playerOrder[i] = playerOrder[randomIndex];
            playerOrder[randomIndex] = temp;
        }

        // �� �Ӽ��� �� ������ ���� �� �ε����� ����
        Hashtable props = new Hashtable
        {
            { TurnOrderKey, playerOrder.ToArray() },
            { CurrentTurnIndexKey, 0 }
        };
        PhotonNetwork.CurrentRoom.SetCustomProperties(props);
    }

    public int[] GetTurnOrder()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(TurnOrderKey, out object turnOrder))
        {
            return (int[])turnOrder;
        }
        return new int[0];
    }

    public int GetCurrentTurnPlayer()
    {
        int[] turnOrder = GetTurnOrder();
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(CurrentTurnIndexKey, out object currentIndex))
        {
            return turnOrder[(int)currentIndex];
        }
        return -1;
    }

    public void NextTurn()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int currentIndex = (int)PhotonNetwork.CurrentRoom.CustomProperties[CurrentTurnIndexKey];
            int nextIndex = (currentIndex + 1) % GetTurnOrder().Length;

            Hashtable props = new Hashtable
            {
                { CurrentTurnIndexKey, nextIndex }
            };
            PhotonNetwork.CurrentRoom.SetCustomProperties(props);
        }
    }
}
