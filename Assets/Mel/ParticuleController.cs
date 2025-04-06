using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem movementParticle;

    [Range(0, 10)]
    [SerializeField] float occurAfterVelocity = 0.5f;

    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod = 0.1f;

    [SerializeField] Rigidbody playerRb;
    [SerializeField] Transform groundCheck; // Un empty sous le joueur
    [SerializeField] float groundDistance = 0.3f;
    [SerializeField] LayerMask groundMask;

    float counter;
    bool isGrounded;

    void Update()
    {
        float verticalSpeed = playerRb.velocity.y;
        float horizontalSpeed = new Vector2(playerRb.velocity.x, playerRb.velocity.z).magnitude;

        if (horizontalSpeed > occurAfterVelocity && verticalSpeed < 0.1f)
        {
            if (!movementParticle.isPlaying)
                movementParticle.Play();
        }
        else
        {
            if (movementParticle.isPlaying)
                movementParticle.Stop();
        }
    }

}
