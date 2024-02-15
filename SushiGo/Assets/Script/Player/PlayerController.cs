using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private Animator anim;
    private bool grounded;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    public sushi Sushi;
    
    [Header("Sound")]
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip coinSound;
    
    [Header("Attack")]
    [SerializeField]  private Vector2 projectileSpawnOffset;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Move the player character horizontally
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        // Flip the player character horizontally based on their movement direction
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }

        // Make the player character jump
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            SoundManager.instance.PlaySound(jumpSound);
            Jump();
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
        
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        grounded = false;
    }

    void Flip()
    {
        // Flip the player's sprite horizontally
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
    void Fire()
    {
        SoundManager.instance.PlaySound(attackSound);
        // Instantiate a new projectile and set its direction and position
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        if (!facingRight)
        {
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, 5f);
        }
        else
        {
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 5f);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
    
    public bool canAttack()
    {
        return horizontalInput == 0 && grounded;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            SoundManager.instance.PlaySound(coinSound);
            Destroy(other.gameObject);
            Sushi.coinCount++;
        }
    }
}
