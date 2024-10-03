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
    public GameObject board;
    public GameObject[,] tablero;
    public GameObject cementeryCats;
    public GameObject cementeryDogs;
    bool onActivation = false;
    bool onPostAction = false;
    public EffectsUsers Effects;
    public ListsOfCards list;
    public GameObject lista;
    private GameObject NewCard;

    void Start()
    {

        if (Effects == null)
        {
            Effects = gameObject.AddComponent<EffectsUsers>();

        }

        List<string> listTokens = TokenizedTexts.Tokenizar(CodeCards.text);
        List<List<string>> tokens = TokenizedTexts.TokenizarCards(listTokens);
        foreach (var tokenList in tokens)
        {
            Debug.Log(string.Join(", ", tokenList));
        }
        CreateGameObjectsForCards(tokens);


    }
    // metodo que por cada carta,  crea un nuevo gameObject y llama al metodo de crear la carta
    public void CreateGameObjectsForCards(List<List<string>> tokens)
    {


        foreach (var cardTokens in tokens)
        {
            Debug.Log("se crea la carta");
            NewCard = new GameObject("UserCard");
            CreateCardsByUsers(tokens);
        }
    }

    public void CreateCardsByUsers(List<List<string>> tokens)//agregar list<codeEffects> como parametros
    {
        string nameCard = null;
        string type = null;
        string range = null;
        string power = null;
        string faction = null;
        //actualizando las propiedades de la carta a crear
        for (int i = 0; i < tokens.Count; i++)
        {
            List<string> effectNames = new List<string>();
            Dictionary<string, List<GameObject>> targets = new Dictionary<string, List<GameObject>>();
            Dictionary<string, List<Tuple<string, object>>> Params = new Dictionary<string, List<Tuple<string, object>>>();

            for (int j = 0; j < tokens[i].Count; j++)
            {
                if (tokens[i][j] == "Name" && !onActivation)
                {
                    j++;
                    for (; j < tokens[i].Count; j++)
                    {
                        if (tokens[i][j] == "Faction")
                        {
                            break;
                        }
                        nameCard += tokens[i][j] + " ";
                        NewCard.name = nameCard;
                    }


                    Debug.Log("El nombre seleccionado es " + nameCard);
                }
                if (tokens[i][j] == "Power" && !onActivation)
                {
                    j++;
                    power = tokens[i][j];
                    Debug.Log("el poder seleccionado es " + power);
                }
                if (tokens[i][j] == "Range" && !onActivation)
                {
                    j++;
                    range = tokens[i][j];
                    Debug.Log("el rango seleccionado es " + range);
                }
                if (tokens[i][j] == "Type" && !onActivation)
                {
                    j++;
                    type = tokens[i][j];
                    Debug.Log("el tipo seleccionado es " + type);
                }
                if (tokens[i][j] == "Faction" && !onActivation)
                {
                    j++;
                    faction = tokens[i][j];
                    Debug.Log("La faccion seleccionada es " + faction);
                }
                if (tokens[i][j] == "OnActivation")
                {
                    onActivation = true;
                    OnActivation(onActivation, NewCard, tokens, effectNames, targets, Params);
                }
            }

            list.RellenarListaDeCartas(nameCard, effectNames, targets, Params);
            //no aumentar i en el ultimo if si no se saltara un token
            Image newImageCard = NewCard.AddComponent<Image>();
            newImageCard.color = Color.black;
            NewCard.AddComponent<Button>();

        }

        //utilizando las propiedades
        if (faction == "Cats")
        {
            //mandar carta al deck de los gatos
            DeckCats.GetComponent<Deck_Cats>().Deck.Add(NewCard);
            NewCard.transform.SetParent(DeckCats.transform);
        }
        else
        {
            //mandar carta al deck de los perros
            var temp = DeckDogs.GetComponent<Deck_Dogs>().Deck;
            temp.Add(NewCard);
            NewCard.transform.SetParent(DeckDogs.transform);
        }
        if (type == "Unit-Card")
        {
            //asignamos propiedades a la nueva carta de unidad
            Unit_Card NewUnitCard = NewCard.AddComponent<Unit_Card>();
            NewUnitCard.tag = "Unit-Cards";
            NewUnitCard.Name = nameCard;
            Debug.Log("El nombre de la carta de unidad es " + NewUnitCard.Name);
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
            NewBuffCard.tag = "Buff";
            NewBuffCard.Name = nameCard;
            Debug.Log("el nombre de la carta buff es " + NewBuffCard.Name);
            NewBuffCard.IsCreatedByUsers = true;
        }
        if (type == "Field")
        {
            //asignamos propiedades a la nueva carta field
            Field_Card NewFieldCard = NewCard.AddComponent<Field_Card>();
            NewFieldCard.tag = "Field";
            NewFieldCard.Name = nameCard;
            Debug.Log("el nombre de la carta field es " + NewFieldCard.Name);
            NewFieldCard.IsCreatedByUsers = true;
        }
        if (type == "Counter-Field")
        {
            //asignamos propiedades a la nueva carta counterfield
            Counterfield_Card NewCounterfieldCard = NewCard.AddComponent<Counterfield_Card>();
            NewCounterfieldCard.Name = nameCard;
            NewCounterfieldCard.tag = "Counterfield";
            Debug.Log("el nombre de la carta counterfield es " + NewCounterfieldCard.Name);
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
    private object ParseParam(string param)
    {
        if (param == "true" || param == "false")
        {
            return bool.Parse(param);
        }
        else if (char.IsDigit(param[0]))
        {
            return double.Parse(param);
        }
        else
        {
            return param;
        }
    }

    public void OnActivation(bool onActivation, GameObject NewCard, List<List<string>> tokens, List<string> nameEffects, Dictionary<string, List<GameObject>> targets, Dictionary<string, List<Tuple<string, object>>> Params)//------------------->ESTE metodo lo que debe hacer,es guardar en una lista la informacion de los efectos de esta carta,es decir, en dicha lista debe estar el orden por nombre de los efectos a ejecutar y otra lista con los "targets" de cada efecto,donde para el primer efecto le corresponda el primer target , el segundo efecto le corrasponda el segundo target y asi.ejemplo: effectosDELaCarta:[Damage,ReturnToDeck,...] targetsDeLosEfectos:[otherField,otherHand,...].EL objetivo de esto es usar ambas listas en EffectsUser script al llamar al metodo que activa el efecto de una carta, y ahi en ese metodo (el de la pila de cosas comentadas que tenia tupla de pila de cosas, context.Hand y toa esa pga) empezar a iterar por las listas activando uno por uno los efecto de dicha carta. Fijate q debe ser creada estas dos listas de las que hable pero por cada carta. Cada carta tendra sus propias dos listas.cuando digo dos listas me refiero a las que hable al principio,la de numbre de efectos y targets de efectos.DEspue vemos como meterselo a las cartas, por ahora logra conseguir dichas listas para una carta que es revisando cada token y sacando la info que te interesa y meterla en las listas..
    {
        list = lista.GetComponent<ListsOfCards>();
        string nameEffect = "";
        for (int i = 0; i < tokens.Count; i++)
        {
            for (int j = 0; j < tokens[i].Count; j++)
            {
                if (tokens[i][j] == "PostActivation")
                {
                    onPostAction = true;
                    PostActivation(tokens, i, nameEffects, targets, Params);
                }
                if (tokens[i][j] == "Effect" && onActivation && !onPostAction)
                {
                    j++;
                    if (tokens[i][j] == "Name")
                    {
                        nameEffect = tokens[i][j + 1];
                        Debug.Log("el nombre del effecto es " + nameEffect);
                        nameEffects.Add(nameEffect);
                    }
                    List<Tuple<string, object>> listParams = new List<Tuple<string, object>>();

                    for (int k = j; tokens[i][k] != "Selector"; k++)
                    {
                        if (k + 1 >= tokens[i].Count)
                        {
                            break;
                        }
                        else if (tokens[i][k] != "Name" && tokens[i][k] != nameEffect)
                        {
                            object param = ParseParam(tokens[i][k + 1]);
                            listParams.Add(new Tuple<string, object>(tokens[i][k], param));
                            Debug.Log("los parametros son:" + string.Join(", ", param));
                        }
                    }
                    Params.Add(nameEffect, listParams);
                }
                if (tokens[i][j] == "Selector" && !onPostAction)
                {
                    Debug.Log("verifica que es selector y llama al metodo");
                    Selector(tokens, nameEffect, targets);
                    // Debug.Log("sale del metodo Selector y va a llamar al EffectsByUsers");
                    // Effects.EffectsByUser(NewCard);//esto dejalo normal como estaba antes al no ser q creas que tu idea esta buena,mejor que lo que te dije.Yo no la entendi bien.
                }
            }
        }
    }

    public void Selector(List<List<string>> tokens, string nameEffect, Dictionary<string, List<GameObject>> targets)
    {
        Debug.Log("entra al metodo selector");
        string source = "";
        string single = "";
        string sourceFather = "";
        string singleFather = "";

        List<GameObject> boardplayer = new List<GameObject>();
        List<GameObject> boardenemy = new List<GameObject>();


        for (int i = 0; i < tokens.Count; i++)
        {
            for (int j = 0; j < tokens[i].Count; j++)
            {
                if (tokens[i][j] == "Source")
                {
                    source = tokens[i][j + 1];
                    Debug.Log(source);
                    if (tokens[i][j + 2] == "Single")
                    {
                        single = tokens[i][j + 3];
                        Debug.Log(single);
                    }
                    if (source == "hand")
                    {
                        sourceFather = source;
                        if (single == "false")
                        {
                            singleFather = single;
                            targets.Add(nameEffect, playerHand.GetComponent<HandScript>().cards);
                        }
                        else
                        {
                            singleFather = single;
                            //hacer que reciba solo una carta
                        }
                    }
                    if (source == "otherHand")
                    {
                        sourceFather = source;
                        if (single == "false")
                        {
                            singleFather = single;
                            targets.Add(nameEffect, enemyHand.GetComponent<HandScript>().cards);

                        }
                        else
                        {
                            singleFather = single;
                            //hacer que reciba solo una carta
                        }
                    }
                    if (source == "deck")
                    {
                        sourceFather = source;
                        if (single == "false")
                        {
                            singleFather = single;
                            targets.Add(nameEffect, DeckCats.GetComponent<Deck_Cats>().Deck);
                        }
                        else
                        {
                            singleFather = single;
                            //hacer que reciba solo una carta
                        }
                    }
                    if (source == "otherDeck")
                    {
                        sourceFather = source;
                        if (single == "false")
                        {
                            singleFather = single;
                            targets.Add(nameEffect, DeckDogs.GetComponent<Deck_Dogs>().Deck);

                        }
                        else
                        {
                            singleFather = single;
                            //hacer que reciba solo una carta
                        }
                    }
                    if (source == "board")
                    {
                        Debug.Log("entra al source board");
                        sourceFather = source;
                        for (int m = 3; m <= 5; m++)
                        {
                            for (int n = 0; n < 5; n++)
                            {
                                tablero = board.GetComponent<MatrixBoard>().Board;

                                if (tablero[m, n] != null)
                                {
                                    boardplayer.Add(tablero[j, n]);
                                    Debug.Log(string.Join(", ", boardplayer));
                                    if (single == "false")
                                    {
                                        singleFather = single;
                                        targets.Add(nameEffect, boardplayer);
                                    }
                                    else
                                    {
                                        singleFather = single;
                                        //hacer que reciba solo una carta
                                    }
                                }
                            }
                        }
                    }
                    if (source == "otherBoard")
                    {
                        sourceFather = source;
                        for (int m = 0; m <= 2; m++)
                        {
                            for (int n = 0; n < tablero.GetLength(1); n++)
                            {
                                if (tablero[m, n] != null)
                                {
                                    boardenemy.Add(tablero[j, n]);
                                    if (single == "false")
                                    {
                                        singleFather = single;
                                        targets.Add(nameEffect, boardenemy);
                                    }
                                    else
                                    {
                                        singleFather = single;
                                        //hacer que reciba solo una carta
                                    }
                                }
                            }
                        }
                    }
                    if (source == "graveryard")
                    {
                        sourceFather = source;
                        if (single == "false")
                        {
                            singleFather = single;
                            targets.Add(nameEffect, cementeryCats.GetComponent<Cementery>().graveyard);

                        }
                        else
                        {
                            singleFather = single;
                            //hacer que reciba solo una carta
                        }
                    }
                    if (source == "parent")
                    {
                        source = sourceFather;
                        single = singleFather;
                        //hacer que se llame al source del padre
                    }

                }
            }

        }
    }

    public void PostActivation(List<List<string>> tokens, int index, List<string> nameEffects, Dictionary<string, List<GameObject>> targets, Dictionary<string, List<Tuple<string, object>>> Params)
    {
        string nameEffectPostAction = " ";
        index++;
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i][index] == "Name")
            {
                nameEffectPostAction = tokens[i][index];
                nameEffects.Add(nameEffectPostAction);
            }

            List<Tuple<string, object>> listParams = new List<Tuple<string, object>>();
            for (int k = index; tokens[i][k] != "Selector" || k + 1 >= tokens[i].Count; k++)
            {
                if (tokens[i][k] != "Name" && tokens[i][k] != nameEffectPostAction)
                {
                    object param = ParseParam(tokens[i][k + 1]);
                    listParams.Add(new Tuple<string, object>(tokens[i][k], param));
                    Debug.Log("los parametros son:" + string.Join(", ", param));
                }
            }
            Params.Add(nameEffectPostAction, listParams);

            if (tokens[i][index] == "Selector")
            {
                Selector(tokens, nameEffectPostAction, targets);
            }
        }
    }
}



