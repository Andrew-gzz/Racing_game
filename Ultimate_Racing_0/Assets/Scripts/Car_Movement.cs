using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car_Movement : MonoBehaviour
{
    // Start is called before the first frame update

    public WheelCollider front_driverCol, front_passCol;

    public WheelCollider back_driverCol, back_passCol;

    public Transform forntDRiver, forntPass;

    public Transform backDRiver, backPass;

    public float _steerAngle = 25.0f;

    public float _motorForce = 1500f;

    public float steerAngl;

    public bool Angl = false;
    public bool motordisable = false;

    private Rigidbody rb;

    private Vector3 startPosition;
    private Quaternion startRotation;

    // Tiempo que ha estado atascado
    private float stuckTime = 0f;
    private float maxStuckTime = 3f; // Si el coche está atascado por más de 3 segundos
    
    float h, v;  
    
    void Start()
    {
        // Obtén el Rigidbody del carro
        rb = GetComponent<Rigidbody>();

        // Ajusta el centro de masa para mejorar la estabilidad
        rb.centerOfMass = new Vector3(0, -0.5f, 0);

        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Inputs();
        Drive();
        SteerCar();
        UpdateWheelPos(front_driverCol,  forntDRiver);
        UpdateWheelPos(front_passCol, forntPass);
        UpdateWheelPos(back_driverCol, backDRiver);
        UpdateWheelPos(back_passCol, backPass);
        CheckIfFlippedOrStuck();
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
    void Inputs()
    {
        h = Input.GetAxis("Horizontal");

        v = Input.GetAxis("Vertical");


    }

    void Drive()
    {
        back_driverCol.motorTorque = v * _motorForce;
        back_passCol.motorTorque = v * _motorForce;

    }
    void SteerCar()
    {
        if (!Angl) { 
        steerAngl = _steerAngle * h;
        front_driverCol.steerAngle = steerAngl;
        front_passCol.steerAngle = steerAngl;
        }
    }

    void UpdateWheelPos(WheelCollider col, Transform t)
    {
        Vector3 pos = t.position;
        Quaternion rot = t.rotation;

        col.GetWorldPose(out pos, out rot);
        t.position = pos;
        t.rotation = rot;

    }
    private void CheckIfFlippedOrStuck()
    {
        // Verificar si el coche está volcado (si el coche está inclinado más de 60 grados)
        if (Vector3.Dot(transform.up, Vector3.up) < 0.5f)
        {
            Debug.Log("El coche está volcado, reseteando...");
            ResetCarPosition();
        }

        // Verificar si el coche está atascado (velocidad cercana a 0 durante un tiempo)
        if (rb.velocity.magnitude < 0.1f)
        {
            stuckTime += Time.deltaTime;

            if (stuckTime >= maxStuckTime)
            {
                Debug.Log("El coche está atascado, reseteando...");
                ResetCarPosition();
                stuckTime = 0f; // Reiniciar el temporizador de atascado
            }
        }
        else
        {
            stuckTime = 0f; // Reiniciar si el coche se está moviendo
        }
    }
    private void ResetCarPosition()
    {
        // Reubicar el coche a la posición y rotación inicial
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Detener cualquier movimiento residual
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto que colisionó es el jugador.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (motordisable)
            {
                StartCoroutine(HandleMotorDisable());
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            // Guardar la posición y rotación actuales del coche
            startPosition = transform.position;
            startRotation = transform.rotation;

            Debug.Log("Nueva posición y rotación guardada en el SpawnPoint");
        }
    }
    private IEnumerator HandleMotorDisable()
    {
        _motorForce = 0f; // Desactiva el motor
        Angl = true;
        steerAngl = 20;
        yield return new WaitForSeconds(5f); // Espera 5 segundos
        _motorForce = 1500f; // Restaura el valor del motor
        steerAngl = 0;
        Angl = false;
        motordisable = false; // Rehabilita la funcionalidad del motor
    }
    public void RestartGame()
    {
        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
