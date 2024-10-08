using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using System.Runtime.CompilerServices;
using GameLibrary.Objects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class CardsUsers : MonoBehaviour
{
    public Text CodeCards;
    public TokenizedTexts TokenizedTexts;
    public GameObject DeckCats;
    public GameObject DeckDogs;
    public GameObject playerHand;
    public GameObject enemyHand;
    public MatrixBoard matrixPlayerBoard;
    public MatrixBoard matrixEnemyBoard;
    public GameObject cementeryCats;
    public GameObject cementeryDogs;
    bool onActivation = false;
    bool onPostAction = false;
    public EffectsUsers Effects;
    public ListsOfCards list;
    public GameObject lista;
    // private GameObject NewCard;
    private Dictionary<string, string> effectWithPredicate = new Dictionary<string, string>();

    void Start()
    {

        if (Effects == null)
        {
            Effects = gameObject.AddComponent<EffectsUsers>();

        }
        List<string> listTokens = TokenizedTexts.Tokenizar(CodeCards.text);
        List<List<string>> tokens = TokenizedTexts.TokenizarCards(listTokens);
        effectWithPredicate = TokenizedTexts.GetEffectsWithPredicate(CodeCards.text);
        foreach (var tokenList in tokens)
        {
            Debug.Log(string.Join(", ", tokenList));
        }
        CreateGameObjectsForCards(tokens);


    }
    // metodo que por cada carta,  crea un nuevo gameObject y llama al metodo de crear la carta
    public void CreateGameObjectsForCards(List<List<string>> tokens)
    {

        //Debug.Log("la cantidad de cartas que van a ser creadas es " + tokens.Count);
        foreach (var cardTokens in tokens)
        {
            //Debug.Log("Creating card with tokens: " + string.Join(", ", cardTokens));
            GameObject NewCard = new GameObject("UserCard");
            Image newImageCard = NewCard.AddComponent<Image>();
            newImageCard.color = Color.black;
            NewCard.AddComponent<Button>();
            CreateCardsByUsers(cardTokens, NewCard);
            //Debug.Log("Card created with components: " + NewCard.GetComponents<Component>().Length);
        }
    }

    public void CreateCardsByUsers(List<string> tokens, GameObject NewCard)//agregar list<codeEffects> como parametros
    {
        string nameCard = " ";
        string type = " ";
        string range = " ";
        string power = " ";
        string faction = " ";
        onActivation = false;
        List<string> effectNames = new List<string>();
        Dictionary<string, string> targets = new Dictionary<string, string>();
        Dictionary<string, List<Tuple<string, object>>> Params = new Dictionary<string, List<Tuple<string, object>>>();
        Dictionary<string, Tuple<string, Expression>> predicates = new Dictionary<string, Tuple<string, Expression>>();
        //Debug.Log("FRANCO///// la cantidad de tokens es " + tokens.Count);

        //actualizando las propiedades de la carta a crear
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i] == "Name" && !onActivation)
            {
                //Debug.Log("entro al if de Name");
                i++;
                for (; i < tokens.Count; i++)
                {
                    if (tokens[i] == "Faction")
                    {
                        break;
                    }
                    nameCard += tokens[i] + " ";
                    NewCard.name = nameCard;
                }
                Debug.Log("El nombre seleccionado es " + nameCard);
            }
            if (tokens[i] == "Power" && !onActivation)
            {
                // Debug.Log("entro al if de Power");
                i++;
                power = tokens[i];
                Debug.Log("el poder seleccionado es " + power);
            }
            if (tokens[i] == "Range" && !onActivation)
            {
                //Debug.Log("entro al if de Range");
                i++;
                range = tokens[i];
                Debug.Log("el rango seleccionado es " + range);
            }
            if (tokens[i] == "Type" && !onActivation)
            {
                //Debug.Log("entro al if de Type");
                i++;
                type = tokens[i];
                Debug.Log("el tipo seleccionado es " + type);
            }
            if (tokens[i] == "Faction" && !onActivation)
            {
                i++;
                faction = tokens[i];
                Debug.Log("La faccion seleccionada es " + faction);
            }
            if (tokens[i] == "OnActivation")
            {
                Debug.Log("entro al if del onactivation");
                onActivation = true;
                OnActivation(onActivation, NewCard, tokens, effectNames, targets, Params, predicates);

            }
            //no aumentar i en el ultimo if si no se saltara un token
        }
        list.RellenarListaDeCartas(nameCard, effectNames, targets, Params, predicates);

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
            if (!DeckDogs.GetComponent<Deck_Dogs>().Deck.Contains(NewCard))
            {
                //DeckDogs.GetComponent<Deck_Dogs>().Deck.Add(NewCard);
                NewCard.transform.SetParent(DeckDogs.transform);
            }

        }
        if (type == "Unit-Card")
        {
            //Debug.Log("entro al if de las cartas de unidad");
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
            //Debug.Log("entro al if de las cartas buff");
            //asignamos propiedades a la nueva carta buff
            Buff_Card NewBuffCard = NewCard.AddComponent<Buff_Card>();
            NewBuffCard.tag = "Buff";
            NewBuffCard.Name = nameCard;
            NewBuffCard.team = faction;
            Debug.Log("el nombre de la carta buff es " + NewBuffCard.Name);
            NewBuffCard.IsCreatedByUsers = true;
        }
        if (type == "Field")
        {
            //Debug.Log("entro al if de las cartas de Field");
            //asignamos propiedades a la nueva carta field
            Field_Card NewFieldCard = NewCard.AddComponent<Field_Card>();
            NewFieldCard.tag = "Field";
            NewFieldCard.Name = nameCard;
            NewFieldCard.team = faction;

            Debug.Log("el nombre de la carta field es " + NewFieldCard.Name);
            NewFieldCard.IsCreatedByUsers = true;
        }
        if (type == "Counter-Field")
        {
            //Debug.Log("entro al if de las cartas CounterField");
            //asignamos propiedades a la nueva carta counterfield
            Counterfield_Card NewCounterfieldCard = NewCard.AddComponent<Counterfield_Card>();
            NewCounterfieldCard.Name = nameCard;
            NewCounterfieldCard.tag = "Counterfield";
            NewCounterfieldCard.team = faction;
            Debug.Log("el nombre de la carta counterfield es " + NewCounterfieldCard.Name);
            NewCounterfieldCard.IsCreatedByUsers = true;
        }
        if (type == "Leader")
        {
            // Debug.Log("entro al if de las cartas lider");
            //asignamos propiedades a la nueva carta lider
            Leader_Card NewLeaderCard = NewCard.AddComponent<Leader_Card>();
            NewLeaderCard.Name = nameCard;
            NewLeaderCard.team = faction;
            // Debug.Log("el nombre de la carta lider es " + NewLeaderCard.Name);
            NewLeaderCard.IsCreatedByUsers = true;
        }
        Debug.Log("Valores finales de la carta  - Name: " + nameCard + ", Type: " + type + ", Range: " + range + ", Power: " + power + ", Faction: " + faction);

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

    public void OnActivation(bool onActivation, GameObject NewCard, List<string> tokens, List<string> nameEffects, Dictionary<string, string> targets, Dictionary<string, List<Tuple<string, object>>> Params, Dictionary<string,  Tuple<string, Expression>> predicates)//------------------->ESTE metodo lo que debe hacer,es guardar en una lista la informacion de los efectos de esta carta,es decir, en dicha lista debe estar el orden por nombre de los efectos a ejecutar y otra lista con los "targets" de cada efecto,donde para el primer efecto le corresponda el primer target , el segundo efecto le corrasponda el segundo target y asi.ejemplo: effectosDELaCarta:[Damage,ReturnToDeck,...] targetsDeLosEfectos:[otherField,otherHand,...].EL objetivo de esto es usar ambas listas en EffectsUser script al llamar al metodo que activa el efecto de una carta, y ahi en ese metodo (el de la pila de cosas comentadas que tenia tupla de pila de cosas, context.Hand y toa esa pga) empezar a iterar por las listas activando uno por uno los efecto de dicha carta. Fijate q debe ser creada estas dos listas de las que hable pero por cada carta. Cada carta tendra sus propias dos listas.cuando digo dos listas me refiero a las que hable al principio,la de numbre de efectos y targets de efectos.DEspue vemos como meterselo a las cartas, por ahora logra conseguir dichas listas para una carta que es revisando cada token y sacando la info que te interesa y meterla en las listas..
    {
        //Debug.Log("entro en el OnActivation");
        list = lista.GetComponent<ListsOfCards>();
        string nameEffect = "";
        for (int i = 0; i < tokens.Count; i++)
        {
            // Debug.Log("se esta analizando el token: " + tokens[i]);
            if (tokens[i] == "PostAction")
            {
                //Debug.Log("////////////////////////////entro a postaction/////////////////////////////");
                onPostAction = true;
                PostAction(tokens, i, nameEffects, targets, Params, predicates);
            }
            if (tokens[i] == "Effect" && onActivation && !onPostAction)
            {
                //Debug.Log(" antes de Name i = " + i);
                i++;
                if (tokens[i] == "Name")
                {
                    i++;
                    nameEffect = tokens[i];
                    Debug.Log("el nombre del effecto es " + nameEffect);
                    nameEffects.Add(nameEffect);
                    if (i + 1 >= tokens.Count)
                    {
                        break;
                    }
                    i++;
                }
                if (tokens[i] == "Effect" || tokens[i] == "Selector")
                {
                    i--;
                    continue;
                }
                //Debug.Log(" Despues de verificar Name i = " + i);
                List<Tuple<string, object>> listParams = new List<Tuple<string, object>>();

                //rellenar listParams con los parametros:
                for (; i < tokens.Count; i++)
                {
                    //Debug.Log("i=" + i);
                    //Debug.Log("se printea" + tokens[i]);
                    if (tokens[i] == "Effect" || tokens[i] == "Selector")
                    {
                        i--;
                        break;
                    }
                    else if (tokens[i] != " " && char.IsLetter(tokens[i][0]))
                    {
                        // Debug.Log("va a empezar a guardar los parametros con " + tokens[i]);
                        object param = ParseParam(tokens[i + 1]);
                        listParams.Add(new Tuple<string, object>(tokens[i], param));
                        // Debug.Log(" MAGELA\\\\\\\\los parametros son:" + tokens[i] + " " + param);
                        i++;
                    }

                }

                Params.Add(nameEffect, listParams);
            }
            if (tokens[i] == "Selector" && !onPostAction)
            {
                //Debug.Log("verifica que es selector y llama al metodo");
                Selector(tokens, nameEffect, targets, predicates);

            }

        }
    }

    public void Selector(List<string> tokens, string nameEffect, Dictionary<string, string> targets, Dictionary<string, Tuple<string, Expression>> predicates)
    {
        //Debug.Log("entra al metodo selector");
        string source = "";
        string single = "";
        string sourceFather = "";
        string singleFather = "";

        List<GameObject> boardplayer = new List<GameObject>();
        List<GameObject> boardenemy = new List<GameObject>();
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i] == "Source")
            {
                i++;
                source = tokens[i];
                //Debug.Log("El source es " + source);
                if (tokens[i + 1] == "Single")
                {
                    single = tokens[i + 2];
                    // Debug.Log("El single es " + single);
                }
                if (source == "hand")
                {
                    sourceFather = source;
                    if (single == "false")
                    {
                        singleFather = single;
                        targets.Add(nameEffect, "hand");
                    }
                    else
                    {
                        singleFather = single;
                        targets.Add(nameEffect, "hand single");
                    }
                }
                if (source == "otherHand")
                {
                    sourceFather = source;
                    if (single == "false")
                    {
                        singleFather = single;
                        targets.Add(nameEffect, "otherHand");

                    }
                    else
                    {
                        singleFather = single;
                        targets.Add(nameEffect, "otherHand single");
                    }
                }
                if (source == "deck")
                {
                    sourceFather = source;
                    if (single == "false")
                    {
                        singleFather = single;
                        targets.Add(nameEffect, "deck");
                    }
                    else
                    {
                        singleFather = single;
                        targets.Add(nameEffect, "deck single");
                    }
                }
                if (source == "otherDeck")
                {
                    sourceFather = source;
                    if (single == "false")
                    {
                        singleFather = single;
                        targets.Add(nameEffect, "otherDeck");

                    }
                    else
                    {
                        singleFather = single;
                        targets.Add(nameEffect, "otherDeck single");
                    }
                }
                if (source == "board")
                {
                    sourceFather = source;
                    for (int m = 0; m < 3; m++)
                    {
                        for (int n = 0; n < 5; n++)
                        {
                            if (matrixPlayerBoard.Board[m, n] != null)
                            {
                                boardplayer.Add(matrixPlayerBoard.Board[i, n]);
                                if (single == "false")
                                {
                                    singleFather = single;
                                    targets.Add(nameEffect, "board");
                                }
                                else
                                {
                                    singleFather = single;
                                    targets.Add(nameEffect, "board single");
                                }
                            }
                        }
                    }
                }
                if (source == "otherBoard")
                {
                    sourceFather = source;
                    for (int m = 0; m < 3; m++)
                    {
                        for (int n = 0; n < 5; n++)
                        {
                            if (matrixEnemyBoard.Board[m, n] != null)
                            {
                                boardenemy.Add(matrixEnemyBoard.Board[i, n]);
                                if (single == "false")
                                {
                                    singleFather = single;
                                    targets.Add(nameEffect, "otherBoard");
                                }
                                else
                                {
                                    singleFather = single;
                                    targets.Add(nameEffect, "otherBoard single");
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
                        targets.Add(nameEffect, "graveryard");
                    }
                    else
                    {
                        singleFather = single;
                        targets.Add(nameEffect, "graveryard single");
                    }
                }
                if (source == "otherGraveryard")
                {
                    sourceFather = source;
                    if (single == "false")
                    {
                        singleFather = single;
                        targets.Add(nameEffect, "otherGraveryard");
                    }
                    else
                    {
                        singleFather = single;
                        targets.Add(nameEffect, "otherGraveryard single");
                    }
                }
                if (source == "parent")
                {
                    source = sourceFather;
                    single = singleFather;
                    //hacer que se llame al source del padre
                }

            }
            if (tokens[i] == "Predicate")
            {
                Debug.Log("ENTRO A PREDICATEEEEEEEEEEEEE");
                if (effectWithPredicate.ContainsKey(nameEffect))
                {
                    int index = 0;
                    List<string> predicateTokens = TokenizedTexts.TokenizarPredicate(effectWithPredicate[nameEffect]).ToList();
                    //example is : (unit) => unit.Attack > 5
                    //we have to parse only what is after the => token
                    string variable = predicateTokens[1];

                    for (int r = 0; r < predicateTokens.Count; r++)
                    {
                        //remove every token before the => token including the => token
                        if (predicateTokens[r] == "=>")
                        {
                            predicateTokens.RemoveRange(0, r + 1);
                            break;
                        }
                    }
                    Expression predicate = AST_Builder.ParseExpression(predicateTokens.ToArray(), ref index);
                    if (predicate != null)
                    {
                        predicates.Add(nameEffect, new Tuple<string, Expression>(variable, predicate));
                    }
                }
                else
                {
                    Debug.Log("Predicate: null");
                    throw new Exception("Predicate: null");
                }
            }
        }
    }

    public void PostAction(List<string> tokens, int x, List<string> nameEffects, Dictionary<string, string> targets, Dictionary<string, List<Tuple<string, object>>> Params, Dictionary<string, Tuple<string, Expression>> predicates)
    {
        string nameEffectPostAction = " ";
        for (int i = x + 1; i < tokens.Count; i++)
        {

            if (tokens[i] == "Name")
            {
                nameEffectPostAction = tokens[i + 1];
                nameEffects.Add(nameEffectPostAction);
                //Debug.Log("el nombre del effecto en el postaction es " + nameEffectPostAction);
                if (i + 1 >= tokens.Count)
                {
                    break;
                }
                i++;
            }

            List<Tuple<string, object>> listParams = new List<Tuple<string, object>>();
            for (; i < tokens.Count; i++)
            {
                if (tokens[i] == "Name" || tokens[i] == "Selector")
                {
                    i--;
                    break;
                }
                else if (tokens[i] != " " && char.IsLetter(tokens[i][0]))
                {
                    if (i + 1 >= tokens.Count)
                    {
                        break;
                    }
                    object param = ParseParam(tokens[i + 1]);
                    listParams.Add(new Tuple<string, object>(tokens[i], param));
                    // Debug.Log(" MAGELA\\\\\\\\los parametros  en postactivation son:" + tokens[i] + " " + param);
                    i++;
                }

            }
            Params.Add(nameEffectPostAction, listParams);
            if (tokens[i] == "Selector")
            {
                Selector(tokens, nameEffectPostAction, targets, predicates);
            }

        }
    }
}









