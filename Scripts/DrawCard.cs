using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class DrawCard : MonoBehaviour
{ 
    public GameObject PlayerHand;
    public GameObject EnemyHand;
    public GameObject PlayerDeck;
    public GameObject EnemyDeck;
    public List<GameObject> Deck1;
    public List<GameObject> Deck2;

     public void FillDeck()
    {
        Transform tempChildPlayer = GameObject.Find("PlayerDeck").transform; //busca el gameObject padre.
        for (int i = 0; i < tempChildPlayer.childCount; i++)
        {
            Transform childTransform = tempChildPlayer.GetChild(i); //utilizo el metodo Getchild para ir iterando por los gameObjects de PlayerDeck.
            Deck1.Add(childTransform.gameObject);
        }
        Transform tempChildEnemy = GameObject.Find("PlayerDeck").transform; //busca el gameObject padre.
        for (int i = 0; i < tempChildEnemy.childCount; i++)
        {
            Transform childTransform = tempChildEnemy.GetChild(i); //utilizo el metodo Getchild para ir iterando por los gameObjects de PlayerDeck.
            Deck2.Add(childTransform.gameObject);
        }
    }
    public void OnClick()
    {
        FillDeck();
        int card_to_draw = 10-0;
        for (int i = 0; i < 10; i++)
        {
            GameObject playerCard = Instantiate(Deck1[Random.Range(0, Deck1.Count)], new Vector3(0, 0, 0), quaternion.identity);
            playerCard.transform.SetParent(PlayerHand.transform, false);
            GameObject enemyCard = Instantiate(Deck2[Random.Range(0, Deck2.Count)], new Vector3(0, 0, 0), quaternion.identity);
            enemyCard.transform.SetParent(EnemyHand.transform,false);
        }
    }

   
}
