﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public GameObject ActiveTetrios;
    [Range(0.01f, 0.8f)]
    public float transparency_value=0;
    public bool is_lose=false;
    public GameObject VfxDeleteLine;
    public static Transform[,,] Grid_Play=new Transform[5,15,5];
    public bool is_Destroy=false;
    public int ScoreByLines=1000;
    public Text Tscore;
    public int Score = 0;
    float timer = 1;
    float tim=0;
    void Update()
    {
        foreach (Transform item in Grid_Play)
        {
            if(item!=null)
                if(Vector3.Distance(new Vector3(item.transform.position.x, Camera.main.transform.position.y, item.transform.position.z), Camera.main.transform.position)
                < Vector3.Distance(new Vector3(ActiveTetrios.transform.position.x, Camera.main.transform.position.y, ActiveTetrios.transform.position.z), Camera.main.transform.position))
                {
                    item.GetComponent<MeshRenderer>().material.SetFloat("_Transparency", transparency_value);
                }
                else
                {
                    item.GetComponent<MeshRenderer>().material.SetFloat("_Transparency", 01f);
                }
        }
        if(tim>timer)
        {
            Score += 5;
            tim = 0;
        }
        CheckForLines();
        if (is_Destroy == true)
            Destroying();
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
                if(Grid_Play[x,i,y]==null)
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
                b.transform.position = Grid_Play[x, i, y].gameObject.transform.position;
                b.GetComponent<ParticleSystemRenderer>().material= Grid_Play[x, i, y].gameObject.GetComponent<MeshRenderer>().material;
                Destroy(b, 5);
                Destroy(Grid_Play[x, i, y].gameObject);
                Grid_Play[x, i, y] = null;
                is_Destroy = true;
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
                    if (Grid_Play[x, z, y] != null)
                    {

                        Grid_Play[x, z - 1, y] = Grid_Play[x, z, y];
                        Grid_Play[x, z, y] = null;
                        Grid_Play[x, z - 1, y].transform.position -= new Vector3(0, 1, 0);

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
                    if (Grid_Play[x, i, y] != null)
                    {
                        if (Grid_Play[x, i - 1, y] == null)
                        {
                            Grid_Play[x, i - 1, y] = Grid_Play[x, i, y];
                            Grid_Play[x, i, y] = null;
                            Grid_Play[x, i - 1, y].transform.position -= new Vector3(0, 1, 0);
                            Destroying();

                        }
                    }

                }
            }
        }
        is_Destroy = false;
    }
}