using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

    public bool PowerShootEnable, startTimer;
    public int extraDamage = 300;
    private Color defGunColor, defLineColor, defParticlesColor;
    Color green = new Color (0,1,0);
    public float powerCD=5f;
    void Awake ()
    {
       
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();

        defGunColor = gunLight.color;
        defLineColor = gunLine.material.color;
        defParticlesColor = gunParticles.startColor;

    }


    void Update ()
    {
        ManagePowerUp();

        //if (startTimer) { CoolDown(powerCD); }

        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }

    void ManagePowerUp()
    {
        
        if (PowerShootEnable)
        {
            damagePerShot = extraDamage;

            gunLight.color = green;

            gunParticles.startColor = green;

            gunLine.material.color = green;
            //startTimer = true;
        }

        else
        {
            damagePerShot = 20;

            gunLight.color = defGunColor;
            gunParticles.startColor = defParticlesColor;
            gunLine.material.color = defLineColor;

        }
    }

    void CoolDown(float timeCD) 
    {

        /*print("Empieza el timer");
        timeCD -= Time.deltaTime;

        if (timeCD <= 0.0f)
        {
            startTimer = false;
            timerEnded();
        }*/
        timeCD -= Time.deltaTime;
        if (timeCD <= 0)
        {

            print("El power up se acabo");
            PowerShootEnable = false;
            timeCD = 5f;

            
        }

    }
    void timerEnded()
    {
        //do your stuff here.
        
    }

}


 
 
 
 