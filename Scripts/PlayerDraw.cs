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
    public List<GameObject> Deck1;

//metodo que llena el deck de los gatos.
    public void FillDeck()
    {
        Transform tempChildPlayer = GameObject.Find("PlayerDeck").transform; //busca el gameObject padre.
        for (int i = 0; i < tempChildPlayer.childCount; i++)
        {
            Transform childTransform = tempChildPlayer.GetChild(i); //utilizo el metodo Getchild para ir iterando por los gameObjects de PlayerDeck.
            Deck1.Add(childTransform.gameObject);
        }

    }

    public void Start()
    {
        Deck1 = new List<GameObject>();
        FillDeck();
    }
    
//metodo que llena la mano de los gatos.
    public void OnClick()
    {
        HandScript handPlayerScript = PlayerHand.GetComponent<HandScript>();
        int card_to_draw = 10 - handPlayerScript.cards.Count;

        Transform tempChildPlayer = GameObject.Find("PlayerDeck").transform;
        for (int i = 0; i < card_to_draw; i++)
        {
            GameObject playerCard = tempChildPlayer.GetChild(Random.Range(0, tempChildPlayer.childCount)).gameObject; 
            //Deck1[index_in_deck_of_card_to_draw];
            //playerCard.transform.SetParent(PlayerHand.transform, false);
            handPlayerScript.AddCard(playerCard);
            // Deck1.RemoveAt(index_in_deck_of_card_to_draw);
        }


    }


}
