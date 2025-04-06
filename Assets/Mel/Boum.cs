using UnityEngine;

public class VFXOnDestroy : MonoBehaviour
{
    public ParticleSystem vfx; // R�f�rence � l'effet visuel

    // Cette fonction est appel�e juste avant que l'objet ne soit d�truit
    void OnDestroy()
    {
        PlayVFX(); // Jouer l'effet visuel lorsque l'objet est d�truit
    }

    // Fonction pour jouer l'effet visuel
    void PlayVFX()
    {
        if (vfx != null && !vfx.isPlaying)
        {
            vfx.Play(); // Jouer l'effet visuel si ce n'est pas d�j� en cours
        }
    }

    // Exemple de destruction manuelle du GameObject avec un d�lai
    public void DestroyObjectWithVFX()
    {
        // Jouer l'effet avant la destruction
        PlayVFX();

        // D�truire l'objet imm�diatement apr�s
        Destroy(gameObject); // D�truire l'objet
    }
}
