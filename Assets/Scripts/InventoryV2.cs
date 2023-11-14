using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryV2 : MonoBehaviour
{
    public static InventoryV2 instance;
    public int inventorySize = 9; // Tamaño del inventario
    public GameObject[] slots; // Arreglo de objetos que representan cada espacio en el inventario

    public List<GameObject> items; // Lista de items en el inventario

    private void Start()
    {
        items = new List<GameObject>(); // Inicializar la lista de items

        // Inicializar el arreglo de slots
        if (transform.childCount >= inventorySize)
        {
            slots = new GameObject[inventorySize];
            for (int i = 0; i < inventorySize; i++)
            {
                slots[i] = transform.GetChild(i).gameObject; // Asignar los hijos del inventario como slots
            }
        }
        else
        {
            Debug.LogError("El número de slots de inventario es mayor que el número de hijos del objeto. Asegúrate de que el objeto tenga suficientes hijos para los slots de inventario.");
        }
    }
    // Añadir item al inventario
    public void AddItem(GameObject item)
    {
        if (items.Count < inventorySize)
        {
            items.Add(item);
            UpdateUI();
            Debug.Log("Item agregado");
        }
        else
        {
            Debug.Log("Inventario lleno");
        }
    }

    // Remover item del inventario
    public void RemoveItem(GameObject item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            UpdateUI();
        }
        else
        {
            Debug.Log("Item no encontrado en el inventario");
        }
    }

    // Actualizar la interfaz de usuario del inventario
    private void UpdateUI()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (i < items.Count)
            {
                slots[i].GetComponent<MeshRenderer>().enabled = true; // Mostrar el item en el slot correspondiente
            }
            else
            {
                slots[i].GetComponent<MeshRenderer>().enabled = false; // Ocultar el slot vacío
            }
        }
    }
}
