using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashDistance = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public float maxSpeed = 5f;
    bool isSlowingTime = false;
    public float startSlowTime = -1f;
    public float cooldownSlowDuration = 1f;
    private bool awaitingAutoExit = false;

    private bool SeconDash = false;
    public LayerMask Ground;
    private Rigidbody rb;
    public Transform target;
    public Vector3 dashDirection;
    public bool isDashing = false;
    private float dashTimeRemaining;
    private float lastDashTime;
    public float radius = 2f;
    public bool CanSlowTime = true;
    private Vector3 normalizedVector;
    private Vector3 centeredMousePosition;
    private Vector3 finalDashPosition;
    private LineRenderer lineRenderer;
    public Movement movement;


    Vector3 originalGravity = Physics.gravity;
    private bool doubledash = false;

    [Header("Effets Visuels")]
    public indicateur indicateur;

    [Header("Ralentie")]
    float slowMotionFactor = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;

        if (target == null)
        {
            GameObject newTarget = new GameObject("Target");
            newTarget.transform.position = transform.position;
            target = newTarget.transform;
        }
    }

    void Update()
    {
        
        LocateMouse();
        UpdateLineRenderer();

        if ((Input.GetMouseButton(0) && Time.time >= lastDashTime + dashCooldown) || SeconDash)
        {
            if (!isSlowingTime && CanSlowTime == true)
            {
                RalentissementDuTemps();
            }
        }
            
        if (isSlowingTime && Input.GetMouseButtonUp(0))
        {
            ResetTime();
            HandleDash();
            SeconDash = false;
           
        }

        if (isSlowingTime && awaitingAutoExit == true && (Time.unscaledTime - startSlowTime >= cooldownSlowDuration))
        {
            HandleDash();
            ResetTime();
            awaitingAutoExit = false;
        }
    }
    void ResetTime()
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
        isSlowingTime = false;
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;

        startSlowTime = -1f;
        awaitingAutoExit = false;
    }
    void RalentissementDuTemps()
    {
        CanSlowTime = false;
        Physics.gravity = Vector3.zero;
        isSlowingTime = true;
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

        startSlowTime = Time.unscaledTime;
        awaitingAutoExit = true;
    }

    void HandleDash()
    {
        Time.timeScale = 1f;
        if (isDashing) return;

        Vector3 pointA = lineRenderer.GetPosition(0);
        Vector3 pointB = lineRenderer.GetPosition(1);
        dashDirection = (pointB - pointA).normalized;

        finalDashPosition = transform.position + (dashDirection * dashDistance);

        if (movement.isTouchingGround && dashDirection.y <= 0 )
        {
            dashDirection = new Vector3(dashDirection.x / 100, 0, 0).normalized;
        }

        isDashing = true;
        rb.velocity = Vector3.zero;
        dashTimeRemaining = dashDuration;

        if (indicateur != null)
        {
            indicateur.positionnement(finalDashPosition);
        }

        lineRenderer.enabled = false;
        StartCoroutine(DashSequence());
    }

    IEnumerator DashSequence()
    {
        float dashSpeed = dashDistance / dashDuration;
        float initialDuration = dashDuration;
        Physics.gravity = Vector3.zero;


        while (dashTimeRemaining > 0f)
        {
            Vector3 nextPosition = rb.position + dashDirection * dashSpeed * Time.fixedDeltaTime;
            rb.MovePosition(nextPosition);

            dashTimeRemaining -= Time.fixedDeltaTime; 
            yield return null;
            if (DetectCollisions())
            {
                StopDash();
                Physics.gravity = originalGravity;
                yield break; // **Arrête immédiatement la coroutine**
            }
        }
        StopDash();
        Physics.gravity = originalGravity;
        StartCoroutine(SlowMotionEffect(0.4f, 0.7f, 0.7f));
        yield return new WaitForSecondsRealtime(0.7f);

       
    }
    bool DetectCollisions()
    {
        float raycastDistance = 0.08f;
        Vector3 boxOrigin = rb.position + dashDirection * raycastDistance;
        bool gonnaTouchTheGround = Physics.BoxCast(boxOrigin, new Vector3(0.5f, 0.1f, 0.5f), dashDirection, out RaycastHit groundHit, Quaternion.identity, raycastDistance, Ground);

        if (gonnaTouchTheGround)
        {
            Debug.Log("Sol détecté, dash annulé !");
            return true;
        }
        return false;
    }

    IEnumerator SlowMotionEffect(float startFactor, float endFactor, float duration)
    {
        Time.timeScale = startFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        float tempsEcoulé = 0f;

        while (tempsEcoulé < duration) 
        {
            tempsEcoulé += Time.unscaledDeltaTime;
            float progress = tempsEcoulé / duration;
            float newTimeScale = Mathf.SmoothStep(startFactor,endFactor,progress);

            Time.timeScale = newTimeScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            yield return null;
        }

        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
    }


void StopDash()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

    }

    void LocateMouse()
    {
        float centerX = Screen.width / 2f;
        float centerY = (Screen.height / 2f) - 70f;

        centeredMousePosition = new Vector3(Input.mousePosition.x - centerX, Input.mousePosition.y - centerY);
        normalizedVector = centeredMousePosition.normalized * radius;
    }

    void UpdateLineRenderer()
    {
        if (target != null)
        {
            lineRenderer.SetPosition(0, target.position); // Point de départ
            lineRenderer.SetPosition(1, target.position + normalizedVector); // Point final
            Vector3 pointB = lineRenderer.GetPosition(1);
            finalDashPosition = lineRenderer.GetPosition(1);
            indicateur.positionnement(pointB);
        }
    }

    public void DashToken()
    {
            StartCoroutine(WaitForOneSecondOrClick());
    }

    IEnumerator WaitForOneSecondOrClick()
    {
        float startTime = Time.unscaledTime;

        while (Time.timeScale > slowMotionFactor + 0.01f)
        {
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            rb.drag = 5f;
            Physics.gravity = new Vector3(0, 0, 0);
                   
            if (Input.GetMouseButtonDown(0)) 
            {
                SeconDash = true;
                Debug.Log("Clic détecté, arrêt de l'attente !");
                break; 
            }

            if (Time.unscaledDeltaTime - startTime >= 1f)
            {
                break;
            }
            yield return null; 
        }


        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
        rb.drag = 0f;
        Physics.gravity = new Vector3(0, -20f, 0);

    }
}
