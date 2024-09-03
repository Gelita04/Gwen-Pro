using System;
using System.Collections;
using System.Collections.Generic;
using GameLibrary.Objects;
using UnityEngine;

public class Wildcard : MonoBehaviour
{
    public string Name;
    public string Effect;
    private GameObject cementery;

    // sustituye una carta selecionada por una wilcard y regresa a tu mano
    public void effectWildcard(GameObject jocker, GameObject cardTarget)
    {
        float jockerposX = jocker.transform.position.x;
        float jockerposY = jocker.transform.position.y;
        float jockerposZ = jocker.transform.position.z;
        float targetposX = cardTarget.transform.position.x;
        float targetposY = cardTarget.transform.position.y;
        float targetposZ = cardTarget.transform.position.z;
        cementery.GetComponent<Cementery>().RemoveCardCementery(cardTarget);
        Debug.Log("carta intercambiada");
        jockerposX = targetposX;
        jockerposY = targetposY;
        jockerposZ = targetposZ;
    }
}
