using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;

public class GameController : MonoBehaviour
{



    GestureRecognition gr;

    public GameObject leftControllerObject;
    public GameObject rightControllerObject;

    public SteamVR_Behaviour_Skeleton leftController;
    public SteamVR_Behaviour_Skeleton rightController;

    [Header("Debug")]
    public TextMeshPro debugText;

    // Start is called before the first frame update
    void Start()
    {
        gr = GetComponent<GestureRecognition>();
    }

    // Update is called once per frame
    void Update()
    {
        //string closestLeftHandSign = gr.findClosestSignBasic(leftController);
        string closestRightHandSign = gr.findClosestSignBasic(rightController, rightControllerObject.transform.position);
        debugText.SetText("Said: " + gr.getSaidWords()[0]);// + "R: " + closestRightHandSign + "\nC: " + gr.getConfidentSignName() + "\n" + gr.getHoldTimer());
        //print("Right: " + RightController.fingerCurls);
    }
}
