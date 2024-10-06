using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDraw : MonoBehaviour
{
    public GameObject EnemyHand;
    public GameObject EnemyDeck;
    private List<GameObject> deckDogs;

    //metodo que llena la mano de los perros.
    public void OnClick()
    {
        HandScript handEnemyScript = EnemyHand.GetComponent<HandScript>();
        int card_to_draw = 10 - handEnemyScript.cards.Count;
        deckDogs = EnemyDeck.GetComponent<Deck_Dogs>().Deck;
        Transform tempChildPlayer = GameObject.Find("EnemyDeck").transform;
        for (int i = 0; i < card_to_draw; i++)
        {
            if (deckDogs.Count==0)
            {
                return;
            }
            GameObject playerCard = tempChildPlayer
                .GetChild(Random.Range(0, tempChildPlayer.childCount))
                .gameObject;
            playerCard.transform.SetParent(EnemyHand.transform, false);
            handEnemyScript.AddCard(playerCard);
            deckDogs.Remove(playerCard);
        }
    }
}
