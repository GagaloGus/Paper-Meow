using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[RequireComponent(typeof(SkoStats))]
public class SkoController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 moveInput, moveDirection;

    private Transform groundPoint;

    SkoStats stats;

    //variables del modelo 3d
    GameObject m_gameobj;
    Animator m_animator;

    Bow bow;

    [Header("Debug Variables")]
    public int gravity;
    [Range(0f, 2f)] public float rayDetectFloorDist, nearGroundDist;
    [SerializeField] private bool isGrounded, isFlipped, isFacingBackwards, canMove, isUsingSkill, isGliding, isRunning, isAttacking;
    [SerializeField] int weaponAmount;
    public int currentAttackNumber;
    public bool canAttackAgain;

    public enum PlayerStates { Idle, Walk, Run, JumpUp, JumpDown, Glide, Attack }
    public PlayerStates playerState;

    KeyCode jump, run, attack, swapPreviousWeapon, swapNextWeapon;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        groundPoint = transform.Find("GroundCheckPoint");

        m_gameobj = transform.Find("3dmodel Sko").gameObject;
        m_animator = m_gameobj.GetComponent<Animator>();

        bow = GetComponentInChildren<Bow>();

        isFlipped = false;
        isFacingBackwards = false;

        isUsingSkill = false;
        canMove = true;

        //Mapeado de teclas
        jump = PlayerKeybinds.jump;
        run = PlayerKeybinds.run;
        attack = PlayerKeybinds.attack;
        swapPreviousWeapon = PlayerKeybinds.swapPrevousWeapon;
        swapNextWeapon = PlayerKeybinds.swapNextWeapon;

        weaponAmount = System.Enum.GetValues(typeof(SkoStats.AttackWeaponIDs)).Length;
    }

    private void Start()
    {
        UpdateStats();
        currentAttackNumber = 0;
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
        //cambia la escala para imitar el "giro" del personaje
        if (canMove && !isAttacking)
        {
            FlipCharacter();
        }

        //le "da la vuelta" al modelo segun los bools
        m_gameobj.transform.localScale = new(
           (isFlipped ? -1 : 1),
           m_gameobj.transform.localScale.y,
           (isFacingBackwards ? -1 : 1));

        #endregion

        //cambia el int del animator
        if(canMove)
        m_animator.SetInteger("player states", (int)playerState);

        m_animator.SetInteger("currentAttack", currentAttackNumber);
    }

    void FlipCharacter()
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

        //Cambia al arma previa o la siguiente, si esta en los extremos salta hacia la primera o ultima
        if (Input.GetKeyDown(swapPreviousWeapon))
        {
            if (stats.weaponSelected == 0) { stats.weaponSelected = (SkoStats.AttackWeaponIDs)weaponAmount - 1; }
            else { stats.weaponSelected--; }
            stats = GetComponent<SkoStats>();

        }
        else if (Input.GetKeyDown(swapNextWeapon))
        {
            if ((int)stats.weaponSelected == weaponAmount - 1) { stats.weaponSelected = 0; }
            else { stats.weaponSelected++; }
            stats = GetComponent<SkoStats>();

        }

        //Todo sobre los ataques
        Attacks();
    }

    public float timeCheckForChargedAttack;

    void Attacks()
    {
        if (Input.GetKeyDown(attack) && !isAttacking)
        {
            AttackStart();
            playerState = PlayerStates.Attack;
            m_animator.SetInteger("attackWeaponID", (int)stats.weaponSelected);
            m_animator.SetTrigger("attack");
        }

        if (isAttacking)
        {
            if(stats.weaponSelected == SkoStats.AttackWeaponIDs.bow)
            {
                timeCheckForChargedAttack -= Time.deltaTime;
                if (timeCheckForChargedAttack <= 0)
                {
                    bow.ChargedAttack();
                }

                if (Input.GetKeyUp(attack))
                {
                    if(timeCheckForChargedAttack < 0)
                    {
                        bow.ShootChargedAttack();
                    }
                    else
                    {
                        bow.NormalAttack();
                    }

                    timeCheckForChargedAttack = 0.1f;
                    isAttacking = false;
                }
            }
            else
            {
                WaitForFollowUpAttack();
            }
        }

        void AttackStart()
        {
            isAttacking = true;
            canAttackAgain = false;
            FlipCharacter();
        }

        void WaitForFollowUpAttack()
        {
            if (Input.GetKeyDown(attack) && canAttackAgain)
            {
                AttackStart();
                currentAttackNumber++;
                m_animator.SetTrigger("nextAttack");
            }

            if (Input.GetKeyDown(run) && moveInput.magnitude > 0)
            {
                currentAttackNumber = 0;
                m_animator.Play("run");
                isAttacking = false; canAttackAgain = true;
            }
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
        //orienta al player a la direccion de la camara al moverse
        if (canMove)
        {
            transform.forward = -moveDirection;
        }

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
    public void UpdateStats()
    {
        stats = GetComponent<SkoStats>();
        Invoke(nameof(UpdateStats), GameManager.instance.playerstatsRefreshRate);
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
