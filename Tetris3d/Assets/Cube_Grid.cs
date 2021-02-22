using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Grid : MonoBehaviour
{
    public Material M1;
    public Material M2;
    // Start is called before the first frame update
    void Start()
    {
        M1 = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
