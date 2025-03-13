using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    public GameObject[] Blocks;
    public GameObject[] AphBlocks;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewBlock(); 
    }

    // Update is called once per frame
    public void NewBlock()
    {
        if (GlobalVariables.currentBoss == "Aphrodite" && GlobalVariables.currentLevel % 3 == 2)
        {
            Instantiate(AphBlocks[Random.Range(0, AphBlocks.Length)], transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(Blocks[Random.Range(0, Blocks.Length)], transform.position, Quaternion.identity);
        }
    }
}
