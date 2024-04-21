using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cementery : MonoBehaviour
{
    public GameObject[,] Board;

    public void RemoveCard()
    {
        for (int i = 0; i < Board.GetLength(0); i++)
        {
            for (int j = 1; j < Board.GetLength(1); j++)
            {
                if (Board[i,j].GetComponent<Unit_Card>().Attack<=0)
                {
                    ;
                }
            }
        }
    }
    
}
