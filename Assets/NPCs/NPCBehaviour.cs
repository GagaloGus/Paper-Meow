using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NPCBehaviour : MonoBehaviour
{
    [SerializeField]
    NPCData npcData = new NPCData();

    public BehaviourType originalBehaviour;
    public Vector3 directionFacing;

    bool interacting, playerInRange;
    public GameObject PressE;

    GameObject player;
    public float playerDistance;

    public bool isFlipped, isFacingBackwards;

    Animator animator;
    private void Awake()
    {
        player = FindObjectOfType<SkoController>().gameObject;
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        interacting = false;
        dialogueInt = 0;
        animator.SetInteger("dialogue", 0);

    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Vector3.Distance(transform.position, player.transform.position) <= playerDistance;

        PressE.SetActive(playerInRange && !interacting);

        if (Input.GetKeyDown(KeyCode.E) && playerInRange && !interacting)
        {
            dialogueInt = 0;
            interacting = true;
            nextDialogue = true;

            GameManager.instance.StartInteraction(gameObject);


        }

        if (interacting)
        {
            InteractWithNPC();
        }

        ReCenterToPlayer();
    }

    void ReCenterToPlayer()
    {
        Vector3 playerFlatPos = CoolFunctions.FlattenVector3(player.transform.position),
            npcFlatPos = CoolFunctions.FlattenVector3(gameObject.transform.position);


        float xDist = Mathf.Pow(player.transform.position.x - transform.position.x, 2);
        float zDist = Mathf.Pow(player.transform.position.z - transform.position.z, 2);

        float angleNPCandPlayer = Mathf.Atan(zDist/xDist);
        Debug.Log(angleNPCandPlayer * 180/Mathf.PI);
    }


    int dialogueInt = 0;
    bool nextDialogue;
    void InteractWithNPC()
    {   
        if(Input.GetKeyDown(KeyCode.C)) 
        {
            dialogueInt++;
            nextDialogue = true;
        }

        if(dialogueInt < npcData.dialogues.Length)
        {
            npcData.Behaviour = BehaviourType.Interact;

            if(nextDialogue)
            {
                nextDialogue = false;
                Debug.Log(npcData.dialogues[dialogueInt].text);
                animator.SetInteger("dialogue", (int)npcData.dialogues[dialogueInt].currentAnimation);

                if(npcData.dialogues[dialogueInt].newQuest != null)
                {
                    AddQuest(npcData.dialogues[dialogueInt].newQuest);
                }
            }
        }
        else
        {
            npcData.Behaviour = originalBehaviour;
            dialogueInt = 0;
            interacting = false;
            animator.SetInteger("dialogue", 0);
            GameManager.instance.EndInteraction(gameObject);
        }
    }

    void AddQuest(Quest questToAdd)
    {
        if (QuestManager.instance != null)
        {
            // Verifica si el jugador puede recibir la misión (puedes personalizar esta lógica).
            if (!QuestManager.instance.HasQuest(questToAdd) && !questToAdd.isCompleted)
            {
                QuestManager.instance.AcceptQuest(questToAdd); // Asigna la misión al jugador.
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerDistance);
    }
}
