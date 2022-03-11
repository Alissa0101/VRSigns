using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;

public class GameController : MonoBehaviour
{



    GestureRecognition gr;

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
        string closestLeftHandSign = gr.findClosestSign(leftController);
        print("Left: " + closestLeftHandSign);
        debugText.SetText("Left: " + closestLeftHandSign);
        //print("Right: " + RightController.fingerCurls);
    }
}
