using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLeader : MonoBehaviour
{
    public GameObject textLeader;
    // Start is called before the first frame update
    void Start()
    {
        textLeader.SetActive(false);
    }

    public void ActivateLeader()
    {
        textLeader.SetActive(true);
    }
}
