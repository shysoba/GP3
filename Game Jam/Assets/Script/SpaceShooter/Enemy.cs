using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed, amplitude;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }
    void Update()
    {
        float verticalMovement = Mathf.Sin(speed * Time.time) * amplitude;
        transform.position = new Vector3(initialPosition.x,initialPosition.y + verticalMovement,initialPosition.z);

    }
}
