using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Mouvement et Saut")]
    public float moveSpeed = 10f;
    public float jumpForce = 10.5f;
    public float gravity = 10f;
    public LayerMask Ground;
    public LayerMask Piege;
    public LayerMask Crystal;
    public LayerMask fin;


    private Vector3 halfExtents = new Vector3(0.5f, 0.1f, 0.5f); 
    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool DéplacementAuSol = true;
    private float horizontal;
    private float vertical;
    public TrailRenderer dashTrail;
    [SerializeField] private float accelerationTime = 1f;
    [SerializeField] private float accelerationTimer = 0f;
    private float currentSpeed = 0f;
    //private bool isTryingToMove = false;

    public float DetectionSol = 0.5f;
    public bool Grounded = false;
    private bool wasGrounded = false;
    private bool wasPieged = false;
    public bool canmove = true;
    public float GroundHeight;
    public bool isTouchingGround;
    private bool isDashing = false;

    public Color detectedColor;

    [Header("Effets et Gameplay")]
    public PlayerRespawn Respawn;
    public Poussiere poussiere;
    public Crystal crystal;
    public Dash Dash;
    public ZoneDeFin ZDF;

    Audio_manager audio_Manager;

    private void Awake()
    {
        audio_Manager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio_manager>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
       
        RaycastCheck();
        if (canmove)
        {
            HandleMovement();
            HandleJump();
            
        }
        if (rb.velocity.y < 0 && isDashing)
        {
            dashTrail.emitting = false;  // Désactive le trail
        }
    }

    void HandleMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (DéplacementAuSol)
        { 
            moveSpeed = 10f;  
        }

        if (!DéplacementAuSol)
        {
            moveSpeed = 5f;
        }

        moveDirection = new Vector3(horizontal, 0, vertical).normalized;
        if (canmove)
        {
            moveDirection = new Vector3(horizontal, 0, vertical).normalized;

            if (moveDirection.magnitude > 0)
            {
                //isTryingToMove = true;
                accelerationTimer += Time.deltaTime;
                float t = Mathf.Clamp01(accelerationTimer / accelerationTime);
                currentSpeed = Mathf.Lerp(0f, moveSpeed, t);
            }
            else
            {
                //isTryingToMove = false;
                accelerationTimer = 0f;
                currentSpeed = 0f;
            }

            if (canmove)
            {
                rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y - gravity * Time.deltaTime, 0);
            }
        }
    }

    void HandleJump()
    { 
        if (Input.GetKeyDown(KeyCode.Space) && Grounded && canmove)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Grounded = false;
            audio_Manager.PlaySFX(audio_Manager.jump);
        }
    }
  

    void RaycastCheck()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        isTouchingGround = Physics.BoxCast(transform.position, halfExtents, - transform.up , out RaycastHit hit, Quaternion.identity, DetectionSol, Ground);
        if (isTouchingGround)
        {
            Dash.CanSlowTime = true;
            Grounded = true;
            DéplacementAuSol = true;
            if (!wasGrounded)
            {
                Dash.isDashing = false;
                GroundHeight = hit.point.y;
                wasGrounded = true;
                Renderer objectRenderer = hit.collider.GetComponent<Renderer>(); // Récupère le Renderer
                if (objectRenderer != null)
                {
                    detectedColor = objectRenderer.material.color; 
                }

                poussiere.PoussiereArrive(GroundHeight);
                poussiere.ColorParticle(detectedColor);
            }
        }
        else
        {
            Grounded = false;
            wasGrounded = false;
            DéplacementAuSol = false;
        }

        if (Physics.BoxCast(transform.position, halfExtents, -transform.up, out hit, Quaternion.identity, DetectionSol, Piege) || Physics.BoxCast(transform.position, halfExtents, transform.up, out hit, Quaternion.identity, DetectionSol, Piege))
        {
            if (wasPieged == false)
            {
                wasPieged = true;
                Respawn.RespawnToken();
            }
        }
    }

    void OnCollisionEnter(Collision Collision)
    {
        if(Collision.gameObject.layer == 8)
        { 
            crystal.Disparition();
            Dash.isDashing = false;
            Dash.DashToken();
        }
    }

    public void AgainPieged()
    {
        wasPieged = false;       
    }

    public void RespawnPose(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
