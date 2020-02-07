using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject powUp;
    public float spawnPowUpTime = 7f;

    private int RndX, RndZ, maxPowUps; 
    public int powUpsPlaced;



    // Start is called before the first frame update
    void Start()
    {
        maxPowUps = 3;
        powUpsPlaced = 0;
        RndX = Random.RandomRange(-20, 20);
        RndZ = Random.RandomRange(-20, 20);

        
        
        InvokeRepeating("SpawnPowUps", spawnPowUpTime, spawnPowUpTime);
        
    }

    // Update is called once per frame
    void SpawnPowUps()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        if (powUpsPlaced < maxPowUps) {

            Instantiate(powUp, new Vector3(RndX, 1f, RndZ), Quaternion.identity);
            powUpsPlaced += 1;

            RndX = Random.RandomRange(-20, 20);
            RndZ = Random.RandomRange(-20, 20);
        }

    }
}
