using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region movement_variables
    public float movespeed;
    #endregion

    #region physics_components
    Rigidbody2D enemyRB;
    #endregion

    #region targeting_variables
    public Transform player;
    #endregion

    #region attack_variables
    public float explosionDamage;
    public float explosionRadius;
    public GameObject explosionObj;
    #endregion

    #region health_variables
    public float maxHealth;
    float currHealth;
    #endregion

    #region Unity_functions

    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();

        currHealth = maxHealth;
    }

    private void Update()
    {
        //Check if we know who player is
        if (player == null)
        {
            return;
        }

        Move();

    }
    #endregion

    #region movement_functions

    private void Move()
    {
        //Calculate movement vector
        Vector2 direction = player.position - transform.position;

        enemyRB.velocity = direction.normalized * movespeed;
    }
    #endregion

    #region attack_functions


    private void Explode()
    {

        //Call sound effects
        FindObjectOfType<AudioManager>().Play("Explosion");

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionRadius,
        Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                //Cause Damage
                Debug.Log("hit the player");
                hit.transform.GetComponent<PlayerController>().TakeDamage(explosionDamage);

                //Spawn Explosion prefab
                Instantiate(explosionObj, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Explode();
        }
    }
    #endregion


    #region health_fuctions
    //Enemy takes damage based on "value" params
    public void TakeDamage(float value)
    {

        //Call sound effect
        FindObjectOfType<AudioManager>().Play("BatHurt");

        currHealth -= value;
        Debug.Log("Health is now " + currHealth.ToString());

        //Check for death
        if (currHealth <= 0)
        {
            Die();
        }
    }

    //Destroys enemy object
    void Die()
    {
        //Destroy Gameobject
        Destroy(this.gameObject);
    }
    #endregion
}
