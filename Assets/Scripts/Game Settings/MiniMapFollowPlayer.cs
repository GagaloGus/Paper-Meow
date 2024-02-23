using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapFollowPlayer : MonoBehaviour
{
    [Header("Follow Axis")]
    public bool X;
    public bool Z;

    GameObject player;
    Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<SkoController>().gameObject;        
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            (X ? player.transform.position.x : originalPos.x),
            originalPos.y,
            (Z ? player.transform.position.z : originalPos.z)
            );
    }
}
