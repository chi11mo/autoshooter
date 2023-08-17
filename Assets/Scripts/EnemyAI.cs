using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 3f;
    public float attackCooldown = 2f;
    public int damage = 10;

    private Transform playerTransform;
    private bool canAttack = true;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
}
