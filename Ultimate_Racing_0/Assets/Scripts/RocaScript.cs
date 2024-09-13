using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocaScript : MonoBehaviour
{
    public Car_Movement Player;
    private bool hasActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Toyota").GetComponent<Car_Movement>();
        Destroy(gameObject, 7f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto que colisionó es el jugador y si aún no se ha activado.
        if (collision.gameObject.CompareTag("Player") && !hasActivated && !Player.motordisable)
        {
            hasActivated = true;
            Player.motordisable = true;
            Player._motorForce = 0f;
            
        }
    }
   
}
