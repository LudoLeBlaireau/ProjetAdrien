
using TMPro;
using UnityEngine;

public class Poussiere : MonoBehaviour
{
    [Header("Références")]
    public Transform target;
    public ParticleSystem particleSystemToStart;

    private Vector3 spawnposition;
    private bool isActive = false;

    [Header("Paramètres")]
    public float DustDuration = 0.5f;

    private void Start()
    {
        if (particleSystemToStart != null)
        {
            particleSystemToStart.Stop();
        }
    }

    void Update()
    {
        
    }
    public void PoussiereArrive(float GroundHeight)
    {

        if (particleSystemToStart != null && !isActive)
        {
            particleSystemToStart.transform.position = new Vector3(target.position.x, GroundHeight + 0.1f, target.position.z);
            particleSystemToStart.Play();

            Invoke(nameof(StopPoussiere), DustDuration);
        }
    }
    private void StopPoussiere()
    {
        if (particleSystemToStart != null)
        {
            isActive = false;
            particleSystemToStart.Stop();

        }
    }

    public void ColorParticle(Color detectedColor)
    {
        var colormodule = particleSystemToStart.colorOverLifetime;
        colormodule.enabled = true;
        Gradient gradient = new Gradient();
        gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(detectedColor, 0.0f), new GradientColorKey(detectedColor, 0.08f), new GradientColorKey(new Color(detectedColor.r,detectedColor.g, detectedColor.b,0), 1.0f ) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f),  new GradientAlphaKey(1.0f, 0.8f), new GradientAlphaKey(0.0f, 1.0f) });
        colormodule.color = new ParticleSystem.MinMaxGradient(gradient); 
    }


}
