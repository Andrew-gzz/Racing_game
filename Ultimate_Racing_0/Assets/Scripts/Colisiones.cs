using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Colisiones : MonoBehaviour
{
    
    public bool Timer = false;
   

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("I'm the trigger 'Salida'");

        if(other.tag == "Player" && !Timer) 
        {
            Debug.Log("Timer start");
            Timer = true;            
        }

    }
 
}
