using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerStats playerStats =  new PlayerStats();

    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    public Animator animator;
    public GameManager gameManager;
    public GameObject projectile;

    public AudioManager audioManager;

    [SerializeField]private LayerMask floorLayerMask;

    public LayerMask enemyLayers;

    private bool moveRight, moveLeft, jump, canAttack;
    public bool attack;
    
    public float horizontalMove, moveSpeed, jumpSpeed, invincibleCooldown, attackCooldown, projectileSpeed;
    public bool facingRight;

    public int killZone, attackDamage, currentCurrency;

    public Transform attackPoint;
    public float attackRange;

    private bool invincible = false;


    public Text currencyText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        moveLeft = false;
        moveRight = false;
        jump = false;
        canAttack = true;

        currentCurrency = 0;
    }

    public void PointerDownLeft()
    {
        moveLeft = true;
    }

    public void PointerUpLeft()
    {
        moveLeft = false;
    }

    public void PointerDownRight()
    {
        moveRight = true;
    }

    public void PointerUpRight()
    {
        moveRight = false;
    }

    public void PointerDownJump()
    {
        jump = true;
    }

    public void PointerUpJump()
    {
        jump = false;
    }

    public void PointerDownAttack()
    {
        if (canAttack)
        {
            Attack();
            StartCoroutine(AttackCooldown());
        }
    }
    
    /*public void PointerUpAttack()
    {
        attack = false;
    }*/

    // Update is called once per frame
    void Update()
    {
        Move();

        Flip();

        if(transform.position.y <= killZone)
        {
            Damage(99999);
        }

        currencyText.text = "" + currentCurrency;

    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, floorLayerMask);

        //Debug.Log(raycastHit.collider);
        animator.SetBool("Jump", false);
        return raycastHit.collider != null;
        
    }

    public void Move()
    {
        if(moveLeft)
        {
            horizontalMove = -moveSpeed;
        }
        else if(moveRight)
        {
            horizontalMove = moveSpeed;
        }
        else
        {
            horizontalMove = 0;
        }

        if(jump && IsGrounded())
        {
            rb.velocity = Vector2.up * jumpSpeed;
            animator.SetBool("Jump", true);
            audioManager.Play("Jump");
        }
    }

    public void Flip()
    {
        if((horizontalMove < 0 && facingRight) || (horizontalMove > 0 && !facingRight))
        {
            facingRight = !facingRight;
            //transform.Rotate(new Vector3(0, 180, 0));
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            //attackPoint.transform.Rotate(new Vector3(0, 90, 0));
        }
    }

    public void Damage(int damage)
    {
        if(invincible == false)
        {
            playerStats.health -= damage;
            StartCoroutine(DamageCooldown());
            audioManager.Play("TakeDamage");
        }

        if(playerStats.health <= 0)
        {
            StartCoroutine(CallPlayerDeath());
        }
    }

    public void Attack()
    {
        // play attack animation
        animator.SetTrigger("Attack");

        // detect enemies in range
        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // play attack sound
        audioManager.Play("ThrowPunch");

        if (facingRight)
        {
            GameObject newProjectile = Instantiate(projectile, attackPoint.position, Quaternion.identity);
            newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed * (moveSpeed) * Time.fixedDeltaTime, 0f);
        }
        else
        {
            GameObject newProjectile = Instantiate(projectile, attackPoint.position, Quaternion.identity);
            newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed * (moveSpeed * -1) * Time.fixedDeltaTime, 0f);
        }
        /* damage enemies in range 
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<PatrolEnemy>().TakeDamage(attackDamage);
            Debug.Log("Hit" + enemy.name);
            audioManager.Play("Punch");
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Won Level");
            gameManager.WinLevel();
        }

        if(other.gameObject.CompareTag("Crystal"))
        {
            Debug.Log("Crystal");
            currentCurrency++;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            Debug.Log("Hit hazard");
            Damage(99999);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    IEnumerator CallPlayerDeath()
    {
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(1.5f);
        GameManager.PlayerDeath(this);
        gameManager.GameOver();
    }

    IEnumerator DamageCooldown()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibleCooldown);
        invincible = false;
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
