using System;
using UnityEngine;
using UnityEngine.Windows;

public class shopShow : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();

    }
    // Update is called once per frame
    void Update()
    {
        
        string objectName = gameObject.name;
        int numero = -1;
        if (objectName.EndsWith("0"))
        {
            numero = 0;
        }
        else if (objectName.EndsWith("1"))
        {
            numero = 1;
        }
        else if (objectName.EndsWith("2"))
        {
            numero = 2;
        }


            objectName = objectName.Substring(0, objectName.Length - 1);
        if (GlobalVariables.currentShop[numero] == objectName)
        {
            Debug.Log("pepe");
            this.spriteRenderer.enabled = true;
        }
        else
        {
            this.spriteRenderer.enabled = false;
        }
    }
}
