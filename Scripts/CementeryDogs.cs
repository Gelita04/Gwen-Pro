using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cementery : MonoBehaviour
{
    public GameObject cementery;
    public List<GameObject> graveyard;

    //metodo  que manda las cartas al cementerio.
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
