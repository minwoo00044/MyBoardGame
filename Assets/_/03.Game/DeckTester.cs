using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckTester : MonoBehaviourPunCallbacks
{
    public List<int> cards = new List<int>();
    public void DrawCard()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int drawnCard = cards[0];
            cards.RemoveAt(0);
            photonView.RPC("UpdateDeck", RpcTarget.All, cards.ToArray());
        }
    }

    [PunRPC]
    void UpdateDeck(int[] updatedCards)
    {
        cards = new List<int>(updatedCards);
    }
}
