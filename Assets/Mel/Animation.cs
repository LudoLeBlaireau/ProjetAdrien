using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private float dashTimer = 2f;
    private bool canDash = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Calcul de la vitesse du joueur (exemple simple avec les axes)
        float speed = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"));

        // On envoie la vitesse à l'Animator
        animator.SetFloat("Speed", speed);

        // Vérifie si Speed > 1
        if (speed > 1)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= 2f)
            {
                canDash = true;
            }
        }
        else
        {
            dashTimer = 0f;
            canDash = false;
        }

        // Si clic gauche et on peut faire le dash
        if (canDash && Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Dash");
            dashTimer = 2f;
            canDash = false;
        }
    }
}
