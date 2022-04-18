using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionInFrontOfPlayer : MonoBehaviour
{

    public Vector3 offset;

    public GameController gc;

    public Vector3 maxMovementsVectorUp;
    public Vector3 maxMovementsVectorDown;

    public float maxRecenterDistance = 0.6f;
    public float Smoothness = 0.02f;

    public bool reverseLookAt;
    private bool recenter = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 forwardTargetPosition = (gc.playerHead.transform.position + gc.playerHead.transform.forward * 2) + offset;

        float distance = Vector3.Distance(forwardTargetPosition, transform.position);
        //gc.debugText.SetText(distance + "");

        if (distance > maxRecenterDistance)
        {
            recenter = true;
        }

        if (recenter == true)
        {
            Vector3 maxMovementsUp = (maxMovementsVectorUp + gc.playerHead.transform.position) + offset;
            Vector3 maxMovementsDown = (maxMovementsVectorDown + gc.playerHead.transform.position) + offset;

            

            if (maxMovementsVectorUp.x != 0)
            {
                if (forwardTargetPosition.x >= maxMovementsUp.x)
                {
                    forwardTargetPosition.x = maxMovementsUp.x;
                }
            }
            if (maxMovementsVectorDown.x != 0)
            {
                if (forwardTargetPosition.x <= maxMovementsDown.x)
                {
                    forwardTargetPosition.x = maxMovementsDown.x;
                }
            }

            if (maxMovementsVectorUp.y != 0)
            {
                if (forwardTargetPosition.y >= maxMovementsUp.y)
                {
                    forwardTargetPosition.y = maxMovementsUp.y;
                }
            }
            if (maxMovementsVectorDown.y != 0)
            {
                if (forwardTargetPosition.y <= maxMovementsDown.y)
                {
                    forwardTargetPosition.y = maxMovementsDown.y;
                }
            }

            if (maxMovementsVectorUp.z != 0)
            {
                if (forwardTargetPosition.z >= maxMovementsUp.z)
                {
                    forwardTargetPosition.z = maxMovementsUp.z;
                }
            }
            if (maxMovementsVectorDown.z != 0)
            {
                if (forwardTargetPosition.z <= maxMovementsDown.z)
                {
                    forwardTargetPosition.z = maxMovementsDown.z;
                }
            }

            transform.position = Vector3.Lerp(transform.position, forwardTargetPosition, Smoothness);
            transform.LookAt(gc.playerHead.transform.position);
            if (reverseLookAt == true)
            {
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + 270, transform.eulerAngles.z);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + 90, transform.eulerAngles.z);
            }

            if (distance < 0.03f)
            {
                recenter = false;
            }
        }
        
        
    }
}
