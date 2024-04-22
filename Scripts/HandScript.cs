using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    public GameObject board;
    public List<GameObject> cards = new List<GameObject>();
    public GameObject selectedCard;
    public GameObject logicManager;

    public void OnCardClick(GameObject card)
    {
        selectedCard = card;
    }
    public void AddCard(GameObject card)
    {
        cards.Add(card);
        card.transform.SetParent(transform, false);
        Button button = card.GetComponent<Button>();
        button.onClick.AddListener(() => OnCardClick(card));
    }

    public void RemoveSelectedCardAndInsertInMatrix(GameObject button)
    {
        GameObject[,] matrix = board.GetComponent<MatrixBoard>().Board;
        IsCardPositionValidScript scriptVar = logicManager.GetComponent<IsCardPositionValidScript>();
        int coordinateX = button.GetComponent<CoordinateInMatrix>().coordinateXInMatrixBoard;
        int coordinateY = button.GetComponent<CoordinateInMatrix>().coordinateYInMatrixBoard;
        if (selectedCard != null && scriptVar.IsPlayerCardPositionValid(matrix, selectedCard, coordinateX, coordinateY))
        {
            cards.Remove(selectedCard);
            selectedCard.transform.SetParent(board.transform, false);
            selectedCard.transform.position = button.transform.position;
            matrix[coordinateX, coordinateY] = selectedCard;
            selectedCard = null;
        }
    }
}