using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
//using UnityEditor.Experimental.GraphView;
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
    public ParticleSystem Boum;

    [Header("Parametre du respawn")]
    bool IsRespawnable = false;
    private Queue<Vector3> TenPosition = new Queue<Vector3>();
    private Vector3 OldPosition = Vector3.zero;
    private List<Vector3> vectorList = new List<Vector3>();

    public float Speed = 5f;
    public static float IndiChiffremort = 0f;

    Audio_manager audio_Manager;

    private void Awake()
    {
        audio_Manager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio_manager>();
    }



    public void Start()
    {
  
        ParticleRender.enabled = false;
    }

    void Update()
    {
        Debug.Log(IndiChiffremort);
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
        IndicateurDeMort();
        
        CharacterRender.enabled = false;
        ParticleRender.enabled = true;
        Poussiere.enabled = false;
        DeadParticle.position = target.position;
        audio_Manager.PlaySFX(audio_Manager.death);


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
       
        //ResetQueueAfterRespawn();
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

    public void IndicateurDeMort()
    {
        IndiChiffremort += 1f;
        Debug.Log("mort : " + IndiChiffremort);
    }

    public void ResetIndicateurDeMort()
    {
        IndiChiffremort = 0f;
        Debug.Log("Reset mort : " + IndiChiffremort);
    }



    //void ResetQueueAfterRespawn()
    //{
    //    TenPosition.Clear();
    //    foreach (var position in vectorList)
    //    {
    //        TenPosition.Enqueue(position);
    //    }

    //    if (TenPosition.Count == 0)
    //    {
    //        TenPosition.Enqueue(target.position);
    //    }
    //}
     
}
