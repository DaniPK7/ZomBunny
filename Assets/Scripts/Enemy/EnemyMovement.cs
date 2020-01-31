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
    bool EnemyMoving, chasePlayer;
    public float range = 150f;

    PlayerMovement PlayerMovScript;
    public Transform Spawn;               // Reference to the spawn's position.


    void Awake()
    {
        // Set up the references.

        player = GameObject.Find("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();

        EnemyAnim= GetComponent<Animator>();

        PlayerMovScript = FindObjectOfType<PlayerMovement>();
        //Spawn = GameObject.Find("SpawnZone").transform;

    }


    void Update()
    {
        float PlayerDist = Vector3.Distance(player.position, transform.position);
        float SpawnDist = Vector3.Distance(Spawn.position, transform.position);
        //print(dist);
        
        if (!PlayerMovScript.PlayerIsSafe)  //If the Player is not in the safe zone...
        {
            if (PlayerDist <= range) //Chase player
            {
                EnemyMoving = true;
                nav.SetDestination(player.position);
            }
            else 
            {
                EnemyMoving = false;
            }           
        }
        else // If the Player is in the safe zone...
        {
            goToSpawn(SpawnDist);
        }

        EnemyAnim.SetBool("InRange", EnemyMoving);

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

    void goToSpawn(float d) 
    {
        nav.SetDestination(Spawn.position);
        if (d < 3)
        {
            EnemyMoving = false;
        }
    }
}
