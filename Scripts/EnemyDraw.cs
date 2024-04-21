using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDraw : MonoBehaviour
{
    public GameObject EnemyHand;
    public GameObject EnemyDeck;
    public List<GameObject> Deck2;
    

    public void FillDeck()
    {
        Transform tempChildPlayer = GameObject.Find("EnemyDeck").transform; //busca el gameObject padre.
        for (int i = 0; i < tempChildPlayer.childCount; i++)
        {
            Transform childTransform = tempChildPlayer.GetChild(i); //utilizo el metodo Getchild para ir iterando por los gameObjects de PlayerDeck.
            Deck2.Add(childTransform.gameObject);
        }
        
    }
    
    public void OnClick()
    {
        FillDeck();
        int card_to_draw = 10 - 0 ;
        for (int i = 0; i < card_to_draw; i++)
        { 
            
            GameObject playerCard = Instantiate(Deck2[Random.Range(0, Deck2.Count)], new Vector3(0, 0, 0), quaternion.identity);
            playerCard.transform.SetParent(EnemyHand.transform, false);
        }
        
    }


}