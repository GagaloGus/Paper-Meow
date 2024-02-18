using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjPool : MonoBehaviour
{
    private List<GameObject> pool;

    [Tooltip("Objeto que se va a meter en la pool")]
    public GameObject objectToPool;

    [Tooltip("Tamaño de la pool")]
    public uint size;

    [Tooltip("Deberia expandirse la pool si se vacia?")]
    public bool shouldExpand;

    private void Awake()
    {
        pool = new();

        for(int i = 0; i < size; i++)
        {
            AddGameObjectToPool();
        }
    }

    public GameObject AddGameObjectToPool()
    {
        GameObject obj = Instantiate(objectToPool, transform);
        obj.SetActive(false);
        pool.Add(obj);

        return obj;
    }

    public GameObject GetFirstInactiveGameObject()
    {
        foreach(GameObject obj in pool)
        {
            //si el objeto esta inactivo
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        
        //si la pool deberia expandirse en caso de que este vacia (for no encontro ningun objeto inactivo)
        if(shouldExpand)
        {
            //instanciamos en ejecucion :(
            return AddGameObjectToPool();
        }

        return null;
    }
}
