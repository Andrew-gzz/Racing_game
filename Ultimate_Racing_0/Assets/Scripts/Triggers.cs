using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Triggers;
using TMPro;


public class Triggers : MonoBehaviour
{
    public bool PlayerNear = false;

    public bool PlayerFar = false;

    public bool TimerStop = false;

    public bool Timer = false;
    public enum TriggerType { Type1, Type2, Meta, Salida }
    public TriggerType triggerType;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("I'm the trigger 'Triggers'");
        
        if (other.tag == "Player" && !PlayerNear) {

            Debug.Log("Jugador cerca");
            switch (triggerType)
            {
                case TriggerType.Type1:               
                    HandleType1();
                    break;
                case TriggerType.Type2: 
                    HandleType2();
                    break;
                case TriggerType.Meta:
                    Meta();
                    break;
                case TriggerType.Salida:
                    Salida();
                    break;
            }
             
        }
    }
    //Activa spawner
    private void HandleType1()
    {
        
        Debug.Log("SpawnerActivado");
        PlayerNear = true;
    }
    //Desactiva spawner
    private void HandleType2()
    {    
        Debug.Log("SpawnerDesactivado");
        PlayerFar = true;
    }

    private void Meta()
    {
        Debug.Log("Timer Stop");
        TimerStop = true;
    }
    private void Salida()
    {       
        Timer = true;
    }

}
