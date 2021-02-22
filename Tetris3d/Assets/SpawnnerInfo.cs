using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnnerInfo : MonoBehaviour
{
    Spawner SP;
    [SerializeField]
    Sprite[] list_sprite;
    [SerializeField]
    Image new_tetris;
    public 
    // Start is called before the first frame update
    void Start()
    {
        SP = FindObjectOfType<Spawner>();

    }

    // Update is called once per frame
    void Update()
    {
        new_tetris.sprite=list_sprite[ SP.i];
    }
}
