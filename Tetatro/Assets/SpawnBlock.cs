using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    public GameObject[] Blocks;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewBlock(); 
    }

    // Update is called once per frame
    public void NewBlock()
    {
        Instantiate(Blocks[Random.Range(0, Blocks.Length)], transform.position, Quaternion.identity);
    }
}
