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
    public string keyword;
    public UnitMember Category;
    public string Type;
    private GameObject cementery;
    private GameObject rowToEliminate;
    private GameObject Deck;
    private GameObject matrixBoard;

     // efecto que pone una carta aumento en la fila, la carta aumento sale del deck.
     public void PutBuffCats() // al llama al metodo es importante verificar que dice la keyword, si es un buff se llama este metodo si es un clima se llama al otro. Y ademas ver de que tipo de carta es.
     {
         List<GameObject> deckCats = Deck.GetComponent<Deck_Cats>().Deck;
         GameObject buffCats = deckCats.Find(obj => obj.CompareTag("Buff"));
         GameObject[,] board = matrixBoard.GetComponent<MatrixBoard>().Board;
         for (int i = 0; i < board.GetLength(1); i++)
         {
             if (board[3,i] == null)
             {
                 board[3, i] = buffCats;
                 break;
             }
         }
     }
     public void PutBuffDogs()
     {
         List<GameObject> deckDogs = Deck.GetComponent<Deck_Dogs>().Deck;
         GameObject buffDogs = deckDogs.Find(obj => obj.CompareTag("Buff"));
         GameObject[,] board = matrixBoard.GetComponent<MatrixBoard>().Board;
         for (int i = 0; i < board.GetLength(1); i++)
         {
             if (board[0,1] == null)
             {
                 board[0, 1] = buffDogs;
                 break;
             }
         }
     }
     
     // efecto que pone una carta clima en la fila, la carta clima sale del deck.
     public void PutFieldCats() // misma situacion que los metodos de arriba. 
     {
         List<GameObject> deckCats = Deck.GetComponent<Deck_Cats>().Deck;
         GameObject fieldCats = deckCats.Find(obj => obj.CompareTag("Field"));
         GameObject[,] board = matrixBoard.GetComponent<MatrixBoard>().Board;
         for (int i = 0; i < board.GetLength(1); i++)
         {
             if (board[0,i] == null)
             {
                 board[0, i] = fieldCats;
                 break;
             }
         }
     } 
     public void PutFieldDogs()
     {
         List<GameObject> deckDogs = Deck.GetComponent<Deck_Cats>().Deck;
         GameObject fieldDogs = deckDogs.Find(obj => obj.CompareTag("Field"));
         GameObject[,] board = matrixBoard.GetComponent<MatrixBoard>().Board;
         for (int i = 0; i < board.GetLength(1); i++)
         {
             if (board[0,i] == null)
             {
                 board[0, i] = fieldDogs;
                 break;
             }
         }
     }
     
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
     
     // efecto que limpia la fila con menos cartas unidad (no vacia, propia o del rival)
     public void CleanRow( GameObject[,] matrix)
     {
         int countRow0 = 0;
         int countRow1 = 0;
         int countRow2 = 0;
         int countRow3 = 0;
         int countRow4 = 0;
         int countRow5 = 0;
         int[] rows = new int[6];
         for (int i = 0; i < matrix.GetLength(1); i++)
         {
             if ( matrix[0,i]!= null && matrix[0,i].GetComponent<Unit_Card>() )
             {
                 countRow0++;
             }
             if (matrix[1,i]!= null && matrix[1,i].GetComponent<Unit_Card>())
             {
                 countRow1++;
             }
             if (matrix[2,i]!= null && matrix[2,i].GetComponent<Unit_Card>())
             {
                 countRow2++;
             }
             if (matrix[3,i]!= null && matrix[3,i].GetComponent<Unit_Card>())
             {
                 countRow3++;
             }
             if (matrix[4,i]!= null && matrix[4,i].GetComponent<Unit_Card>())
             {
                 countRow4++;
             }
             if (matrix[5,i]!= null && matrix[5,i].GetComponent<Unit_Card>())
             {
                 countRow5++;
             }
         }
         rows[0] = countRow0;
         rows[1] = countRow1;
         rows[2] = countRow2;
         rows[3] = countRow3;
         rows[4] = countRow4;
         rows[5] = countRow5;
         int a= Int32.MaxValue;
         int coordenateRowToEliminate = -1;
         for (int i = 0; i < rows.Length; i++)
         {
             if (rows[i]< a)
             {
                 coordenateRowToEliminate = i;
             }
         } 
         GameObject[] x = rowToEliminate.GetComponent<EffectsScript>().Rowselected(matrix, coordenateRowToEliminate);
         for (int i = 0; i < x.Length; i++)
         {
             if (x[i] != null && x[i].GetComponent<Unit_Card>())
             {
                 x[i].transform.SetParent(cementery.transform,false);
             }
         }

     }
     
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
         // ReSharper disable once IntDivisionByZero
         if ( quantityCards > 0)
         {
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
     }
    public void Start()
    {
       
    }
}
