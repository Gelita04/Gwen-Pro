using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using Random = UnityEngine.Random;

public class EffectsScript : MonoBehaviour
{ 
    private GameObject cardTarget;
    private int attackCard;
    private int attackcardTarget;
    private int removeAttack;
    public Cementery cementery; 
    public int SearchNumber(string effect) //!!! busca en un string un numero. Lo immplemente para extraer la cantidad de damage que aparece en el string effect.
      { 
          int attack;
          string number = "";
          if (!string.IsNullOrEmpty(effect))
          {
              foreach (char i in effect)
              {
                  if (char.IsNumber(i))
                  {
                      number += i;
                  }
              } 
              attack = int.Parse(number);
              return attack;
          }
          else
          {
              return attack = -1;
          }
      }

    public GameObject[] Rowselected( GameObject[,] matrixboard, int x)//!!!
    {
        GameObject[] row = { matrixboard[x, 1], matrixboard[x, 2], matrixboard[x, 3], matrixboard[x, 4] };
        return row;
    }

    public GameObject[] ArrayCardNoNull(GameObject[] row)//!!!
    {
        GameObject[] result; 
        int nonNullCount = 0;
        foreach (GameObject obj in row)
        {
            if (obj != null)
            {
                nonNullCount++;
            }
        }
        result = new GameObject[nonNullCount];
        int index = 0;
        foreach (GameObject obj in row)
        {
            if (obj != null)
            {
                result[index] = obj;
                index++;
            }
        }
        return result;
    }
    public int IndexRowEnemy(GameObject[,] board, int x)//!!!
    {
        int indexRowEnemy = 0;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            if (x == 0)
            {
                indexRowEnemy = 5;
            }
            else if (x == 1)
            {
                indexRowEnemy = 4;
            }
            else if (x == 2)
            {
                indexRowEnemy = 3;
            }
            else if (x == 3)
            {
                indexRowEnemy = 2;
            }
            else if (x == 4)
            {
                indexRowEnemy = 1;
            }
            else if (x == 5)
            {
                indexRowEnemy = 0;

            }
            else
            {
                Debug.Log("error en coordenadas");
            }
        }
        return indexRowEnemy;
    }

    
    public void RemoveDamageRandomCards( GameObject[,] board, GameObject cardAttacking, int x) // !!! metodo que se encarga de remover damage a una carta random de la fila del enemigo
    { 
        removeAttack = SearchNumber(cardAttacking.GetComponent<Unit_Card>().Effect);
        int indexRowEnemey = IndexRowEnemy(board, x);
        GameObject[] row = Rowselected(board, indexRowEnemey);
        GameObject[] rowToWork = ArrayCardNoNull(row);
        if (cardAttacking != null)
        {
            attackCard = cardAttacking.GetComponent<Unit_Card>().Attack;
            if (removeAttack != -1)
            {
                if (attackCard == 0)
                {
                    cementery.RemoveCardCementery(cardAttacking);
                }
                else
                {
                    cardTarget = rowToWork[Random.Range(0, rowToWork.Length)];
                    attackcardTarget = cardTarget.GetComponent<Unit_Card>().Attack;
                    attackcardTarget -= removeAttack;
                    cardTarget.GetComponent<Unit_Card>().Attack = attackcardTarget;
                    
                }
            }
        }
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public void EffectsField( GameObject[,] board, GameObject cardField, int x) // metodo efecto cartas clima.
    {
        int indexRowEnemy = IndexRowEnemy(board, x);
        removeAttack = SearchNumber(cardField.GetComponent<Field_Card>().Effect);
        for (int m = 1; m < board.GetLength(0); m++)
        {
            board[x,m].GetComponent<Unit_Card>().Attack =  board[x,m].GetComponent<Unit_Card>().Attack - (removeAttack / 100) * board[x,m].GetComponent<Unit_Card>().Attack; 
            board[indexRowEnemy, m].GetComponent<Unit_Card>().Attack= board[indexRowEnemy,m].GetComponent<Unit_Card>().Attack - (removeAttack / 100) * board[indexRowEnemy,m].GetComponent<Unit_Card>().Attack; 
            Debug.Log(board[x,m].GetComponent<Unit_Card>().Name + board[x,m].GetComponent<Unit_Card>().Name);
            Debug.Log(board[indexRowEnemy,m].GetComponent<Unit_Card>().Name+ board[indexRowEnemy,m].GetComponent<Unit_Card>().Attack);
        }
    }
    public void EffectsCounterField( GameObject cardcounterfield, GameObject cardtarget)
    {
        cementery.RemoveCardCementery(cardtarget);
        cementery.RemoveCardCementery(cardcounterfield);
    }
    public void EffectsBuff( GameObject[,] board, GameObject cardBuff, int x)
    {
        removeAttack = SearchNumber(cardBuff.GetComponent<Buff_Card>().effect);
        if (cardBuff!= null)
        {
            for (int i = 1; i < board.GetLength(0); i++)
            {
                board[x,i].GetComponent<Unit_Card>().Attack = board[x,i].GetComponent<Unit_Card>().Attack + (removeAttack/ 100) * board[x,i].GetComponent<Unit_Card>().Attack;
            }
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
