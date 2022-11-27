using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiv : MonoBehaviour
{
    [HideInInspector]
    public bool patrolling;
    private bool flip, canAttack;
    public int maxHealth;
    private int currentHealth;

    public float moveSpeed, range, attackCooldown, projectileSpeed;
    private float distFromPlayer;

    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D collider;
    public Transform player, attackPos;
    public GameObject projectile;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        patrolling = true;
        canAttack = true;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(patrolling)
        {
            Patrol();
        }

        distFromPlayer = Vector2.Distance(transform.position, player.position);

        if(distFromPlayer <= range)
        {
            if(player.position.x > transform.position.x && transform.localScale.x < 0 || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }

            patrolling = false;
            rb.velocity = Vector2.zero;
            //animator.SetBool("inRange", true);

            if (canAttack)
            {
                StartCoroutine(Attack());
            }
            
        }
        else
        {
            patrolling = true;
            //animator.SetBool("inRange", false);
        }
    }

    void FixedUpdate()
    {
        if(patrolling)
        {
            flip = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
        animator.SetFloat("Speed", Mathf.Abs(moveSpeed));
    }


    void Patrol()
    {
        if(flip || collider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }

        rb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        patrolling = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        moveSpeed *= -1;
        patrolling = true;
    }


   public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Attack()
    {
        //animator.SetBool("isAttacking", true);
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        animator.SetTrigger("Attack");
        GameObject newProjectile = Instantiate(projectile, attackPos.position, Quaternion.identity);

        newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed * moveSpeed * Time.fixedDeltaTime, 0f);
        canAttack = true;
        //animator.SetBool("isAttacking", false);
    }

    IEnumerator Die()
    {
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
