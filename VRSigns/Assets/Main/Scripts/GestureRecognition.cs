using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GestureRecognition : MonoBehaviour
{
    

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
    }

    public String findClosestSign(SteamVR_Behaviour_Skeleton hand)
    {
        float lowestDiff = 100;
        String closestSignName = "NONE";
        for (int i = 0; i < signs.Length; i++)
        {
            SignAtributes sign = signs[i];
            float thumbDiff = Mathf.Abs(sign.thumb - hand.thumbCurl);
            float indexDiff = Mathf.Abs(sign.index - hand.indexCurl);
            float middleDiff = Mathf.Abs(sign.middle - hand.middleCurl);
            float ringDiff = Mathf.Abs(sign.ring - hand.ringCurl);
            float pinkyDiff = Mathf.Abs(sign.pinky - hand.pinkyCurl);

            print("thumbDiff: " + thumbDiff + " indexDiff " + indexDiff + " middleDiff " + middleDiff + " ringDiff " + ringDiff + " pinkyDiff " + pinkyDiff);

            float avgDiff = (thumbDiff + indexDiff + middleDiff + ringDiff + pinkyDiff) / 5;
            if (avgDiff < 0) { avgDiff = 0; }
            if (avgDiff < lowestDiff)
            {
                lowestDiff = avgDiff;
                closestSignName = sign.name;
            }
            print(sign.name + " " + avgDiff);
        }
        return closestSignName;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print("Left: " + leftController.fingerCurls[1]);
    }
}
