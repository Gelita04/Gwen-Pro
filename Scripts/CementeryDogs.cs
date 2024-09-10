using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cementery : MonoBehaviour
{
    public GameObject cementery;
    public List<GameObject> Cementerio;

    //metodo  que manda las cartas al cementerio.
    public void RemoveCardCementery(GameObject card)
    {
        if (card != null)
        {
            Cementerio.Add(card);
            Destroy(card.gameObject);
        }
    }
}
