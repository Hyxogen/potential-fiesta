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

    void OnTriggerEnter(Collider collision) {
        wall.SetActive(false);
    }

    void OnTriggerExit(Collider collision) {
        wall.SetActive(true);
    }
}