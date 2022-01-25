using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool hasWon;

    public float moveSpeed = 7;
    public float smoothMoveTime = 0.1f;
    public float turnSpeed = 8;

    float angle;
    float smoothInputMagnitude;
    float smoothInputVelocity;
    Vector3 velocity;
    
    Rigidbody rigidbody = new Rigidbody();

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        hasWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < 0) {
            GuardController.hasBeenCaught = true;
        }

        Vector3 inputDirection = Vector3.forward;
        if(! GameUI.isGameOver) {
            inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;   
        }
        float inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothInputVelocity, smoothMoveTime);


        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);

        if(GameUI.isGameOver) {
            moveSpeed = 0;
        }

        velocity = transform.forward * moveSpeed * smoothInputMagnitude;
        
        
    }

    void OnTriggerEnter(Collider hitCollider) {
        if(hitCollider.tag == "Finish") {
            hasWon = true;
        }
    }

    void FixedUpdate() {
        rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);
    }
}
