using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public Renderer CrystalRenderer;
    public BoxCollider Box;

   public void Disparition()
   {
        CrystalRenderer.enabled = false;
        Box.enabled = false;
   }

}
