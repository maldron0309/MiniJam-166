using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    //To change between movement types
    public bool dragg = false;

    [SerializeField] private float speed = 5;
    private Vector2 movement;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

    
}
