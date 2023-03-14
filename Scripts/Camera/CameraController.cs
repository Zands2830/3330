using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    NavScript NavScript = null;
    //FriendlyCommandController FCCScript = null;

    public GameObject[] PlayerControlledList;
    public GameObject FriendlyCommandController;

    private RaycastHit raycastHit;

    private void Start()
    {
        PlayerControlledList = GameObject.FindGameObjectsWithTag("PlayerControlledAI");
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && Input.GetKey("left shift"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out raycastHit))
            {
                Vector3 hitPosition = raycastHit.point;
                foreach (GameObject gO in PlayerControlledList)
                {
                    if(gO != null)
                    {
                        NavScript = gO.gameObject.GetComponent<NavScript>();
                        NavScript.RecieveMovePos(hitPosition);
                        NavScript.hasPlayerIndicated = true;
                        NavScript.commandWalk = true;
                    }
                }
            }
        }
        else if(Input.GetMouseButton(0) && Input.GetKey("left ctrl"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit))
            {
                Vector3 hitPosition = raycastHit.point;
                foreach (GameObject gO in PlayerControlledList)
                {
                    if (gO != null)
                    {
                        string rayTagHit = raycastHit.collider.tag;
                        switch (rayTagHit)
                        {
                            case "AIStackDoor":
                                NavScript = gO.gameObject.GetComponent<NavScript>();
                                NavScript.RecieveMovePos(hitPosition);
                                NavScript.hasPlayerIndicated = true;
                                NavScript.RecieveStackUpDoor();
                                break;
                            default:
                                break;
                        }

                    }
                }
            }

        }
        else if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit))
            {
                Vector3 hitPosition = raycastHit.point;
                foreach (GameObject gO in PlayerControlledList)
                {
                    if (gO != null)
                    {
                        NavScript = gO.gameObject.GetComponent<NavScript>();
                        NavScript.RecieveMovePos(hitPosition);
                        NavScript.hasPlayerIndicated = true;
                        NavScript.commandWalk = false;
                    }
                }
            }
        }

    }
}
