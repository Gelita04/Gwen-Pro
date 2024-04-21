using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public GameObject[] Row;
    public GameObject card;
    private GameObject cardTarget;
    int attackCard;
    int attackcardTarget;
    
    public void RemoveDamageRandomCards()
    {
        attackCard = card.GetComponent<Unit_Card>().Attack;
        cardTarget = Row[Random.Range(0, Row.Length)];
        attackcardTarget = cardTarget.GetComponent<Unit_Card>().Attack;


    }
     
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
