using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SkoStats))]
public class SkoController : MonoBehaviour
{
    private Rigidbody rb;
    private ConstantForce force;
    private Vector3 moveInput, moveDirection;

    private Transform groundPoint;

    SkoStats stats;

    [SerializeField] 
    private bool isGrounded, isFlipped, isFacingBackwards, canMove, isUsingSkill, isGliding, isRunning, isAttacking;

    //variables del modelo 3d
    GameObject m_gameobj;
    Animator m_animator;

    [Range(0f, 2f)]
    public float rayDetectFloorDist, nearGroundDist;

    public int gravity;

    public enum PlayerStates { Idle, Walk, Run, JumpUp, JumpDown, Glide, Attack }
    public PlayerStates playerState;

    KeyCode jump, run, attack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        force = GetComponent<ConstantForce>();

        groundPoint = transform.Find("GroundCheckPoint");

        m_gameobj = transform.Find("3dmodel").gameObject;
        m_animator = m_gameobj.GetComponent<Animator>();

        isFlipped = false;
        isFacingBackwards = false;

        isUsingSkill = false;
        canMove = true;

        jump = PlayerKeybinds.jump;
        run = PlayerKeybinds.run;
        attack = PlayerKeybinds.attack;
    }

    private void Start()
    {
        StartCoroutine(UpdateStats());
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded =
           Physics.BoxCast(groundPoint.position, new Vector3(0.3f, 0.05f, 0.05f), Vector3.down, transform.rotation, rayDetectFloorDist, LayerMask.GetMask("Ground"));


        //distintas configuraciones para cuando esta en el suelo y en el aire
            if(isGrounded) { GroundControl(); }
            else { AirControl(); }

        #region Movement
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //direccion hacia adelante de la camara
        moveDirection = CoolFunctions.FlattenVector3(Camera.main.transform.forward);

        isRunning = Input.GetKey(run);
        #endregion

        #region Flip

        //orienta al player a la direccion de la camara al moverse
        if (canMove)
        {
            transform.forward = -moveDirection;
        }

        //cambia la escala para imitar el "giro" del personaje
        if (canMove && !isAttacking)
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

        //cambia el int del animator segun el estado del player
        if(canMove)
        m_animator.SetInteger("player states", (int)playerState);
    }

    void GroundControl()
    {
        rb.drag = 0;
        isGliding = false;

        //Basicamente si estamos dandole a alguna tecla para moverse
        if(moveInput.magnitude > 0.1)
        {
            if(isRunning) { playerState = PlayerStates.Run; }
            else { playerState = PlayerStates.Walk; }
        }
        else
        {
            playerState = PlayerStates.Idle;
        }

        if (Input.GetKeyDown(attack) && !isAttacking)
        {
            isAttacking = true;

            playerState = PlayerStates.Attack;
            m_animator.SetInteger("attackWeaponID", (int)stats.weaponSelected);
            m_animator.SetTrigger("attack");
        }
    }

    void AirControl()
    {
        //raycast que detecta si hay suelo a tanta distancia de nosotros hacia abajo
        bool nearGround = Physics.Raycast(transform.position, Vector3.down, nearGroundDist, LayerMask.GetMask("Ground"));
            //si estamos planeando
        if (isGliding && !isUsingSkill)
        {
            rb.drag = 15;

            playerState = PlayerStates.Glide;

            if (Input.GetKeyDown(jump)) { isGliding = false; }
        }
        else
        {
            rb.drag = 0;

            if (rb.velocity.y > 0f) { playerState = PlayerStates.JumpUp; }
            else if (rb.velocity.y < 0f) { playerState = PlayerStates.JumpDown; }

            //si le damos al espacio y el raycast no detecto un suelo debajo del player podemos planear
            if(Input.GetKeyDown(jump) && !nearGround) { isGliding = true; }
        }
    }

    private void FixedUpdate()
    {
        if (canMove && !isAttacking)
        {
            if (!isUsingSkill)
            {
                Vector3 direction = (moveInput.x * Camera.main.transform.right + moveInput.z * moveDirection);
                Vector3 vel = direction * stats.moveSpeed * (isRunning ? stats.runSpeedMult : 1);
                //Moverse
                rb.velocity = (vel.magnitude < 1f ? rb.velocity : vel + Vector3.up * rb.velocity.y);
            }

            //Saltar
            if (Input.GetKeyDown(jump) && isGrounded)
            { 
                rb.velocity += new Vector3(0, stats.jumpForce, 0);
            }
        }
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
    public bool player_isGrounded
    {
        get { return isGrounded; }
    }

    public bool player_isAttacking
    {
        get { return isAttacking; }
        set { isAttacking = value; }
    }

    IEnumerator UpdateStats()
    {
        stats = GetComponent<SkoStats>();
        yield return new WaitForSeconds(GameManager.instance.playerstatsRefreshRate);

        StartCoroutine(UpdateStats());
    }

    public void StartSkillCooldownCoroutine(float skillUseTime)
    {
        StartCoroutine(UsingSkillCooldown(skillUseTime));
    }

    IEnumerator UsingSkillCooldown(float skillUseTime)
    {
        isUsingSkill = true;
        yield return new WaitForSeconds(skillUseTime);

        isUsingSkill = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Vector3.down * nearGroundDist);
    }
}
