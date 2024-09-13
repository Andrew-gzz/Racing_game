using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool motordisable = false;

    private Rigidbody rb;

    float h, v;
    void Start()
    {
        // Obtén el Rigidbody del carro
        rb = GetComponent<Rigidbody>();

        // Ajusta el centro de masa para mejorar la estabilidad
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
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
        steerAngl = _steerAngle * h;
        front_driverCol.steerAngle = steerAngl;
        front_passCol.steerAngle = steerAngl;
    }

    void UpdateWheelPos(WheelCollider col, Transform t)
    {
        Vector3 pos = t.position;
        Quaternion rot = t.rotation;

        col.GetWorldPose(out pos, out rot);
        t.position = pos;
        t.rotation = rot;

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
    private IEnumerator HandleMotorDisable()
    {
        _motorForce = 0f; // Desactiva el motor
        yield return new WaitForSeconds(5f); // Espera 5 segundos
        _motorForce = 1500f; // Restaura el valor del motor
        motordisable = false; // Rehabilita la funcionalidad del motor
    }


}
