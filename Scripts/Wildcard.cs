using System;
using System.Collections;
using System.Collections.Generic;
using GameLibrary.Objects;
using UnityEngine;

public class Wildcard : MonoBehaviour
{
    public string Name;

    // sustituye una carta selecionada por una wilcard y regresa a tu mano
    public void effectWildcard(GameObject jocker, GameObject cardTarget)
    {
        Vector3 jockerPosition = jocker.transform.position;
        Vector3 targetPosition = cardTarget.transform.position;
        jocker.transform.position = targetPosition;
        cardTarget.transform.position = jockerPosition;
        Debug.Log("carta intercambiada");
    }
}
