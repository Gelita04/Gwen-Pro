using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cementery : MonoBehaviour
{
    public GameObject cementery;
    public List<GameObject> Cementerio;

<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
=======
    //metodo  que manda las cartas al cementerio.
>>>>>>> Stashed changes
>>>>>>> Stashed changes
    public void RemoveCardCementery(GameObject card)
    {
        if (card != null)
        {
            Cementerio.Add(card);
            Destroy(card.gameObject);
<<<<<<< Updated upstream
            
=======
<<<<<<< Updated upstream
            
=======
>>>>>>> Stashed changes
>>>>>>> Stashed changes
        }
    }
}
