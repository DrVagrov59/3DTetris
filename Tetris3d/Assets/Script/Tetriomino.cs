using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tetriomino : MonoBehaviour
{

    public AudioClip FailSound;
    AudioSource AS;

    public Material EndMaterial;
    public List<Material> MaterialColor = new List<Material>();
    public test_cam TC;
    public Vector3 RotationPoint;
    public List<GameObject> ChildGamObj = new List<GameObject>();
    public static int Height = 15;
    public static int Weight = 5;
    public static Transform[,,] Grid = new Transform[Weight, Weight, Height];
    Vector3[] Left = new Vector3[4] { new Vector3(-1, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(0, 0, -1) };
    Vector3[] Right = new Vector3[4] { new Vector3(1, 0, 0), new Vector3(0, 0, -1), new Vector3(-1, 0, 0), new Vector3(0, 0, 1) };
    Vector3[] Up = new Vector3[4] { new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(0, 0, -1), new Vector3(-1, 0, 0) };
    Vector3[] Down = new Vector3[4] { new Vector3(0, 0, -1), new Vector3(-1, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0) };
    private float PreviousTime;
    public float FallTime = 0.8f;
    public Vector3 PreviousPos;
    public Quaternion PreviousRot;
    bool GizmoPrev = true;
    public float DistanceProj=60;
    public int StartChild=0;
    public Projblock[] ProjectionBlock=new Projblock[4];
    GameLogic GL;
    List<GameObject> listgiz = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        FailSound = Resources.Load<AudioClip>("Sound/No");
        AS=gameObject.AddComponent<AudioSource>();
        ProjectionBlock = FindObjectsOfType<Projblock>();
        GL = FindObjectOfType<GameLogic>();
        int num = Random.Range(0, MaterialColor.Count);
        TC = FindObjectOfType<test_cam>();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<MeshRenderer>().material = MaterialColor[num];
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        Move_Tetri();
        Rotation_Tetri();
        Go_Down();
        projection();


    }
    public void Rotation_Tetri()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            move_valid();
            transform.Rotate(0, 90, 0);
            valid_move_move();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            move_valid();
            transform.Rotate(90, 0, 0);
            valid_move_move();
        }

    }

    private void Move_Tetri()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            move_valid();
            transform.position += Left[TC.PosCam];
            valid_move_move();

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            move_valid();
            transform.position += Right[TC.PosCam];
            valid_move_move();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            move_valid();
            transform.position += Up[TC.PosCam];
            valid_move_move();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            move_valid();
            transform.position += Down[TC.PosCam];
            valid_move_move();
        }
    }

    private void valid_move_move()
    {
        if (!ValidMove())
        {
            transform.position = PreviousPos;
            transform.rotation = PreviousRot;
            AS.Stop();
            AS.PlayOneShot(FailSound);
        }
    }
    private void move_valid()
    {

        PreviousPos = transform.position;
        PreviousRot = transform.rotation;

    }
    private void projection()
    {
        LayerMask mask = LayerMask.GetMask("Default");
        for (int i = 0; i < transform.childCount; i++)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.GetChild(i).transform.position, Vector3.down, out hit, Mathf.Infinity,mask))
            {
                if(hit.distance<DistanceProj)
                {

                    DistanceProj = hit.distance;
                    StartChild = i;
                }
            }
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            ProjectionBlock[i].gameObject.transform.position = transform.GetChild(i).transform.position-new Vector3(0,DistanceProj-0.5f,0);
        }
    }

    private void Go_Down()
    {

        if (Time.time - PreviousTime > (Input.GetKey(KeyCode.Space) ? FallTime / 10 : FallTime))
        {
            move_valid();
            transform.position += new Vector3(0, -1, 0);
            PreviousTime = Time.time;
            
            if (!ValidMove())
            {
                transform.position = PreviousPos;
                Add_Grid();
                for (int i = 0; i < transform.childCount; i++)
                {
                    //transform.GetChild(i).GetComponent<MeshRenderer>().material = EndMaterial;
                }
                GizmoPrev = false;
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
                }
                this.enabled = false;
                if(GL.IsLose==false)
                {
                    FindObjectOfType<Spawner>().Spawn(); 
                }
            }
        }
    
        
    }
    private void Add_Grid()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int roundedX = Mathf.RoundToInt(transform.GetChild(i).transform.position.x);
            int roundedY = Mathf.RoundToInt(transform.GetChild(i).transform.position.y);
            int roundedZ = Mathf.RoundToInt(transform.GetChild(i).transform.position.z);
            if(roundedY>Height-1)
            {
                GL.GameOver = true;
            }
            GameLogic.GridPlay[roundedX, roundedY, roundedZ]=transform.GetChild(i);
        }
    }
        private bool ValidMove()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            int roundedX = Mathf.RoundToInt(transform.GetChild(i).transform.position.x);
            int roundedY = Mathf.RoundToInt(transform.GetChild(i).transform.position.y);
            int roundedZ = Mathf.RoundToInt(transform.GetChild(i).transform.position.z);

            if(roundedX<0||roundedX>=Weight||roundedY<0||roundedY>=Height|| roundedZ < 0 || roundedZ >= Weight)
            {
                return false;
            }
            if(GameLogic.GridPlay[roundedX, roundedY, roundedZ] != null)
            {
                return false;
                
            }
        }
        return true;

    }


}
