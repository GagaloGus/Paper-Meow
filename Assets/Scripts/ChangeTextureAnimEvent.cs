using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public struct AnimationPacks
{
    public string name;
    public Texture2D front;
    public Texture2D back;
}

[RequireComponent(typeof(Animator))]
public class ChangeTextureAnimEvent : MonoBehaviour
{
    public GameObject modelo;
    public AnimationPacks[] texturePacks;

    Material frontMat, backMat;

    private void Start()
    {   
        frontMat = modelo.GetComponent<MeshRenderer>().materials[0];
        backMat = modelo.GetComponent<MeshRenderer>().materials[1];
    }

    public void ChangeTexture(int textureInt)
    {
        //busca el respectivo pack de texturas segun lo que escribamos en el textureName
        AnimationPacks textures = texturePacks[textureInt];

        //cambia el albedo del material de adelante y atras
        frontMat.SetTexture("_MainTex", textures.front);
        backMat.SetTexture("_MainTex", textures.back);

    }

    public void AddNumbersToPacks()
    {
        print("Añadiendo...");
        for (int i = 0; i < texturePacks.Length; i++)
        {
            if (!texturePacks[i].name.StartsWith($"[{i}]"))
            {
                texturePacks[i].name = $"[{i}]{texturePacks[i].name}";
            }
        }
        print($"Numeros añadidos a {gameObject.name}");
    }
}

#if UNITY_EDITOR_WIN
[CustomEditor(typeof(ChangeTextureAnimEvent))]
class BotonTrucoParaAñadirOrderANombres : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ChangeTextureAnimEvent myscript = (ChangeTextureAnimEvent)target;
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Add numbers to texture packs", GUILayout.Height(30)))
        {
            myscript.AddNumbersToPacks();
        }

        GUILayout.EndHorizontal();

    }
}
#endif
