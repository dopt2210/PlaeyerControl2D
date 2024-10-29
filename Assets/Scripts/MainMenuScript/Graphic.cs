using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphic : MonoBehaviour
{
    private List<GameObject> disabledMediumObjects = new List<GameObject>();
    private List<GameObject> disabledLowObjects = new List<GameObject>();
    private string currentGraphicsQuality = "High"; // Track current graphics quality

    public void OnMediumButtonClick()
    {
        Debug.Log("Medium button clicked");
        currentGraphicsQuality = "Medium"; // Update current graphics quality
        GameObject[] mediumObjects = GameObject.FindGameObjectsWithTag("Medium");
        GameObject[] mediumLowObjects = GameObject.FindGameObjectsWithTag("MediumLow");

        foreach (GameObject obj in mediumLowObjects)
        {
            foreach (Transform child in obj.transform)
            {
                Debug.Log("Enabling child of MediumLow Object: " + child.gameObject.name);
                child.gameObject.SetActive(true);
            }
        }

        foreach (GameObject obj in mediumObjects)
        {
            if (obj.activeSelf)
            {
                Debug.Log("Disabling Medium Object: " + obj.name);
                obj.SetActive(false);
                disabledMediumObjects.Add(obj);
            }
        }
    }

    public void OnLowButtonClick()
    {
        Debug.Log("Low button clicked");
        currentGraphicsQuality = "Low"; // Update current graphics quality
        GameObject[] mediumObjects = GameObject.FindGameObjectsWithTag("Medium");
        GameObject[] lowObjects = GameObject.FindGameObjectsWithTag("Low");

        foreach (GameObject obj in mediumObjects)
        {
            if (obj.activeSelf)
            {
                Debug.Log("Disabling Medium Object: " + obj.name);
                obj.SetActive(false);
                disabledMediumObjects.Add(obj);
            }
        }

        foreach (GameObject obj in lowObjects)
        {
            if (obj.activeSelf)
            {
                Debug.Log("Disabling Low Object: " + obj.name);
                obj.SetActive(false);
                disabledLowObjects.Add(obj);
            }
        }
    }

    public void OnHighButtonClick()
    {
        Debug.Log("High button clicked");
        currentGraphicsQuality = "High"; // Update current graphics quality

        GameObject[] highObjects = GameObject.FindGameObjectsWithTag("High");
        foreach (GameObject highObj in highObjects)
        {
            foreach (Transform child in highObj.transform)
            {
                Debug.Log("Enabling child of High Object: " + child.gameObject.name);
                child.gameObject.SetActive(true);
            }
        }

        foreach (GameObject obj in disabledMediumObjects)
        {
            Debug.Log("Enabling Medium Object: " + obj.name);
            obj.SetActive(true);
        }
        disabledMediumObjects.Clear();

        foreach (GameObject obj in disabledLowObjects)
        {
            Debug.Log("Enabling Low Object: " + obj.name);
            obj.SetActive(true);
        }
        disabledLowObjects.Clear();
    }

    public void LoadGraphicsState(string quality)
    {
        switch (quality)
        {
            case "High":
                OnHighButtonClick();
                break;
            case "Medium":
                OnMediumButtonClick();
                break;
            case "Low":
                OnLowButtonClick();
                break;
        }
    }

    public string GetCurrentGraphicsQuality()
    {
        return currentGraphicsQuality;
    }
}