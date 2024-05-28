using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public GameObject cementeryCats;
    public GameObject cementeryDogs;
    private GameObject effectwilcard;
    private GameObject leadereffect;
    public GameObject TextEnemy;
    public GameObject catsWinRound;
    public GameObject dogsWinRound;
    public GameObject cardLeaderPlayer;
    public GameObject cardLeaderEnemy;
    public GameObject catsWin;
    public GameObject dogsWin;
    
    



    [ContextMenu("Logic")]
    public void UpdateDataText() //dataMatch es "Round: " + roundCounter + " Player: " + playerScore + " Enemy: " + enemyScore;
    {
        dataMatch.text = "Round: " + roundCounter + "\nCats: " + playerScore + "\nDogs: " + enemyScore;
       
    }
    public void ActivateEffects( GameObject[,] board, GameObject card, int x) // metodo que para activar effecto
    {
        EffectsScript effects = this.effects.GetComponent<EffectsScript>();
        if (card!=null)
        {
            if (card.CompareTag("Field"))
            {
                effects.EffectsField(board,card,x);
            }
            if (card.CompareTag("Buff"))
            {
                effects.EffectsBuff(board,card,x);
            }
            if (card.CompareTag("Unit-Cards"))
            {
                effects.RemoveDamageRandomCards(board,card,x);
            }
            if (card.CompareTag("Counterfield"))
            {
                //effects.EffectsCounterField(card, );
            }
            if (card.CompareTag("Wildcard"))
            {
                effectwilcard.GetComponent<EffectWildcard>().effectWildcard(board, card);
            }
        }
        
    }
    // ReSharper disable Unity.PerformanceAnalysis
    //este metodo no se sobrecarga, solo se llama si cumple la condicion, no 30 veces por sengundo.
    public long GetBattleResult( GameObject[,] board) //  devuelve el result que es quien gano esa ronda, 1:playerWin -1:enemyWin 0:draw
    {
        long playerTotalAttack = 0;
        long enemyTotalAttack = 0;
        long result;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 1; j < board.GetLength(1); j++)
            {
                if(board[i,j] != null)
                {
                    ActivateEffects(board, board[i, j], i);
                    if (board[i, j].CompareTag("Unit-Cards"))
                    {
                        if (board[i, j].GetComponent<Unit_Card>().Attack <= 0)
                        {
                            if (i<3)
                            {
                                board[i, j].transform.SetParent(cementeryDogs.transform, false);   
                            }
                            board[i, j].transform.SetParent(cementeryCats.transform, false);
                            
                        }
                        else if (i < 2)
                        {
                            enemyTotalAttack += board[i, j].GetComponent<Unit_Card>().Attack;
                        }
                        else
                        {
                            playerTotalAttack += board[i, j].GetComponent<Unit_Card>().Attack;
                        }
                    }
                }
            }
        }
        if (playerTotalAttack > enemyTotalAttack)
        {
            result = 1;
            Debug.Log(playerTotalAttack);
        }
        else if (playerTotalAttack < enemyTotalAttack)
        {
            result = -1;
            Debug.Log(enemyTotalAttack);
        }
        else
        {
            result = 0;
            
        }
        return result;
    }
    
    public void PassTurn() // se llama al presionar el boton de pasar turno
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

    public void ChangeRound( GameObject[,] board)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i,j] != null && i<3)
                {
                    board[i,j].transform.SetParent(cementeryDogs.transform, false);
                }
                else if (board[i,j]!= null && i >=3 )
                {
                    board[i,j].transform.SetParent(cementeryCats.transform,false);
                }
                
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void CheckReadyForBattle()
    {
        long result = 0;
        if (playerPassTurnBeingReadyForBattle && enemyPassTurnBeingReadyForBattle)
        {
            matrixBoard = matrix.GetComponent<MatrixBoard>().Board;
            result = GetBattleResult(matrixBoard);
            roundCounter++;
            if (result == 1)
            {
                playerScore++; 
                Debug.Log("Player win this round");
                catsWinRound.GetComponent<TextPlayerWin>().WinRound();
                ChangeRound(matrixBoard);

            }
            else if (result == -1)
            {
                enemyScore++;
                Debug.Log("Enemy win this round");
                dogsWinRound.GetComponent<TextEnemyWin>().WinRound();
                ChangeRound(matrixBoard);
            }
            else
            {
                cardLeaderPlayer.GetComponent<EffectPepe>().ActivateEffect(playerScore);
                //textleader.GetComponent<TextLeader>().ActivateLeader();
                //TextPlayer.GetComponent<TextPlayerWin>().ActiveText();
                Debug.Log("Player Win");
                ChangeRound(matrixBoard);
            }
            UpdateDataText();
            playerPassTurnBeingReadyForBattle = false;
            enemyPassTurnBeingReadyForBattle = false;
        }
    }
    // ReSharper disable Unity.PerformanceAnalysis

    public void CheckEndGame() // se llama constantemente, metodo para verificar la cantidad de rondas.
    {
        if (roundCounter == 2)
        {
            if (playerScore == 2 && enemyScore != 2)
            {
                playerWon = true;
                Debug.Log("Cats Win");
                catsWin.GetComponent<TextPlayerWin>().WinGame();
            }
            else if (playerScore != 2 && enemyScore == 2)
            {
                enemyWon = true;
                Debug.Log("Dogs Win");
                dogsWin.GetComponent<TextEnemyWin>().WinGame();
            }
        }
        else if (roundCounter == 3)
        {
            if (playerScore > enemyScore)
            {
                playerWon = true;
                Debug.Log("Cats Win");
                catsWin.GetComponent<TextPlayerWin>().WinGame();
            }
            else if (playerScore < enemyScore)
            {
                enemyWon = true;
                Debug.Log("Dogs Win");
                dogsWin.GetComponent<TextEnemyWin>().WinGame();
                
            }
        }
    }
    
    void Start()
    {
        
    }
    void Update()
    {
        CheckReadyForBattle();
        CheckEndGame();
    }
}
