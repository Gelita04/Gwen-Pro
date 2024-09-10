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
        Debug.Log("texto  win round gatos activado");
        CatsWinRound.SetActive(true);
    }

    public void DesactivateWinRound()
    {
<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
=======
        Debug.Log(" texto win round gatos desactivado");
>>>>>>> Stashed changes
>>>>>>> Stashed changes
        CatsWinRound.SetActive(false);
    }

    public void WinGame()
    {
        Debug.Log("texto win gatos activado");
        CatsWin.SetActive(true);
    }
<<<<<<< Updated upstream

    }

<<<<<<< Updated upstream
    }

=======
=======
}
>>>>>>> Stashed changes
>>>>>>> Stashed changes
