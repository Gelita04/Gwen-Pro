using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPlayerWin : MonoBehaviour
{
    public GameObject CatsWinRound;
    public GameObject CatsWin;

    void Start()
    {
        CatsWinRound.SetActive(false);
        CatsWin.SetActive(false);
    }

    public void ActivateWinRound()
    {
       
        CatsWinRound.SetActive(true);
    }

    public void DesactivateWinRound()
    {
        
        CatsWinRound.SetActive(false);
    }

    public void WinGame()
    {
       
        CatsWin.SetActive(true);
    }
}
