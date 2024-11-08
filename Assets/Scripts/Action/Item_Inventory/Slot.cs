using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public static Slot selectedSlot;
    public int slotNumber;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSlot(3);
        }

        if (Input.GetKeyDown(KeyCode.G) && selectedSlot == this)
        {
            DropItem();
        }
    }

    void SelectSlot(int number)
    {
        if (slotNumber == number)
        {
            selectedSlot = this;
        }
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
