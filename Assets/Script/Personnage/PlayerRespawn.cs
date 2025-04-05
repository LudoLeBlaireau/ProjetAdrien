using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerRespawn : MonoBehaviour
{

    [Header("Référence")]
    public Transform target;
    public Transform DeadParticle;
    public Renderer CharacterRender;
    public Renderer ParticleRender;
    public Camera Maincamera;
    public Movement Movement;
    public Renderer Poussiere;
      
    [Header("Parametre du respawn")]
    bool IsRespawnable = false;
    private Queue<Vector3> TenPosition = new Queue<Vector3>();
    private Vector3 OldPosition = Vector3.zero;
    private List<Vector3> vectorList = new List<Vector3>();

    public float Speed = 5f;
  

    public void Start()
    {
  
        ParticleRender.enabled = false;
    }

    void Update()
    {
        TrackLastPositions();
    }
    public void TrackLastPositions()
    {
        Vector3 currentPosition = target.transform.position;
        if (Vector3.Distance(currentPosition, OldPosition) > 0.01f)
        {
            OldPosition = currentPosition;
            TenPosition.Enqueue(currentPosition);
            if (TenPosition.Count > 100)
            {
                TenPosition.Dequeue();
            }
        }
    }


    public void RespawnToken()
    {
        if (!IsRespawnable)
        { 
        IsRespawnable = true;
        Respawnable();
        }
    }

    public async void Respawnable()
    {
        if (!IsRespawnable) return;
        
        CharacterRender.enabled = false;
        ParticleRender.enabled = true;
        Poussiere.enabled = false;
        DeadParticle.position = target.position;
 

        Movement.canmove = false;

        ConvertQueueToList();

        if (vectorList.Count > 0)
        {
            for (int i = vectorList.Count - 1; i >= 0; i--)
            {
                target.position = vectorList[i];
                DeadParticle.position = vectorList[i];
                await Task.Delay(5);
            }
        }
      
        IsRespawnable = false;
        ParticleRender.enabled = false;
        CharacterRender.enabled = true;
        Poussiere.enabled = true;
        Movement.canmove = true;
       
        ResetQueueAfterRespawn();
        Movement.AgainPieged();

    }


    void ConvertQueueToList()
    {
        vectorList.Clear();
        foreach (var position in TenPosition)
        {
            vectorList.Add(position);
        }
    }

    void ResetQueueAfterRespawn()
    {
        TenPosition.Clear();
        foreach (var position in vectorList)
        {
            TenPosition.Enqueue(position);
        }

        if (TenPosition.Count == 0)
        {
            TenPosition.Enqueue(target.position);
        }
    }
     
}
