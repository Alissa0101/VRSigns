using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Feedback : MonoBehaviour
{
    

    private GestureRecognition gr;

    private GameObject leftControllerObject;
    private GameObject rightControllerObject;

    private SteamVR_Behaviour_Skeleton leftController;
    private SteamVR_Behaviour_Skeleton rightController;

    public Material correctMeterial;
    public Material incorrectMaterial;

    public float maxError = 0.1f;

    public void init(GameObject left, GameObject right, GestureRecognition gr)
    {
        leftControllerObject = left;
        rightControllerObject = right;

        leftController = leftControllerObject.GetComponent<SteamVR_Behaviour_Skeleton>();
        rightController = rightControllerObject.GetComponent<SteamVR_Behaviour_Skeleton>();

        this.gr = gr;
    }



    public void run(String leftOrRight, String targetSignName)///CHANGE NAME LATER
    {
        GameObject controllerObject = leftControllerObject;
        SteamVR_Behaviour_Skeleton hand = leftController;
        GestureRecognition.SignAtributes targetSign = gr.signs[0];

        for (int i = 0; i < gr.signs.Length; i++)
        {
            if (gr.signs[i].name == targetSignName) { targetSign = gr.signs[i]; }
        }

        //print(targetSign.name);

        if (leftOrRight == "right")
        {
            controllerObject = rightControllerObject;
            hand = rightController;
        }


        float thumbDiff = Mathf.Abs(targetSign.thumb - hand.thumbCurl);
        float indexDiff = Mathf.Abs(targetSign.index - hand.indexCurl);
        float middleDiff = Mathf.Abs(targetSign.middle - hand.middleCurl);
        float ringDiff = Mathf.Abs(targetSign.ring - hand.ringCurl);
        float pinkyDiff = Mathf.Abs(targetSign.pinky - hand.pinkyCurl);




    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
