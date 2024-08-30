using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f; 

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -10f) 
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Earth"))
        {
            Debug.Log("Test");
        }
    }
}
