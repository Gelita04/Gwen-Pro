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
        Debug.Log("se activo el texto del lider de los perros");
        textLeaderDogs.SetActive(true);
    }
<<<<<<< Updated upstream
    public void DescativateTextsLeaderDogs()
    {
=======
<<<<<<< Updated upstream
    public void DescativateTextsLeaderDogs()
    {
=======

    public void DescativateTextsLeaderDogs()
    {
        Debug.Log("se desactivo el efecto del lider de los perros");
>>>>>>> Stashed changes
>>>>>>> Stashed changes
        textLeaderDogs.SetActive(false);
    }
}
