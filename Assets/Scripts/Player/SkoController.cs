using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;

[RequireComponent(typeof(SkoStats))]
public class SkoController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 moveInput, moveDirection;

    private Transform groundPoint;

    SkoStats stats;

    [SerializeField] private bool isGrounded, isFlipped, isFacingBackwards, isRunning, canMove;

    //variables del modelo 3d
    GameObject m_gameobj;
    Animator m_animator;

    [Range(0f, 2f)]
    public float rayDetectFloorDist;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
       
        groundPoint = transform.Find("GroundCheckPoint");

        m_gameobj = transform.Find("3dmodel").gameObject;
        m_animator = m_gameobj.GetComponent<Animator>();

        isFlipped = false;
        isFacingBackwards = false;
    }

    private void Start()
    {
        StartCoroutine(UpdateStats());
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0 ,Input.GetAxis("Vertical"));

        //direccion hacia adelante de la camara
        moveDirection = CoolFunctions.FlattenVector3(Camera.main.transform.forward);  

        #endregion

        #region Flip

        //orienta al player a la direccion de la camara al moverse
        if(canMove)
        {
            transform.forward = -moveDirection;
        }

        //cambia la escala para imitar el "giro" del personaje
        if (isGrounded && canMove)
        {
            if ((!isFlipped && moveInput.x > 0) || (isFlipped && moveInput.x < 0))
            {
                isFlipped = !isFlipped;
            }

            if ((!isFacingBackwards && moveInput.z > 0) || (isFacingBackwards && moveInput.z < 0))
            {
                isFacingBackwards = !isFacingBackwards;
            }
        }

        //le "da la vuelta" al modelo segun los bools
        m_gameobj.transform.localScale = new(
           (isFlipped ? -1 : 1),
           m_gameobj.transform.localScale.y,
           (isFacingBackwards ? -1 : 1));

        #endregion

        if (canMove)
        {
            #region Animation
            //corre si estamos pulsando el Shift y nos movemos
            isRunning = Input.GetKey(KeyCode.LeftShift) && isGrounded && moveInput.magnitude > 0.2f;
            m_animator.SetBool("isRunning", isRunning);

            //Anda si nos movemos
            m_animator.SetBool("isWalking", moveInput.magnitude != 0);

            m_animator.SetBool("grounded", isGrounded);

            //si saltamos cambia a Arriba o Abajo segun nuestra vel en el eje y
            if (!isGrounded)
            {
                if (rb.velocity.y > 0f) { m_animator.SetBool("jumpingUp", true); }
                else if (rb.velocity.y < 0f) { m_animator.SetBool("jumpingUp", false); }
            }

            //ataca segun el arma que tengamos
            if (Input.GetKeyDown(KeyCode.P) && isGrounded)
            {
                m_animator.SetInteger("attackWeaponID", (int)stats.weaponSelected);
                m_animator.SetTrigger("attack");
            }
            #endregion
        }

    }

    private void FixedUpdate()
    {

        if (canMove)
        {
            Vector3 direction = (moveInput.x * Camera.main.transform.right + moveInput.z * moveDirection);

            //Moverse
            rb.velocity = direction *
                stats.moveSpeed *
                (isRunning ? stats.runSpeedMult : 1)
                + Vector3.up * rb.velocity.y;

            //Saltar
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                rb.velocity += new Vector3(0, stats.jumpForce, 0);
        }

        isGrounded =
           Physics.BoxCast(groundPoint.position, new Vector3(0.3f, 0.05f, 0.05f), Vector3.down, transform.rotation, rayDetectFloorDist, LayerMask.GetMask("Ground"));
    
    }
    
    //llamado desde el gamemanager
    void StartInteraction(GameObject npc)
    {
        canMove = false;

        //Centra el personaje para que apunte al npc
        transform.forward = -CoolFunctions.FlattenVector3(Camera.main.transform.forward);

        bool right = CoolFunctions.IsRightOfVector(transform.position, transform.forward, npc.transform.position);

        bool up = !CoolFunctions.IsRightOfVector(transform.position, transform.right, npc.transform.position);

        isFlipped = !right;
        isFacingBackwards = !up;
    }

    //llamado desde el gamemanager
    void EndInteraction()
    {
        canMove = true;
    }
    public bool player_canMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    IEnumerator UpdateStats()
    {
        stats = GetComponent<SkoStats>();
        yield return new WaitForSeconds(GameManager.instance.playerStats_refreshRate);

        StartCoroutine(UpdateStats());
    }
}
