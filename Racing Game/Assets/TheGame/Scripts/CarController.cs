using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float moveVertical, moveHorizontal;
    [SerializeField] private float acceleration;
    [SerializeField] float currentSpeed;
    [SerializeField] private bool isBreaking;
    [SerializeField] private float breakingForce;
    [SerializeField] private float rotationSpeed;

    private Rigidbody2D myRb2D;

    // Start is called before the first frame update
    private void Start()
    {
        myRb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        moveVertical = Input.GetAxis("Vertical");
        moveHorizontal = Input.GetAxis("Horizontal");
        isBreaking = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        if(moveVertical != 0)
        {
            // Aktuelle Geschwindigkeit berechnen
            currentSpeed = moveVertical * acceleration;
        }
        else if (!isBreaking)
        {
            // Es wird nicht beschleunigt und auch nicht gebremst
            // Auto ausrollen lassen bis auf "0" Stillstand
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, acceleration);
        }

        if (isBreaking)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, breakingForce);
        }

        // Errechnete Geschwindigkeit auf das Fahrzeug übertragen
        Vector2 movement = transform.up * currentSpeed;
        // Wert dem Rigidbody zuweisen
        myRb2D.velocity = movement;

        if (currentSpeed < -2)
        {
            // Fahrzeug fährt vorwärts
            myRb2D.angularVelocity = moveHorizontal * rotationSpeed;
        }
        else if(currentSpeed > 2)
        {
            // Fahrzeug fährt rückwärts
            // Movehorizontal * Rotationspeed
            myRb2D.angularVelocity = moveHorizontal * -rotationSpeed;
        }
        else 
        {
            // Fahrzeug steht
            myRb2D.angularVelocity = 0f;
            Debug.Log("Tempo = null");
        }
        
    }
}
