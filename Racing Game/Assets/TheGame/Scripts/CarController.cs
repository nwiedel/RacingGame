using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float moveVertical, moveHorizontal;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    float currentSpeed;

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
    }

    private void FixedUpdate()
    {
        if(moveVertical != 0)
        {
            currentSpeed = moveVertical * acceleration;

            Vector2 movement = transform.up * currentSpeed;

            myRb2D.velocity = movement;
        }
    }
}
