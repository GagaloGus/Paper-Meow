using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected GameObject player;

    public WeaponData weaponData;

    protected virtual void Start()
    {
        player = GetComponentInParent<SkoController>().gameObject;
    }
    
    public void UpdateWeapon()
    {
        if(weaponData.itemData.weaponType != WeaponType.Garra)
        {
            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }

            Instantiate(weaponData.itemData.itemPrefab, transform);
        }
    }

}

[System.Serializable]
public class WeaponData
{
    public string name;
    public float damageMultiplier;
    public Item itemData;
}
