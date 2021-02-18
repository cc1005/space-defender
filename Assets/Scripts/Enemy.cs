using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Lasers")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float enemyProjectileSpeed = 5f;

    [Header("Death")]
    [SerializeField] float health = 100;
    [SerializeField] GameObject explosion;
    [SerializeField] float durationOfExplosion = 1f;

    [Header("Sound")]
    [SerializeField] AudioClip laserFireSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float fireSoundVolume = 0.5f;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.5f;

    // Use this for initialization
    private void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            AudioSource.PlayClipAtPoint(laserFireSound, Camera.main.transform.position, fireSoundVolume);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
                 enemyLaserPrefab,
                 transform.position,
                 Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyProjectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
        damageDealer.Hit();
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject explosionInstance = Instantiate(
                         explosion,
                         transform.position,
                         transform.rotation);
        Destroy(explosionInstance, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        Destroy(gameObject);
    }
}
