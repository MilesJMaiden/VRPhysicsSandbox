using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class ContinuousMovementPhysics : MonoBehaviour {

    public float speed = 1f;
    public float turnSpeed = 60f;
    private float jumpVelocity = 7;
    public float jumpHeight = 1.5f;
    public float snapTurnAngle = 45f;

    public bool onlyMoveWhenGrounded = false;
    public bool jumpWithHand = true;

    public float minJumpWithHandSpeed = 2f;
    public float maxJumpWithHandSpeed = 7f;


    public InputActionProperty moveInputSource;
    public InputActionProperty turnInputSource;
    public InputActionProperty jumpInputSource;

    public Rigidbody rb;
    public Rigidbody rightHandRB;
    public Rigidbody leftHandRB;
    public LayerMask groundLayer;
    public Transform directionSource;
    public Transform turnSource;

    public CapsuleCollider bodyCollider;
    private bool isGrounded;

    private Vector2 inputMoveAxis;
    private float inputTurnAxis;

    // Update is called once per frame
    void Update()
    {
        inputMoveAxis = moveInputSource.action.ReadValue<Vector2>();
        inputTurnAxis = turnInputSource.action.ReadValue<Vector2>().x;

        bool jumpInput = jumpInputSource.action.WasPressedThisFrame();

        if (!jumpWithHand) {

                if(jumpInput && isGrounded)
            {
                jumpVelocity = Mathf.Sqrt(2 * -Physics.gravity.y * jumpHeight);
                rb.velocity = Vector3.up * jumpVelocity;
            }
        } else
        {
            bool inputJumpPressed = jumpInputSource.action.IsPressed();

            float handSpeed = ((leftHandRB.velocity - rb.velocity).magnitude + (rightHandRB.velocity - rb.velocity).magnitude) / 2;

            if (inputJumpPressed && isGrounded && handSpeed > minJumpWithHandSpeed)
            {
                rb.velocity = Vector3.up * Mathf.Clamp(handSpeed, minJumpWithHandSpeed, maxJumpWithHandSpeed);
            }
        }

    }

    void FixedUpdate()
    {
        isGrounded = CheckIfGrounded();

        if(isGrounded || (onlyMoveWhenGrounded && isGrounded))
        {
            Quaternion yaw = Quaternion.Euler(0f, directionSource.eulerAngles.y, 0f);
            Vector3 direction = yaw * new Vector3(inputMoveAxis.x, 0f, inputMoveAxis.y);

            Vector3 targetMovePosition = rb.position + direction * Time.fixedDeltaTime * speed;

            Vector3 axis = Vector3.up;
            float angle = turnSpeed* Time.fixedDeltaTime * inputTurnAxis;

            Quaternion q = Quaternion.AngleAxis(angle, axis);

            rb.MoveRotation(rb.rotation * q);

            Vector3 newPosition = q*(targetMovePosition - turnSource.position) + turnSource.position;

            rb.MovePosition(newPosition);

        }
    }

    public bool CheckIfGrounded()
    {
        Vector3 start = bodyCollider.transform.TransformPoint(bodyCollider.center);
        float rayLength = bodyCollider.height / 2 - bodyCollider.radius + 0.05f;

        bool hasHit = Physics.SphereCast(start, bodyCollider.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);

        return hasHit;
    }
}
