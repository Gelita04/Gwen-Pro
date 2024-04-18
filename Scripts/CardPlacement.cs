using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class CardPlacement : MonoBehaviour
{
    private GameObject selectedCard;

    public void SelectCard(GameObject card)
    {
        selectedCard = card;
    }

    public void PlaceCardOnButton(Button button)
    {
        if (selectedCard != null)
        {
            Vector3 buttonPosition = button.transform.position;
            buttonPosition.z = 0; // Asegurarse de que la coordenada Z sea 0 (2D)
            selectedCard.transform.position = buttonPosition;
        }
    }
    
}
