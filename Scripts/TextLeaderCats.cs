using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLeader : MonoBehaviour
{
    public GameObject textLeaderCats;
    
    // Start is called before the first frame update
    void Start()
    {
        textLeaderCats.SetActive(false);
        
    }
    public void ActivateTextsLeaderCats()
    {
        textLeaderCats.SetActive(true);
    }
    
}
