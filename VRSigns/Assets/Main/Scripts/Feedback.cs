using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Feedback : MonoBehaviour
{
    

    private GestureRecognition gr;
    
    public SteamVR_Behaviour_Skeleton rightController;

    public SkinnedMeshRenderer feedBackHand;

    public float maxError = 0.1f;

    [Header("Red Materials")]
    public Material red_Thumb;
    public Material red_Index;
    public Material red_Middle;
    public Material red_Ring;
    public Material red_Pinky;

    [Header("Green Materials")]
    public Material green_Thumb;
    public Material green_Index;
    public Material green_Middle;
    public Material green_Ring;
    public Material green_Pinky;

    

    public void init(GameObject left, GameObject right, GestureRecognition gr)
    {

        this.gr = gr;
    }



    public void run(String leftOrRight, String targetSignName)///CHANGE NAME LATER
    {
        SteamVR_Behaviour_Skeleton hand = rightController;
        GestureRecognition.SignAtributes targetSign = gr.getSignByName(targetSignName);//gr.signs[0];

        //if (targetSign.followedBy.Length > 0 && gr.lastConfidentSign.name == targetSignName)
        //{
        //    targetSign = gr.getSignByName(targetSign.followedBy);
        //}

        //for (int i = 0; i < gr.signs.Length; i++)
        //{
        //    if (gr.signs[i].name == targetSignName) { targetSign = gr.signs[i]; }
        //}

        //print(targetSign.name);

        if (leftOrRight == "right")
        {
            hand = rightController;
        }


        float thumbDiff = Mathf.Abs(targetSign.thumb - hand.thumbCurl);
        float indexDiff = Mathf.Abs(targetSign.index - hand.indexCurl);
        float middleDiff = Mathf.Abs(targetSign.middle - hand.middleCurl);
        float ringDiff = Mathf.Abs(targetSign.ring - hand.ringCurl);
        float pinkyDiff = Mathf.Abs(targetSign.pinky - hand.pinkyCurl);

        print(targetSignName + " " + targetSign.name);

        Material[] mats = feedBackHand.materials;

        if (thumbDiff > maxError)
        {
            mats[1] = red_Thumb;
        }
        else
        {
            mats[1] = green_Thumb;
        }

        if (indexDiff > maxError)
        {
            mats[2] = red_Index;
        }
        else
        {
            mats[2] = green_Index;
        }

        if (middleDiff > maxError)
        {
            mats[3] = red_Middle;
        }
        else
        {
            mats[3] = green_Middle;
        }

        if (ringDiff > maxError)
        {
            mats[4] = red_Ring;
        }
        else
        {
            mats[4] = green_Ring;
        }

        if (pinkyDiff > maxError)
        {
            mats[5] = red_Pinky;
        }
        else
        {
            mats[5] = green_Pinky;
        }

        feedBackHand.materials = mats;

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
