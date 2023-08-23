using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 3f;
    public float attackCooldown = 2f;
    public int damage = 10;

    public int maxHits = 5; // Maximum number of hits before enemy is destroyed

    public float rotationSpeed = 5f; // Adjust as needed

    private int hitCount = 0;

    private Animator animator;
    private Transform playerTransform;
    private bool canAttack = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator.SetBool("isRunning", true);
    }

    private void Update()
    {
        // Move towards the player
        // Vector3 directionToPlayer = playerTransform.position - transform.position;
        //transform.Translate(directionToPlayer.normalized * moveSpeed * Time.deltaTime);
        animator.SetBool("isRunning", true);
        RotateTowardsPlayer();
        // Check distance to player for attacking
        // if (directionToPlayer.magnitude <= attackRange && canAttack)
        // {

        // AttackPlayer();
        //  }
        //  else
        //  {
        //       animator.SetBool("isRunning", true);
        //  }
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
                 animator.SetBool("isDead", true);
               // Destroy(gameObject); // Destroy the enemy if hit count exceeds maxHits
            }


            Destroy(collision.gameObject); // Destroy the bullet
        }
    }
    private void RotateTowardsPlayer()
    {
        if (playerTransform != null)
        {
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.y = 0f; // Ensure no vertical rotation
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

