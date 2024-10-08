using System.Collections;
using System.Collections.Generic;
using GameLibrary.Objects;
using UnityEngine;

public class IsCardPositionValidScript : MonoBehaviour
{

    // metodo que verifica si la posicion donde va a ser colocada la carta de los gatos es valida
    public bool IsCardPositionValid(GameObject[,] playerBoard, GameObject[,] enemyBoard, GameObject card, int x, int y, bool isPlayerTurn)
    {
        string team = "";
        if (card.CompareTag("Unit-Cards"))
        {
            team = card.GetComponent<Unit_Card>().team;
        }
        else if (card.CompareTag("Buff"))
        {
            team = card.GetComponent<Buff_Card>().team;
        }
        else if (card.CompareTag("Field"))
        {
            team = card.GetComponent<Field_Card>().team;
        }
        else if (card.CompareTag("Counterfield"))
        {
            team = card.GetComponent<Counterfield_Card>().team;
        }
        else if (card.CompareTag("Wildcard"))
        {
            team = card.GetComponent<Wildcard>().team;
        }
        if (isPlayerTurn && playerBoard[x, y] == null && team == "Cats")
        {
            if (card.CompareTag("Field") && y == 0)
            {
                return true;
            }
            else if (
                card.CompareTag("Unit-Cards")
                && card.GetComponent<Unit_Card>().Category == UnitMember.hero
                && x == 0
                && y != 0

            )
            {
                return true;
            }
            else if (
                card.CompareTag("Unit-Cards")
                && card.GetComponent<Unit_Card>().Category == UnitMember.archers
                && x == 1
                && y != 0
            )
            {
                return true;
            }
            else if (
                card.CompareTag("Unit-Cards")
                && card.GetComponent<Unit_Card>().Category == UnitMember.siege
                && x == 2
                && y != 0
            )
            {
                return true;
            }
            else if (card.CompareTag("Buff") && y != 0)
            {
                return true;
            }
            else if (card.CompareTag("Wildcard") && y != 0)
            {
                return true;
            }
            else if (card.CompareTag("Counterfield") && y != 0)
            {
                return true;
            }
            else
            {
                Debug.Log("Invalid position");
                return false;
            }

        }
        else if (!isPlayerTurn && enemyBoard[x, y] == null && team == "Dogs")
        {
            if (card.CompareTag("Field") && y == 0)
            {
                return true;
            }
            else if (
                card.CompareTag("Unit-Cards")
                && card.GetComponent<Unit_Card>().Category == UnitMember.hero
                && x == 2
                && y != 0
            )
            {
                return true;
            }
            else if (
                card.CompareTag("Unit-Cards")
                && card.GetComponent<Unit_Card>().Category == UnitMember.archers
                && x == 1
                &&y!=0
            )
            {
                return true;
            }
            else if (
                card.CompareTag("Unit-Cards")
                && card.GetComponent<Unit_Card>().Category == UnitMember.siege
                && x == 0
                &&y!=0
            )
            {
                return true;
            }
            else if (card.CompareTag("Buff") && y != 0)
            {
                return true;
            }
            else if (card.CompareTag("Wildcard") && y != 0)
            {
                return true;
            }
            else if (card.CompareTag("Counterfield") && y != 0)
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
