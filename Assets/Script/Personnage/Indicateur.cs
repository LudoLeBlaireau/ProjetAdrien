using UnityEngine;
using UnityEngine.Android;
using static UnityEngine.EventSystems.EventTrigger;

public class indicateur : MonoBehaviour
{
    public ParticleSystem SystemParcile;
    private ParticleSystem.VelocityOverLifetimeModule velocityModule;
    private Vector3 centeredmouseposition;
    Vector3 CurrentMouseLocation;
    Vector3 NewMouseLocation;
    float centerx = Screen.width / 2f;
    float centery = (Screen.height / 2f) - 70f;

    private void Start()
    {
        if (SystemParcile == null)
            SystemParcile = GetComponent<ParticleSystem>();
        velocityModule = SystemParcile.velocityOverLifetime;
        velocityModule.enabled = true;

        CurrentMouseLocation = new Vector3(Input.mousePosition.x - centerx, Input.mousePosition.y - centery);
    }
    private void Update()
    {
        Inertie();
        
    }
    public void positionnement(Vector3 pointB)
    {
        transform.position = pointB;
    }
    void Inertie()
    {
       if(CurrentMouseLocation != null)
        {
            NewMouseLocation = new Vector3(Input.mousePosition.x - centerx, Input.mousePosition.y - centery);
            if(CurrentMouseLocation == NewMouseLocation)
            {
                velocityModule.x = 0;
                velocityModule.y = 0;
            }
            if (CurrentMouseLocation != NewMouseLocation && CurrentMouseLocation != null && NewMouseLocation != CurrentMouseLocation)
            {
                Vector3 MouseLocationNormalized = NewMouseLocation.normalized;
                velocityModule.x = (MouseLocationNormalized.x * 2);
                velocityModule.y = (MouseLocationNormalized.y * 2);
                CurrentMouseLocation = NewMouseLocation;
            }
        }
    }
}
