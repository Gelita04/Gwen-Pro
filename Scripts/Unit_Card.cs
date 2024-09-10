using System;
using System.Collections.Generic;
using GameLibrary.Objects;
<<<<<<< Updated upstream
using Unity.VisualScripting;
using UnityEditor.Build.Content;
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
>>>>>>> Stashed changes
using UnityEngine;
using UnityEngine.UIElements;

public class Unit_Card : MonoBehaviour
{
    public string team;
    public string Name;
    public long Attack;
    public string keywordAction;
    public string keywordObjetive;

    public UnitMember Category;
    private GameObject cementery;
    private GameObject rowToEliminate;
    private GameObject Deck;
    private GameObject matrixBoard;
    private EffectsScript effectRandom;
    private GameObject cardTarget;
    public GameObject[,] board;

    // efecto que quita una cantidad random de ataque a una carta random del campo
    public void RandomCards()
    {
<<<<<<< Updated upstream
        Debug.Log("entro a RandomCards");
=======
<<<<<<< Updated upstream
        Debug.Log("entro a RandomCards");
=======
>>>>>>> Stashed changes
>>>>>>> Stashed changes
        System.Random randomvalor = new System.Random();
        long removeAttack = randomvalor.Next(20, 61);
        long attackCardTarget = cardTarget.GetComponent<Unit_Card>().Attack;

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j].CompareTag("Unit-Cards"))
                {
                    cardTarget = board[i, j];
<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
>>>>>>> Stashed changes
                    Debug.Log("carta objetivo" + cardTarget);
                    Debug.Log(
                        "poder de la carta objetivo antes de activar efecto" + attackCardTarget
                    );
                    if (cardTarget != null)
                    {
                        attackCardTarget -= removeAttack;
                        Debug.Log("poder de la carta luego del efecto" + attackCardTarget);
<<<<<<< Updated upstream
=======
=======
                    if (cardTarget != null)
                    {
                        attackCardTarget -= removeAttack;
>>>>>>> Stashed changes
>>>>>>> Stashed changes
                    }
                    else
                    {
                        Debug.Log("no hay cartas en el campo del enemigo");
                    }
                }
            }
        }
    }

    // efecto que pone una carta aumento en la fila, la carta aumento sale del deck.
    public void PutBuffCats()
    {
        List<GameObject> deckCats = Deck.GetComponent<Deck_Cats>().Deck;
<<<<<<< Updated upstream
        GameObject buffCats = deckCats.Find(obj => obj.CompareTag("Buff")); // esto lo que haces que devuelve el primer elemento que encuentra que tenga la etiqueta Buff
        if (buffCats != null)
        {
            Debug.Log(buffCats);
<<<<<<< Updated upstream
=======
=======
        GameObject buffCats = deckCats.Find(obj => obj.CompareTag("Buff")); //devuelve el primer elemento que encuentra que tenga la etiqueta Buff
        if (buffCats != null)
        {
            // Debug.Log(buffCats);
>>>>>>> Stashed changes
>>>>>>> Stashed changes
            for (int i = 0; i < board.GetLength(1); i++)
            {
                if (board[3, i] == null)
                {
<<<<<<< Updated upstream
                    Debug.Log(board[3, i]);
                    board[3, i] = buffCats;
                    Debug.Log(board[3, i]);
=======
<<<<<<< Updated upstream
                    Debug.Log(board[3, i]);
                    board[3, i] = buffCats;
                    Debug.Log(board[3, i]);
=======
                    // Debug.Log(board[3, i]);
                    board[3, i] = buffCats;
                    // Debug.Log(board[3, i]);
>>>>>>> Stashed changes
>>>>>>> Stashed changes
                    break;
                }
            }
        }
        else
        {
            Debug.Log("no quedan cartas Buff en el deck");
        }
    }

    public void PutBuffDogs()
    {
        List<GameObject> deckDogs = Deck.GetComponent<Deck_Dogs>().Deck;
        GameObject buffDogs = deckDogs.Find(obj => obj.CompareTag("Buff"));

        if (buffDogs != null)
        {
<<<<<<< Updated upstream
            Debug.Log(buffDogs);
=======
<<<<<<< Updated upstream
            Debug.Log(buffDogs);
=======
            // Debug.Log(buffDogs);
>>>>>>> Stashed changes
>>>>>>> Stashed changes
            for (int i = 0; i < board.GetLength(1); i++)
            {
                if (board[0, 1] == null)
                {
<<<<<<< Updated upstream
                    Debug.Log(board[0, 1]);
                    board[0, 1] = buffDogs;
                    Debug.Log(board[0, 1]);
=======
<<<<<<< Updated upstream
                    Debug.Log(board[0, 1]);
                    board[0, 1] = buffDogs;
                    Debug.Log(board[0, 1]);
=======
                    //Debug.Log(board[0, 1]);
                    board[0, 1] = buffDogs;
                    //Debug.Log(board[0, 1]);
>>>>>>> Stashed changes
>>>>>>> Stashed changes
                    break;
                }
            }
        }
        else
        {
            Debug.Log("no quedan cartas Buff en el deck");
        }
    }

    // efecto que pone una carta clima en la fila, la carta clima sale del deck.
    public void PutFieldCats()
    {
        List<GameObject> deckCats = Deck.GetComponent<Deck_Cats>().Deck;
        GameObject fieldCats = deckCats.Find(obj => obj.CompareTag("Field"));

        if (fieldCats != null)
        {
<<<<<<< Updated upstream
            Debug.Log(fieldCats);
=======
<<<<<<< Updated upstream
            Debug.Log(fieldCats);
=======
           // Debug.Log(fieldCats);
>>>>>>> Stashed changes
>>>>>>> Stashed changes
            for (int i = 0; i < board.GetLength(1); i++)
            {
                if (board[0, i] == null)
                {
<<<<<<< Updated upstream
                    Debug.Log(board[0, i]);
                    board[0, i] = fieldCats;
                    Debug.Log(board[0, i]);
=======
<<<<<<< Updated upstream
                    Debug.Log(board[0, i]);
                    board[0, i] = fieldCats;
                    Debug.Log(board[0, i]);
=======
                    //Debug.Log(board[0, i]);
                    board[0, i] = fieldCats;
                    //Debug.Log(board[0, i]);
>>>>>>> Stashed changes
>>>>>>> Stashed changes
                    break;
                }
            }
        }
        else
        {
            Debug.Log(" no quedan cartas Fields en el deck");
        }
    }

    public void PutFieldDogs()
    {
        List<GameObject> deckDogs = Deck.GetComponent<Deck_Cats>().Deck;
        GameObject fieldDogs = deckDogs.Find(obj => obj.CompareTag("Field"));
        if (fieldDogs != null)
        {
<<<<<<< Updated upstream
            Debug.Log(fieldDogs);
=======
<<<<<<< Updated upstream
            Debug.Log(fieldDogs);
=======
            //Debug.Log(fieldDogs);
>>>>>>> Stashed changes
>>>>>>> Stashed changes
            for (int i = 0; i < board.GetLength(1); i++)
            {
                if (board[0, i] == null)
                {
<<<<<<< Updated upstream
                    Debug.Log(board[0, i]);
                    board[0, i] = fieldDogs;
                    Debug.Log(board[0, i]);
=======
<<<<<<< Updated upstream
                    Debug.Log(board[0, i]);
                    board[0, i] = fieldDogs;
                    Debug.Log(board[0, i]);
=======
                   //Debug.Log(board[0, i]);
                    board[0, i] = fieldDogs;
                    //Debug.Log(board[0, i]);
>>>>>>> Stashed changes
>>>>>>> Stashed changes
                    break;
                }
            }
        }
        else
        {
            Debug.Log(" no quedan cartas Fields en el deck");
        }
    }

    // efecto que elimina la carta con mas poder del campo (propio o del adversario)
    public void MaxPower()
    {
        long temp = 0;
        GameObject cardTarget = new GameObject();
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] != null && board[i, j].GetComponent<Unit_Card>().Attack > temp)
                {
<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
>>>>>>> Stashed changes
                    Debug.Log(board[i, j]);
                    Debug.Log(cardTarget);
                    temp = board[i, j].GetComponent<Unit_Card>().Attack;
                    cardTarget = board[i, j];
                    Debug.Log(cardTarget);
<<<<<<< Updated upstream
=======
=======
                    // Debug.Log(board[i, j]);
                    // Debug.Log(cardTarget);
                    temp = board[i, j].GetComponent<Unit_Card>().Attack;
                    cardTarget = board[i, j];
                    //Debug.Log(cardTarget);
>>>>>>> Stashed changes
>>>>>>> Stashed changes
                }
            }
        }
        cementery.GetComponent<Cementery>().RemoveCardCementery(cardTarget);
    }

    //  efecto que elimina la carta con menos poder del campo (solo del rival)
    public void MinPower()
    {
        long temp = long.MaxValue;
        GameObject cardTarget = new GameObject();
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] != null && board[i, j].GetComponent<Unit_Card>().Attack < temp)
                {
<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
>>>>>>> Stashed changes
                    Debug.Log(board[i, j]);
                    Debug.Log(cardTarget);
                    temp = board[i, j].GetComponent<Unit_Card>().Attack;
                    cardTarget = board[i, j];
                    Debug.Log(cardTarget);
<<<<<<< Updated upstream
=======
=======
                    // Debug.Log(board[i, j]);
                    // Debug.Log(cardTarget);
                    temp = board[i, j].GetComponent<Unit_Card>().Attack;
                    cardTarget = board[i, j];
                    //Debug.Log(cardTarget);
>>>>>>> Stashed changes
>>>>>>> Stashed changes
                }
            }
        }
        cementery.GetComponent<Cementery>().RemoveCardCementery(cardTarget);
    }

    // efecto que multiplica su ataque por la cantidad de cartas que hay puestas en el campo
    public void PowerPlusCards(GameObject attackingCard)
    {
        long quantityCards = 0;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] != null)
                {
                    quantityCards++;
                }
            }
        }
