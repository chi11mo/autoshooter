using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 3f;
    public float attackCooldown = 2f;
    public int damage = 10;

    public int maxHits = 5; // Maximum number of hits before enemy is destroyed
   

     private int hitCount = 0;

    private Vector3 originalPosition;   
    private Transform playerTransform;
    private bool canAttack = true;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        originalPosition = transform.position;
    }

    private void Update()
    {
        // Move towards the player
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        transform.Translate(directionToPlayer.normalized * moveSpeed * Time.deltaTime);

        // Check distance to player for attacking
        if (directionToPlayer.magnitude <= attackRange && canAttack)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        // Perform attack logic here
        // For example, reduce player's health or apply damage

        canAttack = false;
        Invoke(nameof(ResetAttackCooldown), attackCooldown);
    }

    private void ResetAttackCooldown()
    {
        canAttack = true;
    }
     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hitCount++;

            if (hitCount >= maxHits)
            {
                Destroy(gameObject); // Destroy the enemy if hit count exceeds maxHits
            }
            else
            {
                AdjustPositionOnHit();
            }

            Destroy(collision.gameObject); // Destroy the bullet
        }
    }
      private void AdjustPositionOnHit()
    {
        // Adjust the enemy's position on hit
        float xOffset = Random.Range(-0.5f, 0.5f); // Change in x position
        float zOffset = Random.Range(-0.5f, 0.5f); // Change in z position

        Vector3 newPosition = new Vector3(originalPosition.x + xOffset, originalPosition.y, originalPosition.z + zOffset);
        transform.position = newPosition;
    }
}
