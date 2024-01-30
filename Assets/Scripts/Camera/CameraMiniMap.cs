using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMiniMap : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<SkoController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new(player.transform.position.x, transform.position.y, player.transform.position.z);
    }
}
