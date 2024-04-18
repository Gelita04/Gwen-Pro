using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLibrary.Objects;

public class Deck_Cats : MonoBehaviour
{
    public GameObject PlayerDeck;
    public List<GameObject> Deck;
  
    void Start()
    {
        Transform parentTransform = GameObject.Find("PlayerDeck").transform; //busca el gameObject padre.
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i); //utilizo el metodo Getchild para ir iterando por los gameObjects de PlayerDeck.
            Deck.Add(childTransform.gameObject);
        }
        
    }
}
