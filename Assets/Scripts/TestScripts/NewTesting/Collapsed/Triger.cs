using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triger : MonoBehaviour
{
    public StateCtrl stateCtrl;
    void Start()
    {
        this.stateCtrl = transform.parent.GetComponent<StateCtrl>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        stateCtrl.action.Act();
    }

}
