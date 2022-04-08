using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignList : MonoBehaviour
{
    
    public Animator handAnimator;

    public Signs[] signs;

    [Serializable]
    public struct Signs
    {
        public String name;
        //public String animationBoolName;
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
        //public TextMeshPro nameText;
        //public GameObject border;
        //public GameObject completeObject;
        public ListObject listObject;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        createList();
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

    public void startAnim(String name)
    {
        for (int i = 0; i < signs.Length; i++)
        {
            Signs sign = signs[i];
            handAnimator.SetBool(sign.name, false);
            sign.complete = false;
            if (sign.name == name)
            {
                sign.complete = true;
            }
        }
        handAnimator.SetBool(name, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
