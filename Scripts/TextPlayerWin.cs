using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPlayerWin : MonoBehaviour
{
    public GameObject TextPlayer;
    void Start()
    {
        TextPlayer.SetActive(false);
    }

    public void ActiveText()
    {
        TextPlayer.SetActive(true);
    }

    
    
}
