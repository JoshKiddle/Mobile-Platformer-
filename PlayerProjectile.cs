using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float deathTime;
    public int damage;
    public GameObject projectileShatter;

    //public PatrolEnemy patrolEnemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
        //patrolEnemy = GameObject.FindObjectOfType(typeof(PatrolEnemy)) as PatrolEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //patrolEnemy.TakeDamage(damage);
            other.gameObject.GetComponent<PatrolEnemy>().TakeDamage(damage);
            Die();
        }
        else if(other.gameObject.CompareTag("Shiv"))
        {
            other.gameObject.GetComponent<Shiv>().TakeDamage(damage);
            Die();
        }
        else
        {
            Die();
        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    void Die()
    {
        GameObject effectInstance = (GameObject)Instantiate(projectileShatter, transform.position, transform.rotation);
        Destroy(effectInstance, 5f);

        Destroy(gameObject);
    }
}
