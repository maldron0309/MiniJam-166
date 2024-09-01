using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private float speed = 2f;
    private int life = 3;
    [SerializeField] private Transform lifebarFilling;
    private Vector2 direction;
    [SerializeField] private int dropPercentage = 40;
     [SerializeField] private List<GameObject> powerups;
    private Rigidbody2D rb;
    private int maxHealth;
    private ScoreManagerScript scoreManager;

    private AudioSource source;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip deathClip;

    private ParticleSystem particleSystem;

    void Start(){
        maxHealth = life;
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        particleSystem = this.GetComponent<ParticleSystem>();
    }

    public void Initialize(Vector2 dir, float time){
        direction = dir;
        speed = Random.Range(0.8f + (time /10), 3 + (time /10));
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManagerScript>();
    }

    void Update()
    {
        rb.linearVelocity = speed * direction;
    }

    public void GetDamage(int damage){
        life -= damage;
        float healthPercent = (float)life / maxHealth;
        lifebarFilling.localScale = new Vector2(healthPercent, lifebarFilling.localScale.y);
        StartCoroutine(Squish());
        CameraScript.Instance.TriggerShake(0.1f, 0.1f);

        bool drop = true;
        if(damage == 1000000){
            drop = false;
        }
        if(drop && life > 0){
            source.PlayOneShot(hitClip);
        }
        if(life <= 0){
            Die(drop);
        }
    }

    private void Die(bool drop){
        int isDrop = Random.Range(0, 100);
        if (isDrop <= dropPercentage && drop){
            int randomObjectIndex = Random.Range(0, powerups.Count);
            GameObject objectToDrop = powerups[randomObjectIndex];
            Instantiate(objectToDrop, this.transform.position, objectToDrop.transform.rotation);
            scoreManager.EnemyKilled();
        }
        if(drop){
        source.PlayOneShot(deathClip);
        }
        StartCoroutine(Destroying());
        speed = 0;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GameObject livebar = gameObject.transform.GetChild(0).gameObject;
        Destroy(livebar);
    }

    IEnumerator Destroying(){
        particleSystem.Play();
        yield return new WaitForSeconds(0.3f);
        particleSystem.Stop();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private IEnumerator Squish(){
            transform.localScale = new Vector3(transform.localScale.x / 1.2f, transform.localScale.y / 1.2f, transform.localScale.z);
            yield return new WaitForSeconds(0.2f);
            transform.localScale = new Vector3(transform.localScale.x * 1.2f, transform.localScale.y * 1.2f, transform.localScale.z);
            yield return new WaitForSeconds(0.2f);
    }

    public void IncreaseSpeed(float s) {
        this.speed *= s;
    }

}
