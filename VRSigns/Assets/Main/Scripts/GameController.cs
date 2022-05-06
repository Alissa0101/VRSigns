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

    public GameObject menuParent;

    public GameObject mainMenu;

    public GameObject handAndSignListParent;

    public GameObject playerHead;

    public GameObject signListObject;

    public GameObject room;
    public Vector3 roomOffset;

    private SignList signList;

    private bool movedRoom = false;

    GameObject lineRight;// = new GameObject();
    GameObject lineLeft;// = new GameObject();

    RaycastHit lastRayHit;

    bool learningStarted = false;


    [Header("Debug")]
    public TextMeshPro debugText;

    // Start is called before the first frame update
    void Start()
    {
        gr = GetComponent<GestureRecognition>();
        fb = GetComponent<Feedback>();
        fb.init(leftControllerObject, rightControllerObject, gr);

        signList = signListObject.GetComponent<SignList>();
        print(playerHead.transform.position);
        

        lineRight = new GameObject();
        //lineLeft = new GameObject();

        lineRight.AddComponent<LineRenderer>();
        //lineLeft.AddComponent<LineRenderer>();

        lineRight.transform.position = new Vector3(9999, 9999 ,9999);
        //lineLeft.transform.position = new Vector3(9999, 9999, 9999);

        lineRight.GetComponent<LineRenderer>().material = new Material(Shader.Find("Specular"));

        handAndSignListParent.transform.position = new Vector3(handAndSignListParent.transform.position.x, 9999, handAndSignListParent.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHead.transform.position != new Vector3(-5.1f, -0.3f, 3.0f) && movedRoom == false)
        {
            room.transform.position = playerHead.transform.position + roomOffset;
            movedRoom = true;
        }
        
        //string closestLeftHandSign = gr.findClosestSignBasic(leftController);
        string closestRightHandSign = gr.findClosestSignBasic(rightController, rightControllerObject.transform.position);
        SignList.Signs selectedSign = signList.signs[signList.selectedSignIndex];
        debugText.SetText(signList.selectedSignIndex + " " + selectedSign.name + "\n" + closestRightHandSign + " " + gr.getSaidWords()[0]);

        fb.run("right", selectedSign.rawName);

        if (gr.getSaidWords()[0] == selectedSign.name && learningStarted == true)
        {
            //signList.signs[signList.selectedSignIndex].timesComplete += 1;
            //print(signList.signs[signList.selectedSignIndex].timesComplete);
            //signList.selectedSignIndex += 1;
            print("Next word");
            signList.nextWord();
        }


        menuParent.transform.LookAt(playerHead.transform);

        menuParent.transform.rotation = Quaternion.Euler(0, menuParent.transform.eulerAngles.y + 90, menuParent.transform.eulerAngles.z);

        //debugText.SetText("Said: " + gr.getSaidWords()[0]);// + "R: " + closestRightHandSign + "\nC: " + gr.getConfidentSignName() + "\n" + gr.getHoldTimer());
        //print("Right: " + RightController.fingerCurls);
        if (learningStarted == false)
        {
            laserPointer(rightControllerObject, lineRight, rightController);
        }
    }

   public void startLearning()
    {
        //handAndSignListParent.transform.position = new Vector3(0, 0.04636137f, 0);
        handAndSignListParent.transform.position = new Vector3(handAndSignListParent.transform.position.x, 0.7f, handAndSignListParent.transform.position.z);
        mainMenu.transform.position = new Vector3(0, 9999, 0);
        lineRight.SetActive(false);
        learningStarted = true;
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
        //drawLine.sharedMaterial.SetColor("_Color", Color.gray);
        

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
            if (lastRayHit.collider != null)
            {
                if (lastRayHit.collider.GetComponent<Interactable>() != null)
                {
                    lastRayHit.collider.GetComponent<Interactable>().onLeaveIntersect(lastRayHit.collider.gameObject, isIndexTriggerPressed(controllerSkeleton));
                }
            }
            
        }
        lastRayHit = hit;
    }

    public bool isIndexTriggerPressed(SteamVR_Behaviour_Skeleton controllerSkeleton)
    {
        return (controllerSkeleton.indexCurl >= 0.9);
    }
}
