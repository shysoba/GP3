using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private Rigidbody rigidBody;
    private MeshRenderer meshRenderer;
    PlayerScript playerScript;
    AudioSource missleAudio;

    private Transform grabLocation;

    [SerializeField] private Transform objectLookPointTransform;
    [SerializeField] private GameObject objectlookPoint;
    [SerializeField] private bool fired;
    [SerializeField] private float projectileSpeed;

    public GameObject dead;

    private Collision collisionObject;

    [SerializeField] private Transform enemyPosition;




    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();


        if (objectLookPointTransform == null)
        {
            objectlookPoint = GameObject.FindGameObjectWithTag("Look");

            if (objectlookPoint != null)
            {
                objectLookPointTransform = objectlookPoint.transform;
            }
        }

        fired = false;

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

    }

    public void Grab(Transform grabLocation)
    {
        this.grabLocation = grabLocation;
        rigidBody.useGravity = false;
    }

    public void Drop()
    {
        this.grabLocation = null;
        rigidBody.useGravity = true;
    }

    public void Fired()
    {

        this.grabLocation = null;
        rigidBody.velocity = -transform.right * projectileSpeed;
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        fired = true;
    }

    private void FixedUpdate()
    {
        if (grabLocation != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, grabLocation.position, Time.deltaTime * 10f);
            rigidBody.MovePosition(newPosition);

            Quaternion rotationFix = objectLookPointTransform.rotation * Quaternion.Euler(new Vector3(0, 90, 0));

            transform.rotation = rotationFix;
        }


    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") && fired)
        {
            collisionObject = collision;
            StartCoroutine(EnemyDeath());
            meshRenderer.enabled = false;
            dead.SetActive(true);

        }


    }

    public IEnumerator EnemyDeath()
    {
        if (collisionObject.transform.position != null)
        {
            yield return new WaitForSeconds(0);
            Instantiate(dead, collisionObject.transform.position, Quaternion.identity);
            Destroy(collisionObject.gameObject);
            Destroy(gameObject);

        }

    }


}
