using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public static Slot selectedSlot;
    public int slotNumber;
    private Inventory _inventory;
    public int i;

    private void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

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

        if (transform.childCount <= 0)
        {
            _inventory.isFull[i] = false;
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
            child.GetComponent<Spawn>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject);
        }
    }
}
