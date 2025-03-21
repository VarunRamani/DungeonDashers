using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int enemySpeed = 5;
    public float detectionRadius = 10f;
    public LayerMask detectionLayer;

    private GameObject player;
    private Rigidbody2D rb;
    private float currentHealth;

    private GameObject gameController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = gameController.GetComponent<GameController>().enemyBasicHealth;
    }

    void FixedUpdate()
    {
        bool playerInRange = Physics2D.OverlapCircle(transform.position, detectionRadius, detectionLayer);

        Debug.Log(playerInRange);

        if (playerInRange)
        {
            Vector2 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Convert angle back to 2D direction vector
            Vector2 moveDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            Vector2 movement = moveDirection * enemySpeed * Time.deltaTime;

            rb.MovePosition(rb.position + movement);
        }
        else
        {
            // Player is out of range, stop movement
            rb.velocity = Vector2.zero; // Stop the enemy from moving.
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("PlayerBullet")) //Check if collided with layer
        {
            currentHealth -= 1;
            if (currentHealth <= 0) {

                Destroy(this.gameObject);

            }
            Destroy(collision.gameObject);
        }

    }
}