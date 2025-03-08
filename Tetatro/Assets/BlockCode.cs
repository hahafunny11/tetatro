using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class BlockCode : MonoBehaviour
{
    public Vector3 rotationPoint;//all tetris pieces have a set point they rotate around
    private float previousTime;
    public float startFallTime = 0.8f;
    public float fallTime = 0.8f;
    public static int height = 23;//grid size, if any blocks are placed in top 2, you lose.
    public static int width = 10;
    private static Transform[,] grid = new Transform[width, height]; //collision
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
        audioSource = GetComponent<AudioSource>();
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
                    CheckForLines();
                    CheckIfDead();
                    if (LinesCleared == 1)
                    {
                        Scoring.instance.AddPoint(40);
                        GlobalVariables.clearedLines += 1;

                    }
                    else if (LinesCleared == 2)
                    {
                        Scoring.instance.AddPoint(100);
                        GlobalVariables.clearedLines += 2;

                    }
                    else if (LinesCleared == 3)
                    {
                        Scoring.instance.AddPoint(300);
                        GlobalVariables.clearedLines += 3;

                    }
                    else if (LinesCleared == 4)
                    {
                        Scoring.instance.AddPoint(1200);
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
                    LinesCleared = 0;
                    IsPlacedDown = true;
                    audioSource.Play();
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
                            Scoring.instance.AddPoint(40);
                            GlobalVariables.clearedLines += 1;
                        }
                        else if (LinesCleared == 2)
                        {
                            Scoring.instance.AddPoint(100);
                            GlobalVariables.clearedLines += 2;
                        }
                        else if (LinesCleared == 3)
                        {
                            Scoring.instance.AddPoint(300);
                            GlobalVariables.clearedLines += 3;
                        }
                        else if (LinesCleared == 4)
                        {
                            Scoring.instance.AddPoint(1200);
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
                        LinesCleared = 0;
                        IsPlacedDown = true;
                        audioSource.Play();
                        FindObjectOfType<SpawnBlock>().NewBlock();
                    }
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
