using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{

    void onIntersect(GameObject obj, bool trigger);
    void onLeaveIntersect(GameObject obj, bool trigger);

}
