using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    private float timeToLive = 5f;
    private float timeAlive = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToLive < timeAlive){
            Destroy(gameObject);
        }
        timeAlive += Time.deltaTime;
    }
}
