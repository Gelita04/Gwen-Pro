using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using Random = UnityEngine.Random;

public class Effects : MonoBehaviour
{ 
    private GameObject cardTarget;
    private int attackCard;
    private int attackcardTarget;
    private int removeAttack;
    public GameObject cementery;
   public int  SearchNumber( string effect)
    { 
        int attack;
        string number = "";
        foreach (char i in effect)
        {
            if (char.IsNumber(effect,i))
            {
                number += i.ToString();
            }
        }

        attack = int.Parse(number);
        return attack;
    }
    public void RemoveDamageRandomCards( GameObject[,] board, GameObject cardAttacking, int x) // metodo que se encarga de remover damage a una carta randon de la fila 
    {
       
        removeAttack = SearchNumber(cardAttacking.GetComponent<Unit_Card>().Effect);
        attackCard = cardAttacking.GetComponent<Unit_Card>().Attack;
        cardTarget = board[x, Random.Range(1, board.GetLength(0))];
        attackcardTarget = cardTarget.GetComponent<Unit_Card>().Attack;
        attackcardTarget -= removeAttack;
        cardTarget.GetComponent<Unit_Card>().Attack = attackcardTarget;
    }
    public void EffectsField( GameObject[,] board, GameObject cardField, int x, int j) // metodo efecto cartas clima.
    {
        
        int indexRowEnemy;
        removeAttack = SearchNumber(cardField.GetComponent<Field_Card>().Effect);
        for (int i = 0; i < board.GetLength(0); i++)
        {
            if (x==0)
            {
                indexRowEnemy = 5;
                for (int m = 1; m < board.GetLength(0); m++)
                {
                    board[x,m].GetComponent<Unit_Card>().Attack =  board[x,m].GetComponent<Unit_Card>().Attack - (removeAttack / 100) * board[x,m].GetComponent<Unit_Card>().Attack; 
                    board[indexRowEnemy, m].GetComponent<Unit_Card>().Attack= board[indexRowEnemy,m].GetComponent<Unit_Card>().Attack - (removeAttack / 100) * board[indexRowEnemy,m].GetComponent<Unit_Card>().Attack; 
                }
            }
            else if (x==1)
            {
                indexRowEnemy = 4;
                for (int m = 1; m < board.GetLength(0); m++)
                {
                    board[x,m].GetComponent<Unit_Card>().Attack =  board[x,m].GetComponent<Unit_Card>().Attack - (removeAttack / 100) * board[x,m].GetComponent<Unit_Card>().Attack; 
                    board[indexRowEnemy, m].GetComponent<Unit_Card>().Attack= board[indexRowEnemy,m].GetComponent<Unit_Card>().Attack - (removeAttack / 100) * board[indexRowEnemy,m].GetComponent<Unit_Card>().Attack; 
                }
            }
            else if (x==2)
            {
                indexRowEnemy = 3;
                for (int m = 1; m < board.GetLength(0); m++)
                {
                    board[x,m].GetComponent<Unit_Card>().Attack =  board[x,m].GetComponent<Unit_Card>().Attack - (removeAttack / 100) * board[x,m].GetComponent<Unit_Card>().Attack; 
                    board[indexRowEnemy, m].GetComponent<Unit_Card>().Attack= board[indexRowEnemy,m].GetComponent<Unit_Card>().Attack - (removeAttack / 100) * board[indexRowEnemy,m].GetComponent<Unit_Card>().Attack; 
                }
            }
            else if (x==3)
            {
                indexRowEnemy = 2;
                for (int m = 1; m < board.GetLength(0); m++)
                {
                    board[x,m].GetComponent<Unit_Card>().Attack =  board[x,m].GetComponent<Unit_Card>().Attack - (removeAttack / 100) * board[x,m].GetComponent<Unit_Card>().Attack; 
                    board[indexRowEnemy, m].GetComponent<Unit_Card>().Attack= board[indexRowEnemy,m].GetComponent<Unit_Card>().Attack - (removeAttack / 100) * board[indexRowEnemy,m].GetComponent<Unit_Card>().Attack; 
                }
            }
            else if (x==4)
            {
                indexRowEnemy = 1;
                for (int m = 1; m < board.GetLength(0); m++)
                {
                    board[x,m].GetComponent<Unit_Card>().Attack =  board[x,m].GetComponent<Unit_Card>().Attack - (removeAttack / 100) * board[x,m].GetComponent<Unit_Card>().Attack; 
                    board[indexRowEnemy, m].GetComponent<Unit_Card>().Attack= board[indexRowEnemy,m].GetComponent<Unit_Card>().Attack - (removeAttack / 100) * board[indexRowEnemy,m].GetComponent<Unit_Card>().Attack; 
                }
            }
            else if (x==5)
            {
                indexRowEnemy = 0;
                for (int m = 1; m < board.GetLength(0); m++)
                {
                    board[x,m].GetComponent<Unit_Card>().Attack =  board[x,m].GetComponent<Unit_Card>().Attack - (removeAttack / 100) * board[x,m].GetComponent<Unit_Card>().Attack; 
                    board[indexRowEnemy, m].GetComponent<Unit_Card>().Attack= board[indexRowEnemy,m].GetComponent<Unit_Card>().Attack - (removeAttack / 100) * board[indexRowEnemy,m].GetComponent<Unit_Card>().Attack; 
                }
            }
            else
            {
                Debug.Log("error en coordenadas");
            }
            
        }
       
        
    }
    public void EffectsCounterField( GameObject cardcounterfield, GameObject cardtarget)
    {
        cardtarget.transform.SetParent(cementery.transform,false);
        cardcounterfield.transform.SetParent(cementery.transform,false);
    }
    public void EffectsBuff( GameObject[,] board, GameObject cardBuff, int x)
    {
        removeAttack = SearchNumber(cardBuff.GetComponent<Buff_Card>().effect);
        for (int i = 1; i < board.GetLength(0); i++)
        {
            board[x,i].GetComponent<Unit_Card>().Attack = board[x,i].GetComponent<Unit_Card>().Attack + (removeAttack/ 100) * board[x,i].GetComponent<Unit_Card>().Attack;
        }
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
