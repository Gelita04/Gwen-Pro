using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EffectsUsers : MonoBehaviour
{
    public Text CodigoEffects;
    public GameObject TokenizedTexts;
    //public string[] tokenizedCodigoEffects;
    public GameContext context;
    public List<CodedEffect> listOfCodedEffects;

    public ListsOfCards list;
    public MatrixBoard board;
    public GameObject hand;
    public GameObject otherHand;
    public GameObject deck;
    public GameObject otherDeck;
    public GameObject graveyard;


    // public delegate void EffectMethodList(List<GameObject> list);
    // public delegate void EffectMethodListTarget(List<GameObject> list, GameObject target);
    // public delegate GameObject EffectMethodListTargetReturn(List<GameObject> list);

    void Start()
    {
        listOfCodedEffects = new List<CodedEffect>();
        CreateEffectsByUsers();
    }

    public void CreateEffectsByUsers()
    {

        List<Tuple<string, List<string>>> tokens = TokenizedTexts
            .GetComponent<TokenizedTexts>()
            .TokenizarEffects(CodigoEffects.text);
        for (int i = 0; i < tokens.Count; i++)
        {
            // Debug.Log("el nombre del efecto es " + tokens[i].Item1);
            Debug.Log("Token list: " + string.Join(", ", tokens[i].Item2));
        }

        foreach (var codedEffect in tokens)
        {
            string name = codedEffect.Item1;
            listOfCodedEffects.Add(CodedEffectBuilder.BuildEffect(codedEffect.Item2.ToArray(), name));
        }

    }
    public void EffectsByUser(GameObject card)
    {
        Debug.Log("entro al effectsByUsers");
        Debug.Log("la carta que recibe effectsbyUser es " + card.name);
        //activar el efecto correspondiente a la carta que se esta jugando
        Debug.Log("la cantidad de cartas en la lista de cartas es " + list.listOfCards.Count);
        //print every element in list.listOfCards
        for (int i = 0; i < list.listOfCards.Count; i++)
        {
            Debug.Log("la carta en la lista de cartas es " + list.listOfCards[i].Item1);
        }
        //search for the card
        for (int i = 0; i < list.listOfCards.Count; i++)
        {

            if (list.listOfCards[i].Item1 == card.name)
            {
                Debug.Log("La cantidad de efectos de la carta " + list.listOfCards[i].Item1 + " es " + list.listOfCards[i].Item2.Count);

                //search for all the effect of the card 
                List<string> effectsList = list.listOfCards[i].Item2;
                for (int j = 0; j < effectsList.Count; j++)
                {
                    Debug.Log("el efecto que se va a buscar es " + effectsList[j]);
                    for (int k = 0; k < listOfCodedEffects.Count; k++)
                    {
                        string effectName = listOfCodedEffects[k].name;
                        Dictionary<string, string> sourceList = list.listOfCards[i].Item3;
                        Dictionary<string, List<Tuple<string, object>>> paramList = list.listOfCards[i].Item4;
                        Dictionary<string, Find> predicateList = list.listOfCards[i].Item5;

                        if (effectName == effectsList[j])
                        {
                            List<Tuple<string, object>> enviromentVariables = new List<Tuple<string, object>>();

                            //agregar context al enviroment de las variables
                            enviromentVariables.Add(new Tuple<string, object>("context", context));
                            Debug.Log("definiendo triggerPLayer: " + context.TriggersPlayer(card));
                            enviromentVariables.Add(new Tuple<string, object>("TriggerPlayer", context.TriggersPlayer(card)));

                            Debug.Log("el efecto que se va a ejecutar es " + listOfCodedEffects[k].name);
                            //list.listOfCards[i].Item3 es Dictionary<string, List<GameObject>> donde key es el nombre del efecto y value es la lista de targets
                            //agregar targets al enviroment de las variables checkeando
                            // Debug.Log("Cantidad de targets en la carta " + list.listOfCards[i].Item1 + " es " + list.listOfCards[i].Item3.Count + " para el efecto " + effectName);

                            // foreach (var item in list.listOfCards[i].Item3)
                            // {
                            //     ////
                            // }
                            if (sourceList.ContainsKey(effectName))
                            {
                                List<GameObject> targets = new List<GameObject>();
                                string source = sourceList[effectName];
                                Debug.Log("el source es " + source);
                                if (source.Split(" ").Length > 1)
                                {
                                    source = source.Split(" ")[0];
                                    if (source == "hand")
                                        targets.Add(hand.GetComponent<HandScript>().cards[0]);
                                    else if (source == "otherHand")
                                        targets.Add(otherHand.GetComponent<HandScript>().cards[0]);
                                    else if (source == "deck")
                                        targets.Add(deck.GetComponent<Deck_Cats>().Deck[0]);
                                    else if (source == "otherDeck")
                                        targets.Add(otherDeck.GetComponent<Deck_Cats>().Deck[0]);
                                    else if (source == "graveyard")
                                        targets.Add(graveyard.GetComponent<Cementery>().graveyard[0]);
                                    else if (source == "otherGraveyard")
                                        targets.Add(graveyard.GetComponent<Cementery>().graveyard[0]);
                                    else if (source == "field") { }
                                    // targets=list.listOfCards[i].Item3[effectName].Item2.GetComponent<>;
                                    else if (source == "otherField") { }
                                    // targets=list.listOfCards[i].Item3[effectName].Item2.GetComponent<>;
                                    enviromentVariables.Add(new Tuple<string, object>("targets", targets));
                                }
                                else
                                {
                                    if (source == "hand")
                                        targets = hand.GetComponent<HandScript>().cards;
                                    else if (source == "otherHand")
                                        targets = otherHand.GetComponent<HandScript>().cards;
                                    else if (source == "deck")
                                        targets = deck.GetComponent<Deck_Cats>().Deck;
                                    else if (source == "otherDeck")
                                        targets = otherDeck.GetComponent<Deck_Dogs>().Deck;
                                    else if (source == "graveyard")
                                        targets = graveyard.GetComponent<Cementery>().graveyard;
                                    else if (source == "otherGraveyard")
                                        targets = graveyard.GetComponent<Cementery>().graveyard;
                                    else if (source == "field") { }
                                    // targets=list.listOfCards[i].Item3[effectName].Item2.GetComponent<>;
                                    else if (source == "otherField") { }
                                    // targets=list.listOfCards[i].Item3[effectName].Item2.GetComponent<>;}
                                    enviromentVariables.Add(new Tuple<string, object>("targets", targets));

                                }

                                //list.listOfCards[i].Item4 es Dictionary<string, List<Tuple<string, object>>> donde key es el nombre del efecto y value es la lista de parametros
                            }
                            //agregar parametros al enviroment de las variables
                            Debug.Log("Cantidad de parametros en la carta " + list.listOfCards[i].Item1 + " es " + paramList.Count + " para el efecto " + effectName);
                            foreach (var item in paramList)
                            {
                                Debug.Log("la cantidad de parametros para " + item.Key + " es " + item.Value);
                            }
                            if (paramList.ContainsKey(effectName))
                            {
                                enviromentVariables.AddRange(paramList[effectName]);
                            }
                            //execute the effect

                            listOfCodedEffects[k].Execute(enviromentVariables, predicateList.ContainsKey(effectName) ? predicateList[effectName] : null);

                        }

                    }
                }
                break;//efect of card executed successfully
            }
        }
    }
}