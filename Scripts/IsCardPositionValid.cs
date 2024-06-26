using System.Collections;
using System.Collections.Generic;
using GameLibrary.Objects;
using UnityEngine;

public class IsCardPositionValidScript : MonoBehaviour
{
    //checkea si la posicion donde va a ser colocada la carta es valida
    public bool IsPlayerCardPositionValid(GameObject[,] matrixBoard, GameObject card, int x, int y)
    {
        if (matrixBoard[x, y] == null)
        {
            if (card.CompareTag("Field") && (x == 3 || x == 4 || x == 5) && y == 0)
            {
                return true;
            }
            else if (card.CompareTag("Unit-Cards") && card.GetComponent<Unit_Card>().Category == UnitMember.hero && x == 3 && (y == 1 || y == 2 || y == 3 || y == 4))
            {
                return true;
            }
            else if (card.CompareTag("Unit-Cards") && card.GetComponent<Unit_Card>().Category == UnitMember.archers && x == 4 && (y == 1 || y == 2 || y == 3 || y == 4))
            {
                return true;
            }
            else if (card.CompareTag("Unit-Cards") && card.GetComponent<Unit_Card>().Category == UnitMember.siege && x == 5 && (y == 1 || y == 2 || y == 3 || y == 4))
            {
                return true;
            }
            else if (card.CompareTag("Buff") && y != 0)
            {
                return true;
            }
            else
            {
                Debug.Log("Invalid position");
                return false;
            }
        }
        else
        {
            Debug.Log("Invalid position");
            return false;
        }
    }

    public bool IsEnemyCardPositionValidate(GameObject[,] board, GameObject card, int x, int y)
    {
        if (board[x, y] == null)
        {
            if (card.CompareTag("Field") && (x == 0 || x == 1 || x == 2) && y == 0)
            {
                return true;
            }
            else if (card.CompareTag("Unit-Cards") && card.GetComponent<Unit_Card>().Category == UnitMember.siege && x == 0 && (y == 1 || y == 2 || y == 3 || y == 4))
            {
                return true;
            }
            else if (card.CompareTag("Unit-Cards") && card.GetComponent<Unit_Card>().Category == UnitMember.archers && x == 1 && (y == 1 || y == 2 || y == 3 || y == 4))
            {
                return true;
            }
            else if (card.CompareTag("Unit-Cards") && card.GetComponent<Unit_Card>().Category == UnitMember.hero && x == 2 && (y == 1 || y == 2 || y == 3 || y == 4))
            {
                return true;
            }
            else if (card.CompareTag("Buff") && y != 0)
            {
                return true;
            }
            else
            {
                Debug.Log("Invalid position");
                return false;
            }
        }
        else
        {
            Debug.Log("Invalid position");
            return false;
        }
    }
}
