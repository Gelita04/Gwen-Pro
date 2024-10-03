using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ListsOfCards : MonoBehaviour
{
    // public List<Tuple<string, List<string>>> cardNameEffectsName;
    // List<string> nameCards;
    // public List<string> nameEffects;
    // public List<Tuple<string, List<GameObject>>> targets;
    // public List<Tuple<string, List<Tuple<string, object>>>> Params;
    public List<Tuple<string, List<string>, Dictionary<string, List<GameObject>>, Dictionary<string, List<Tuple<string, object>>>>> listOfCards;

    public void Awake()
    {
        // cardNameEffectsName = new List<Tuple<string, List<string>>>();
        // nameEffects = new List<string>();
        // targets = new List<Tuple<string, List<GameObject>>>();
        // Params = new List<Tuple<string, List<Tuple<string, object>>>>();
        listOfCards = new List<Tuple<string, List<string>, Dictionary<string, List<GameObject>>, Dictionary<string, List<Tuple<string, object>>>>>();
    }

    // public void RellenarListaDeCartas(List<Tuple<string, List<string>>> cardNameEffectsName, List<string> nameEffects, List<Tuple<string, List<GameObject>>> targets, List<Tuple<string, List<Tuple<string, object>>>> Params)
    public void RellenarListaDeCartas(string cardName, List<string> nameEffects, Dictionary<string,List<GameObject>> targets, Dictionary<string, List<Tuple<string, object>>> Params)
    {
        Debug.Log("entro a rellenar lista de cartas");
        
        
        listOfCards.Add(new Tuple<string, List<string>, Dictionary<string, List<GameObject>>, Dictionary<string, List<Tuple<string, object>>>>(cardName, nameEffects, targets, Params)); 
    }
}
