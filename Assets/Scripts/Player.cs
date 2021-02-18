using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config Params
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float wallPadding;
    [SerializeField] int health = 200;
    [SerializeField] GameObject explosion;
    [SerializeField] float durationOfExplosion = 1f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    [Header("Sound")]
    [SerializeField] AudioClip playerFireSound;
    [SerializeField] AudioClip playerDeathSound;
    [SerializeField] AudioClip playerImpactSound;
    [SerializeField] [Range(0, 1)] float playerFireSoundVolume = 0.5f;
    [SerializeField] [Range(0, 1)] float playerDeathSoundVolume = 0.5f;
    [SerializeField] [Range(0, 1)] float playerImpactSoundVolume = 0.5f;

   Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }
    
    private IEnumerator FireContinuously()
    {
        while (true)
        { 
            FireOneLaser();
            AudioSource.PlayClipAtPoint(playerFireSound, Camera.main.transform.position, playerFireSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void FireOneLaser()
    {
        GameObject laser = Instantiate(
                laserPrefab,
                transform.position,
                Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
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
        AudioSource.PlayClipAtPoint(playerImpactSound, Camera.main.transform.position, playerImpactSoundVolume);
        PublishHealthRemaining();
        Die();
    }

    public int PublishHealthRemaining()
    {
        return health;
    }

    private void Die()
    {
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position, playerDeathSoundVolume);
            GameObject explosionInstance = Instantiate(
                         explosion,
                         transform.position,
                         transform.rotation);
            Destroy(explosionInstance, durationOfExplosion);
            Destroy(gameObject);
            FindObjectOfType<Level>().LoadGameOver();
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + wallPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - wallPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + wallPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - wallPadding;
    }
}
