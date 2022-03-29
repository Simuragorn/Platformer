 using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemDB))]
public class ItemDBEditor : Editor
{
    private ItemDB itemDB;

    private void Awake()
    {
        itemDB = (ItemDB)target;
    }
    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("New Item"))
        {
            itemDB.CreateItem();
        }
        if(GUILayout.Button("Remove Item"))
        {
            itemDB.RemoveItem();
        }
        if (GUILayout.Button("<="))
        {
            itemDB.PrevItem();
        }
        if (GUILayout.Button("=>"))
        {
            itemDB.NextItem();
        }
        GUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }
}
