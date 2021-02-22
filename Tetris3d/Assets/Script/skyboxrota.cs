using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyboxrota : MonoBehaviour
{
    [SerializeField]
    float rota_speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time*rota_speed);
       if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
