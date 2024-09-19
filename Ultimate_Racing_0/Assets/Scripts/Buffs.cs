using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Buffs : MonoBehaviour
{

    public enum Type { Default, Rampa, MoreTime, LittleMoreTime, LessTime  }
    public Type type;
    
    //Variables
    public GameObject Secreto1;
    public Timer Time;
    //Audio
    public AudioSource collision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            switch (type) { 
            
                case Type.Rampa:
                    Rampa();
                break;
                case Type.MoreTime:
                    MoreTime();
                break;
                case Type.LittleMoreTime:
                    LittleMoreTime();
                break;
                case Type.LessTime:
                    LessTime();
                break;
                
            }

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto que colisionó es el jugador y si aún no se ha activado.
       if (collision.gameObject.CompareTag("Player")) {
            switch (type)
            {
                case Type.Default:
                    Default_();
                    break;
            }
            

       }
        
    }
    private void Rampa() {
        Debug.Log("Secreto1 Activado");
        Secreto1.SetActive(true);
        Destroy(gameObject, 0f);
    }
    private void MoreTime()
    {
        Debug.Log("+5s");
        Time.timer += 5;
        Destroy(gameObject, 0f);
    }
    private void LittleMoreTime()
    {
        Debug.Log("+1s");
        Time.timer += 1;
        Destroy(gameObject, 0f);

    }
    private void LessTime()
    {
        Debug.Log("-1s");
        Time.timer -= 1;
        Destroy(gameObject, 0f);

    }
    private void Default_()
    {
        collision.Play();
        Destroy(gameObject, 3f);
    }
}
