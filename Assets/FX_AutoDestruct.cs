using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class FX_AutoDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hasLightingEffects;
    public GameObject lightVFX;

    void OnEnable()
    {
        Invoke("Deactivate", 1);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

[CustomEditor(typeof(FX_AutoDestruct))]
public class ScriptEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as FX_AutoDestruct;

        myScript.hasLightingEffects = GUILayout.Toggle(myScript.hasLightingEffects, "hasLightingEffects");

        if (myScript.hasLightingEffects)
            myScript.lightVFX = (GameObject)EditorGUILayout.ObjectField("Fire Effect", myScript.lightVFX, typeof(GameObject), true);

    }
}
