using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using TMPro;


public class Timer : MonoBehaviour
{
    public float timer = 0;   
    public TextMeshProUGUI texto;
    public Triggers Start, Stop;
    public GameObject Object;
    // Update is called once per frame
    void Update()
    {

         if (Start.Timer==true){
         
            Debug.Log("Funcionó");
            Object.SetActive(true);

            if (!Stop.TimerStop) {
                Debug.Log("Timer start");
                timer += Time.deltaTime;
                texto.text = "" + timer.ToString("f1"); 
            }
            
         }       
    }
}
