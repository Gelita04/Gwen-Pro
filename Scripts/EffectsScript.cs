using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using Random = UnityEngine.Random;

public class EffectsScript : MonoBehaviour
{
    private GameObject cardTarget;
    public MatrixBoard playerBoard;
    public MatrixBoard enemyBoard;
    private long attackCard;
    private long attackcardTarget;
    private long removeAttack;
    public GameObject cementeryCats;
    public GameObject cementeryDogs;

    //metodo que selecciona una fila dada la coordenada de la misma.
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

    //metodo que dado la cordenada de la fila de uno de los jugador devuelve la coordenada de la fila del otro jugador.
    public int IndexRowEnemy(int x)
    {
        int indexRowEnemy = 0;
        for (int i = 0; i < 5; i++)
        {
            if (x == 0)
            {
                indexRowEnemy = 2;
            }
            else if (x == 1)
            {
                indexRowEnemy = 1;
            }
            else if (x == 2)
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

    //metodo efecto de las cartas climas que afecta a los dos jugadores.
    public void EffectsField(GameObject card, int x) // metodo efecto cartas clima.
    {
        Debug.Log("entro al efecto de las cartas Field");
        int indexRowEnemy = IndexRowEnemy(x);
        removeAttack = card.GetComponent<Field_Card>().powerToTake;
        Debug.Log("carta field va a atacar a la fila " + x + "y a la fila " + indexRowEnemy + "quitara " + removeAttack + "de poder");
        for (int i = 1; i < 5; i++)
        {
            if (playerBoard.Board[x, i] != null)
            {
                Debug.Log("...carta de los gatos en los field");
                List<long> powerByCardsBeforeEffect = new List<long>();
                List<long> powerByCardsAfterEffet = new List<long>();
                powerByCardsBeforeEffect.Add(playerBoard.Board[x, i].GetComponent<Unit_Card>().Attack);
                playerBoard.Board[x, i].GetComponent<Unit_Card>().Attack =
                    playerBoard.Board[x, i].GetComponent<Unit_Card>().Attack
                    - removeAttack;
                powerByCardsAfterEffet.Add(playerBoard.Board[x, i].GetComponent<Unit_Card>().Attack);
                Debug.Log(" el poder de las cartas puestas en el campo antes de ser activado el efecto es " + string.Join(", ", powerByCardsBeforeEffect));
                Debug.Log(" el poder de las cartas puestas en el campo despues de ser activado el efecto es " + string.Join(", ", powerByCardsAfterEffet));


            }
            if (enemyBoard.Board[indexRowEnemy, i] != null)
            {
                Debug.Log("...carta de los gatos en los field");
                List<long> powerByCardsBeforeEffect = new List<long>();
                List<long> powerByCardsAfterEffet = new List<long>();
                powerByCardsBeforeEffect.Add(playerBoard.Board[x, i].GetComponent<Unit_Card>().Attack);
                enemyBoard.Board[x, i].GetComponent<Unit_Card>().Attack =
                    enemyBoard.Board[x, i].GetComponent<Unit_Card>().Attack
                    - removeAttack;
                powerByCardsAfterEffet.Add(enemyBoard.Board[x, i].GetComponent<Unit_Card>().Attack);
                Debug.Log(" el poder de las cartas puestas en el campo antes de ser activado el efecto es " + string.Join(", ", powerByCardsBeforeEffect));
                Debug.Log(" el poder de las cartas puestas en el campo despues de ser activado el efecto es " + string.Join(", ", powerByCardsAfterEffet));


            }
            else
            {
                Debug.Log("no hay cartas en la fila");
            }
        }
    }

    //metodo efecto de las cartas despeje de los gatos.
    public void EffectsCounterField(GameObject card)
    {
        Debug.Log("entro al efecto de las cartas counterfield");
        for (int i = 0; i < 3; i++)
        {
            if (playerBoard.Board[i, 0] != null && playerBoard.Board[i, 0].CompareTag("Field"))
            {
                Debug.Log(" la carta field que va a ser eliminada es " + playerBoard.Board[i, 0]);
                cementeryCats.GetComponent<CementeryCats>().RemoveCardCementery(playerBoard.Board[i, 0]);
                cementeryCats.GetComponent<CementeryCats>().RemoveCardCementery(card);

            }
            if (enemyBoard.Board[i, 0] != null && enemyBoard.Board[i, 0].CompareTag("Field"))
            {
                Debug.Log(" la carta field que va a ser eliminada es " + playerBoard.Board[i, 0]);
                cementeryDogs.GetComponent<CementeryCats>().RemoveCardCementery(playerBoard.Board[i, 0]);
                cementeryCats.GetComponent<CementeryCats>().RemoveCardCementery(card);
            }


        }
    }

    //metodo efecto de las cartas aumento
    public void EffectsBuff(GameObject card, int x)
    {
        Debug.Log("entro al efecto de las cartas buff");
        removeAttack = card.GetComponent<Buff_Card>().powerBuff;
        Debug.Log("cantidad de poder a curar " + removeAttack);
        for (int i = 1; i < 5; i++)
        {
            if (playerBoard.Board[x, i] != card)
            {
                if (playerBoard.Board[x, i] != null)
                {
                    Debug.Log("ataque de " + playerBoard.Board[x, i] + " antes de sumarle powerBuff es " + playerBoard.Board[x, i].GetComponent<Unit_Card>().Attack);
                    playerBoard.Board[x, i].GetComponent<Unit_Card>().Attack = playerBoard.Board[x, i].GetComponent<Unit_Card>().Attack + removeAttack;
                    Debug.Log("ataque de " + playerBoard.Board[x, i] + " despues de sumarle powerBuff es " + playerBoard.Board[x, i].GetComponent<Unit_Card>().Attack);

                }
            }
        }
    }
}