<<<<<<< Updated upstream
        Debug.Log(attackingCard.GetComponent<Unit_Card>().Attack);
        attackingCard.GetComponent<Unit_Card>().Attack =
            attackingCard.GetComponent<Unit_Card>().Attack * quantityCards;
        Debug.Log(attackingCard.GetComponent<Unit_Card>().Attack);
=======
<<<<<<< Updated upstream
        Debug.Log(attackingCard.GetComponent<Unit_Card>().Attack);
        attackingCard.GetComponent<Unit_Card>().Attack =
            attackingCard.GetComponent<Unit_Card>().Attack * quantityCards;
        Debug.Log(attackingCard.GetComponent<Unit_Card>().Attack);
=======
        //Debug.Log(attackingCard.GetComponent<Unit_Card>().Attack);
        attackingCard.GetComponent<Unit_Card>().Attack =
            attackingCard.GetComponent<Unit_Card>().Attack * quantityCards;
       // Debug.Log(attackingCard.GetComponent<Unit_Card>().Attack);
>>>>>>> Stashed changes
>>>>>>> Stashed changes
    }

    // efecto que limpia la fila con menos cartas unidad (no vacia, propia o del rival)
    public void CleanRow()
    {
<<<<<<< Updated upstream
        Debug.Log("entro al metodo cleanRow");
=======
<<<<<<< Updated upstream
        Debug.Log("entro al metodo cleanRow");
=======
       // Debug.Log("entro al metodo cleanRow");
>>>>>>> Stashed changes
>>>>>>> Stashed changes
        int countRow0 = 0;
        int countRow1 = 0;
        int countRow2 = 0;
        int countRow3 = 0;
        int countRow4 = 0;
        int countRow5 = 0;
        int[] rows = new int[6];
        for (int i = 0; i < board.GetLength(1); i++)
        {
            if (board[0, i] != null && board[0, i].GetComponent<Unit_Card>())
<<<<<<< Updated upstream
            {
                Debug.Log(board[0, i]);
                countRow0++;
                Debug.Log(countRow0);
            }
            if (board[1, i] != null && board[1, i].GetComponent<Unit_Card>())
            {
                Debug.Log(board[1, i]);
                countRow1++;
                Debug.Log(countRow1);
            }
            if (board[2, i] != null && board[2, i].GetComponent<Unit_Card>())
            {
                Debug.Log(board[2, i]);
                countRow2++;
                Debug.Log(countRow2);
            }
            if (board[3, i] != null && board[3, i].GetComponent<Unit_Card>())
            {
                Debug.Log(board[3, i]);
                countRow3++;
                Debug.Log(countRow3);
            }
            if (board[4, i] != null && board[4, i].GetComponent<Unit_Card>())
            {
                Debug.Log(board[4, i]);
                countRow4++;
                Debug.Log(countRow4);
            }
            if (board[5, i] != null && board[5, i].GetComponent<Unit_Card>())
            {
                Debug.Log(board[5, i]);
                countRow5++;
                Debug.Log(countRow5);
=======
            {
<<<<<<< Updated upstream
                Debug.Log(board[0, i]);
                countRow0++;
                Debug.Log(countRow0);
            }
            if (board[1, i] != null && board[1, i].GetComponent<Unit_Card>())
            {
                Debug.Log(board[1, i]);
                countRow1++;
                Debug.Log(countRow1);
            }
            if (board[2, i] != null && board[2, i].GetComponent<Unit_Card>())
            {
                Debug.Log(board[2, i]);
                countRow2++;
                Debug.Log(countRow2);
            }
            if (board[3, i] != null && board[3, i].GetComponent<Unit_Card>())
            {
                Debug.Log(board[3, i]);
                countRow3++;
                Debug.Log(countRow3);
            }
            if (board[4, i] != null && board[4, i].GetComponent<Unit_Card>())
            {
                Debug.Log(board[4, i]);
                countRow4++;
                Debug.Log(countRow4);
            }
            if (board[5, i] != null && board[5, i].GetComponent<Unit_Card>())
            {
                Debug.Log(board[5, i]);
                countRow5++;
                Debug.Log(countRow5);
=======
                //Debug.Log(board[0, i]);
                countRow0++;
                //Debug.Log(countRow0);
            }
            if (board[1, i] != null && board[1, i].GetComponent<Unit_Card>())
            {
                //Debug.Log(board[1, i]);
                countRow1++;
                //Debug.Log(countRow1);
            }
            if (board[2, i] != null && board[2, i].GetComponent<Unit_Card>())
            {
                //Debug.Log(board[2, i]);
                countRow2++;
               // Debug.Log(countRow2);
            }
            if (board[3, i] != null && board[3, i].GetComponent<Unit_Card>())
            {
                //Debug.Log(board[3, i]);
                countRow3++;
                //Debug.Log(countRow3);
            }
            if (board[4, i] != null && board[4, i].GetComponent<Unit_Card>())
            {
                //Debug.Log(board[4, i]);
                countRow4++;
                //Debug.Log(countRow4);
            }
            if (board[5, i] != null && board[5, i].GetComponent<Unit_Card>())
            {
                //Debug.Log(board[5, i]);
                countRow5++;
               // Debug.Log(countRow5);
>>>>>>> Stashed changes
>>>>>>> Stashed changes
            }
        }
        rows[0] = countRow0;
        rows[1] = countRow1;
        rows[2] = countRow2;
        rows[3] = countRow3;
        rows[4] = countRow4;
        rows[5] = countRow5;
        int a = Int32.MaxValue;
        int coordenateRowToEliminate = -1;
        for (int i = 0; i < rows.Length; i++)
        {
            if (rows[i] < a)
            {
                coordenateRowToEliminate = i;
            }
        }
        GameObject[] x = rowToEliminate
            .GetComponent<EffectsScript>()
            .Rowselected(board, coordenateRowToEliminate);
        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] != null && x[i].GetComponent<Unit_Card>())
            {
                x[i].transform.SetParent(cementery.transform, false);
            }
        }
    }

    // efecto que calcula el promedio de poder de todas las cartas puestas en el campo, luego iguala todas las cartas del campo a ese mismo promedio (propia o del rival)
    public void CardsSamePower()
    {
<<<<<<< Updated upstream
        Debug.Log("entro al metodo CardsSamePower");
=======
<<<<<<< Updated upstream
        Debug.Log("entro al metodo CardsSamePower");
=======
       // Debug.Log("entro al metodo CardsSamePower");
>>>>>>> Stashed changes
>>>>>>> Stashed changes
        long quantityCards = 0;
        long attackCards = 0;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] != null)
                {
                    quantityCards++;
                    attackCards += board[i, j].GetComponent<Unit_Card>().Attack;
                }
            }
        }
        if (quantityCards > 0)
        {
            long promedy = attackCards / quantityCards;
<<<<<<< Updated upstream
            Debug.Log(promedy);
=======
<<<<<<< Updated upstream
            Debug.Log(promedy);
=======
            //Debug.Log(promedy);
>>>>>>> Stashed changes
>>>>>>> Stashed changes
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] != null && board[i, j].CompareTag("Unit_Card"))
                    {
<<<<<<< Updated upstream
                        Debug.Log(board[i, j].GetComponent<Unit_Card>().Attack);
                        board[i, j].GetComponent<Unit_Card>().Attack = promedy;
                        Debug.Log(board[i, j].GetComponent<Unit_Card>().Attack);
=======
<<<<<<< Updated upstream
                        Debug.Log(board[i, j].GetComponent<Unit_Card>().Attack);
                        board[i, j].GetComponent<Unit_Card>().Attack = promedy;
                        Debug.Log(board[i, j].GetComponent<Unit_Card>().Attack);
=======
                        //Debug.Log(board[i, j].GetComponent<Unit_Card>().Attack);
                        board[i, j].GetComponent<Unit_Card>().Attack = promedy;
                        //Debug.Log(board[i, j].GetComponent<Unit_Card>().Attack);
>>>>>>> Stashed changes
>>>>>>> Stashed changes
                    }
                }
            }
        }
    }

    //este metodo antes de llamar a los efectos de las cartas de unidad primero verifica cada palabra clave.
    public void EffectsUnitCardsAtivate(GameObject card)
    {
        if (keywordAction == "Pone" && keywordObjetive == "Buff" && team == "Cats") // la accion "Pone" viene acompañado de una carta Buff o Field.
        {
            PutBuffCats();
        }
        else if (keywordAction == "Pone" && keywordObjetive == "Buff" && team == "Dogs")
        {
            PutBuffDogs();
        }
        else if (keywordAction == "Pone" && keywordObjetive == "Field" && team == "Cats")
        {
            PutFieldCats();
        }
        else if (keywordAction == "Pone" && keywordObjetive == "Field" && team == "Dogs")
        {
            PutFieldDogs();
        }
        else if (keywordAction == "Elimina" && keywordObjetive == "mayor") // la accion "Elimina" viene acompañado de mayor o menor.
        {
            MaxPower();
        }
        else if (keywordAction == "Elimina" && keywordObjetive == "menor")
        {
            MinPower();
        }
        else if (keywordAction == "Dismimuye" && keywordObjetive == "random") // la accion "Disminuye" viene acompañado de random
        {
            RandomCards();
        }
<<<<<<< Updated upstream
        else if (keywordAction == "Iguala" && keywordObject == "promedio") // la accion "Iguala" viene acompañado de promedio.
=======
        else if (keywordAction == "Iguala" && keywordObjetive == "promedio") // la accion "Iguala" viene acompañado de promedio.
>>>>>>> Stashed changes
        {
            CardsSamePower();
        }
        else if (keywordAction == "Multiplica" && keywordObjetive == "mismo") // la accion multiplica viene acompañado de mismo.
        {
            PowerPlusCards(card);
        }
        else if (keywordAction == "Limpia" && keywordObjetive == "menos")
        {
            CleanRow();
        }
    }
}
