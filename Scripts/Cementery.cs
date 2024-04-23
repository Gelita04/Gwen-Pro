using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cementery : MonoBehaviour
{
    public GameObject cementery;

    public void RemoveCardCementery( GameObject card)
    {
        card.transform.SetParent(cementery.transform, false);
        
    }
    
}
