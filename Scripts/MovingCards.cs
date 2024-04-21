using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCards : MonoBehaviour
{
    public GameObject card;
    public GameObject button;
    private bool isCardTouched = false;

    void MoveCardOnButton()
    {
        // metodo que hace que la carta se ponga encima del boton
        card.transform.position = button.transform.position;
        
    }
    
    void Update()
    {
        
        
    }

    
   

    
}
