using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosest : MonoBehaviour
{
    //Scripts
    FindClosest tempaiScript = null;
    NavScript NavScript = null;
    AIController aiScript = null;
    //Ints
    //Doubles
    //Floats
    public float closestDistance, distance, distanceEval, distanceEvalBuffer, rescanTimer;
    //Strings
    //Bools
    private bool hasScanned;
    //GameObjects
    public GameObject closestEnemy;
    public GameObject[] PlayerControlledList;
    //Raycasts
    private RaycastHit raycastHit;

    private void Start()
    {
        PlayerControlledList = GameObject.FindGameObjectsWithTag("PlayerControlledAI");
        aiScript = gameObject.GetComponent<AIController>();
        NavScript = gameObject.GetComponent<NavScript>();
        //distanceEvalBuffer is the maximum distance that ai will focus on enemy before switching to next closest
        distanceEvalBuffer = 5;
        rescanTimer = 2f;
        hasScanned = false;
        Scan();
    }

    private void FixedUpdate()
    {
        //If an enemy has been detected, then draw a debug line to it. Then perform a raycast LOS check
        //if LOS check returns any other object other than whats specified, then null enemy variables and
        //force a rescan via if statement null check below
        if (closestEnemy != null)
        {
            Debug.DrawLine(gameObject.transform.position, closestEnemy.transform.position, Color.red);
            if (Physics.Raycast(transform.position, (closestEnemy.transform.position - transform.position), out raycastHit))
            {
                if (raycastHit.collider.tag != "PlayerControlledAI")
                {
                    closestEnemy = null;
                    aiScript.Enemy = null;
                }
            }
        }
        if (hasScanned == false)
        {
            Scan();
        }
        if (hasScanned == true)
        {
            rescanTimer -= Time.deltaTime;
        }
        if (rescanTimer <= 0)
        {
            hasScanned = false;
        }
        //if closestEnemy has been killed or force nulled, force scan
        if (closestEnemy == null)
        {
            Scan();
        }
    }

    //Function will scan for NPC not on the same team as current controller
    private void Scan()
    {
        rescanTimer = 2f;
        PlayerControlledList = GameObject.FindGameObjectsWithTag("PlayerControlledAI");

        //These loops will iterate through each "isEnemyTeam" variable of each GameObject in
        //the array and check if it has been enabled/disabled.
        //(Also i think g0 != gameObject is detecting whether g0 is itself, probably just a waste of code)
        //These loops are jumbled as fuck, maybe i can find a cleaner way of checking what team gO is on rather
        //than duplicating all those if statements. I tried doing a self check to see if isEnemyTeam != current
        //selection but comparing a variable to itself doesn't work i guess
        foreach (GameObject gO in PlayerControlledList)
        {
            tempaiScript = gO.gameObject.GetComponent<FindClosest>();
            if (gameObject.tag == "EnemyAI")
            {
                if (Physics.Raycast(transform.position, (gO.gameObject.transform.position - transform.position), out raycastHit))
                {
                    if (gO != gameObject && gameObject != null && gO != null && gO.gameObject.tag == "PlayerControlledAI" && raycastHit.collider.tag == "PlayerControlledAI")
                    {
                        distance = Vector3.Distance(gameObject.transform.position, gO.transform.position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestEnemy = gO;
                            aiScript.enemyDistance = closestDistance;
                            aiScript.Enemy = closestEnemy;
                            NavScript.RecieveEnemyVariable(closestEnemy);
                        }
                        //Line below must execute after if statement above
                        if (closestEnemy != null)
                        {
                            distanceEval = Vector3.Distance(gameObject.transform.position, closestEnemy.transform.position);
                            distanceEval += distanceEvalBuffer;
                        }
                        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

                        if (distanceEval > closestDistance)
                        {
                            closestDistance = distanceEval;
                            closestEnemy = gO;
                            aiScript.enemyDistance = closestDistance;
                            aiScript.Enemy = closestEnemy;
                        }
                        if (closestEnemy == null)
                        {
                            closestDistance = 9999;
                        }
                    }

                }
            }

        }
        hasScanned = true;
    }
}
