using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    public Transform table;
    public Transform [] Balls;

    private float kof=1.4f;//коефіціент відстані між кульками

    // Start is called before the first frame update
    void Start()
    {
      
        Transform first = Balls[0];//позиція першої кульки
       // Balls[0].position = new Vector3(Balls[0].position.x, Balls[0].position.y, Balls[0].position.z - Balls[0].localScale.z );
        
        int RowLength = 1;//довжина ряду/номер
        
        for(int i=1;i< Balls.Length;i+=RowLength)
        {
             RowLength++;
             for (int k=0; k < RowLength; k++)
             {
                Balls[i+k].position = new Vector3(first.position.x+Balls[i+k].localScale.x * (RowLength-1) * kof , first.position.y, first.position.z + Balls[i+k].localScale.z * kof *k - Balls[i + k].localScale.z * RowLength/2);
               
             }
           
          
        }
    }

     
}
