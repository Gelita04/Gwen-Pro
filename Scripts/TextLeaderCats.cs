using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLeaderCats : MonoBehaviour
{
    public GameObject textLeaderCats;

    // Start is called before the first frame update
    void Start()
    {
        textLeaderCats.SetActive(false);
    }

    public void ActivateTextsLeaderCats()
    {
        Debug.Log("se activo el texto del lider de los gatos");
        textLeaderCats.SetActive(true);
    }

    public void DescativateTextsLeaderCats()
    {
        Debug.Log("se desactivo el texto del lider de los gatos");
        textLeaderCats.SetActive(false);
    }
}
