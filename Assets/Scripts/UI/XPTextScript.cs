using UnityEngine;

public class XPTextScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeInHierarchy){
            transform.position += new Vector3(0, Time.deltaTime * 30, 0);
        }
    }
}
