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

    void Start()
    {
        //  CreateEffectsByUsers();
    }

    public void CreateEffectsByUsers()
    {
        Debug.Log(CodigoEffects.text);

        string[] tokens = TokenizedTexts
            .GetComponent<TokenizedTexts>()
            .TokenizarEffects(CodigoEffects.text);

        Debug.Log($"[{string.Join(", ", tokens)}]");

    }
    public void EffectsByUser(GameObject[,] board, GameObject card, int x, List<GameObject> hand = null, List<GameObject> otherHand = null, List<GameObject> deck = null, List<GameObject> otherDeck = null)
    {
        //darle la informacion al ast del efecto y llamar al root.Evaluate

    }
}
