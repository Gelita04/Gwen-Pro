using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    public List<GameObject> cards = new List<GameObject>();
    public GameObject selectedCard;

    public void OnCardClick(GameObject card)
    {
        selectedCard = card;
    }
    public void AddCard(GameObject card)
    {
        cards.Add(card);
        card.transform.SetParent(transform);
        Button button = card.GetComponent<Button>();
        button.onClick.AddListener(() => OnCardClick(card));
    }

    public void RemoveCard(GameObject card)
    {
        cards.Remove(card);
    }
}