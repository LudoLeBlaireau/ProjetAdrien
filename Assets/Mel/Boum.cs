using UnityEngine;

public class VFXOnDestroy : MonoBehaviour
{
    public ParticleSystem vfx; // Référence à l'effet visuel

    // Cette fonction est appelée juste avant que l'objet ne soit détruit
    void OnDestroy()
    {
        PlayVFX(); // Jouer l'effet visuel lorsque l'objet est détruit
    }

    // Fonction pour jouer l'effet visuel
    void PlayVFX()
    {
        if (vfx != null && !vfx.isPlaying)
        {
            vfx.Play(); // Jouer l'effet visuel si ce n'est pas déjà en cours
        }
    }

    // Exemple de destruction manuelle du GameObject avec un délai
    public void DestroyObjectWithVFX()
    {
        // Jouer l'effet avant la destruction
        PlayVFX();

        // Détruire l'objet immédiatement après
        Destroy(gameObject); // Détruire l'objet
    }
}
