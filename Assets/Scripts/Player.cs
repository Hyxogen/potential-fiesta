using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private Vector2 rotation;
    private Vector3 velocity;
    private Rigidbody item;
    private Transform target;

    private int buttonPresses = 0;

    public float sensitivity = 3.0f;
    public float speed = 5.0f;
    public float jump = 5.0f;
    public float strength = 20.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        characterController = GetComponent<CharacterController>();
        target = Camera.main.transform.Find("Target");
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

        while (buttonPresses > 0)
        {
            if (item)
            {
                item.velocity = Vector3.zero;
                item.excludeLayers = LayerMask.GetMask();
                item = null;
            }
            else
            {
                Transform cameraTransform = Camera.main.transform;
                LayerMask mask = LayerMask.GetMask("Object");
                RaycastHit info;

                if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out info, 1.5f, mask))
                {
                    item = info.rigidbody;
                    item.excludeLayers = LayerMask.GetMask("Player");
                }
            }

            buttonPresses -= 1;
        }

        if (item)
        {
            item.velocity = (target.position - item.position) * strength;
        }
    }

    void Update()
    {
        rotation.x -= Input.GetAxis("Mouse Y") * sensitivity;
        rotation.y += Input.GetAxis("Mouse X") * sensitivity;
        rotation.x = Mathf.Clamp(rotation.x, -90.0f, 90.0f);

        Camera.main.transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, 0.0f);

        if (Input.GetButtonDown("Fire1"))
        {
            buttonPresses += 1;
        }
    }
}
