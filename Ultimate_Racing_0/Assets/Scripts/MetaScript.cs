using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MetaScript : MonoBehaviour
{
    public Colisiones Salida;
    public bool TimerStop = false;    
    private void OnTriggerExit(Collider meta) 
    {
        
        Debug.Log("I'm the trigger 'Meta' ");
       if (meta.tag == "Player" && Salida.Timer)
        {
            Debug.Log("Timer stop");
            TimerStop = true;
        }

    }

}
