using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLibrary.Objects;

public class MatrixBoard : MonoBehaviour
{
    //added
    public IsCardPositionValidScript isCardPositionValidScript;
    ///////

    public GameObject[,] matrixBoard = new GameObject[6, 5];
    public GameObject P_hero;
    public GameObject P_archers;
    public GameObject P_siege;
    public GameObject P_field;
    public GameObject E_hero;
    public GameObject E_archers;
    public GameObject E_siege;
    public GameObject E_field;


    //added
    public void Start()
    {
        isCardPositionValidScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<IsCardPositionValidScript>();
    }
    ///////


    public void VerifyMatrix()
    {
        if (matrixBoard[0, 0] != E_field || matrixBoard[1, 0] != E_field || matrixBoard[2, 0] != E_field)
        {
            Console.WriteLine("error");
        }
        if (matrixBoard[3, 0] != P_field || matrixBoard[4, 0] != P_field || matrixBoard[5, 0] != P_field)
        {
            Console.WriteLine("error");
        }
        if (matrixBoard[0, 1] != E_siege || matrixBoard[0, 2] != E_siege || matrixBoard[0, 3] != E_siege || matrixBoard[0, 4] != E_siege)
        {
            Console.WriteLine("error");
        }
        if (matrixBoard[1, 1] != E_archers || matrixBoard[1, 2] != E_archers || matrixBoard[1, 3] != E_archers || matrixBoard[1, 4] != E_archers)
        {
            Console.WriteLine("error");
        }
        if (matrixBoard[2, 1] != E_hero || matrixBoard[2, 2] != E_hero || matrixBoard[2, 3] != E_hero || matrixBoard[2, 4] != E_hero)
        {
            Console.WriteLine("error");
        }
        if (matrixBoard[3, 1] != P_hero || matrixBoard[3, 2] != P_hero || matrixBoard[3, 3] != P_hero || matrixBoard[3, 4] != P_hero)
        {
            Console.WriteLine("error");
        }
        if (matrixBoard[4, 1] != P_archers || matrixBoard[4, 2] != P_archers || matrixBoard[4, 3] != P_archers || matrixBoard[4, 4] != P_archers)
        {
            Console.WriteLine("error");
        }
        if (matrixBoard[5, 1] != P_siege || matrixBoard[5, 2] != P_siege || matrixBoard[5, 3] != P_siege || matrixBoard[5, 4] != P_siege)
        {
            Console.WriteLine("error");
        }
    }

}
