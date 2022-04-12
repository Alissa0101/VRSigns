using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;

public class GameController : MonoBehaviour
{
    GestureRecognition gr;
    Feedback fb;

    public GameObject leftControllerObject;
    public GameObject rightControllerObject;

    public SteamVR_Behaviour_Skeleton leftController;
    public SteamVR_Behaviour_Skeleton rightController;

    public SignList signList;

    GameObject lineRight;// = new GameObject();
    GameObject lineLeft;// = new GameObject();
    

    [Header("Debug")]
    public TextMeshPro debugText;

    // Start is called before the first frame update
    void Start()
    {
        gr = GetComponent<GestureRecognition>();
        fb = GetComponent<Feedback>();
        fb.init(leftControllerObject, rightControllerObject, gr);

        lineRight = new GameObject();
        lineLeft = new GameObject();

        lineRight.AddComponent<LineRenderer>();
        lineLeft.AddComponent<LineRenderer>();

        lineRight.transform.position = new Vector3(9999, 9999 ,9999);
        lineLeft.transform.position = new Vector3(9999, 9999, 9999);
    }

    // Update is called once per frame
    void Update()
    {
        //string closestLeftHandSign = gr.findClosestSignBasic(leftController);
        string closestRightHandSign = gr.findClosestSignBasic(rightController, rightControllerObject.transform.position);
        SignList.Signs selectedSign = signList.signs[signList.selectedSignIndex];
        debugText.SetText(signList.selectedSignIndex + " " + selectedSign.name + "\n" + closestRightHandSign + " " + gr.getSaidWords()[0]);
        

        if (gr.getSaidWords()[0] == selectedSign.name)
        {
            //signList.signs[signList.selectedSignIndex].timesComplete += 1;
            //print(signList.signs[signList.selectedSignIndex].timesComplete);
            //signList.selectedSignIndex += 1;
            print("Next word");
            signList.nextWord();
        }

        
        
        //fb.run("right", "ThankYou_1");
        //debugText.SetText("Said: " + gr.getSaidWords()[0]);// + "R: " + closestRightHandSign + "\nC: " + gr.getConfidentSignName() + "\n" + gr.getHoldTimer());
        //print("Right: " + RightController.fingerCurls);
        //laserPointer(rightControllerObject, lineRight, rightController);
    }

    void laserPointer(GameObject controller, GameObject line, SteamVR_Behaviour_Skeleton controllerSkeleton)
    {
        
        RaycastHit hit;

        Vector3 pos = controller.transform.position;

        //Debug.DrawRay(pos, controller.transform.TransformDirection(Vector3.forward) * 1000, Color.black);

        
        LineRenderer drawLine = line.GetComponent<LineRenderer>();//line.AddComponent<LineRenderer>();
        drawLine.startWidth = 0.01f;
        drawLine.endWidth = 0.01f;
        drawLine.startColor = Color.black;
        drawLine.endColor = Color.white;

        //drawLine.positionCount = 1;
        drawLine.SetPosition(0, pos);
        drawLine.SetPosition(1, controller.transform.TransformDirection(Vector3.forward) * 1000);
        

        if (Physics.Raycast(pos, controller.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                hit.collider.GetComponent<Interactable>().onIntersect(hit.collider.gameObject, isIndexTriggerPressed(controllerSkeleton));
            }
        }
        else
        {
            //Debug.Log("Did not Hit");
        }
    }

    public bool isIndexTriggerPressed(SteamVR_Behaviour_Skeleton controllerSkeleton)
    {
        return (controllerSkeleton.indexCurl >= 0.9);
    }
}
