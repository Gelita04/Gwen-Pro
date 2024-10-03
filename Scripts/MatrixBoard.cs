using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLibrary.Objects;

public class MatrixBoard : MonoBehaviour
{
    public IsCardPositionValidScript isCardPositionValidScript; 
    public GameObject[,] Board;
    
    //instancia la matriz que sera el tablero.
    public void Awake()
    {
        Board = new GameObject[6, 5];
        isCardPositionValidScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<IsCardPositionValidScript>();
    }
    
    
}
