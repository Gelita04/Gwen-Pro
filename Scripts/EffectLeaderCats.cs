using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//efecto de la carta lider de los gatos que en caso de empate gana el gato
public class EffectLeaderCats : MonoBehaviour
{
    public GameObject text;

    public void ActivateEffect(int playerscore)
    {
        Debug.Log("Entro al metodo del Machi");
        playerscore++;
    }
}
