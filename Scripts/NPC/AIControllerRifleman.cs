using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControllerRifleman : MonoBehaviour
{
    //Scripts
    AIController EnemyAIScript = null;
    //Ints
    public int AITeamNumber;
    //Doubles
    public double health, baseDamage;
    //Floats
    public float enemyDistance, reloadTimer, projectileSpeed, meleeAttackTimer, fireRange, rifleAttackTimer;
    //Strings
    public string AIType;
    //Bools
    public bool canAttack;
    //Lists
    public List<GameObject> RangedWeapons = new List<GameObject>();
    public List<GameObject> MeleeWeapons = new List<GameObject>();
    //GameObjects
    public GameObject Enemy;
    public GameObject ProjectileOne;
    public GameObject ProjectileSpawnPoint;
    //Animations

    private void Start()
    {
        health = 100.0;
        fireRange = 75f;
        projectileSpeed = 90;
        reloadTimer = 2.5f;
        meleeAttackTimer = 1.0f;
        rifleAttackTimer = 0.1f;
        baseDamage = 10;
        canAttack = true;
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
              canAttack = false;
              Fire();
              StartCoroutine("TimerHandling");
        }
    }


    //-------------------Attacking and Firing Functions-------------------//
    //-------------------Attacking and Firing Functions-------------------//

    //This function is called if canFire = true and enemy is within firing range.
    //Function resets reload timer, instantiates a prefab and directs fire towards enemy game object
    //defined in FindClosest script
    private void Fire()
    {
        reloadTimer = 2.5f;
        Vector3 pos = ProjectileSpawnPoint.transform.position;
        GameObject proj = (GameObject)Instantiate(ProjectileOne, pos, ProjectileSpawnPoint.transform.rotation);
        proj.GetComponent<Rigidbody>().velocity = ProjectileSpawnPoint.transform.up * projectileSpeed;
    }

    //If AI is using a MeleeAttack, then reset meleeAttack timer, call animations, do stuff
    private void MeleeAttack()
    {
        meleeAttackTimer = 1.0f;
        EnemyAIScript = Enemy.GetComponent<AIController>();
        EnemyAIScript.RecieveDamage("Knife");
    }

    //-------------------Attacking and Firing Functions-------------------//
    //-------------------Attacking and Firing Functions-------------------//


    //-------------------Coroutines-------------------//
    //-------------------Coroutines-------------------//

    private IEnumerator TimerHandling()
    {
        while (canAttack == false)
        {
            rifleAttackTimer -= Time.deltaTime;

            if (meleeAttackTimer <= 0)
            {
                canAttack = true;
            }

            if (rifleAttackTimer <= 0)
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
            case "Knife":
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