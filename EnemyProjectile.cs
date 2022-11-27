using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float deathTime; 
    public int damage;
    public GameObject projectileShatter;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
        player = GameObject.FindObjectOfType(typeof(Player)) as Player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.Damage(damage);
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
