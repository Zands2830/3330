using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavScript : MonoBehaviour
{
    //Scripts
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    //Ints
    //Doubles
    //Floats
    public float enemyDistance, aiSpeedRunning, aiSpeedWalking, lineStackSpacing;
    //Strings
    public string AIType;
    //Bools
    public bool isPlayerControlled;
    public bool hasPlayerIndicated;
    public bool commandWalk;
    public bool commandStackUpDoor;
    //Lists
    private List<GameObject> friendlyAgents;
    //GameObjects
    public GameObject Enemy;
    //Animations
    //Other
    public Transform moveToTransform;
    public Vector3 moveToPos;

    private void Awake()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        friendlyAgents = new List<GameObject>(GameObject.FindGameObjectsWithTag("PlayerControlledAI"));
        aiSpeedRunning = 5f;
        aiSpeedWalking = 2f;
        navMeshAgent.speed = aiSpeedRunning;
        navMeshAgent.angularSpeed = 360f;
        navMeshAgent.acceleration = 20f;
        lineStackSpacing = 2f;
    }

    private void FixedUpdate()
    {
        //if (Enemy == null)
        //{
        //    navMeshAgent.destination = transform.position;
        //}
        switch (gameObject.tag)
        {
            case "PlayerControlledAI":
                if (hasPlayerIndicated == true)
                {
                    if (commandWalk == true)
                    {
                        navMeshAgent.speed = aiSpeedWalking;
                        navMeshAgent.destination = moveToPos;
                        navMeshAgent.stoppingDistance = 0f;
                        hasPlayerIndicated = false;
                    }
                    else
                    {
                        navMeshAgent.speed = aiSpeedRunning;
                        navMeshAgent.destination = moveToPos;
                        navMeshAgent.stoppingDistance = 0f;
                        hasPlayerIndicated = false;
                    }
                }
                break;
            default:
                break;
        }

        if (friendlyAgents.Contains(gameObject))
        {
            Vector3 destinationPosition = moveToPos + transform.right * friendlyAgents.IndexOf(gameObject) * lineStackSpacing;
            navMeshAgent.SetDestination(destinationPosition);
        }
    }

    public void RecieveEnemyVariable(GameObject PassedEnemy)
    {
        Enemy = PassedEnemy;
        moveToTransform = Enemy.transform;
    }

    public void RecieveMovePos(Vector3 Pos)
    {
        moveToPos = Pos;
    }

    public void RecieveStackUpDoor()
    {

    }
}

