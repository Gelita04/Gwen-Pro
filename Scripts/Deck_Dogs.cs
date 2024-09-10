using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck_Dogs : MonoBehaviour
{
    public GameObject EnemyDeck;
    public List<GameObject> Deck;

    //se llena el deck de los perros.
    void Start()
    {
        Transform parentTransform = GameObject.Find("EnemyDeck").transform; //busca el gameObject padre.
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i); //utilizo el metodo Getchild para ir iterando por los gameObjects de PlayerDeck.
            Deck.Add(childTransform.gameObject);
        }

        foreach (GameObject go in Deck) { }
    }
}
