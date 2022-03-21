using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GestureRecognition : MonoBehaviour
{
    

    [Header("Settings")]
    public float timeToHoldSign = 1;
    private float holdTimer = 0;
    public GameObject playerHead;


    public SignAtributes[] signs;

    [Serializable]
    public struct SignAtributes
    {
        public String name;


        public float thumb;
        public float index;
        public float middle;
        public float ring;
        public float pinky;

        public float distance;

        public String followedBy;
        public String result;
    }

    private SignAtributes lastSign;
    private SignAtributes lastConfidentSign;
    private String lastConfidentSignName;

    private List<String> saidWords = new List<String>();

    public String findClosestSignBasic(SteamVR_Behaviour_Skeleton hand, Vector3 handPosition)
    {
        float lowestDiff = 100;
        String closestSignName = "NONE";
        SignAtributes closestSign = new SignAtributes();


        for (int i = 0; i < signs.Length; i++)
        {
            SignAtributes sign = signs[i];
            float thumbDiff = Mathf.Abs(sign.thumb - hand.thumbCurl);
            float indexDiff = Mathf.Abs(sign.index - hand.indexCurl);
            float middleDiff = Mathf.Abs(sign.middle - hand.middleCurl);
            float ringDiff = Mathf.Abs(sign.ring - hand.ringCurl);
            float pinkyDiff = Mathf.Abs(sign.pinky - hand.pinkyCurl);

            //print(handPosition);

            float dist = Mathf.Abs(sign.distance - Vector3.Distance(handPosition, playerHead.transform.position));

            if (sign.distance == -1)
            {
                dist = 0;
            }

            float avgDiffFingerDiff = ((thumbDiff + indexDiff + middleDiff + ringDiff + pinkyDiff) / 5) + dist;
            


            if (avgDiffFingerDiff < 0) { avgDiffFingerDiff = 0; }
            if (avgDiffFingerDiff < lowestDiff)
            {
                lowestDiff = avgDiffFingerDiff;
                closestSignName = sign.name;
                closestSign = sign;
            }
            print(sign.name + " " + avgDiffFingerDiff + " " + dist);
        }
        

        if (lastSign.name != closestSignName || closestSignName == "EMPTY")
        {
            holdTimer = 0;
        }
        
        if (holdTimer >= timeToHoldSign)
        {
            lastConfidentSign = closestSign;
            lastConfidentSignName = closestSignName;
        }

        //Return the result if there is a followed by
        if (lastConfidentSign.followedBy == closestSignName)
        {
            closestSignName = lastConfidentSign.result;
            if (saidWords[0] != closestSignName)
            {
                saidWords[0] = closestSignName;
            } 
            holdTimer = 0;
        }
        

        holdTimer += Time.deltaTime;
        lastSign = closestSign;

        return closestSignName;
    }

    public String getConfidentSignName()
    {
        return lastConfidentSignName;
    }

    public float getHoldTimer()
    {
        return holdTimer;
    }

    public List<String> getSaidWords()
    {
        return saidWords;
    }

    private void Start()
    {
        saidWords.Add("START");
    }
}
