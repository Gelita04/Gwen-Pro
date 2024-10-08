using System;
using System.Collections.Generic;
using System.Linq;
using GameLibrary.Objects;
using UnityEngine;

public class Unit_Card : MonoBehaviour
{
    public string team; //accesible Owner
    public string Name; //accesible Name
    public long Attack; //accesible Power
    public string keywordAction;
    public string keywordObjetive;

    public bool IsCreatedByUsers = false;

    public UnitMember Category;
    public GameObject cementeryCats;
    public GameObject cementeryDogs;
    private GameObject rowToEliminate;
    private GameObject Deck;
    private GameObject matrixBoard;
    private EffectsScript effectRandom;
    public MatrixBoard playerBoard;
    public MatrixBoard enemyBoard;
    // efecto que quita una cantidad random de ataque a una carta random del campo
    public void RandomCards(GameObject card)
    {
        System.Random randomvalor = new System.Random();
        long removeAttack = randomvalor.Next(20, 61);
        //getting team of card

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (card.GetComponent<Unit_Card>().team == "Dogs")
                {

                    if (playerBoard.Board[i, j] != null && playerBoard.Board[i, j].CompareTag("Unit-Cards"))
                    {
                        Debug.Log("La carta de Cats target del efecto RandomCards es " + playerBoard.Board[i, j]);
                        playerBoard.Board[i, j].GetComponent<Unit_Card>().Attack =
                            playerBoard.Board[i, j].GetComponent<Unit_Card>().Attack - removeAttack;
                        break;
                    }
                }
                else if (card.GetComponent<Unit_Card>().team == "Cats")
                {
                    if (enemyBoard.Board[i, j] != null && enemyBoard.Board[i, j].CompareTag("Unit-Cards"))
                    {
                        Debug.Log("La carta de Dogs target  del efecto RandomCards es " + enemyBoard.Board[i, j]);
                        enemyBoard.Board[i, j].GetComponent<Unit_Card>().Attack =
                            enemyBoard.Board[i, j].GetComponent<Unit_Card>().Attack - removeAttack;
                        break;
                    }
                }

            }
        }

    }

    // efecto que pone una carta aumento en la fila, la carta aumento sale del deck.
    public void PutBuffCats()
    {
        List<GameObject> deckCats = Deck.GetComponent<Deck_Cats>().Deck;
        GameObject buffCats = deckCats.Find(obj => obj.CompareTag("Buff")); //devuelve el primer elemento que encuentra que tenga la etiqueta Buff
        if (buffCats != null)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int i = 1; i < 5; i++)
                {
                    if (playerBoard.Board[j, i] == null)
                    {
                        playerBoard.Board[j, i] = buffCats;
                        //changing parent from deck to board of the card
                        buffCats.transform.SetParent(playerBoard.transform);
                        break;
                    }
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
            for (int j = 0; j < 3; j++)
            {
                for (int i = 1; i < 5; i++)
                {
                    if (enemyBoard.Board[j, i] == null)
                    {
                        enemyBoard.Board[j, i] = buffDogs;
                        //changing parent from deck to board of the card
                        buffDogs.transform.SetParent(enemyBoard.transform);
                        break;
                    }
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
        GameObject fieldCats = deckCats.Find(obj => obj.CompareTag("Field")); //devuelve el primer elemento que encuentra que tenga la etiqueta Buff
        if (fieldCats != null)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int i = 1; i < 5; i++)
                {
                    if (playerBoard.Board[j, i] == null)
                    {
                        playerBoard.Board[j, i] = fieldCats;
                        //changing parent from deck to board of the card
                        fieldCats.transform.SetParent(playerBoard.transform);
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.Log("no quedan cartas Buff en el deck");
        }
    }

    public void PutFieldDogs()
    {
        List<GameObject> deckDogs = Deck.GetComponent<Deck_Cats>().Deck;
        GameObject fieldDogs = deckDogs.Find(obj => obj.CompareTag("Field")); //devuelve el primer elemento que encuentra que tenga la etiqueta Buff
        if (fieldDogs != null)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int i = 1; i < 5; i++)
                {
                    if (enemyBoard.Board[j, i] == null)
                    {
                        enemyBoard.Board[j, i] = fieldDogs;
                        //changing parent from deck to board of the card
                        fieldDogs.transform.SetParent(enemyBoard.transform);
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.Log("no quedan cartas Buff en el deck");
        }
    }

    // efecto que elimina la carta con mas poder del campo (propio o del adversario)
    public void MaxPower()
    {
        long temp = 0;
        GameObject cardTarget = new GameObject();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (playerBoard.Board[i, j] != null && playerBoard.Board[i, j].GetComponent<Unit_Card>().Attack > temp)
                {
                    temp = playerBoard.Board[i, j].GetComponent<Unit_Card>().Attack;
                    cardTarget = playerBoard.Board[i, j];
                }
                if (enemyBoard.Board[i, j] != null && enemyBoard.Board[i, j].GetComponent<Unit_Card>().Attack > temp)
                {
                    temp = enemyBoard.Board[i, j].GetComponent<Unit_Card>().Attack;
                    cardTarget = enemyBoard.Board[i, j];
                }
            }
        }
        if (cardTarget != null)
        {
            if (cardTarget.GetComponent<Unit_Card>().team == "Dogs")
            {
                cementeryDogs.GetComponent<Cementery>().RemoveCardCementery(cardTarget);
            }
            else
            {
                cementeryCats.GetComponent<CementeryCats>().RemoveCardCementery(cardTarget);
            }
        }
        else
            Debug.Log("no hay cartas en el campo para buscar el poder maximo para mandar al cementerio");

    }

    //  efecto que elimina la carta con menos poder del campo (solo del rival)
    public void MinPower()
    {
        long temp = 0;
        GameObject cardTarget = new GameObject();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (playerBoard.Board[i, j] != null && playerBoard.Board[i, j].GetComponent<Unit_Card>().Attack <= temp)
                {
                    temp = playerBoard.Board[i, j].GetComponent<Unit_Card>().Attack;
                    cardTarget = playerBoard.Board[i, j];
                }
                if (enemyBoard.Board[i, j] != null && enemyBoard.Board[i, j].GetComponent<Unit_Card>().Attack <= temp)
                {
                    temp = enemyBoard.Board[i, j].GetComponent<Unit_Card>().Attack;
                    cardTarget = enemyBoard.Board[i, j];
                }
            }
        }
        if (cardTarget != null)
        {
            if (cardTarget.GetComponent<Unit_Card>().team == "Dogs")
            {
                cementeryDogs.GetComponent<Cementery>().RemoveCardCementery(cardTarget);
            }
            else
            {
                cementeryCats.GetComponent<CementeryCats>().RemoveCardCementery(cardTarget);
            }
        }
        else
            Debug.Log("no hay cartas en el campo para buscar el poder maximo para mandar al cementerio");
    }

    // efecto que multiplica su ataque por la cantidad de cartas que hay puestas en el campo
    public void PowerPlusCards(GameObject attackingCard)
    {
        long quantityCards = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (playerBoard.Board[i, j] != null)
                {
                    quantityCards++;
                }
                if (enemyBoard.Board[i, j] != null)
                {
                    quantityCards++;
                }
            }
        }
        //Debug.Log(attackingCard.GetComponent<Unit_Card>().Attack);
        attackingCard.GetComponent<Unit_Card>().Attack =
            attackingCard.GetComponent<Unit_Card>().Attack * quantityCards;
        // Debug.Log(attackingCard.GetComponent<Unit_Card>().Attack);
    }

    // efecto que limpia la fila con menos cartas unidad (no vacia, propia o del rival)
    public void CleanRow()
    {
        int countRow0 = 0;
        int countRow1 = 0;
        int countRow2 = 0;
        int countRow3 = 0;
        int countRow4 = 0;
        int countRow5 = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (playerBoard.Board[i, j] != null)
                {
                    if (i == 0)
                    {
                        countRow0++;
                    }
                    if (i == 1)
                    {
                        countRow1++;
                    }
                    if (i == 2)
                    {
                        countRow2++;
                    }
                }
                if (enemyBoard.Board[i, j] != null)
                {
                    if (i == 0)
                    {
                        countRow3++;
                    }
                    if (i == 1)
                    {
                        countRow4++;
                    }
                    if (i == 2)
                    {
                        countRow5++;
                    }
                }
            }
        }
        //getting the row with less cards
        int[] rows = { countRow0, countRow1, countRow2, countRow3, countRow4, countRow5 };
        //getting the index of the row with less cards
        int index = Array.IndexOf(rows, rows.Min());
        //removing all the cards in the row with less cards
        for (int i = 0; i < 5; i++)
        {
            //the index of the row target could be max 6, but if the index is from 0 to 2, the row is from the player, if the index is from 3 to 5, the row is from the enemy
            if (index >= 0 && index <= 2)
            {
                if (playerBoard.Board[index, i] != null)
                {
                    cementeryCats.GetComponent<CementeryCats>().RemoveCardCementery(playerBoard.Board[index, i]);
                }
            }
            else
            {
                if (enemyBoard.Board[index, i] != null)
                {
                    cementeryDogs.GetComponent<Cementery>().RemoveCardCementery(enemyBoard.Board[index, i]);
                }
            }
        }

    }

    // efecto que calcula el promedio de poder de todas las cartas puestas en el campo, luego iguala todas las cartas del campo a ese mismo promedio (propia o del rival)
    public void CardsSamePower()
    {
        long quantityCards = 0;
        long attackCards = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (playerBoard.Board[i, j] != null)
                {
                    quantityCards++;
                    attackCards += playerBoard.Board[i, j].GetComponent<Unit_Card>().Attack;
                }
                if (enemyBoard.Board[i, j] != null)
                {
                    quantityCards++;
                    attackCards += enemyBoard.Board[i, j].GetComponent<Unit_Card>().Attack;
                }
            }
        }
        if (quantityCards > 0)
        {
            long promedy = attackCards / quantityCards;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (playerBoard.Board[i, j] != null && playerBoard.Board[i, j].CompareTag("Unit_Card"))
                    {
                        playerBoard.Board[i, j].GetComponent<Unit_Card>().Attack = promedy;
                    }
                    if (enemyBoard.Board[i, j] != null && enemyBoard.Board[i, j].CompareTag("Unit_Card"))
                    {
                        enemyBoard.Board[i, j].GetComponent<Unit_Card>().Attack = promedy;
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
            RandomCards(card);
        }
        else if (keywordAction == "Iguala" && keywordObjetive == "promedio") // la accion "Iguala" viene acompañado de promedio.
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
