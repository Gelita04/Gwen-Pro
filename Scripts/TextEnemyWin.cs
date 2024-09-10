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

    public void ActivateWinRound()
    {
        Debug.Log("texto win round perros activado");
        DogsWinRound.SetActive(true);
    }
    public void DesactivateWinRound()
    {
        DogsWinRound.SetActive(false);
    }
<<<<<<< Updated upstream
=======

    public void DesactivateWinRound()
    {
        Debug.Log("texto win round perros desactivado");
        DogsWinRound.SetActive(false);
    }
>>>>>>> Stashed changes

    public void WinGame()
    {
        Debug.Log(" texto win perros activado");
        DogsWin.SetActive(true);
    }
}
