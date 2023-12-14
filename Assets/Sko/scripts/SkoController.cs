using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkoController : MonoBehaviour
{
    public float moveSpeed, speedMult, jumpForce;

    private Rigidbody rb;
    private Vector3 moveInput, moveDirection;

    private Transform groundPoint;
    [SerializeField] 
    private bool isGrounded, isFlipped, isFacingBackwards, isRunning, canMove;

    //variables del modelo 3d
    GameObject m_gameobj;
    Animator m_animator;

    public enum attackWeaponIDs { garra, cutter}
    public attackWeaponIDs weaponSelected;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        groundPoint = transform.Find("GroundCheckPoint");

        m_gameobj = transform.Find("3dmodel").gameObject;
        m_animator = m_gameobj.GetComponent<Animator>();

        isFlipped = false;
        isFacingBackwards = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0 ,Input.GetAxis("Vertical"));

        //direccion hacia adelante de la camara
        moveDirection = CoolFunctions.FlattenVector3(Camera.main.transform.forward);
        

        isGrounded =
           Physics.Raycast(
               groundPoint.position,          // posicion de origen del rayo
               Vector3.down,                  // vector de direccion del rayo
               0.2f,                          // distancia del rayo
               LayerMask.GetMask("Ground"));  // Mascara del suelo, para que solo detecte el suelo

        #endregion

        #region Flip

        //orienta al player a la direccion de la camara
        if(moveInput.magnitude > 0)
        {
            transform.forward = -moveDirection;
        }

        //cambia la escala para imitar el "giro" del personaje
        if (isGrounded && canMove)
        {
            if ((!isFlipped && moveInput.x > 0) || (isFlipped && moveInput.x < 0))
            {
                X_Flip();
            }

            if ((!isFacingBackwards && moveInput.z > 0) || (isFacingBackwards && moveInput.z < 0))
            {
                Back_Flip();
            }

        }
        #endregion

        if(canMove)
        {
            #region Animation
            isRunning = Input.GetKey(KeyCode.LeftShift) && isGrounded && moveInput.magnitude > 0.2f;

            m_animator.SetBool("isRunning", isRunning);
            m_animator.SetBool("isWalking", moveInput.magnitude != 0);

            m_animator.SetBool("grounded", isGrounded);
            if (!isGrounded)
            {
                if (rb.velocity.y > 0f) { m_animator.SetBool("jumpingUp", true); }
                else if (rb.velocity.y < 0f) { m_animator.SetBool("jumpingUp", false); }
            }

            if (Input.GetKeyDown(KeyCode.P) && isGrounded)
            {
                m_animator.SetInteger("attackWeaponID", (int)weaponSelected);
                m_animator.SetTrigger("attack");
            }
            #endregion
        }

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            //Moverse
            rb.velocity = (moveInput.x * Camera.main.transform.right + moveInput.z * moveDirection) * moveSpeed * (isRunning ? speedMult : 1) + Vector3.up * rb.velocity.y;

            //Saltar
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.velocity += new Vector3(0, jumpForce, 0);
            }
        }
        
    }

    void X_Flip()
    {
        m_gameobj.transform.localScale = new(
            -1 * m_gameobj.transform.localScale.x,
            m_gameobj.transform.localScale.y,
            m_gameobj.transform.localScale.z);

        isFlipped = !isFlipped;
    }

    void Back_Flip()
    {
        m_gameobj.transform.localScale = new(
            m_gameobj.transform.localScale.x,
            m_gameobj.transform.localScale.y,
            -1 * m_gameobj.transform.localScale.z);

        isFacingBackwards = !isFacingBackwards;
    }

    public bool player_canMove
    {
        get { return canMove; }
        set { canMove = value; }
    }
}
