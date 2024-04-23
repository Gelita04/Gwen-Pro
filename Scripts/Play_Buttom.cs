using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play_Buttom : MonoBehaviour
{
    public void Go_To_Select_Deck()
    {
        SceneManager.LoadScene("Gewn");
    }
}
