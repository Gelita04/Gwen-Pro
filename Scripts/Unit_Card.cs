using System;
using System.Collections;
using System.Collections.Generic;
using GameLibrary.Objects;
using Unity.VisualScripting;
using UnityEngine;

public class Unit_Card : MonoBehaviour
{
    public string Name;
    public long Attack;
    public string Effect;
    public UnitMember Category;
    public string Type;
    public GameObject cementery;

     // efecto que pone una carta aumento en la fila
     
     // efecto que pone una carta clima en la fila
     
     // efecto que elimina la carta con mas poder del campo (propio o del adversario)
     public void MaxPower( GameObject[,] matrix)
     {
         long temp= Int32.MinValue;
         GameObject cardTarget = new GameObject();
         for (int i = 0; i < matrix.GetLength(0); i++)
         {
             for (int j = 0; j < matrix.GetLength(1); j++)
             {
                 if ( matrix[i,j]!= null && matrix[i,j].GetComponent<Unit_Card>().Attack > temp)
                 {
                     cardTarget = matrix[i, j];
                 }
             }
         }
         cardTarget.transform.SetParent(cementery.transform, false);
     }
     
     //  efecto que elimina la carta con menos poder del campo (solo del rival)
     public void MinPower(GameObject[,] matrix)
     {
         long temp= Int32.MaxValue;
         GameObject cardTarget = new GameObject();
         for (int i = 0; i < matrix.GetLength(0); i++)
         {
             for (int j = 0; j < matrix.GetLength(1); j++)
             {
                 if ( matrix[i,j]!= null && matrix[i,j].GetComponent<Unit_Card>().Attack < temp)
                 {
                     cardTarget = matrix[i, j];
                 }
             }
         }
         cardTarget.transform.SetParent(cementery.transform, false);
     }
    
     // efecto que te permite robar una carta extra
     
     
     // efecto que multiplica su ataque por la cantidad de cartas que hay puestas en el campo
     public void PowerPlusCards( GameObject[,] matrix, GameObject attackingCard)
     { 
         long quantityCards = 0;
         for (int i = 0; i < matrix.GetLength(0); i++)
         {
             for (int j = 0; j < matrix.GetLength(1); j++)
             {
                 if (matrix[i,j]!= null)
                 {
                     quantityCards++;
                 }
             }
         }
         attackingCard.GetComponent<Unit_Card>().Attack = attackingCard.GetComponent<Unit_Card>().Attack * quantityCards;
     }
     
     // effecto que limpia la fila con menos cartas unidad (no vacia, ppropia o del rival)
     
     
     // efecto que calcula el promedio de poder de todas las cartas puestas en el campo, luego iguala todas las cartas del campo a ese mismo promedio (propia o del rival)
     public void CardsSamePower(GameObject[,] matrix)
     {
         long quantityCards = 0;
         long attackCards = 0;
         for (int i = 0; i < matrix.GetLength(0); i++)
         {
             for (int j = 0; j < matrix.GetLength(1); j++)
             {
                 if ( matrix[i,j]!= null)
                 {
                     quantityCards++;
                     attackCards += matrix[i, j].GetComponent<Unit_Card>().Attack;
                 }
             }
         }

         long promedy = attackCards / quantityCards;
         for (int i = 0; i < matrix.GetLength(0); i++)
         {
             for (int j = 0; j < matrix.GetLength(1); j++)
             {
                 if (matrix[i,j]!= null)
                 {
                     matrix[i, j].GetComponent<Unit_Card>().Attack = promedy;
                 }
             }   
         }
         
     }
    public void Start()
    {
       
    }
}
