using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public float health;
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(12);
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
}
