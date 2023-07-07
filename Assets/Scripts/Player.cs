using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private Vector2 rotation;
    private Vector3 velocity;

    public float sensitivity = 3.0f;
    public float speed = 5.0f;
    public float jump = 5.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal") * speed;
        float y = Input.GetAxis("Vertical") * speed;
        Quaternion rot = Quaternion.AngleAxis(rotation.y, Vector3.up);
        Vector3 movement = rot * new Vector3(x, 0.0f, y);

        velocity.x = movement.x;
        velocity.z = movement.z;
        velocity += Physics.gravity * Time.fixedDeltaTime;
        characterController.Move(velocity * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.Space) && characterController.isGrounded)
        {
            velocity.y = jump;
        }
    }

    void Update()
    {
        rotation.x -= Input.GetAxis("Mouse Y") * sensitivity;
        rotation.y += Input.GetAxis("Mouse X") * sensitivity;
        rotation.x = Mathf.Clamp(rotation.x, -90.0f, 90.0f);

        Camera.main.transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, 0.0f);
    }
}
