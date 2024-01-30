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
    public KeyCode changeDialogue;
     
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

        icon = GameObject.FindGameObjectWithTag("Npc icon").GetComponent<Image>();
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
        ReCenterToPlayer();
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
    void ReCenterToPlayer()
    {
        //el npc apunte hacia donde mira la camara
        transform.forward = -CoolFunctions.FlattenVector3(Camera.main.transform.forward);

        //checkea si el player esta a la derecha y arriba del npc
        bool right = CoolFunctions.IsRightOfVector(transform.position, transform.forward, player.transform.position);

        bool up = !CoolFunctions.IsRightOfVector(transform.position, transform.right, player.transform.position);

        isFlipped = !right;
        isFacingBackwards = !up;
    }
}
