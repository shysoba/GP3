using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceShooter : MonoBehaviour
{
    public SpaceshipController SpaceShip;

    [SerializeField] public int health;
    public float minFR, MaxFR;
    private float FireRate;
    private float storedFireRate;
    public float BulletSpeed;
    public GameObject BulletPrefab;

    public float moveSpeed;
    public float moveInterval;

    public Vector3 InitialPosition;
    void Start()
    {
        InitialPosition = transform.position;

        FireRate = Random.Range(minFR, MaxFR);
        storedFireRate = FireRate;

        InvokeRepeating("MoveEnemy",5, moveInterval);
        
    }


    void Update()
    {
        FireRate -= Time.deltaTime;
        if (FireRate <= 0)
        {
            SpawnBullet();
            FireRate = storedFireRate;
        }
        if (health <= 0)
        {
            SpaceShip.score++;
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }

    }

    public void HealthReset()
    {

        health = 5;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            health--;
            Destroy(collision.gameObject);
        }
    }

    public void SpawnBullet()
    {
        GameObject bullet = Instantiate(BulletPrefab,transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = new Vector2(0f, -BulletSpeed);
    }
    
    public void MoveEnemy()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
}
