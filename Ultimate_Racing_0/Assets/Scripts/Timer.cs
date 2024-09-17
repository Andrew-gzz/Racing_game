using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using TMPro;


public class Timer : MonoBehaviour
{
    public float timer;   
    public TextMeshProUGUI texto;
    public Triggers Start, Stop;
    public GameObject Object;
    public GameObject ThirtStar;
    public GameObject SecondStar;
    public GameObject FirstStar;
    // Update is called once per frame
    void Update()
    {

         if (Start.Timer==true){
         
            Debug.Log("Funcionó");
            Object.SetActive(true);

            if (!Stop.TimerStop && timer>0) {
                Debug.Log("Timer start");
                if (timer < 170) {
                    ThirtStar.SetActive(false);
                }
                if (timer < 150)
                {
                    SecondStar.SetActive(false);
                }
                if (timer < 110)
                {
                    FirstStar.SetActive(false);
                }

                timer -= Time.deltaTime;
                if (timer < 0)                
                    timer = 0;                
                int minutes = Mathf.FloorToInt(timer / 60);
                int seconds = Mathf.FloorToInt(timer % 60);

                texto.text = string.Format("{0:00}:{1:00}", minutes, seconds); 
                
            }
            
         }       
    }
}
