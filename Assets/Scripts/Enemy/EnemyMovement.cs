using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;               // Reference to the player's position.
    PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    NavMeshAgent nav;               // Reference to the nav mesh agent.

    Animator EnemyAnim;
    bool isInRange;
    public float range = 150f;

    void Awake()
    {
        // Set up the references.

        player = GameObject.Find("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();

        EnemyAnim= GetComponent<Animator>();
    }


    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        print(dist);
        if (dist <= range) 
        {
            isInRange = true;
            nav.SetDestination(player.position);
        }
        else 
        { 
            isInRange = false; 
        }

        EnemyAnim.SetBool("InRange", isInRange);

        // If the enemy and the player have health left...

        /*if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            // ... set the destination of the nav mesh agent to the player.
            nav.SetDestination(player.position);
        }
        // Otherwise...
        else
        {
            // ... disable the nav mesh agent.
            nav.enabled = false;
        }*/

    }
}
