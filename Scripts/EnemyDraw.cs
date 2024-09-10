using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDraw : MonoBehaviour
{
    public GameObject EnemyHand;
    public GameObject EnemyDeck;

    //metodo que llena el deck de los perros.
    public void FillDeck()
    {
        Transform tempChildPlayer = GameObject.Find("EnemyDeck").transform; //busca el gameObject padre.
        for (int i = 0; i < tempChildPlayer.childCount; i++)
        {
            Transform childTransform = tempChildPlayer.GetChild(i); //utilizo el metodo Getchild para ir iterando por los gameObjects de PlayerDeck.
        }
    }

    public void Start()
    {
        FillDeck();
    }

    //metodo que llena la mano de los perros.
    public void OnClick()
    {
        HandScript handEnemyScript = EnemyHand.GetComponent<HandScript>();
        int card_to_draw = 10 - handEnemyScript.cards.Count;

        Transform tempChildPlayer = GameObject.Find("EnemyDeck").transform;
        for (int i = 0; i < card_to_draw; i++)
        {
            GameObject playerCard = tempChildPlayer
                .GetChild(Random.Range(0, tempChildPlayer.childCount))
                .gameObject; //Deck2[index_in_deck_of_card_to_draw];
            playerCard.transform.SetParent(EnemyHand.transform, false);
            handEnemyScript.AddCard(playerCard);
        }
    }
}
