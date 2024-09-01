using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{

    //To change between movement types
    public bool dragg = false;
    public float spawnDelay = 1.0f;

    [SerializeField] private float speed = 5;
    private float originalSpeed;
    [SerializeField] private PlayerInput input;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Transform spawnPoint;
    private Vector2 movement;
    private Rigidbody2D rb;
    private bool shieldUp = false;

    private AudioSource source;
    //[SerializeField] private AudioClip moveClip;
    [SerializeField] private AudioClip crashClip;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        sprite = GetComponent<SpriteRenderer>();
        originalSpeed = speed;
        source = GetComponent<AudioSource>();
    }

    private void Update(){
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    private void FixedUpdate()
    {
        if(!dragg){
            //Movement type 1: No dragg, the player start at 100% of the speed and stops right when the button is released
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
        else{ 
            //Movement type 2: Dragg, the player slows down when no input is given
            if(movement.x != 0 || movement.y != 0){
                rb.linearVelocity = movement * speed;
             }
        }
    }

    private void OnMove (InputValue value) {
        movement = value.Get<Vector2>();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){
            if(!shieldUp){
                CameraScript.Instance.TriggerShake(0.1f, 0.5f);
                StartCoroutine(Die());
                source.PlayOneShot(crashClip);
                other.GetComponent<Enemy>().GetDamage(100);
            }
            else{
                other.GetComponent<Enemy>().GetDamage(100);
            }
        }
    }

    IEnumerator Die(){
        sprite.enabled = false;
        input.enabled = false;
        yield return new WaitForSeconds(spawnDelay);
        transform.position = spawnPoint.position;
        sprite.enabled = true;
        input.enabled = true;
    }

    public void ToggleShield(){
        shieldUp =! shieldUp;
        if(shieldUp){
            sprite.color = Color.blue;
        }
        else{
            sprite.color = Color.white;
        }
    }

    public void SetSpeed(float s){
        speed = s;
        if (s < 0){
            speed = originalSpeed;
        }
    }
    
}
