using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource Audio1;
    public AudioSource Audio2;
    public AudioSource Radio;
    public Car_Movement Car;
    private bool isRadioOn = false;
    private float Volumen = 0f,Volumen2 = 0f, volumeLerpSpeed = 2f;// Velocidad de interpolacion del volumen

    public float minSpeed = 0f;

    public float maxSpeed = 50f;

    private void Start()
    {
        if (Audio1 != null) {
            Audio1.volume = 0f;
        }
        if (Audio2 != null)
        {
            Audio2.volume = 0f;
        }
        if (Radio != null)
        {
            Radio.Stop(); // Asegúrate de que la radio esté apagada al inicio
        }
    }
    private void Update()
    {
        if (Car != null && Audio1 != null)
        {
            // Obtén la velocidad actual del coche desde el Rigidbody
            float carSpeed = Car.GetCurrentSpeed();

            // Normaliza la velocidad entre 0 y 1 para ajustar el volumen (usando minSpeed y maxSpeed)
            float normalizedSpeed = Mathf.InverseLerp(minSpeed, maxSpeed, carSpeed);

            // Interpola el volumen suavemente hacia la velocidad normalizada
            Volumen = Mathf.Lerp(Audio1.volume, normalizedSpeed, Time.deltaTime * volumeLerpSpeed);

            // Ajusta el volumen de Audio_Moving
            Audio1.volume = Volumen;
            // Invertir la normalización para Audio2 (frenado), cuanto menor sea la velocidad, mayor será el volumen
            float invertedSpeed = 1f - normalizedSpeed;
            Volumen2 = Mathf.Lerp(Audio2.volume, invertedSpeed, Time.deltaTime * volumeLerpSpeed);
            Audio2.volume = Volumen2;
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (Radio != null)
                {
                    if (isRadioOn)
                    {
                        Radio.Stop(); // Apaga la radio
                        isRadioOn = false;
                    }
                    else
                    {
                        Radio.Play(); // Enciende la radio
                        isRadioOn = true;
                    }
                }
            }
        }
    }

}
