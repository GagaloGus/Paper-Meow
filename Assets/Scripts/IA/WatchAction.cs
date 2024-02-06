using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Actions/Watch")]
public class WatchAction : Action
{
    [Tooltip("Distancia de vision")]
    public float distance = 10;

    [Tooltip("Angulo de vision")]
    public float angle = 30;

    [Tooltip("Altura de los ojos del personaje")]
    public float height;

    public override void StartAction()
    {
        base.StartAction();
    }

    public override bool Check(GameObject owner)
    {
        //sube o baja el punto de vision
        Vector3 eyePoint = owner.transform.position + Vector3.up * height;

        //direccion entre el player y el enemigo
        Vector3 direction = target.transform.position - eyePoint;

        //coje el angulo entre la direccion y el enemigo
        float watchAngle = Vector3.Angle(direction, owner.transform.forward);

        //si el angulo esta dentro del angulo y distancia devuelve true
        if(-angle/2 < watchAngle && 
            watchAngle < angle/2 && 
            Vector3.Distance(eyePoint, target.transform.position) <= distance)
        {
            return true;
        }
        return false;
        
    }
    public override void DrawGizmo(GameObject owner)
    {
        Vector3 eyePoint = owner.transform.position + Vector3.up * height;

        Vector3 bottomLeft = Quaternion.Euler(-angle /2, -angle /2, 0) * owner.transform.forward * distance + eyePoint;
        Vector3 bottomRight = Quaternion.Euler(-angle /2, angle /2, 0) * owner.transform.forward * distance + eyePoint;
        Vector3 topRight = Quaternion.Euler(angle /2, angle /2, 0) * owner.transform.forward * distance + eyePoint;
        Vector3 topLeft = Quaternion.Euler(angle /2, -angle /2, 0) * owner.transform.forward * distance + eyePoint;
        
        //aristas de la piramide
        Gizmos.DrawRay(eyePoint, bottomLeft - eyePoint);
        Gizmos.DrawRay(eyePoint, bottomRight - eyePoint);
        Gizmos.DrawRay(eyePoint, topRight - eyePoint);
        Gizmos.DrawRay(eyePoint, topLeft - eyePoint);

        //base
        Gizmos.DrawRay(topLeft, topRight - topLeft);
        Gizmos.DrawRay(topLeft, bottomLeft - topLeft);
        Gizmos.DrawRay(bottomRight, topRight - bottomRight);
        Gizmos.DrawRay(bottomRight, bottomLeft - bottomRight);
    }

}
