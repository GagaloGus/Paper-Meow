using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public float health;
    [SerializeField] bool died = false;
    SkoStats player;
    Material material;
    TMP_Text vidaText;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        player = FindObjectOfType<SkoStats>();
        vidaText = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(12);
        }

        vidaText.text = $"Vida: {health}";

        if(health <= 0 && !died)
        {
            died = true;
            GetComponent<MeshRenderer>().material.color = Color.black;
            vidaText.color = Color.red;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
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
