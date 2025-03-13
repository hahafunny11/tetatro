using System;
using System.Collections;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Random = System.Random;
public class BlockCode : MonoBehaviour
{
    public Vector3 rotationPoint;//all tetris pieces have a set point they rotate around
    private float previousTime;
    public float startFallTime = 0.8f;
    public float fallTime = 0.8f;
    public static int height = 23;//grid size, if any blocks are placed in top 2, you lose.
    public static int width = 10;
    public double scoreGain = 0;
    public int rand = 0;
    Random random = new Random();
    private static Transform[,] grid = new Transform[width, height]; //collision
    public AudioSource blockdropsfx;
    public AudioSource blast;
    public AudioSource lineclear;
    //public 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
        AudioSource[] audioSource = GetComponents<AudioSource>();
        blockdropsfx = audioSource[0];
        blast = audioSource[1];
        lineclear = audioSource[2];
        if (!ValidMove())
        {
            transform.position -= new Vector3(0, -1, 0);
        }
        if (GlobalVariables.currentBoss == "Hermes" && GlobalVariables.currentLevel % 3 == 2)
        {
            startFallTime = 0.6f;
        }
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        fallTime = startFallTime - GlobalVariables.gamespeedMult;
        if (transform.childCount == 0) Destroy(this.gameObject); //if all of the parts of a block are removed, delete the block parent. (otherwise the erased blocks would be wasting memory...)
        if (!IsPlacedDown)
        {
            if (Input.GetKeyDown(KeyCode.A))//moveleft
            {
                transform.position += new Vector3(-1, 0, 0);
                if (!ValidMove())
                    transform.position -= new Vector3(-1, 0, 0);

            }
            else if (Input.GetKeyDown(KeyCode.D))//moveright
            {
                transform.position += new Vector3(1, 0, 0);
                if (!ValidMove())
                    transform.position -= new Vector3(1, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightArrow)) //rotate clockwise
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                if (!ValidMove())
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftArrow)) //rotate counterclockwise
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                if (!ValidMove())
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
            if (Time.time - previousTime > (Input.GetKey(KeyCode.S) ? fallTime / 10 : fallTime)) //blockfalling code, moves faster if holding s
            {
                transform.position += new Vector3(0, -1, 0);
                if (!ValidMove())
                { //placeblock
                    transform.position -= new Vector3(0, -1, 0);
                    AddToGrid();
                    LinesCleared = CheckScore();
                    CheckForLines();
                    CheckIfDead();

                    if (LinesCleared == 1)
                    {
                        scoreGain = 40;
                        if (GlobalVariables.items.Contains("MinMaxTet"))
                        {
                            scoreGain /= 10;
                        }
                        if (GlobalVariables.items.Contains("InARowBonus"))
                        {
                            scoreGain *= GlobalVariables.chain;
                        }
                        if (GlobalVariables.items.Contains("SingleChain"))
                        {
                            scoreGain *= GlobalVariables.sChain;
                        }
                        scoreGain *= GlobalVariables.baseMult * GlobalVariables.singleMult;
                        Scoring.instance.AddPoint((int)scoreGain);
                        GlobalVariables.clearedLines += 1;

                    }
                    else if (LinesCleared == 2)
                    {
                        scoreGain = 100;
                        if (GlobalVariables.items.Contains("MinMaxTet"))
                        {
                            scoreGain /= 10;
                        }
                        if (GlobalVariables.items.Contains("InARowBonus"))
                        {
                            scoreGain *= GlobalVariables.chain;
                        }
                        if (GlobalVariables.items.Contains("SingleChain") && GlobalVariables.items.Contains("2Singles"))
                        {
                            scoreGain *= GlobalVariables.sChain;
                        }
                        scoreGain *= GlobalVariables.baseMult * GlobalVariables.doubleMult;
                        Scoring.instance.AddPoint((int)scoreGain);
                        GlobalVariables.clearedLines += 2;

                    }
                    else if (LinesCleared == 3)
                    {
                        scoreGain = 300;
                        if (GlobalVariables.items.Contains("MinMaxTet"))
                        {
                            scoreGain /= 10;
                        }
                        if (GlobalVariables.items.Contains("InARowBonus"))
                        {
                            scoreGain *= GlobalVariables.chain;
                        }
                        if (GlobalVariables.items.Contains("TripleTriple"))
                        {
                            rand = random.Next(0, 3);
                            if (rand == 2)
                            {
                                scoreGain *= 3;
                            }
                        }
                        scoreGain *= GlobalVariables.baseMult * GlobalVariables.tripleMult;
                        Scoring.instance.AddPoint((int)scoreGain);
                        GlobalVariables.clearedLines += 3;

                    }
                    else if (LinesCleared == 4)
                    {
                        scoreGain = 1200;
                        if (GlobalVariables.items.Contains("MinMaxTet"))
                        {
                            scoreGain *= 3;
                        }
                        if (GlobalVariables.items.Contains("InARowBonus"))
                        {
                            scoreGain *= GlobalVariables.chain;
                        }
                        scoreGain *= GlobalVariables.baseMult * GlobalVariables.tetrisMult;
                        Scoring.instance.AddPoint((int)scoreGain);
                        GlobalVariables.clearedLines += 4;

                    }
                    if (GlobalVariables.clearedLines >= 10 && LinesCleared != 0)
                    {
                        if (GlobalVariables.gamespeedMult < .3f){
                            GlobalVariables.gamespeedMult += .1f;
                        }
                        else if (GlobalVariables.gamespeedMult < .45f){
                            GlobalVariables.gamespeedMult += .075f;
                        }
                        else if (GlobalVariables.gamespeedMult < .7f){
                            GlobalVariables.gamespeedMult += .05f;
                        }
                        GlobalVariables.gamespeedMult *= 100;
                        Math.Round(GlobalVariables.gamespeedMult);
                        GlobalVariables.gamespeedMult /= 100;
                        GlobalVariables.clearedLines -= 10;
                    }

                    if (LinesCleared > 0)
                    {
                        GlobalVariables.chain += .5;
                        if (LinesCleared == 1 || LinesCleared == 2 && GlobalVariables.items.Contains("2Singles"))
                        {
                            GlobalVariables.sChain += 1;
                            if (LinesCleared == 2 && GlobalVariables.items.Contains("2Singles"))
                            {
                                GlobalVariables.sChain += 1;
                            }
                        }
                        else
                        {
                            GlobalVariables.sChain = 1;
                        }
                        if (GlobalVariables.currentLevel % 3 == 2 & GlobalVariables.currentBoss == "Poseidon")
                        {
                            DeleteColumn(0);
                            DeleteColumn(width-1);
                        }
                    }
                    else
                    {
                        GlobalVariables.chain = 1;
                        GlobalVariables.sChain = 1;
                    }
                    if (LinesCleared == 0)
                    {
                        blockdropsfx.Play();
                    }
                    else
                    {
                        lineclear.Play();
                    }
                    LinesCleared = 0;
                    scoreGain = 0;
                    IsPlacedDown = true;
                    FindObjectOfType<SpawnBlock>().NewBlock();
                }
                previousTime = Time.time;
            }
            if (Input.GetKeyDown(KeyCode.P))   //debug tool for testing game speed
            {
                if (GlobalVariables.gamespeedMult < .5f)
                {
                    GlobalVariables.gamespeedMult += .1f;
                }
                else if (GlobalVariables.gamespeedMult < .65f)
                {
                    GlobalVariables.gamespeedMult += .075f;
                }
                else if (GlobalVariables.gamespeedMult < .75f)
                {
                    GlobalVariables.gamespeedMult += .05f;
                }
                GlobalVariables.gamespeedMult *= 100;
                Math.Round(GlobalVariables.gamespeedMult);
                GlobalVariables.gamespeedMult /= 100;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                while (IsPlacedDown == false)
                {
                    transform.position += new Vector3(0, -1, 0);
                    if (!ValidMove())
                    { //placeblock
                        transform.position -= new Vector3(0, -1, 0);
                        AddToGrid();
                        LinesCleared = CheckScore();
                        CheckForLines();
                        CheckIfDead();

                        if (LinesCleared == 1)
                        {
                            scoreGain = 40;
                            if (GlobalVariables.items.Contains("MinMaxTet"))
                            {
                                scoreGain /= 10;
                            }
                            if (GlobalVariables.items.Contains("InARowBonus"))
                            {
                                scoreGain *= GlobalVariables.chain;
                            }
                            if (GlobalVariables.items.Contains("SingleChain"))
                            {
                                scoreGain *= GlobalVariables.sChain;
                            }
                            scoreGain *= GlobalVariables.baseMult * GlobalVariables.singleMult;
                            Scoring.instance.AddPoint((int)scoreGain);
                            GlobalVariables.clearedLines += 1;

                        }
                        else if (LinesCleared == 2)
                        {
                            scoreGain = 100;
                            if (GlobalVariables.items.Contains("MinMaxTet"))
                            {
                                scoreGain /= 10;
                            }
                            if (GlobalVariables.items.Contains("InARowBonus"))
                            {
                                scoreGain *= GlobalVariables.chain;
                            }
                            if (GlobalVariables.items.Contains("SingleChain") && GlobalVariables.items.Contains("2Singles"))
                            {
                                scoreGain *= GlobalVariables.sChain;
                            }
                            scoreGain *= GlobalVariables.baseMult * GlobalVariables.doubleMult;
                            Scoring.instance.AddPoint((int)scoreGain);
                            GlobalVariables.clearedLines += 2;

                        }
                        else if (LinesCleared == 3)
                        {
                            scoreGain = 300;
                            if (GlobalVariables.items.Contains("MinMaxTet"))
                            {
                                scoreGain /= 10;
                            }
                            if (GlobalVariables.items.Contains("InARowBonus"))
                            {
                                scoreGain *= GlobalVariables.chain;
                            }
                            if (GlobalVariables.items.Contains("TripleTriple"))
                            {
                                rand = random.Next(0, 3);
                                if (rand == 2)
                                {
                                    scoreGain *= 3;
                                }
                            }
                            scoreGain *= GlobalVariables.baseMult * GlobalVariables.tripleMult;
                            Scoring.instance.AddPoint((int)scoreGain);
                            GlobalVariables.clearedLines += 3;

                        }
                        else if (LinesCleared == 4)
                        {
                            scoreGain = 1200;
                            if (GlobalVariables.items.Contains("MinMaxTet"))
                            {
                                scoreGain *= 3;
                            }
                            if (GlobalVariables.items.Contains("InARowBonus"))
                            {
                                scoreGain *= GlobalVariables.chain;
                            }
                            scoreGain *= GlobalVariables.baseMult * GlobalVariables.tetrisMult;
                            Scoring.instance.AddPoint((int)scoreGain);
                            GlobalVariables.clearedLines += 4;

                        }
                        if (GlobalVariables.clearedLines >= 10 && LinesCleared != 0)
                        {
                            if (GlobalVariables.gamespeedMult < .3f)
                            {
                                GlobalVariables.gamespeedMult += .1f;
                            }
                            else if (GlobalVariables.gamespeedMult < .45f)
                            {
                                GlobalVariables.gamespeedMult += .075f;
                            }
                            else if (GlobalVariables.gamespeedMult < .7f)
                            {
                                GlobalVariables.gamespeedMult += .05f;
                            }
                            GlobalVariables.gamespeedMult *= 100;
                            Math.Round(GlobalVariables.gamespeedMult);
                            GlobalVariables.gamespeedMult /= 100;
                            GlobalVariables.clearedLines -= 10;
                        }

                        if (LinesCleared > 0)
                        {
                            GlobalVariables.chain += .5;
                            if (LinesCleared == 1 || LinesCleared == 2 && GlobalVariables.items.Contains("2Singles"))
                            {
                                GlobalVariables.sChain += 1;
                                if (LinesCleared == 2 && GlobalVariables.items.Contains("2Singles"))
                                {
                                    GlobalVariables.sChain += 1;
                                }
                            }
                            else
                            {
                                GlobalVariables.sChain = 1;
                            }
                            if (GlobalVariables.currentLevel % 3 == 2 & GlobalVariables.currentBoss == "Poseidon")
                            {
                                DeleteColumn(0);
                                DeleteColumn(width - 1);
                            }
                        }
                        else
                        {
                            GlobalVariables.chain = 1;
                            GlobalVariables.sChain = 1;
                        }
                        if (LinesCleared == 0)
                        {
                            blockdropsfx.Play();
                        }
                        else
                        {
                            lineclear.Play();
                        }
                        LinesCleared = 0;
                        scoreGain = 0;
                        IsPlacedDown = true;
                        FindObjectOfType<SpawnBlock>().NewBlock();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (GlobalVariables.items.Contains("Nuke"))
                {
                    blast.Play();
                    AddToGrid();
                    for (int i = height - 1; i >= 0; i--)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if (HasBlock(j, i))
                            {
                                scoreGain += 30;
                            }
                        }
                    }
                    scoreGain *= GlobalVariables.chain;
                    scoreGain *= GlobalVariables.baseMult;
                    for (int i = height - 1; i >= 0; i--)
                    {
                        if (LineNotEmpty(i))
                        {
                            GlobalVariables.chain += .5;
                        }
                    }
                    NukeCheck();
                    Scoring.instance.AddPoint((int)scoreGain);
                    scoreGain = 0;
                    GlobalVariables.items.Remove("Nuke");
                    GlobalVariables.availableShopItems.Add("Nuke");
                    FindObjectOfType<SpawnBlock>().NewBlock();
                    IsPlacedDown = true;
                }
            }

        }
    }

    void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    void NukeCheck()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (PlsWork(i))
            {
                DeleteBlock(i);
            }

        }
    }

    bool PlsWork(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] != null)
            {
                return true;
            }
        }
        return false;
    }
    bool HasBlock(int j, int i)
    {
            if (grid[j, i] != null)
            {
                return true;
            }
        return false;
    }

    bool LineNotEmpty(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] != null)
            {
                return true;
            }

        }
        return false;
    }

    bool HasLine(int i) //checks if a row is full
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j,i] == null)
            {
                return false;
            }

        }
        return true;
    }

    private bool IsPlacedDown = false;

    public int LinesCleared = 0;

    public int CheckScore()
    {
        int c = 0;
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i)) {
                c++;
            }
            
        }
        return c;
    }

    void DeleteLine(int i) //delete cleared rows
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
            

        }
    }

    void DeleteBlock(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] != null)
            {
                Destroy(grid[j, i].gameObject);
                grid[j, i] = null;
            }
        }
    }

    void DeleteColumn(int j)
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (grid[j, i] != null)
            {
                Destroy(grid[j, i].gameObject);
                grid[j, i] = null;
            }
        }
    }

    void RowDown(int i) // move rows down
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j,y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    void CheckIfDead() //checks if any blocks are in the top two rows, if so, you die.
    {
        for(int y = height - 3; y < height-1; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    SceneManager.LoadSceneAsync("GameOver");
                    //SceneManager.UnloadSceneAsync("Tetris");
                }
            }
        }
    }
    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }
    bool ValidMove() //checks if a given move is valid
    {
        foreach (Transform children in transform) //browse all children
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x); //round x and y positions
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height) //if one is bigger than the grid size
            {
                return false;
            }
            if (grid[roundedX,roundedY] != null) //if position is taken already
            {
                return false;
            }
        }
        return true;
    }

    
}
