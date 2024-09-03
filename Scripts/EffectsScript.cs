using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using Random = UnityEngine.Random;

public class EffectsScript : MonoBehaviour
{
    private GameObject cardTarget;
    private long attackCard;
    private long attackcardTarget;
    private long removeAttack;
    public GameObject cementery;

    //metodo que selecciona una fila dada la coordenada de la misma
    public GameObject[] Rowselected(GameObject[,] matrixboard, int x) 
    {
        GameObject[] row =
        {
            matrixboard[x, 1],
            matrixboard[x, 2],
            matrixboard[x, 3],
            matrixboard[x, 4]
        };
        return row;
    }

    //metodo que analiza cuantos espacios en la matriz no estan en null, o sea devuelve cuantas cartas hay puesta en una fila
    public GameObject[] ArrayCardNoNull(GameObject[] row) //!!!
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

    //metodo que dado la cordenada de la fila del player 1 devuelve la coordenada de la fila del player enemy
    public int IndexRowEnemy(GameObject[,] board, int x) //!!!
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

    //metodo efecto de las cartas climas
    //como este metodo afecta a los dos jugadores se lo quita a las dos filas.
    public void EffectsField(GameObject[,] board, GameObject cardField, int x) // metodo efecto cartas clima.
    {
        int indexRowEnemy = IndexRowEnemy(board, x);
        removeAttack = cardField.GetComponent<Field_Card>().powerToTake;
        Debug.Log(removeAttack);
        for (int m = 1; m < board.GetLength(0); m++)
        {
            board[x, m].GetComponent<Unit_Card>().Attack =
                board[x, m].GetComponent<Unit_Card>().Attack
                - ((removeAttack * 100) / board[x, m].GetComponent<Unit_Card>().Attack);
                Debug.Log(board[x,m].GetComponent<Unit_Card>().Attack);
            board[indexRowEnemy, m].GetComponent<Unit_Card>().Attack =
                board[indexRowEnemy, m].GetComponent<Unit_Card>().Attack
                - (removeAttack * 100) / board[indexRowEnemy, m].GetComponent<Unit_Card>().Attack;
                Debug.Log(board[indexRowEnemy, m].GetComponent<Unit_Card>().Attack);
        }
    }

    //metodo efecto de las cartas despeje de los gatos
    public void EffectsCounterFieldCats(GameObject cardcounterfield, GameObject[,] board)
    {
        for (int i = 3; i < board.GetLength(0); i++)
        {
            Debug.Log(board[i, 0]);
            Debug.Log(cardcounterfield);
            if (board[i, 0] != null && board[i, 0].CompareTag("Field"))
            {
                Debug.Log("carta sera mandada al cementerio");
                cementery.GetComponent<Cementery>().RemoveCardCementery(board[i, 0]);
                cementery.GetComponent<Cementery>().RemoveCardCementery(cardcounterfield);
            }
            
            cementery.GetComponent<Cementery>().RemoveCardCementery(cardcounterfield);
        }
    }

    public void EffectsCounterFieldDogs(GameObject cardcounterfield, GameObject[,] board)
    {
        for (int i = 0; i <= 2; i++)
        {
            Debug.Log(board[i, 0]);
            if (board[i, 0] != null && board[i, 0].CompareTag("Field"))
            {
                Debug.Log("carta mandada al cementerio");
                cementery.GetComponent<Cementery>().RemoveCardCementery(board[i, 0]);
                cementery.GetComponent<Cementery>().RemoveCardCementery(cardcounterfield);
            }
        }
    }

    //metodo efecto de las cartas aumento
    public void EffectsBuff(GameObject[,] board, GameObject cardBuff, int x)
    {
        removeAttack = cardBuff.GetComponent<Buff_Card>().powerBuff;
        if (cardBuff != null)
        {
            for (int i = 1; i < board.GetLength(0); i++)
            {
                board[x, i].GetComponent<Unit_Card>().Attack =
                    board[x, i].GetComponent<Unit_Card>().Attack + removeAttack;
            }
        }
    }

    //efecto de las cartas despeje
}
