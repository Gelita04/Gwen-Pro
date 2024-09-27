using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLeaderDogs : MonoBehaviour
{
    public GameObject textLeaderDogs;

    // Start is called before the first frame update
    void Start()
    {
        textLeaderDogs.SetActive(false);
    }

    public void ActivateTextsLeaderDogs()
    {
        
        textLeaderDogs.SetActive(true);
    }

    public void DescativateTextsLeaderDogs()
    {
        
        textLeaderDogs.SetActive(false);
    }
}
