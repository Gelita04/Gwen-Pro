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
    public bool isPlayerTurnFinished = false;
    public bool isEnemyTurnFinished = false;
    public int playerScore = 0;
    public int enemyScore = 0;
    public int roundCounter = 0;
    public bool playerWon = false;
    public bool enemyWon = false;
    public GameObject board;
    public GameObject[,] matrixBoard;
    public Effects effects;
    public GameObject cementery;
    public EffectWildcard effectwilcard;
    
    
    
    [ContextMenu("Logic")]
    public void addPlayerScore() //dataMatch es "Round: " + roundCounter + " Player: " + playerScore + " Enemy: " + enemyScore;
    {
        playerScore++;
        dataMatch.text = "Round: " + roundCounter + "\nPlayer: " + playerScore + "\nEnemy: " + enemyScore;
    }
    public void addEnemyScore() //dataMatch es "Round: " + roundCounter + " Player: " + playerScore + " Enemy: " + enemyScore;
    {
         enemyScore++;
        dataMatch.text = "Round: " + roundCounter + "\nPlayer: " + playerScore + "\nEnemy: " + enemyScore;
    }


    public void ActivateEffects( GameObject[,] board, GameObject card, int x, int y) // metodo que para activar effecto
    {
        if (card.CompareTag("Field"))
        {
            effects.EffectsField(board,card,x ,y);
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
            
        }
        if (card.CompareTag("Wildcard"))
        {
            effectwilcard.effectWildcard(board,card);
        }
    }
    public int GetBattleResult( GameObject[,] board) //1:playerWin -1:enemyWin 0:draw
    {
        int playerTotalAttack = 0;
        int enemyTotalAttack = 0;
        int result;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 1; j < board.GetLength(1); j++)
            {
                ActivateEffects(board,board[i,j],i,j);
                if (board[i,j].CompareTag("Unit-Cards"))
                {
                    if (board[i,j].GetComponent<Unit_Card>().Attack<=0)
                    {
                        board[i,j].transform.SetParent(cementery.transform,false);
                    }
                    else if (i<2)
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
        if (playerTotalAttack > enemyTotalAttack)
        {
            result = 1;
        }
        else if (playerTotalAttack < enemyTotalAttack)
        {
            result = -1;
        }
        else
        {
            result = 0;
        }

        return result;
    }
   
    
    
    /// //////////////////////////////////////////////////////
    ///ARREGLAR AQUI////
    public void PassTurn() //se llama al presionar el boton de pasar turno
    {
        if (isPlayerTurn)
        {
            isPlayerTurn = false;
        }
        else
        {
            isPlayerTurn = true;
        }
        turnCounter++;
    }
    public void UpdateRoundCounter()
    {
        if (turnCounter == 2)
        {
            roundCounter++;
            turnCounter = 0;
        }
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public void UpdateScore()//se llama constantemente
    {
        if (isPlayerTurnFinished && isEnemyTurnFinished)
        {
            int battleResult = GetBattleResult(matrixBoard); //1:playerWin -1:enemyWin 0:draw
            if (battleResult == 1)
            {
                playerScore++;
                Debug.Log("Player wins");
            }
            else if (battleResult == -1)
            {
                enemyScore++;
                Debug.Log("Enemy wins");
            }
            else
            {
                Debug.Log("Draw");
            }
            isPlayerTurnFinished = false;
            isEnemyTurnFinished = false;
        }
    }

    public void CheckEndGame() // se llama constantemente
    {
        if (roundCounter == 2)
        {
            if (playerScore == 2 && enemyScore != 2)
            {
                playerWon = true;
            }
            else if (playerScore != 2 && enemyScore == 2)
            {
                enemyWon = true;
            }
        }
        else if (roundCounter == 3)
        {
            if (playerScore > enemyScore)
            {
                playerWon = true;
            }
            else if (playerScore < enemyScore)
            {
                enemyWon = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        matrixBoard = board.GetComponent<MatrixBoard>().Board;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRoundCounter();
        UpdateScore();
        CheckEndGame();
    }
}
