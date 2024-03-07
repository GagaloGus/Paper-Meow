using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(AnimationEvents))]
[RequireComponent(typeof(CanvasNPC))]
public class NPCBehaviour : MonoBehaviour
{
    public NPCData npcData;
    public GameObject npcGameObject;
    [Header("Ink file")]
    public TextAsset inkJSON;

    Animator animator;
    [SerializeField] GameObject player;
    public float turnSpeed;
    public bool turned;

    private void Awake()
    {
        npcGameObject = gameObject;
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        npcData.originalRot = transform.rotation;
        turned = false;

        player = FindObjectOfType<SkoController>().gameObject;
    }

    public void StartInteraction()
    {
        if (inkJSON != null)
        {
            DialogueManager.instance.StartDialogue(inkJSON, this);
            //StartCoroutine(TurnTowardsTargetPos(player.transform.position));
        }
        else
        {
            Debug.LogWarning($"Npc no tiene un archivo de dialogo: {gameObject.name}");
        }
    }

    public void RotateTowardsPlayer()
    {
        if(!turned)
        StartCoroutine(TurnTowardsTargetPos(player.transform.position));
    }

    public void RotateTowardsOriginalRot()
    {
        StartCoroutine(TurnNPCTowardsTargetRot(npcData.originalRot, true));
    }
    
    public void RotateTowardsRotation(Quaternion rotation)
    {
        StartCoroutine(TurnNPCTowardsTargetRot(rotation, false));
    }

    public void StopRotationCoroutine()
    {
        StopCoroutine(nameof(TurnNPCTowardsTargetRot));
    }

    IEnumerator TurnTowardsTargetPos(Vector3 target)
    {
        if(turnSpeed > 0)
        {
            turned = true;
            Quaternion startRot = transform.rotation;

            Quaternion targetRot = Quaternion.LookRotation(CoolFunctions.FlattenVector3(target) - CoolFunctions.FlattenVector3(transform.position));

            float elapsedTime = 0;
            while (elapsedTime < 1)
            {
                transform.rotation = Quaternion.Slerp(startRot, targetRot, elapsedTime);
                elapsedTime += Time.deltaTime * turnSpeed;
                yield return null;
            }

            transform.rotation = targetRot;
        }
        yield return null;
    }

    IEnumerator TurnNPCTowardsTargetRot(Quaternion targetRot, bool original)
    {
        if(turnSpeed > 0)
        {
            if (original) { turned = false; }

            Quaternion startRot = transform.rotation;

            float elapsedTime = 0;
            while (elapsedTime < 1)
            {
                transform.rotation = Quaternion.Slerp(startRot, targetRot, elapsedTime);
                elapsedTime += Time.deltaTime * turnSpeed;
                yield return null;
            }

            transform.rotation = targetRot;
        }
        yield return null;
    }

    public void EnableInteractable()
    {
        GetComponent<Interactable>().enabled = true;
    }
}