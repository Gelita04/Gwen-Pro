using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cementery : MonoBehaviour
{
    public GameObject cementery;
    public List<GameObject> Cementerio;

    public void RemoveCardCementery(GameObject card)
    {
        if (card != null)
        {
            Cementerio.Add(card);
            Destroy(card.gameObject);
            
        }
    }
}
