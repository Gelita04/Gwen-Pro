using System.Collections;
using System.Collections.Generic;
using GameLibrary.Objects;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    public string owner; //----propiedad context.owner que devuelve la faccion de la carta
    public GameObject[,] board;
    public GameObject playerHand;
    public GameObject enemyHand;
    public GameObject playerDeck;
    public GameObject enemyDeck;
    public GameObject cementeryCats;
    public GameObject cementeryDogs;

    //devuelve el id del player que tiro el efecto
    public string TriggersPlayer(GameObject target)
    {
        if (target.CompareTag("Unit-Cards"))
        {
            return target.GetComponent<Unit_Card>().team;
        }
        else if (target.CompareTag("Buff"))
        {
            return target.GetComponent<Buff_Card>().team;
        }
        else if (target.CompareTag("Field"))
        {
            return target.GetComponent<Field_Card>().team;
        }
        else if (target.CompareTag("Counterfield"))
        {
            return target.GetComponent<Counterfield_Card>().team;
        }
        else if (target.CompareTag("Wildcard"))
        {
            return target.GetComponent<Wildcard>().team;
        }
        return "no es una carta definida en el juego";
    }
    //devuelve una lista con todas las cartas puestas en el tablero
    public List<GameObject> Board()
    {
        List<GameObject> cardsInBoard = new List<GameObject>();
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] != null)
                {
                    cardsInBoard.Add(board[i, j]);
                }
            }
        }
        return cardsInBoard;
    }

    //devuelve una lista con todas las cartas en la mano del player
    public List<GameObject> handOfPlayer(string team)
    {
        if (team == "Cats")
        {
            return playerHand.GetComponent<HandScript>().cards;
        }
        return enemyHand.GetComponent<HandScript>().cards;// posible error
    }

    public List<GameObject> Hand(string team)
    {
        if (team == "Cats")
        {
            return playerHand.GetComponent<HandScript>().cards;
        }
        return enemyHand.GetComponent<HandScript>().cards;// posible error
    }
    //devuelve una lista con todas las cartas del deck dependiendo del jugador
    public List<GameObject> deckOfPlayer(string team)
    {
        if (team == "Cats")
        {
            return playerDeck.GetComponent<Deck_Cats>().Deck;
        }
        return enemyDeck.GetComponent<Deck_Dogs>().Deck;
    }

    public List<GameObject> Deck(string team)
    {
        if (team == "Cats")
        {
            return playerDeck.GetComponent<Deck_Cats>().Deck;
        }
        return enemyDeck.GetComponent<Deck_Dogs>().Deck;
    }


    //devuelve una lista con todas las cartas puesta en el tablero dependiendo del player
    public List<GameObject> fieldOfPlayer(string team)
    {
        List<GameObject> playerBoard = new List<GameObject>();
        List<GameObject> enemyBoard = new List<GameObject>();
        if (team == "Cats")
        {
            for (int i = 3; i <= 5; i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] != null)
                    {
                        playerBoard.Add(board[i, j]);
                    }
                }
            }
            return playerBoard;
        }
        else
        {
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] != null)
                    {
                        enemyBoard.Add(board[i, j]);
                    }
                }
            }
            return enemyBoard;
        }
    }
    public List<GameObject> Field(string team)
    {
        List<GameObject> playerBoard = new List<GameObject>();
        List<GameObject> enemyBoard = new List<GameObject>();
        if (team == "Cats")
        {
            for (int i = 3; i <= 5; i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] != null)
                    {
                        playerBoard.Add(board[i, j]);
                    }
                }
            }
            return playerBoard;
        }
        else
        {
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] != null)
                    {
                        enemyBoard.Add(board[i, j]);
                    }
                }
            }
            return enemyBoard;
        }
    }


    //devueluna lista con todas la cartas que ahy dentro del cementerio
    public List<GameObject> graveryardOfPlayer(string team)
    {
        if (team == "Cats")
        {
            return cementeryCats.GetComponent<Cementery>().graveyard;
        }
        return cementeryDogs.GetComponent<Cementery>().graveyard;
    }
    public List<GameObject> Graveyard(string team)
    {
        if (team == "Cats")
        {
            return cementeryCats.GetComponent<Cementery>().graveyard;
        }
        return cementeryDogs.GetComponent<Cementery>().graveyard;
    }


    //devuelve una lista con todas las cartas que cumplen con un predicado
    public void Find()
    {
        //tiene que ver con el predicate
    }
    //agrega una carta al tope de la lista
    public void Push(List<GameObject> targets, GameObject target)
    {
        Debug.Log("Entro a Push");
        Debug.Log(targets.Count);
        targets.Add(target);
    }
    //agrega una carta al fondo de la lista
    public void SendBottom(List<GameObject> targets, GameObject target)
    {
        Debug.Log("Entro a SendBottom");
        Debug.Log(targets.Count);
        targets[0] = target;
    }
    //quita una carta que esta al tope de la lista y la devuelve
    public GameObject Pop(List<GameObject> targets)
    {
        Debug.Log("Entro a Pop");
        Debug.Log(targets.Count);
        Debug.Log(targets[targets.Count - 1]);
        GameObject result = targets[targets.Count - 1];
        targets.RemoveAt(targets.Count - 1);
        return result;
    }
    //remueve una carta de la lista
    public void Remove(List<GameObject> targets, GameObject target)
    {
        Debug.Log("Entro a Remove");
        Debug.Log(targets.Count);
        targets.Remove(target);
    }
    //mezcla la lista
    public void Shuffle(List<GameObject> targets)
    {
        Debug.Log("Entro a Shuffle");
        Debug.Log(targets.Count);
        int n = targets.Count;
        for (int i = 0; i < n; i++)
        {
            // Generar un índice aleatorio entre i y n-1
            int j = Random.Range(i, n);
            // Intercambiar los elementos
            GameObject temp = targets[i];
            targets[i] = targets[j];
            targets[j] = temp;
        }
    }

}
