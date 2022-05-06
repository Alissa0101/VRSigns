using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuButton : MonoBehaviour, Interactable
{

    public GameObject background;

    public UnityEvent action;
    

    public void onIntersect(GameObject obj, bool trigger)
    {
        print("Name: " + obj.name + " " + trigger);
        background.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        if (trigger == true)
        {
            action.Invoke();
        }
    }

    public void onLeaveIntersect(GameObject obj, bool trigger)
    {
        print("ENDED Name: " + obj.name + " " + trigger);
        background.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        background.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }
}
