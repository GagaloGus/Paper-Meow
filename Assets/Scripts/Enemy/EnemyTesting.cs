using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyTesting : MonoBehaviour
{
    [SerializeField] bool died = false;
    SkoStats player;
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        player = FindObjectOfType<SkoStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        print($"au {damage}");

        StopAllCoroutines();
        material.color = Color.white;
        StartCoroutine(ChangeColDamage());
    }

    IEnumerator ChangeColDamage()
    {

        for (float i = 0; i <= 1; i+= 0.01f)
        {
            material.color = new Color(1, i, i);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void OnTriggerEnter(Collider trigger)
    {
        Weapon weapon = trigger.gameObject.GetComponentInParent<Weapon>();
        if (weapon && !died)
        {
            TakeDamage(player.currentStats.ATK * weapon.weaponData.damageMultiplier);
        }
    }
}
