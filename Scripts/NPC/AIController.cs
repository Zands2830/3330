using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    //Scripts
    AIController EnemyAIScript = null;
    //Ints
    public int AITeamNumber;
    //Doubles
    public double health, baseDamage;
    //Floats
    public float enemyDistance, reloadTimer, projectileSpeed, projectileRifleSpeed, meleeAttackTimer, fireRange, rifleAttackTimer;
    //Strings
    public string AIType;
    //Bools
    public bool canAttack;
    //Lists
    public List<GameObject> RangedWeapons = new List<GameObject>();
    public List<GameObject> MeleeWeapons = new List<GameObject>();
    public List<GameObject> Shields = new List<GameObject>();
    //GameObjects
    public GameObject Enemy;
    public GameObject Arrow;
    public GameObject Bullet;
    public GameObject ProjectileSpawnPoint;
    //Animations
    public Animator axeAnimator;

    private void Start()
    {
        health = 100.0;
        fireRange = 75f;
        projectileSpeed = 90;
        projectileRifleSpeed = 150;
        reloadTimer = 2.5f;
        meleeAttackTimer = 1.0f;
        rifleAttackTimer = 0.05f;
        baseDamage = 10;
        canAttack = true;
        switch(AIType)
        {
            case "Axeman":
                fireRange = 2.1f;
                break;
            case "Archer":
                fireRange = 75f;
                break;
            case "Rifleman":
                fireRange = 75f;
                break;
            default:
                fireRange = 75f;
                break;
        }
    }

    private void FixedUpdate()
    {
        //Checks if there is an enemy assigned by the FindClosest script and if so, rotate towards it
        if (Enemy != null)
        {
            enemyDistance = Vector3.Distance(gameObject.transform.position, Enemy.transform.position);
            var targetRotation = Quaternion.LookRotation(Enemy.transform.position - transform.position);
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetRotation, 6 * Time.deltaTime);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (canAttack == true && enemyDistance <= fireRange && Enemy != null)
        {
            switch(AIType)
            {
                case "Archer":
                    canAttack = false;
                    ArcherFire();
                    StartCoroutine("TimerHandling");
                    break;
                case "Axeman":
                    canAttack = false;
                    MeleeAttack();
                    StartCoroutine("TimerHandling");
                    break;
                case "Rifleman":
                    canAttack = false;
                    RifleFire();
                    StartCoroutine("TimerHandling");
                    break;
                default:
                    break;
            }
        }
    }


    //-------------------Attacking and Firing Functions-------------------//
    //-------------------Attacking and Firing Functions-------------------//

    //If AI is Archer, this function is called if canFire = true and enemy is within firing range.
    //Function resets reload timer, instantiates an arrow prefab and directs fire towards enemy game object
    //defined in FindClosest script
    private void ArcherFire()
    {
        reloadTimer = 2.5f;
        Vector3 pos = ProjectileSpawnPoint.transform.position;
        GameObject proj = (GameObject)Instantiate(Arrow, pos, ProjectileSpawnPoint.transform.rotation);
        proj.GetComponent<Rigidbody>().velocity = ProjectileSpawnPoint.transform.up * projectileSpeed;
    }

    //If AI is using a MeleeAttack, then reset meleeAttack timer, call animations, do stuff
    private void MeleeAttack()
    {
        meleeAttackTimer = 1.0f;
        EnemyAIScript = Enemy.GetComponent<AIController>();
        EnemyAIScript.RecieveDamage("Axe");
        axeAnimator.SetTrigger("Swing");
    }

    private void RifleFire()
    {
        rifleAttackTimer = 0.1f;
        Vector3 pos = ProjectileSpawnPoint.transform.position;
        GameObject proj = (GameObject)Instantiate(Bullet, pos, ProjectileSpawnPoint.transform.rotation);
        proj.GetComponent<Rigidbody>().velocity = ProjectileSpawnPoint.transform.up * projectileRifleSpeed;
    }

    //-------------------Attacking and Firing Functions-------------------//
    //-------------------Attacking and Firing Functions-------------------//


    //-------------------Coroutines-------------------//
    //-------------------Coroutines-------------------//

    private IEnumerator TimerHandling()
    {
        while (canAttack == false)
        {
            switch (AIType)
            {
                case "Archer":
                    reloadTimer -= Time.deltaTime;
                    break;
                case "Axeman":
                    meleeAttackTimer -= Time.deltaTime;
                    break;
                case "Rifleman":
                    rifleAttackTimer -= Time.deltaTime;
                    break;
                default:
                    break;
            }
            if (reloadTimer <= 0 || meleeAttackTimer <= 0 || rifleAttackTimer <= 0)
            {
                canAttack = true;
            }
            yield return null;
        }
        yield return null;
    }

    //-------------------Coroutines-------------------//
    //-------------------Coroutines-------------------//


    //-------------------Passed Functions-------------------//
    //-------------------Passed Functions-------------------//

    public void RecieveDamage(string DamageType)
    {
        switch (DamageType)
        {
            case "Arrow":
                health -= baseDamage;
                break;
            case "Axe":
                health -= baseDamage;
                break;
            case "Bullet":
                health -= baseDamage;
                break;
            default:
                break;
        }
    }

    //-------------------Passed Functions-------------------//
    //-------------------Passed Functions-------------------//
}
