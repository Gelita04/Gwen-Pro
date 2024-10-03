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
        Debug.Log($"[{string.Join(", ", tokens)}]");

    }
    public void EffectsByUser(GameObject card)
    {
        Debug.Log("entro al effectsByUsers");
        Debug.Log("la carta que recibe effectsbyUser es " + card.name);
        //activar el efecto correspondiente a la carta que se esta jugando
        Debug.Log("la cantidad de cartas en la lista de cartas es " + list.listOfCards.Count);

        //agregar targets al enviroment de las variables
        for (int i = 0; i < list.listOfCards.Count; i++)
        {
            //list.listOfCards es List<Tuple<string, List<string>, Dictionary<string, List<GameObject>>, Dictionary<string, List<Tuple<string, object>>>>>
            if (list.listOfCards[i].Item1 == card.name)
            {
                List<Tuple<string, object>> enviromentVariables = new List<Tuple<string, object>>();

                //agregar context al enviroment de las variables
                enviromentVariables.Add(new Tuple<string, object>("context", context));
                enviromentVariables.Add(new Tuple<string,object>("TriggerPlayer",context.TriggersPlayer(card)));
                
                Debug.Log("la carta que se va a comparar es " + list.listOfCards[i].Item1 + " con " + card.name);

                //search for the effect order
                for (int j = 0; j < list.listOfCards[i].Item2.Count; j++)
                {
                    //list.listOfCards[i].Item2 es List<string>
                    Debug.Log("el efecto que se va a comparar es " + list.listOfCards[i].Item2[j]);
                    for (int k = 0; k < listOfCodedEffects.Count; k++)
                    {
                        string effectName = listOfCodedEffects[k].name;
                        if (effectName == list.listOfCards[i].Item2[j])
                        {
                            Debug.Log("el efecto que se va a ejecutar es " + listOfCodedEffects[k].name);
                            //list.listOfCards[i].Item3 es Dictionary<string, List<GameObject>> donde key es el nombre del efecto y value es la lista de targets
                            //agregar targets al enviroment de las variables checkeando
                            if (list.listOfCards[i].Item3.ContainsKey(effectName))
                            {
                                enviromentVariables.Add(new Tuple<string, object>("targets", list.listOfCards[i].Item3[effectName]));
                            }
                            //list.listOfCards[i].Item4 es Dictionary<string, List<Tuple<string, object>>> donde key es el nombre del efecto y value es la lista de parametros
                            //agregar parametros al enviroment de las variables
                            if (list.listOfCards[i].Item4.ContainsKey(effectName))
                            {
                                enviromentVariables.AddRange(list.listOfCards[i].Item4[effectName]);
                            }
                            //execute the effect
                            listOfCodedEffects[k].Execute(enviromentVariables);
                        }
                    }
                }
                break;//ya se encontro la carta
            }
        }
    }
}
// defaultVariables.Add(new Tuple<string, object>("context.Board", context.Board()));
// defaultVariables.Add(new Tuple<string, object>("context.HandOfPlayer", context.handOfPlayer(context.TriggersPlayer(card))));
// defaultVariables.Add(new Tuple<string, object>("context.Hand", context.handOfPlayer(context.TriggersPlayer(card))));
// defaultVariables.Add(new Tuple<string, object>("context.FieldOfPlayer", context.fieldOfPlayer(context.TriggersPlayer(card))));
// defaultVariables.Add(new Tuple<string, object>("context.Field", context.fieldOfPlayer(context.TriggersPlayer(card))));
// defaultVariables.Add(new Tuple<string, object>("context.GraveyardOfPlayer", context.graveryardOfPlayer(context.TriggersPlayer(card))));
// defaultVariables.Add(new Tuple<string, object>("context.Graveyard", context.graveryardOfPlayer(context.TriggersPlayer(card))));
// defaultVariables.Add(new Tuple<string, object>("context.DeckOfPlayer", context.deckOfPlayer(context.TriggersPlayer(card))));
// defaultVariables.Add(new Tuple<string, object>("context.Deck", context.deckOfPlayer(context.TriggersPlayer(card))));

//agregar metodos al enviroment de las variables PENDIENTE FIND
// defaultVariables.Add(new Tuple<string, object>("Shuffle", new EffectsUsers.EffectMethodList(context.Shuffle)));
// defaultVariables.Add(new Tuple<string, object>("Push", new EffectsUsers.EffectMethodListTarget(context.Push)));
// defaultVariables.Add(new Tuple<string, object>("Pop", new EffectsUsers.EffectMethodListTargetReturn(context.Pop)));
// defaultVariables.Add(new Tuple<string, object>("Remove", new EffectsUsers.EffectMethodListTarget(context.Remove)));
// defaultVariables.Add(new Tuple<string, object>("SendBottom", new EffectsUsers.EffectMethodListTarget(context.SendBottom)));
