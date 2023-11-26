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

public class ChangeTextureAnimEvent : MonoBehaviour
{
    public AnimationPacks[] texturePacks;

    Material frontMat, backMat;

    private void Start()
    {
        GameObject modelo = transform.Find("modelo").gameObject;
        
        frontMat = modelo.GetComponent<MeshRenderer>().materials[0];
        backMat = modelo.GetComponent<MeshRenderer>().materials[1];

    }

    public void ChangeTexture(string textureName)
    {
        //busca el respectivo pack de texturas segun lo que escribamos en el textureName
        AnimationPacks textures = Array.Find(texturePacks, x => x.name == textureName);

        //cambia el albedo del material de adelante y atras
        frontMat.SetTexture("_MainTex", textures.front);
        backMat.SetTexture("_MainTex", textures.back);

    }


}
