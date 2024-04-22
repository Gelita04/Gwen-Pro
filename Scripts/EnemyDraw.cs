using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDraw : MonoBehaviour
{
    public GameObject EnemyHand;
    public GameObject EnemyDeck;
    // public List<GameObject> Deck2;


    public void FillDeck()
    {
        Transform tempChildPlayer = GameObject.Find("EnemyDeck").transform; //busca el gameObject padre.
        for (int i = 0; i < tempChildPlayer.childCount; i++)
        {
            Transform childTransform = tempChildPlayer.GetChild(i); //utilizo el metodo Getchild para ir iterando por los gameObjects de PlayerDeck.
            // Deck2.Add(childTransform.gameObject);
        }

    }
    public void Start()
    {
        // Deck2 = new List<GameObject>();
        FillDeck();
    }
    public void OnClick()
    {
        HandScript handEnemyScript = EnemyHand.GetComponent<HandScript>();
        int card_to_draw = 10 - handEnemyScript.cards.Count;

        Transform tempChildPlayer = GameObject.Find("EnemyDeck").transform;
        for (int i = 0; i < card_to_draw; i++)
        {
            GameObject playerCard = tempChildPlayer.GetChild(Random.Range(0, tempChildPlayer.childCount)).gameObject; //Deck2[index_in_deck_of_card_to_draw];
            playerCard.transform.SetParent(EnemyHand.transform, false);
            handEnemyScript.AddCard(playerCard);
           // Deck2.RemoveAt(index_in_deck_of_card_to_draw);
        }
        // HandScript handEnemyScript = EnemyHand.GetComponent<HandScript>();
        // int card_to_draw = 10 - handEnemyScript.cards.Count;

        // for (int i = 0; i < card_to_draw; i++)
        // {
        //     int index_in_deck_of_card_to_draw = Random.Range(0, Deck2.Count);
        //     GameObject playerCard = Deck2[Random.Range(0, Deck2.Count)];
        //     playerCard.transform.SetParent(EnemyHand.transform, false);
        //     handEnemyScript.AddCard(playerCard);
        //     // Deck2.RemoveAt(index_in_deck_of_card_to_draw);
        // }
    }


}