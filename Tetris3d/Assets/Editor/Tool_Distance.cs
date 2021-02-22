using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Tool_Distance : EditorWindow
// Start is called before the first frame update
{

    GameObject Obj1;
    GameObject Obj2;
    string text;
    MessageType type;

    [MenuItem("Tools/Ruler")]
    static void Init()
    {
        Tool_Distance window = (Tool_Distance)EditorWindow.GetWindow(typeof(Tool_Distance));
        window.Show();
    }
    void OnGUI()
    {
        GUILayout.Label("Put 2 gameobject for calcule", EditorStyles.boldLabel);
        Obj1 = (GameObject)EditorGUILayout.ObjectField(Obj1, typeof(GameObject), true);
        Obj2 = (GameObject)EditorGUILayout.ObjectField(Obj2, typeof(GameObject), true);
        if (Obj1 == null && Obj2 == null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Obj1.transform.position, Obj2.transform.position);
        }
            if (GUILayout.Button("Search!"))
        {
            if (Obj1 == null&&Obj2==null)
            {
                text = "object Missing";
                type = MessageType.Warning;

            }
            else
            {
                text = (Vector3.Distance(Obj1.transform.position,Obj2.transform.position)).ToString();
                type = MessageType.Info;
            }
        }
        EditorGUILayout.HelpBox(text,type,true);
        EditorGUILayout.EndToggleGroup();
        EditorGUILayout.EndHorizontal();
        
    }

}
