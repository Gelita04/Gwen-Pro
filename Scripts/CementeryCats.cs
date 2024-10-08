using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CementeryCats : MonoBehaviour
{
    public GameObject cementery;
    public List<GameObject> graveyard;
    public void RemoveCardCementery(GameObject card)
    {
        if (card != null)
        {
            graveyard.Add(card);
            card.transform.SetParent(cementery.transform, false);
            card.transform.position = cementery.transform.position;
            // card.SetActive(false);
            /////Asegurarse que cuando 
        }
    }
}
