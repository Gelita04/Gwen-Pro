using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//efecto de la carta lider de los perros que obtiene un punto luego de la primera ronda
public class EffectLeaderDogs : MonoBehaviour
{
    public void ActivateLeaderDogs(int enemyscore)
    {
        Debug.Log("Entro al metodo de Firulais");
        enemyscore++;
    }
}
