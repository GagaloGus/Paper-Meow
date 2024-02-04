using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Interactable))]
public class NPCBehaviour : MonoBehaviour
{
    [SerializeField]
    NPCData npcData = new NPCData();

    public BehaviourType originalBehaviour;
    Quaternion directionFacing;
    KeyCode changeDialogue;
     
    bool interacting;

    GameObject player;
    Image icon;
    public GameObject model;


    [SerializeField] bool isFlipped, isFacingBackwards;

    Animator animator;
    private void Awake()
    {
        player = FindObjectOfType<SkoController>().gameObject;
        animator = GetComponent<Animator>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //Establece las variables en 0 
        interacting = false;
        dialogueInt = 0;
        animator.SetInteger("dialogue", 0);

        //Se guarda la rotacion inicial
        directionFacing = transform.rotation;

        changeDialogue = PlayerKeybinds.skipDialogue;
    }

    // Update is called once per frame
    void Update()
    {
        if (interacting)
        {
            InteractWithNPC();
        }

        //Cambia la escala del modelo segun los bools, da la sensacion de que se de la vuelta
       model.transform.localScale = new Vector3(
            ( isFlipped ? -1 : 1 ),
            model.transform.localScale.y,
            ( isFacingBackwards ? -1 : 1 ));
    }

    #region Interacion con el NPC

    int dialogueInt = 0;
    bool nextDialogue;
    public void StartInteraction()
    {
        dialogueInt = 0;
        interacting = true;
        nextDialogue = true;

        icon.sprite = npcData.icon;

        GameManager.instance.StartInteraction(gameObject);

        //apunta el npc para que mire al player
        StartCoroutine(TurnTowards(transform, player.transform, 0.05f));
    }

    void InteractWithNPC()
    {   
        //Cambia al siguiente dialogo al darle a la tecla
        if(Input.GetKeyDown(changeDialogue)) 
        {
            dialogueInt++;
            nextDialogue = true;
        }

        if(dialogueInt < npcData.dialogues.Length)
        {
            //Cambia el enumerado a interact
            npcData.Behaviour = BehaviourType.Interact;

            if(nextDialogue)
            {
                //pone la variable de siguiente dialogo en falso
                nextDialogue = false;

                //Debugea el dialogo
                Debug.Log(npcData.dialogues[dialogueInt].text);

                //cambia la animacion a la que pusimos en el inspector
                animator.SetInteger("dialogue", (int)npcData.dialogues[dialogueInt].currentAnimation);

                //si hay un quest añadido, lo añade
                if(npcData.dialogues[dialogueInt].newQuest != null)
                {
                    QuestManager.instance.AcceptQuest(npcData.dialogues[dialogueInt].newQuest);

                }
            }
        }
        else
        {
            //End Interaction
            npcData.Behaviour = originalBehaviour;
            transform.rotation = directionFacing;

            dialogueInt = 0;
            interacting = false;
            animator.SetInteger("dialogue", 0);
            icon.sprite = null;

            isFacingBackwards = isFlipped = false;
            GetComponent<Interactable>().enabled = true;
            GameManager.instance.EndInteraction(gameObject);
        }
    }

    #endregion
    IEnumerator TurnTowards(Transform victim, Transform target, float turnSpeed)
    {
        Quaternion startRot = victim.transform.rotation;
        Quaternion targetRot = Quaternion.LookRotation(CoolFunctions.FlattenVector3(target.transform.position) - CoolFunctions.FlattenVector3(victim.transform.position));

        float elapsedTime = 0;
        while (elapsedTime < 1)
        {
            victim.transform.rotation = Quaternion.Slerp(startRot, targetRot, elapsedTime);
            elapsedTime += Time.deltaTime * turnSpeed;
            yield return null;
        }

        victim.transform.rotation = targetRot;
    }
}
