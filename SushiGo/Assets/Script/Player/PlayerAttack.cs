using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Vector2 projectileSpawnOffset;
    private bool facingRight = true;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    
    private PlayerController playerController;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer > attackCooldown && playerController.canAttack())
            Attack();
        
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        cooldownTimer = 0;

        GameObject projectile = Instantiate(projectilePrefab, 
            (Vector2)projectileSpawnPoint.position + projectileSpawnOffset, Quaternion.identity);

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if (projectileRb != null)
        {
            float velocityX = facingRight ? 10f : -10f;
            if (!facingRight) // if facing left, reverse the velocity
            {
                projectile.transform.localScale = new Vector3(-1, 1, 1); // flip the projectile
                velocityX *= -1;
            }

            projectileRb.velocity = new Vector2(velocityX, 5f);
        }
    }
    
}