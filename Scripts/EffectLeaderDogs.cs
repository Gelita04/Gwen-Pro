using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//efecto de la carta lider de los perros que evita que evita que los gatos obtengan un punto
public class EffectLeaderDogs : MonoBehaviour
{
    public void ActivateLeaderDogs(int playerscore)
    {
        Debug.Log("Entro al metodo de Firulais");
        playerscore--;
    }
}
