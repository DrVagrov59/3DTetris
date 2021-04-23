using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class test_cam : MonoBehaviour
{
    public float Cible=90;
    public float CibleActu = 0;
    public int PosCam = 0;
    public float Speed=2;
    bool Moving=false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)&&!Moving)
        {
            PosCam += 1;

            Moving = true;
            transform.DORotate(transform.eulerAngles+ new Vector3(0, 90, 0),Speed).OnComplete(switchMove);
          

        }
        if (Input.GetKeyDown(KeyCode.D) && !Moving)
        {
            PosCam -= 1;



            transform.DORotate(transform.eulerAngles - new Vector3(0, 90, 0), Speed).OnComplete(switchMove);



        }
        if (PosCam<0)
        {
            PosCam = 3;
        }
        if (PosCam > 3)
        {
            PosCam = 0;
        }

    }
    public void switchMove()
    {
        Moving = false;
    }



}
