using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class test_cam : MonoBehaviour
{
    public float cible=90;
    public float cible_actu = 0;
    public int pos_cam = 0;
    public float speed=2;
    bool moving=false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)&&!moving)
        {
            pos_cam += 1;

            moving = true;
            transform.DORotate(transform.eulerAngles+ new Vector3(0, 90, 0),speed).OnComplete(switchMove);
          

        }
        if (Input.GetKeyDown(KeyCode.D) && !moving)
        {
            pos_cam -= 1;



            transform.DORotate(transform.eulerAngles - new Vector3(0, 90, 0), speed).OnComplete(switchMove);



        }
        if (pos_cam<0)
        {
            pos_cam = 3;
        }
        if (pos_cam > 3)
        {
            pos_cam = 0;
        }

    }
    public void switchMove()
    {
        moving = false;
    }



}
