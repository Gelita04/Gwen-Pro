using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using GameLibrary.Objects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardsUsers : MonoBehaviour
{
    public Text CodeCards;
    public TokenizedTexts TokenizedTexts;
    public GameObject DeckCats;
    public GameObject DeckDogs;
    public GameObject playerHand;
    public GameObject enemyHand;
    public GameObject[,] board;
    public GameObject cementeryCats;
    public GameObject cementeryDogs;

    void Start()
    {
        CreateCardsByUsers();
    }

    public void CreateCardsByUsers()
    {
        Debug.Log(CodeCards.text);
        int count = 0;
        string[] tokens = TokenizedTexts.GetComponent<TokenizedTexts>().TokenizarCards(CodeCards.text);
        Debug.Log($"[{string.Join(", ", tokens)}]");
        string nameCard = null;
        string type = null;
        string range = null;
        string power = null;
        string faction = null;
        //actualizando las propiedades de la carta a crear
        for (int i = 0; i < tokens.Length; i++)
        {
            if (tokens[i] == "card")
            {
                count++;
            }
            if (tokens[i] == "Name")
            {
                Debug.Log("Aqui empieza a guardar el nombre .... " + tokens[i]);
                if (tokens[i + 1] == "Faction")
                {
                    break;
                }
                else
                {
                    nameCard = nameCard + tokens[i + 1] + " ";

                }

                Debug.Log("el nombre seleccionado es " + nameCard);
            }
            if (tokens[i] == "Power")
            {
                // Debug.Log(tokens[i]);
                power = tokens[i + 1];
                //Debug.Log("el poder seleccionado es " + power);
            }
            if (tokens[i] == "Range")
            {
                //Debug.Log(tokens[i]);
                range = tokens[i + 1];
                //Debug.Log("el rango seleccionado es " + range);
            }
            if (tokens[i] == "Type")
            {
                //Debug.Log(tokens[i]);
                type = tokens[i + 1];
                // Debug.Log("el tipo seleccionado es " + type);
            }
            if (tokens[i] == "Faction")
            {
                // Debug.Log(tokens[i]);
                faction = tokens[i + 1];
                //Debug.Log("La faccion seleccionada es " + faction);
            }
        }

        //utilizando las propiedades
        GameObject NewCard = new GameObject(nameCard);

        if (faction == "Cats")
        {
            //mandar carta al deck de los gatos
            DeckCats.GetComponent<Deck_Cats>().Deck.Add(NewCard);
            NewCard.transform.SetParent(DeckCats.transform);
        }
        else
        {
            //mandar carta al deck de los perros
            DeckDogs.GetComponent<Deck_Dogs>().Deck.Add(NewCard);
            NewCard.transform.SetParent(DeckDogs.transform);
        }
        if (type == "Unit-Card")
        {
            //asignamos propiedades a la nueva carta de unidad
            Unit_Card NewUnitCard = NewCard.AddComponent<Unit_Card>();
            NewUnitCard.tag = "Unit-Cards";
            NewUnitCard.Name = nameCard;
            //Debug.Log("El nombre de la carta de unidad es " + NewUnitCard.Name);
            NewUnitCard.Attack = long.Parse(power);
            // Debug.Log("El poder de la carta de unidad es " + NewUnitCard.Attack);
            NewUnitCard.team = faction;
            //Debug.Log("La faccion de la carta de unidad es " + NewUnitCard.team);
            if (range == "Hero" || range == "Archers" || range == "Siege")
            {
                if (range == "Hero")
                {
                    range = "hero";
                    NewUnitCard.Category = (UnitMember)Enum.Parse(typeof(UnitMember), range);
                    // Debug.Log("La categoria de las carta de las unidad es " + NewUnitCard.Category);
                }
                else if (range == "Archers")
                {
                    range = "archers";
                    NewUnitCard.Category = (UnitMember)Enum.Parse(typeof(UnitMember), range);
                    // Debug.Log("La categoria de las carta de las unidad es " + NewUnitCard.Category);
                }
                else if (range == "Siege")
                {
                    range = "siege";
                    NewUnitCard.Category = (UnitMember)Enum.Parse(typeof(UnitMember), range);
                    //Debug.Log("La categoria de las carta de las unidad es " + NewUnitCard.Category);
                }
            }
            else
            {
                Debug.Log("Lo siento, carta invalida");
            }
            NewUnitCard.IsCreatedByUsers = true;
        }
        if (type == "Buff")
        {
            //asignamos propiedades a la nueva carta buff
            Buff_Card NewBuffCard = NewCard.AddComponent<Buff_Card>();
            NewBuffCard.Name = nameCard;
            //Debug.Log("el nombre de la carta buff es " + NewBuffCard.Name);
            NewBuffCard.IsCreatedByUsers = true;
        }
        if (type == "Field")
        {
            //asignamos propiedades a la nueva carta field
            Field_Card NewFieldCard = NewCard.AddComponent<Field_Card>();
            NewFieldCard.Name = nameCard;
            //Debug.Log("el nombre de la carta field es " + NewFieldCard.Name);
            NewFieldCard.IsCreatedByUsers = true;
        }
        if (type == "Counter-Field")
        {
            //asignamos propiedades a la nueva carta counterfield
            Counterfield_Card NewCounterfieldCard = NewCard.AddComponent<Counterfield_Card>();
            NewCounterfieldCard.Name = nameCard;
            NewCounterfieldCard.tag = "Counterfield";
            //Debug.Log("el nombre de la carta counterfield es " + NewCounterfieldCard.Name);
            NewCounterfieldCard.IsCreatedByUsers = true;
        }
        if (type == "Leader")
        {
            //asignamos propiedades a la nueva carta lider
            Leader_Card NewLeaderCard = NewCard.AddComponent<Leader_Card>();
            NewLeaderCard.Name = nameCard;
            // Debug.Log("el nombre de la carta lider es " + NewLeaderCard.Name);
            NewLeaderCard.IsCreatedByUsers = true;
        }
    }
// public string PredicateAsString()
// {
// //empezar a iterar por el string cuando salga predicate empezar a concatenar hasta 
// }
    public void OnActivation()
    {
        string[] tokens = TokenizedTexts.TokenizarCards(CodeCards.text);
        List<List<GameObject>> listOfTargets = new List<List<GameObject>>();
        List<GameObject> boardplayer = new List<GameObject>();
        List<GameObject> boardenemy = new List<GameObject>();
        string source = "";
        string single = "";
        for (int i = 0; i < tokens.Length; i++)
        {
            if (tokens[i] == "Source")
            {
                source = tokens[i + 1];
                if (tokens[i + 2] == "Single")
                {
                    single = tokens[i + 3];
                }
                if (source == "hand")
                {
                    if (single == "true")
                    {
                        List<GameObject> temp = new List<GameObject>();
                        temp.Add(playerHand.GetComponent<HandScript>().cards[0]);
                        listOfTargets.Add(temp);
                    }
                    listOfTargets.Add(playerHand.GetComponent<HandScript>().cards);
                    //posible error en el hand script,no hay manera de diferenciar las manos
                }
                if (source == "otherHand")
                {
                    //dependiendo del de arriba
                }
                if (source == "deck")
                {
                    if (single == "true")
                    {
                        List<GameObject> temp = new List<GameObject>();
                        temp.Add(DeckCats.GetComponent<Deck_Cats>().Deck[0]);
                        listOfTargets.Add(temp);
                    }
                    listOfTargets.Add(DeckCats.GetComponent<Deck_Cats>().Deck);

                }
                if (source == "otherDeck")
                {
                    if (single == "true")
                    {
                        List<GameObject> temp = new List<GameObject>();
                        temp.Add(DeckDogs.GetComponent<Deck_Dogs>().Deck[0]);
                        listOfTargets.Add(temp);
                    }
                    listOfTargets.Add(DeckDogs.GetComponent<Deck_Dogs>().Deck);
                }
                if (source == "board")
                {
                    for (int j = 3; j <= 5; j++)
                    {
                        for (int n = 0; n < board.GetLength(1); n++)
                        {
                            if (board[j, n] != null)
                            {
                                boardplayer.Add(board[j, n]);
                            }

                        }
                    }
                    if (single == "true")
                    {
                        List<GameObject> temp = new List<GameObject>();
                        temp.Add(boardplayer[0]);
                        listOfTargets.Add(temp);
                    }
                    listOfTargets.Add(boardplayer);
                }
                if (source == "otherBoard")
                {
                    for (int j = 0; j <= 2; j++)
                    {
                        for (int n = 0; n < board.GetLength(1); n++)
                        {
                            if (board[j, n] != null)
                            {
                                boardenemy.Add(board[j, n]);
                            }

                        }
                    }
                    if (single == "true")
                    {
                        List<GameObject> temp = new List<GameObject>();
                        temp.Add(boardenemy[0]);
                        listOfTargets.Add(temp);
                    }
                    listOfTargets.Add(boardenemy);
                }
                if (source == "graveryard")
                {
                    if (single == "true")
                    {
                        List<GameObject> temp = new List<GameObject>();
                        temp.Add(cementeryCats.GetComponent<Cementery>().graveyard[0]);
                        listOfTargets.Add(temp);
                    }
                    listOfTargets.Add(cementeryCats.GetComponent<Cementery>().graveyard);
                }
                if (source == "parent")
                {
                    //hacer que se llame al source del padre
                }

            }
            if (tokens[i] == "Single")
            {
                single = tokens[i + 1];
                if (single == "true")
                {

                }
            }


        }

    }
}
