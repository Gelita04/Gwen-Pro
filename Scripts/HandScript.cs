using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    public GameObject board;
    public List<GameObject> cards;
    public GameObject selectedCard;
    public GameObject logicManager;
    public GameObject textcats;
    public GameObject textdogs;

    //propierties of expand card when clicked functionality:
    public GameObject cardDisplaySpace; // Reference to the invisible space (UI Image)
    public Toggle toggleButton; // Reference to the toggle button
    private bool isCardDisplayActive = false;

    public void Start()
    {
        cards = new List<GameObject>();
        // Ensure the card display space is initially inactive
        cardDisplaySpace.SetActive(false);

        // Add a listener to the toggle button
        toggleButton.onValueChanged.AddListener(OnToggleChanged);
    }

    public void OnCardClick(GameObject card)
    {
        selectedCard = card;
        // if (card.CompareTag("Wildcard"))
        // {
        //     //llamar efecto joker
        // }
        if (isCardDisplayActive)
        {
            // Show the clicked card in the display space
            ShowCardInDisplaySpace(card);
        }
    }

    //metodo  que agrega a la mano las 10 cartas sacadas del deck.
    public void AddCard(GameObject card)
    {
        cards.Add(card);
        card.transform.SetParent(transform, false);
        Button button = card.GetComponent<Button>();
        button.onClick.AddListener(() => OnCardClick(card));
        textcats.GetComponent<TextPlayerWin>().DesactivateWinRound();
        textdogs.GetComponent<TextEnemyWin>().DesactivateWinRound();
    }

    //metodo que quita una carta de la mano y la pone en el tablero
    public void RemoveSelectedCardAndInsertInMatrix(GameObject button)
    {
        GameObject[,] matrix = board.GetComponent<MatrixBoard>().Board;
        IsCardPositionValidScript scriptVar =
            logicManager.GetComponent<IsCardPositionValidScript>();
        int coordinateX = button.GetComponent<CoordinateInMatrix>().coordinateXInMatrixBoard;
        int coordinateY = button.GetComponent<CoordinateInMatrix>().coordinateYInMatrixBoard;
        bool isPlayerTurn = logicManager.GetComponent<GameLogic>().isPlayerTurn;
        if (
            (
                (
                    !isPlayerTurn
                    && coordinateX < 3
                    && scriptVar.IsEnemyCardPositionValidate(
                        matrix,
                        selectedCard,
                        coordinateX,
                        coordinateY
                    )
                )
                || (
                    isPlayerTurn
                    && coordinateX >= 3
                    && scriptVar.IsPlayerCardPositionValid(
                        matrix,
                        selectedCard,
                        coordinateX,
                        coordinateY
                    )
                )
            )
            && selectedCard != null
            && matrix[coordinateX, coordinateY] == null
        )
        {
            cards.Remove(selectedCard);
            selectedCard.transform.SetParent(board.transform, false);
            selectedCard.transform.position = button.transform.position;
            matrix[coordinateX, coordinateY] = selectedCard;
            selectedCard = null;
            if (isPlayerTurn)
            {
                logicManager.GetComponent<GameLogic>().isPlayerReadyForBattle = false;
            }
            else
            {
                logicManager.GetComponent<GameLogic>().isEnemyReadyForBattle = false;
            }
        }
    }

    //metodo que hace que se muestre la carta mas grande
    private void ShowCardInDisplaySpace(GameObject card)
    {
        // Activate the display space
        cardDisplaySpace.SetActive(true);

        // Get the RectTransform of the display space
        RectTransform displaySpaceRectTransform = cardDisplaySpace.GetComponent<RectTransform>();

        // Optionally, set the image of the card to the display space if using UI Image
        Image cardImage = card.GetComponent<Image>();
        if (cardImage != null)
        {
            Image displayImage = cardDisplaySpace.GetComponent<Image>();
            displayImage.sprite = cardImage.sprite; // Set the sprite of the display space

            // Optionally set the size of the display space to a larger fixed size for better visibility
            displaySpaceRectTransform.sizeDelta = new Vector2(200, 300); // Adjust this size as needed
        }
        else
        {
            Debug.LogError("The clicked card does not have an Image component!");
        }

        // Debug logs
        Debug.Log("Displaying card image: " + cardImage.sprite.name);
    }

    //metodo que controla el estado del boton de mostrar cartas
    private void OnToggleChanged(bool isOn)
    {
        Debug.Log("Entered OnToggleChanged");
        isCardDisplayActive = isOn;

        // Optionally hide the display space if toggled off
        if (!isOn)
        {
            cardDisplaySpace.SetActive(false);
        }
    }
}
