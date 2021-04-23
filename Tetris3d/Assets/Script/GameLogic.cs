using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public GameObject ActiveTetrios;
    [Range(0.01f, 0.8f)]
    public float TransparencyValue=0;
    public bool IsLose=false;
    public GameObject VfxDeleteLine;
    public static Transform[,,] GridPlay=new Transform[5,15,5];
    public bool IsDestroy=false;
    public int ScoreByLines=1000;
    public Text Tscore;
    public int Score = 0;
    public GameObject ScoreHighlight;
    float timer = 1;
    float tim=0;
    public bool PauseMode = false;
    public bool GameOver = false;
    public int HighScore = 0000;
    public AudioSource AS;
    public AudioClip AC;
    private void Start()
    {
        AS = GetComponent<AudioSource>();
        if (ScoreHighlight != null)
            ScoreHighlight.SetActive(false);
    }
    void Update()
    {
        if(SceneManager.GetActiveScene()==SceneManager.GetSceneByBuildIndex(0))
        {
            if (HighScore < Score)
                HighScore = Score;
            GameObject b=GameObject.FindGameObjectWithTag("Finish");
                b.GetComponent<Text>().text= "HighScore = " + HighScore;
        }
        if (PauseMode||GameOver)
        {
            Time.timeScale = 0;
            if(ScoreHighlight!=null)
                ScoreHighlight.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            Time.timeScale = 1;
            if (ScoreHighlight != null)
                ScoreHighlight.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseMode = !PauseMode;
        }
        
        if(tim>timer)
        {
            Score += 5;
            tim = 0;
        }
        CheckForLines();
        if (IsDestroy == true)
            Destroying();
        if(Tscore!=null)
            Tscore.text = "Score : \n" + Score;
    }
    private void FixedUpdate()
    {
        tim += Time.deltaTime;
    }
    void CheckForLines()
    {
        for (int i = 0; i < 15; i++)
        {
            if(HasLines(i))
            {
                AS.PlayOneShot(AC);
                DeleteLines(i);
                RowDown(i);

            }
        }
    }
    bool HasLines(int i)
    {
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                if(GridPlay[x,i,y]==null)
                {
                    return false;
                }
                

            }
        }
        return true;
    }
    void DeleteLines(int i)
    {
        Score += ScoreByLines;
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                GameObject b =Instantiate(VfxDeleteLine);
                b.transform.position = GridPlay[x, i, y].gameObject.transform.position;
                b.GetComponent<ParticleSystemRenderer>().material= GridPlay[x, i, y].gameObject.GetComponent<MeshRenderer>().material;
                Destroy(b, 5);
                Destroy(GridPlay[x, i, y].gameObject);
                GridPlay[x, i, y] = null;
                IsDestroy = true;
            }
        }
    }
    void RowDown(int i)
    {
        for (int z = i; z < 15; z++)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (GridPlay[x, z, y] != null)
                    {

                        GridPlay[x, z - 1, y] = GridPlay[x, z, y];
                        GridPlay[x, z, y] = null;
                        GridPlay[x, z - 1, y].transform.position -= new Vector3(0, 1, 0);

                    }
                }
            }
        }
    }
    void Destroying()
    {
        for (int i = 1; i < 15; i++)
        {

            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (GridPlay[x, i, y] != null)
                    {
                        if (GridPlay[x, i - 1, y] == null)
                        {
                            GridPlay[x, i - 1, y] = GridPlay[x, i, y];
                            GridPlay[x, i, y] = null;
                            GridPlay[x, i - 1, y].transform.position -= new Vector3(0, 1, 0);
                            Destroying();

                        }
                    }

                }
            }
        }
        IsDestroy = false;
    }

}
