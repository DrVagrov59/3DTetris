using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyboxrota : MonoBehaviour
{
    [SerializeField]
    float rota_speed = 2f;


    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time*rota_speed);
       if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
