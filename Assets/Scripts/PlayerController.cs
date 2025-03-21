

using System.Collections;
using System.Globalization;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5f;
    public LayerMask collisionLayer; // Assign the collision layer in the Inspector

    private Rigidbody2D rb;
    private float shootCdLive;
    public float shootCd;
    private Collider2D playerCollider;

    public GameObject bullet;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();

        if (rb == null || playerCollider == null)
        {
            Debug.LogError("Rigidbody2D or Collider2D missing!");
        }
    }

    void FixedUpdate()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; // Normalize directly
        Vector2 movement = moveDirection * playerSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(worldMousePos.x - transform.position.x, worldMousePos.y - transform.position.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);


        shootCdLive += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && (shootCdLive >= shootCd)) // 0 represents the left mouse button
        {
            StartCoroutine(Shoot(2, 2, 0.3f));
            shootCdLive = 0;
        }
        
    }

    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collisionLayer.value & (1 << collision.gameObject.layer)) != 0) //Check if collided with layer
        {
            Debug.Log("Collision with: " + collision.gameObject.name);
        }
    }

    void newLevel(Vector3 startPosition)
    {
        transform.position = startPosition;
        rb.position = startPosition; // Important to reset the rigid body position as well
    }


    public IEnumerator Shoot(int numBullets, int numShots, float timeDelay)
    {
        for (int i = 0; i < numShots; i++)
        {
            for (int k = 0; k < numBullets; k++)
            {
                GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation); // Use the spawner's rotation as a base

                float angleOffset = -((numBullets - 1) * 6f) + (12f * k); // Calculate the angle offset
                newBullet.transform.Rotate(0f, 0f, angleOffset); // Apply the angle offset

                // Optional: Add bullet movement
                
                
            }

            yield return new WaitForSeconds(timeDelay);

        }
    }

    
}