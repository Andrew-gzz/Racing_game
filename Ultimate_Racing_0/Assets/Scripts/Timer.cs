using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using TMPro;


public class Timer : MonoBehaviour
{
    public float timer = 0;   
    public TextMeshProUGUI texto;
    public Colisiones colison;
    public MetaScript meta;
    public GameObject Object;
    // Update is called once per frame
    void Update()
    {

         if (colison.Timer==true){
         
            Debug.Log("Funcionó");
            Object.SetActive(true);

            if (!meta.TimerStop) {  
                timer += Time.deltaTime;
                texto.text = "" + timer.ToString("f1"); 
            }
            
         }       
    }
}
