using UnityEngine;

public class TrailFollowDash : MonoBehaviour
{
    public Dash dashScript; // à lier dans l'inspecteur

    private TrailRenderer trail;

    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        if (trail != null)
            trail.enabled = false;
    }

    void Update()
    {
        if (dashScript == null || trail == null) return;

        trail.enabled = dashScript.isDashing;
    }
}
