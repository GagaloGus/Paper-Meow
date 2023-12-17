using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class NPCBehaviour : MonoBehaviour
{
    [SerializeField]
    NPCData npcData = new NPCData();

    public BehaviourType originalBehaviour;
    public Vector3 directionFacing;

    bool interacting, playerInRange;
    public GameObject PressE;

    GameObject player, model;
    public float playerDistance;

    public bool isFlipped, isFacingBackwards;

    Animator animator;
    private void Awake()
    {
        player = FindObjectOfType<SkoController>().gameObject;
        animator = GetComponent<Animator>();

        model = GetComponentInChildren<MeshFilter>().gameObject;
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

            ReCenterToPlayer();
        }

        if (interacting)
        {
            InteractWithNPC();
        }


        model.transform.localScale = new Vector3(
            ( isFlipped ? -1 : 1 ),
            model.transform.localScale.y,
            ( isFacingBackwards ? -1 : 1 ));
    }

    void ReCenterToPlayer()
    {
        bool right = CoolFunctions.IsRightOfVector(transform.position, transform.forward, player.transform.position);

        bool up = !CoolFunctions.IsRightOfVector(transform.position, transform.right, player.transform.position);

        isFlipped = !right;
        isFacingBackwards = !up;

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

            isFacingBackwards = isFlipped = false;

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

    public IEnumerator RotateTowards(Vector3 to, float turnSpeed)
    {
        Quaternion _lookRotation = Quaternion.LookRotation((to - transform.position).normalized);

        for (int i = 0; i < 30; i++) 
        { 
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
