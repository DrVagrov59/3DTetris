using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tetriomino : MonoBehaviour
{
    public Material EndMaterial;
    public List<Material> materialcolor = new List<Material>();
    public test_cam TC;
    public Vector3 rotationPoint;
    public static int height = 15;
    public List<GameObject> ChildGamObj=new List<GameObject>();
    public static int weight = 5;
    public static Transform[,,] grid = new Transform[weight,weight,height];
    Vector3[] Left = new Vector3[4] {new Vector3(-1,0,0), new Vector3(0, 0, 1), new Vector3(1, 0, 0),new Vector3(0, 0, -1)};
    Vector3[] Right = new Vector3[4] { new Vector3(1, 0, 0), new Vector3(0, 0, -1), new Vector3(-1, 0, 0), new Vector3(0, 0, 1) };
    Vector3[] Up = new Vector3[4] { new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(0, 0, -1), new Vector3(-1, 0, 0) };
    Vector3[] Down = new Vector3[4] { new Vector3(0, 0, -1), new Vector3(-1, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0) };
    private float previousTime;
    public float fallTime = 0.8f;
    public Vector3 PreviousPos;
    public Quaternion PreviousRot;
    bool GizmoPrev = true;
    GameLogic GL;
    List<GameObject> listgiz=new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GL = FindObjectOfType<GameLogic>();
        int num = Random.Range(0, materialcolor.Count);
        for (int i = 0; i < transform.childCount;  i++)
        {
            transform.GetChild(i).GetComponent<MeshRenderer>().material = materialcolor[num];
        }
        
        GameObject[] allGame = (GameObject[])FindObjectsOfType(typeof(GameObject));
        foreach (GameObject item in allGame)
        {
            if (item.GetComponent("test_cam"))
                TC = item.GetComponent<test_cam>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        Move_Tetri();
        Rotation_Tetri();
        Go_Down();


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
            transform.Rotate(90, 0,0);
            valid_move_move();
        }

    }

    private void Move_Tetri()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            move_valid();
            transform.position += Left[TC.pos_cam];
            valid_move_move();

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            move_valid();
            transform.position += Right[TC.pos_cam];
            valid_move_move();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            move_valid();
            transform.position += Up[TC.pos_cam];
            valid_move_move();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            move_valid();
            transform.position += Down[TC.pos_cam];
            valid_move_move();
        }
    }

    private void valid_move_move()
    {
        if (!ValidMove())
        {
            transform.position = PreviousPos;
            transform.rotation = PreviousRot;
            
        }
    }
    private void move_valid()
    {
        
        PreviousPos = transform.position;
        PreviousRot = transform.rotation;

    }

    private void Go_Down()
    {

        if (Time.time - previousTime > (Input.GetKey(KeyCode.Space) ? fallTime / 10 : fallTime))
        {
            move_valid();
            transform.position += new Vector3(0, -1, 0);
            previousTime = Time.time;
            
            if (!ValidMove())
            {
                transform.position = PreviousPos;
                Add_Grid();
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<MeshRenderer>().material = EndMaterial;
                }
                GizmoPrev = false;
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
                }
                this.enabled = false;
                if(GL.is_lose==false)
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
            GameLogic.Grid_Play[roundedX, roundedY, roundedZ]=transform.GetChild(i);
        }
    }
        private bool ValidMove()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            int roundedX = Mathf.RoundToInt(transform.GetChild(i).transform.position.x);
            int roundedY = Mathf.RoundToInt(transform.GetChild(i).transform.position.y);
            int roundedZ = Mathf.RoundToInt(transform.GetChild(i).transform.position.z);

            if(roundedX<0||roundedX>=weight||roundedY<0||roundedY>=height|| roundedZ < 0 || roundedZ >= weight)
            {
                return false;
            }
            if(GameLogic.Grid_Play[roundedX, roundedY, roundedZ] != null)
            {
                return false;
            }
        }
        return true;

    }


}
