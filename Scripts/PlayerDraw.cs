using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class DrawCard : MonoBehaviour
{
    public GameObject PlayerHand;
    public GameObject PlayerDeck;
    private List<GameObject> deckCats;


    //metodo que llena la mano de los gatos.
    public void OnClick()
    {

        HandScript handPlayerScript = PlayerHand.GetComponent<HandScript>();
        int card_to_draw = 10 - handPlayerScript.cards.Count;
        deckCats = PlayerDeck.GetComponent<Deck_Cats>().Deck;
        Transform tempChildPlayer = GameObject.Find("PlayerDeck").transform;
        for (int i = 0; i < card_to_draw; i++)
        {
            if (deckCats.Count==0)
            {
                return;
            }
            GameObject playerCard = tempChildPlayer.GetChild(Random.Range(0, tempChildPlayer.childCount)).gameObject;
            handPlayerScript.AddCard(playerCard);
            //remove card added to the hand from the deck
            deckCats.Remove(playerCard);

        }
    }
}



