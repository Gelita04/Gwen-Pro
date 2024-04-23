using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEnemyWin : MonoBehaviour
{
    public GameObject TextEnemy;
    void Start()
    {
        TextEnemy.SetActive(false);
    }

    public void ActiveText()
    {
        TextEnemy.SetActive(true);
    }

    
}
