using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public Text dataMatch;
    public int turnCounter = 0;
    public bool isPlayerTurn = true;
    public bool isPlayerReadyForBattle = true;
    public bool isEnemyReadyForBattle = true;
    public bool playerPassTurnBeingReadyForBattle = false;
    public bool enemyPassTurnBeingReadyForBattle = false;
    private int playerScore = 0;
    private int enemyScore = 0;
    private int roundCounter = 0;
    public bool playerWon = false;
    public bool enemyWon = false;
    public GameObject matrix;
    public GameObject[,] matrixBoard;
    public GameObject effects;
    public GameObject cementery;
    private GameObject effectwilcard;
    private GameObject leadereffect;
    public GameObject catsWinRound;
    public GameObject dogsWinRound;
    public GameObject cardLeaderPlayer;
    public GameObject cardLeaderEnemy;
    public GameObject catsWin;
    public GameObject dogsWin;
    public GameObject textCatsTurn;
    public GameObject textDogsTurn;

    public void Start()
    {
        textCatsTurn.SetActive(false);
        textDogsTurn.SetActive(false);
    }

    [ContextMenu("Logic")]
    public void UpdateDataText() //dataMatch es "Round: " + roundCounter + " Player: " + playerScore + " Enemy: " + enemyScore;
    {
        dataMatch.text =
            "Round: " + roundCounter + "\nCats: " + playerScore + "\nDogs: " + enemyScore;
    }

    //este metodo resetea la escena cuando se acaba el juego
    public void ResetScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.name);
    }

    //metodo que pone el letrero del turno de cada jugador
    public void TextTurnsPlayers()
    {
        if (isPlayerTurn)
        {
            textDogsTurn.SetActive(false);
            textCatsTurn.SetActive(true);
        }
        else
        {
            textCatsTurn.SetActive(false);
            textDogsTurn.SetActive(true);
        }
    }

    //metodo que para activar los efectos de todas las cartas puestas en el tablero
    public void ActivateEffects(GameObject[,] board, GameObject card, int x, List<GameObject> hand = null, List<GameObject> otherHand = null, List<GameObject> deck = null, List<GameObject> otherDeck = null)
    {
        //---------------------------------------> agregar hand, otherHand,deck y otherDeck como parametros
        EffectsScript Effects = effects.GetComponent<EffectsScript>();
        Unit_Card effectsUnitCards = effects.GetComponent<Unit_Card>();
        EffectsUsers effectsUsers = effects.GetComponent<EffectsUsers>();

        // Wildcard effect = effects.GetComponent<Wildcard>();
        if (card != null)
        {
            if (card.CompareTag("Field"))
            {
                Field_Card cardfield = card.GetComponent<Field_Card>();
                if (cardfield.IsCreatedByUsers)
                {
                    Debug.Log("Carta De Usuario " + cardfield);
                    //llamar al efecto
                    //si no pincha el gameCOntext, se puede instanciar aqui un GameContext y ponerle los valores actuales de cada propiedad y pasarlo como parametro al metodo de abajo de este comentario
                    effectsUsers.EffectsByUser(card);
                }
                else
                {
                    // Debug.Log("Carta Field " + card);
                    // Debug.Log("Efecto de cartas Field va a ser activado");
                    // // Effects.EffectsField(board, card, x);
                    // Debug.Log("Efecto de cartas Field ya fue activado");
                }
            }
            if (card.CompareTag("Buff"))
            {
                Buff_Card cardbuff = card.GetComponent<Buff_Card>();
                if (cardbuff.IsCreatedByUsers)
                {
                    Debug.Log("Carta De Usuario " + cardbuff);
                    //llamar al efecto
                    effectsUsers.EffectsByUser(card);
                }
                else
                {
                    // Debug.Log("Carta Buff " + card);
                    // Debug.Log("Efecto de cartas Buff va a ser activado");
                    // // Effects.EffectsBuff(board, card, x);
                    // Debug.Log("Efecto de cartas Buff ya fue activado");
                }
            }
            if (card.CompareTag("Unit-Cards"))
            {
                Unit_Card unitcard = card.GetComponent<Unit_Card>();
                if (unitcard.IsCreatedByUsers)
                {
                    Debug.Log("Carta De Usuario " + card);
                    //llamar al efecto
                    effectsUsers.EffectsByUser(card);
                }
                else
                {
                    // Debug.Log("Carta de Unidad " + card);
                    // Debug.Log("Efecto de cartas de unidad va ser activado");
                    // // effectsUnitCards.EffectsUnitCardsAtivate(card);
                    // Debug.Log("Efecto de cartas de unidad ya fue activado");
                }
            }
            if (
                card.CompareTag("Counterfield")
                && card.GetComponent<Counterfield_Card>().team == "Cats"
            )
            {
                Counterfield_Card countercard = card.GetComponent<Counterfield_Card>();
                if (countercard.IsCreatedByUsers)
                {
                    Debug.Log("Carta De Usuario " + card);
                    //llamar al efecto
                    effectsUsers.EffectsByUser(card);
                }
                else
                {
                //     Debug.Log("Carta CounterField de los gatos " + card);
                //     Debug.Log("Efecto de cartas Counterfield de los gatos  va a ser activado");
                //     // Effects.EffectsCounterFieldCats(card, board);
                //     Debug.Log("Efecto de cartas CounterField de los gatos ya fue activado");
                }
            }
            if (
                card.CompareTag("Counterfield")
                && card.GetComponent<Counterfield_Card>().team == "Dogs"
            )
            {
                Counterfield_Card countercard = card.GetComponent<Counterfield_Card>();
                if (countercard.IsCreatedByUsers)
                {
                    Debug.Log("Carta De Usuario " + card);
                    //llamar al efecto
                    effectsUsers.EffectsByUser(card);
                }
                else
                {
                    // Debug.Log("Carta CounterField de los perros " + card);
                    // Debug.Log("Efecto de cartas CounterField de los perros  va a ser activado");
                    // // Effects.EffectsCounterFieldDogs(card, board);
                    // Debug.Log("Efecto de cartas CounterField ya fue activado");
                }
            }
        }
    }

    //devuelve el result que es quien gano esa ronda, 1:playerWin -1:enemyWin 0:draw
    public long GetBattleResult(GameObject[,] board)
    {
        long playerTotalAttack = 0;
        long enemyTotalAttack = 0;
        long result;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] != null)
                {
                    Debug.Log(board[i, j]);
                    ActivateEffects(board, board[i, j], i);
                    if (board[i, j].CompareTag("Unit-Cards"))
                    {
                        if (board[i, j].GetComponent<Unit_Card>().Attack <= 0) //elimina las cartas que tienen 0 de poder en el campo y las manda para el cementerio
                        {
                            cementery.GetComponent<Cementery>().RemoveCardCementery(board[i, j]);
                        }
                        else if (i < 2)
                        {
                            enemyTotalAttack += board[i, j].GetComponent<Unit_Card>().Attack;
                            Debug.Log("el ataque total de los perros es " + enemyTotalAttack);
                        }
                        else
                        {
                            playerTotalAttack += board[i, j].GetComponent<Unit_Card>().Attack;
                            Debug.Log("el ataque total de los gatos es " + playerTotalAttack);
                        }
                    }
                }
            }
        }
        if (playerTotalAttack > enemyTotalAttack)
        {
            result = 1;
            Debug.Log(" El ataque total de los gatos es " + playerTotalAttack);
        }
        else if (playerTotalAttack < enemyTotalAttack)
        {
            result = -1;
            Debug.Log("El ataque total de los perros es " + enemyTotalAttack);
        }
        else
        {
            result = 0;
        }
        return result;
    }

    // se llama al presionar el boton de pasar turno
    public void PassTurn()
    {
        if (isPlayerTurn)
        {
            if (isPlayerReadyForBattle)
                playerPassTurnBeingReadyForBattle = true;

            isPlayerTurn = false;
            isPlayerReadyForBattle = true;
        }
        else
        {
            if (isEnemyReadyForBattle)
                enemyPassTurnBeingReadyForBattle = true;
            isPlayerTurn = true;
            isEnemyReadyForBattle = true;
        }
    }

    //metodo que cambia de ronda, elimina las cartas del campo y las manda para el cementerio
    public void ChangeRound(GameObject[,] board)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] != null)
                {
                    cementery.GetComponent<Cementery>().RemoveCardCementery(board[i, j]);
                }
            }
        }
    }

    //metodo que verifica si se puede cambiar de ronda y de quien es el turno
    public void CheckReadyForBattle()
    {
        long result = 0;
        int countDraw = 0;
        if (playerPassTurnBeingReadyForBattle && enemyPassTurnBeingReadyForBattle)
        {
            matrixBoard = matrix.GetComponent<MatrixBoard>().Board;
            result = GetBattleResult(matrixBoard);
            Debug.Log("roundcounter antes de sumarle 1-  " + roundCounter);
            roundCounter++;
            Debug.Log("roundcounter despues de sumarle 1-  " + roundCounter);
            if (roundCounter == 1)
            {
                Debug.Log("Carta lider de los perros activada");
                enemyScore++;
                cardLeaderEnemy.GetComponent<TextLeaderDogs>().ActivateTextsLeaderDogs();
                ChangeRound(matrixBoard);
            }
            if (countDraw == 1)
            {
                Debug.Log("Carta lider de los gatos activada");
                playerScore++;
                cardLeaderPlayer.GetComponent<TextLeaderCats>().ActivateTextsLeaderCats();
                ChangeRound(matrixBoard);
            }
            if (result == 1)
            {
                playerScore++;
                catsWinRound.GetComponent<TextPlayerWin>().ActivateWinRound();
                ChangeRound(matrixBoard);
            }
            else if (result == -1)
            {
                enemyScore++;
                Debug.Log("Perros ganan esta ronda");
                dogsWinRound.GetComponent<TextEnemyWin>().ActivateWinRound();
                ChangeRound(matrixBoard);
            }
            else
            {
                countDraw++;
            }
            UpdateDataText();
            playerPassTurnBeingReadyForBattle = false;
            enemyPassTurnBeingReadyForBattle = false;
        }
    }

    //metodo que analiza el fin del juego
    public void CheckEndGame()
    {
        if (roundCounter == 2)
        {
            if (playerScore == 2 && enemyScore != 2)
            {
                playerWon = true;
                Debug.Log("Gatos ganan el juego");
                catsWin.GetComponent<TextPlayerWin>().WinGame();
            }
            else if (playerScore != 2 && enemyScore == 2)
            {
                enemyWon = true;
                Debug.Log("Perros ganan el juegos");
                dogsWin.GetComponent<TextEnemyWin>().WinGame();
            }
            ResetScene();
        }
        else if (roundCounter == 3)
        {
            if (playerScore > enemyScore)
            {
                playerWon = true;
                Debug.Log("Gatos ganan el juego");
                catsWin.GetComponent<TextPlayerWin>().WinGame();
                ResetScene();
            }
            else if (playerScore < enemyScore)
            {
                enemyWon = true;
                Debug.Log("Perros ganan el juego");
                dogsWin.GetComponent<TextEnemyWin>().WinGame();
                ResetScene();
            }
        }
    }

    void Update()
    {
        TextTurnsPlayers();
        CheckReadyForBattle();
        CheckEndGame();
    }
}
