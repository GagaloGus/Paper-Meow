using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        for (int i = 0; i < texturePacks.Length; i++)
        {
            texturePacks[i].name = $"[{i}] {texturePacks[i].name}";
        }   

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


}
