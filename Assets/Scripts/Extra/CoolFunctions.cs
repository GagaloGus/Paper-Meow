using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class CoolFunctions
{
    public static float MapValues(float value, float leftMin, float leftMax, float rightMin, float rightMax)
    {
        return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
    }

    public static Vector3 FlattenVector3(Vector3 value, float newYValue = 0)
    {
        value.y = newYValue;
        return value;
    }

    public static bool IsRightOfVector(Vector3 center, Vector3 direction, Vector3 target)
    {
        Vector3 vectorB = center + direction;

        float result = (target.x - center.x) * (vectorB.z - center.z) - (target.z - center.z) * (vectorB.x - center.x);

        return result >= 0;
    }

    public static Vector3 MultipyVectorValues(Vector3 v1, Vector3 v2)
    {
        Vector3 newVector = new Vector3(
            v1.x * v2.x,
            v1.y * v2.y,
            v1.z * v2.z
            );

        return newVector;
    }

    public static Vector3 VectorMoveAlongTransformAxis(Vector3 v1, Transform axis)
    {
        Vector3 newVector = v1.x * axis.right + v1.y * axis.up + v1.z * axis.forward;

        return newVector;
    }

    public static string FirstLetterUppercase(string texto)
    {
        if (string.IsNullOrEmpty(texto))
        {
            Debug.LogWarning("ta vacio");
            return texto;
        }

        // Convertir la primera letra a mayúscula y concatenar el resto del string
        return char.ToUpper(texto[0]) + texto.Substring(1);
    }

    public static Color ChangeRarityColor(Rarity itemRar)
    {
        Color rarityColor = Color.gray;
        switch (itemRar)
        {
            case Rarity.Common:
                rarityColor = Color.gray;
                break;
            case Rarity.Uncommon:
                rarityColor = Color.green;
                break;
            case Rarity.Rare:
                rarityColor = Color.blue;
                break;
            case Rarity.Epic:
                rarityColor = Color.magenta;
                break;
            case Rarity.Legendary:
                rarityColor = Color.yellow;
                break;
        }

        return rarityColor;
    }

    //// Método para cargar objetos de forma recursiva y devolver una lista de ellos
    //public static List<UnityEngine.Object> ResourcesLoadAllFolders(string folderPath)
    //{
    //    // Lista para almacenar todos los objetos cargados
    //    List<UnityEngine.Object> loadedObjects = new List<UnityEngine.Object>();

    //    // Cargar objetos de la carpeta principal y sus subcarpetas
    //    LoadObjectsRecursive(folderPath, loadedObjects);

    //    // Devolver la lista de objetos cargados
    //    return loadedObjects;
    //}

    //public static List<ScriptableObject> ResourcesLoadAllScriptableObjsFolders(string folderPath)
    //{
    //    // Filtrar los ScriptableObjects de la lista de objetos y devolverlos en una nueva lista
    //    List<ScriptableObject> scriptableObjects = ResourcesLoadAllFolders(folderPath).OfType<ScriptableObject>().ToList();
    //    return scriptableObjects;
    //}

    //#region Cargado de datos recursivo
    //// Método para cargar objetos de forma recursiva
    //private static void LoadObjectsRecursive(string folderPath, List<UnityEngine.Object> loadedObjects)
    //{
    //    // Obtener la ruta completa de la carpeta en el sistema de archivos
    //    string fullPath = Path.Combine(Application.dataPath, "Resources", folderPath);

    //    // Obtener todos los archivos y subcarpetas en la carpeta actual
    //    string[] files = Directory.GetFiles(fullPath);
    //    string[] subFolders = Directory.GetDirectories(fullPath);

    //    // Iterar sobre todos los archivos en la carpeta actual
    //    foreach (string filePath in files)
    //    {
    //        // Comprueba si el archivo es un recurso válido (ScriptableObject o GameObject)
    //        if (IsResource(filePath))
    //        {
    //            // Cargar el recurso y agregarlo a la lista de objetos cargados
    //            UnityEngine.Object loadedObject = Resources.Load<UnityEngine.Object>(GetResourcePath(filePath));
    //            if (loadedObject != null)
    //            {
    //                loadedObjects.Add(loadedObject);
    //            }
    //        }
    //    }

    //    // Llamar a LoadObjectsRecursive para cargar objetos de forma recursiva desde cada subcarpeta
    //    foreach (string subFolder in subFolders)
    //    {
    //        // Llamar recursivamente a LoadObjectsRecursive para cargar objetos de la subcarpeta
    //        string subFolderName = Path.GetFileName(subFolder);
    //        LoadObjectsRecursive(Path.Combine(folderPath, subFolderName), loadedObjects);
    //    }
    //}

    //// Método para verificar si un archivo es un recurso válido (ScriptableObject o GameObject)
    //private static bool IsResource(string filePath)
    //{
    //    // Comprueba si el archivo tiene una extensión de archivo reconocida como recurso
    //    string extension = Path.GetExtension(filePath).ToLower();
    //    return (extension == ".prefab" || extension == ".png" || extension == ".jpg" || extension == ".mp3" || extension == ".asset");
    //}

    //// Método para obtener la ruta del recurso relativa al directorio Resources
    //private static string GetResourcePath(string filePath)
    //{
    //    // Elimina la parte de la ruta del sistema de archivos antes de "Resources/"
    //    int index = filePath.IndexOf("Resources/");
    //    string resourcePath = filePath.Substring(index + 10); // 10 es la longitud de "Resources/"

    //    // Elimina la extensión del archivo
    //    resourcePath = Path.ChangeExtension(resourcePath, null);

    //    // Reemplaza las barras invertidas con barras inclinadas
    //    resourcePath = resourcePath.Replace("\\", "/");

    //    return resourcePath;
    //}
    //#endregion
}

