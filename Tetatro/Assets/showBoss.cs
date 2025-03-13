using System;
using UnityEngine;
using UnityEngine.Windows;

public class showBoss : MonoBehaviour
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
        if (objectName == GlobalVariables.currentBoss && GlobalVariables.currentLevel % 3 == 2)
        {
            this.spriteRenderer.enabled = true;
        }
        else
        {
            this.spriteRenderer.enabled = false;
        }
    }
}
