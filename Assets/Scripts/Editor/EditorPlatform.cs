using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorPlatform : EditorWindow
{
    private float xValue;
    private int count;

    GameObject parent;

    [MenuItem("Window/PlatformEditor")]

    public static void ShowWindow()
    {
        GetWindow<EditorPlatform>("PlatformEditor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create From Selected Object :", EditorStyles.boldLabel);
        GUILayout.Label("X Value :", EditorStyles.boldLabel);
        xValue = EditorGUILayout.FloatField(xValue);
        GUILayout.Label("Count :", EditorStyles.boldLabel);
        count = EditorGUILayout.IntField(count);
        parent = GameObject.FindGameObjectWithTag("Platform");

        if (GUILayout.Button("Create"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                for (int i = 0; i < count; i++)
                {
                    GameObject g = Instantiate(obj);
                    Vector3 pos = obj.transform.position;
                    pos.x += xValue * (i + 1);
                    g.transform.position = pos;
                    g.transform.SetParent(parent.transform);
                }
            }
        }
    }
}
