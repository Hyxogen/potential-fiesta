using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject wall;

    void Start()
    { 
        wall.SetActive(true);
    }

    void OnCollisionEnter(Collision collision) {
        wall.SetActive(false);
    }
    void OnCollisionExit(Collision collision) {
        wall.SetActive(true);
    }
}