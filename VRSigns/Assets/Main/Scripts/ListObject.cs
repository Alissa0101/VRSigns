using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListObject : MonoBehaviour, Interactable
{

    public string animationBoolName;
    public TextMeshPro nameText;
    public GameObject background;
    public GameObject border;
    public GameObject complete;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onIntersect(GameObject obj, bool trigger)
    {
        print("Hit: " + obj.name + "Trigger: " + trigger);
        if (trigger == true)
        {
            transform.parent.GetComponent<SignList>().startAnim(animationBoolName);
        }
    }
}
