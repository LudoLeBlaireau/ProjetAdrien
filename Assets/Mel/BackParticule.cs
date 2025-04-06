using UnityEngine;

public class ParticleGroundTester : MonoBehaviour
{
    public Rigidbody playerRb;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public float moveThreshold = 0.1f;

    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        if (playerRb == null || groundCheck == null)
            Debug.LogError("Tu as oublié d’assigner playerRb ou groundCheck !");
    }

    void Update()
    {
        if (playerRb == null || groundCheck == null) return;

        // Est-ce que le joueur est au sol ?
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Est-ce qu’il bouge horizontalement ?
        Vector3 velocity = playerRb.velocity;
        float horizontalSpeed = new Vector3(velocity.x, 0, velocity.z).magnitude;

        if (isGrounded && horizontalSpeed > moveThreshold)
        {
            if (!ps.isPlaying) ps.Play();
        }
        else
        {
            if (ps.isPlaying) ps.Stop();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
