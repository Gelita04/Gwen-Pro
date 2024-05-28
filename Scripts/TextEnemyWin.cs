using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEnemyWin : MonoBehaviour
{
    public GameObject DogsWinRound;
    public GameObject DogsWin;
    void Start()
    {
       DogsWinRound.SetActive(false);
       DogsWin.SetActive(false);
    }

    public void WinRound()
    {
        DogsWinRound.SetActive(true);
    }

    public void WinGame()
    {
        DogsWin.SetActive(true);
    }

    
}
