using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCardPositionValidScript : MonoBehaviour
{
    //checkea si la posicion donde va a ser colocada la carta es valida
    public bool IsCardPositionValid(GameObject[,] matrixBoard, GameObject card, int x, int y)
    {
        if (matrixBoard[x, y] == null)
        {
            if (card.tag == "E_field" && (x == 0 || x == 1 || x == 2) && y == 0)
            {
                return true;
            }
            else if (card.tag == "P_field" && (x == 3 || x == 4 || x == 5) && y == 0)
            {
                return true;
            }
            else if (card.tag == "E_siege" && x == 0 && (y == 1 || y == 2 || y == 3 || y == 4))
            {
                return true;
            }
            else if (card.tag == "E_archers" && x == 1 && (y == 1 || y == 2 || y == 3 || y == 4))
            {
                return true;
            }
            else if (card.tag == "E_hero" && x == 2 && (y == 1 || y == 2 || y == 3 || y == 4))
            {
                return true;
            }
            else if (card.tag == "P_hero" && x == 3 && (y == 1 || y == 2 || y == 3 || y == 4))
            {
                return true;
            }
            else if (card.tag == "P_archers" && x == 4 && (y == 1 || y == 2 || y == 3 || y == 4))
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
