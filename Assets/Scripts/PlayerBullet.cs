using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float bulletSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        transform.position += transform.right * bulletSpeed * Time.deltaTime;
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "top layer") //Check if collided with layer
        {
            Destroy(this.gameObject);
        }
    }
}
