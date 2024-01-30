using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayectoryLine : MonoBehaviour
{
    public GameObject selectedArrow;

    [Header("Trayectory Line Smoothness/Length")]
    [SerializeField] int segmentCount = 50;
    [SerializeField] float curveLenght = 3;
    [SerializeField] float speed = 10;

    Vector3[] segments;
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //inicializa los segmentos
        segments = new Vector3[segmentCount];

        //Coje el line renderer y establece su numero de puntos
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segmentCount;
    }

    // Update is called once per frame
    void Update()
    {
        //punto de inicio
        Vector3 startPos = transform.position;
        segments[0] = startPos;
        lineRenderer.SetPosition(0, startPos);

        //velocidad
        Vector3 startVelocity = transform.forward * speed;

        for(int i = 1; i < segmentCount; i++)
        {
            //variacion de tiempo
            float timeOffset = i * Time.fixedDeltaTime * curveLenght;

            //variacion por gravedad
            Vector3 gravityOffset = 
                Vector3.up * 
                0.5f * 
                selectedArrow.GetComponent<ConstantForce>().force.y * 
                Mathf.Pow(timeOffset, 2);

            //establecer la posicion de los puntos de la linea
            segments[i] = segments[0] + startVelocity * timeOffset + gravityOffset;
            lineRenderer.SetPosition(i, segments[i]);

        }
    }
}
