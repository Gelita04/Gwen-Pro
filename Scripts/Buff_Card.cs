using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLibrary.Objects;

public class Buff_Card : MonoBehaviour
{
    public string Name;
    public string effect;
    public void Start()
    {
        Card Michidante = new Buff(Name, effect);
        
    }
}
