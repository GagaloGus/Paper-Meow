using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NPCBehaviour : MonoBehaviour
{
    [SerializeField]
    NPCData npcData = new NPCData();

    public BehaviourType originalBehaviour;

    bool interacting, playerInRange;
    public GameObject PressE;

    GameObject player;
    public float playerDistance;

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
        }

        if (interacting)
        {
            InteractWithNPC();
        }

        
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
        }
    }

    void AddQuest(Quest questToAdd)
    {
        QuestManager questManager = FindObjectOfType<QuestManager>(); // Encuentra el QuestManager en la escena (ajusta según tu estructura).

        if (questManager != null)
        {
            // Verifica si el jugador puede recibir la misión (puedes personalizar esta lógica).
            if (!questManager.HasQuest(questToAdd) && !questToAdd.isCompleted)
            {
                questManager.AcceptQuest(questToAdd); // Asigna la misión al jugador.
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerDistance);
    }
}
