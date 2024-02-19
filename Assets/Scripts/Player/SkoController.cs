using Cinemachine;
using System.Collections;
using System.Collections.Generic;
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

    public GameObject bow;

    [Header("Debug Variables")]
    public int gravity;
    [Range(0f, 2f)] public float rayDetectFloorDist;
    public float nearGroundDist;
    [SerializeField] private bool isGrounded, isFlipped, isFacingBackwards, canMove, isUsingSkill, isGliding, isRunning, isAttacking, isInside;
    public int currentAttackNumber;
    public bool canAttackAgain;

    public enum PlayerStates { Idle, Walk, Run, JumpUp, JumpDown, Glide, Attack }
    public PlayerStates playerState;

    [Header("Has Items")]
    [SerializeField] bool hasGlider;

    KeyCode jump, run, attack, swapPreviousWeapon, swapNextWeapon;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        groundPoint = transform.Find("GroundCheckPoint");

        m_gameobj = transform.Find("3dmodel Sko").gameObject;
        m_animator = m_gameobj.GetComponent<Animator>();

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
    }

    private void OnEnable()
    {
        GameEventsManager.instance.inventoryEvents.onWeaponSwap += ChangeWeapon;
        GameEventsManager.instance.inventoryEvents.onItemAdded += CheckIfItemAdded;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inventoryEvents.onWeaponSwap -= ChangeWeapon;
        GameEventsManager.instance.inventoryEvents.onItemAdded -= CheckIfItemAdded;
    }

    private void Start()
    {
        UpdateStats();
        nearGroundDist = rayDetectFloorDist * stats.jumpForce;
        //GameManager.instance.GetPlayer(gameObject);
        currentAttackNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            //distintas configuraciones para cuando esta en el suelo y en el aire
            if (isGrounded) { GroundControl(); }
            else { AirControl(); }

        }

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

        if (!GameManager.instance.gamePaused && canMove)
        {
            m_animator.SetInteger("player states", (int)playerState);
        }
    }

    void CheckIfItemAdded(Item item)
    {
        if(item.itemName == "Paper Glider")
        {
            hasGlider = true;
        }
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

        //Saltar
        if (Input.GetKeyDown(jump))
        { 
            rb.velocity += new Vector3(0, stats.jumpForce, 0);
        }

        //el cambio de armas por el quickswap se hace en el Inventory manager
        if (Input.GetKeyDown(swapPreviousWeapon))
        {
            InventoryManager.instance.SwapWeapon(true);
        }
        else if (Input.GetKeyDown(swapNextWeapon))
        {
            InventoryManager.instance.SwapWeapon(false);
        }

        //Todo sobre los ataques
        Attacks();
    }

    #region Attack and weapons
    public void ChangeWeapon(Item weapon)
    {
        stats.weaponSelected = (SkoStats.AttackWeaponIDs)(int)weapon.weaponType;

        Weapon weaponSelected;
        if (weapon.weaponType == WeaponType.Sword)
        {
            weaponSelected = GetComponentInChildren<Sword>(true);
        }
        else if (weapon.weaponType == WeaponType.Hammer)
        {
            weaponSelected = GetComponentInChildren<HammerW>(true);
        }
        else
        {
            weaponSelected = GetComponentInChildren<Garra>(true);
        }

        
        weaponSelected.weaponData.itemData = weapon;
        weaponSelected.UpdateWeapon();
    }

    public float timeCheckForChargedAttack;

    void Attacks()
    {
        //Si pulsamos la tecla de ataque y no estamos atacando aun
        if (Input.GetKeyDown(attack) && !isAttacking)
        {
            //setup
            AttackStart();

            //cambia el estado
            playerState = PlayerStates.Attack;

            //movidas del animator
            m_animator.SetInteger("attackWeaponID", (int)stats.weaponSelected);
            m_animator.SetTrigger("attack");
        }

        //El ataque
        if (isAttacking)
        {
            //configuracion extra solo para el arco (por ahora no)
            /*if(stats.weaponSelected == SkoStats.AttackWeaponIDs.bow)
            {
                bow.SetActive(true);

                //determina si dejamos la tecla pulsada mucho tiempo si es un ataque cargado o uno basico
                timeCheckForChargedAttack -= Time.deltaTime;
                if (timeCheckForChargedAttack <= 0)
                {
                    m_animator.SetBool("aiming", true);
                    bow.GetComponent<Bow>().ChargedAttack();
                }

                //Al levantar la tecla de ataque mira cuanto tiempo hemos mdejada pulsada la tecla y hace un cargado o el basico
                if (Input.GetKeyUp(attack))
                {
                    if(timeCheckForChargedAttack < 0)
                    {
                        bow.GetComponent<Bow>().ShootChargedAttack();
                    }
                    else
                    {
                        bow.GetComponent<Bow>().NormalAttack();
                    }

                    m_animator.SetTrigger("attack");
                    m_animator.SetBool("aiming", false);
                    timeCheckForChargedAttack = 0.1f;
                    isAttacking = false;
                }
            }
            else
            {
                //Si no estamos usando arco, podemos encadenar otro ataque
            }*/
                
            WaitForFollowUpAttack();
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
                //setup
                AttackStart();
                m_animator.SetTrigger("nextAttack");
            }

            //cancela todo el ataque si corremos
            if (Input.GetKeyDown(run) && moveInput.magnitude > 0)
            {
                m_animator.Play("run");
                isAttacking = false; canAttackAgain = true;
            }
        }
    }
    #endregion

    void AirControl()
    {
        isAttacking = false;
        //raycast que detecta si hay suelo a tanta distancia de nosotros hacia abajo
        bool nearGround = Physics.Raycast(transform.position, Vector3.down, nearGroundDist, LayerMask.GetMask("Ground"));
            //si estamos planeando
        if (isGliding && !isUsingSkill && hasGlider)
        {
            //fisicas
            rb.drag = 15;

            playerState = PlayerStates.Glide;

            if (Input.GetKeyDown(jump)) { isGliding = false; }
        }
        else
        {
            //fisicas 2
            rb.drag = 0;

            if (rb.velocity.y > 0f) { playerState = PlayerStates.JumpUp; }
            else if (rb.velocity.y < 0f) { playerState = PlayerStates.JumpDown; }

            //si le damos al espacio y el raycast no detecto un suelo debajo del player podemos planear
            if(Input.GetKeyDown(jump) && !nearGround) { isGliding = true; }
        }
    }

    private void FixedUpdate()
    {
        //detecta si hay suelo usando un boxcast
        /*isGrounded =
           Physics.BoxCast(groundPoint.position, 
           new Vector3(0.3f, 0.05f, 0.05f), 
           Vector3.down, 
           transform.rotation, 
           rayDetectFloorDist, 
           LayerMask.GetMask("Ground"));*/

        Ray detectGround = new Ray(groundPoint.position, Vector3.down);

        RaycastHit hit;

        if(Physics.Raycast(detectGround, out hit, rayDetectFloorDist, LayerMask.GetMask("Ground")))
        {
            isGrounded = true;
            GameEventsManager.instance.playerEvents.PlayerSendGroundTag(hit.collider.gameObject.tag);
        }
        else { isGrounded = false; }


        if (canMove)
        {
            if (!isUsingSkill && !isAttacking)
            {
                Vector3 direction = (moveInput.x * Camera.main.transform.right + moveInput.z * moveDirection);
                float multiplySpeedFac = (float)(1 * (isRunning && isGrounded ? stats.runSpeedMult : 1) * (isGliding ? stats.runSpeedMult/1.5 : 1) * stats.currentStats.SPD/10);

                Vector3 vel = direction * stats.moveSpeed * multiplySpeedFac;
                //Moverse
                rb.velocity = (vel.magnitude < 1f ? rb.velocity : vel + Vector3.up * rb.velocity.y) * GameManager.instance.gameTime;
            }

            if (!isAttacking)
            {
                //orienta al player a la direccion de la camara
                transform.forward = -moveDirection;
            }
        }
    }
    public void PausedGame(bool paused)
    {
        if (isGrounded && !GameManager.instance.isInteracting)
        {
            if (paused)
            {
                canMove = false;
                m_animator.SetBool("gamePaused", true);

                isFlipped = false;
                isFacingBackwards = false;
                FlipCharacter();

                EnablePhysics(false);
            }
            else
            {
                canMove = true;
                m_animator.SetBool("gamePaused", false);
                EnablePhysics(true);
            }
        }
    }

    void EnablePhysics(bool enable)
    {
        GetComponent<ConstantForce>().enabled = enable;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }


    //llamado desde el dialogue manager

    public void StartInteraction(GameObject npc)
    {
        playerState = PlayerStates.Idle;
        m_animator.SetInteger("player states", 0);
        canMove = false;
        //Centra el personaje para que apunte al npc
        transform.forward = -CoolFunctions.FlattenVector3(Camera.main.transform.forward);

        bool right = CoolFunctions.IsRightOfVector(transform.position, transform.forward, npc.transform.position);

        bool up = !CoolFunctions.IsRightOfVector(transform.position, transform.right, npc.transform.position);

        isFlipped = !right;
        isFacingBackwards = !up;
    }

    public void EndInteraction()
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
        isGliding = false;
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
        groundPoint = transform.Find("GroundCheckPoint");
        Gizmos.DrawRay(groundPoint.position, Vector3.down*rayDetectFloorDist);
    }
}
