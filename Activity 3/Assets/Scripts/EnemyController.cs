using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // components
    private Rigidbody rigidBody;
    private PlayerScript playerScript;

    // enemy settings
    [SerializeField] private bool kill = false;
    [SerializeField] private float speed;

    // target/player
    [SerializeField] private GameObject player;
    [SerializeField] private Transform target;

    void Awake()
    {
        // initialize 
        rigidBody = GetComponent<Rigidbody>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        // player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // move towards the player
        transform.position = Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);

        // look at the player
        transform.LookAt(player.transform.position);
    }

    // handle collision with the player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
            playerScript.isGameOver = true;
        }
    }
}
