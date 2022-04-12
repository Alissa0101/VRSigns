using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignList : MonoBehaviour
{
    
    public Animator handAnimator;

    public Signs[] signs;

    public int amountToComplete = 3;

    [HideInInspector]
    public int selectedSignIndex = -1;


    [Serializable]
    public struct Signs
    {
        public String name;
        //public String animationBoolName;
        
        private int hiddenTimesComplete;
        public int timesComplete
        {
            get { return hiddenTimesComplete; }
            set
            {
                hiddenTimesComplete = value;

                if (value >= 3)
                {
                    this.complete = true;
                }
                else
                {
                    listObject.complete.GetComponent<Renderer>().material.SetColor("_Color", Color.Lerp(Color.yellow, Color.red, value/3));
                    listObject.border.GetComponent<Renderer>().material.SetColor("_Color", Color.Lerp(Color.yellow, Color.red, value / 3));
                }

            }
        }
        private bool hiddenComplete;
        public bool complete
        {
            get { return hiddenComplete; }
            set
            {
                hiddenComplete = value;
                if (value == true)
                {
                    //listObject.complete.GetComponent<Material>().color = Color.green;
                    //listObject.border.GetComponent<Material>().color = Color.green;
                    listObject.complete.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                    listObject.border.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                }
                else
                {
                    listObject.complete.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    listObject.border.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }
            }
        }
        private bool hiddenSelected;
        public bool selected
        {
            get { return hiddenSelected; }
            set
            {
                hiddenSelected = value;
                if (value == true)
                {
                    //listObject.complete.GetComponent<Material>().color = Color.green;
                    //listObject.border.GetComponent<Material>().color = Color.green;
                    listObject.complete.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                    listObject.border.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                }
                else if (complete == false)
                {
                    listObject.complete.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    listObject.border.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }
            }
        }
        //public TextMeshPro nameText;
        //public GameObject border;
        //public GameObject completeObject;
        public ListObject listObject;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        selectedSignIndex = -1;
        createList();
        nextWord();
    }
    
    void createList()
    {
        for (int i = 0; i < signs.Length; i++)
        {
            Signs sign = signs[i];
            //sign.nameText.SetText(sign.name);
            sign.listObject.nameText.SetText(sign.name);
        }
    }

    public void startAnim(String targetName)
    {
        for (int i = 0; i < signs.Length; i++)
        {
            Signs sign = signs[i];
            handAnimator.SetBool(sign.name, false);
            sign.selected = false;
            if (sign.name == targetName)
            {
                sign.selected = true;
                selectedSignIndex = i;
            }
        }
        handAnimator.SetBool(targetName, true);
    }

    public void nextWord()
    {
        if (selectedSignIndex >= 0) { signs[selectedSignIndex].complete = true; }
        selectedSignIndex += 1;
        if (selectedSignIndex >= signs.Length)
        {
            selectedSignIndex = 0;
        }
        for (int i = 0; i < signs.Length; i++)
        {
            Signs sign = signs[i];
            if (sign.name == signs[selectedSignIndex].name)
            {
                sign.selected = true;
                handAnimator.SetBool(sign.name, true);
                print(sign.name);
            }
            else
            {
                handAnimator.SetBool(sign.name, false);
                sign.selected = false;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {


        
    }

    
}
