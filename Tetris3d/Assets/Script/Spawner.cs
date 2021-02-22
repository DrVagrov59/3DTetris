using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [HideInInspector]
    public int i=0;
    public GameObject[] Item;
    UnityEngine.Random rd = new UnityEngine.Random();
    
    void Start()
    {
        i = Random.Range(0, Item.Length);
        Spawn();
    }

    public void Spawn()
    {
        
        GameObject b=Instantiate(Item[i]);
        b.transform.position = transform.position;
        FindObjectOfType<GameLogic>().ActiveTetrios = b;
        i = Random.Range(0, Item.Length);
    }

}
