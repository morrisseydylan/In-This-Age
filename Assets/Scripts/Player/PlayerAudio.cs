using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    // Add Audioclips
    [Header("Audio Clips for the Player")]
    public AudioClip WalkAudioClip;
    public bool LoopWalkAudio = false;
    public AudioClip AttackAudioClip;
    public bool LoopAttackAudio = false;
    public AudioClip DeathAudioClip;
    public bool LoopDeathAudio = false;
    public AudioClip JumpAudioClip;
    public bool LoopJumpAudio = false;

    [Range(0,1)]
    public float VolumeLevel = 1;


    // Create the respective AudioSource
    [HideInInspector] public AudioSource WalkSource;
    [HideInInspector] public AudioSource AttackSource;
    [HideInInspector] public AudioSource DeathSource;
    [HideInInspector] public AudioSource JumpSource;

    void Start()
    {
        SetUpAudio();
    }

    void SetUpAudio()
    {
        GameObject WalkGameObject = new GameObject("WalkAudioSource");
        GameObject AttackGameObject = new GameObject("AttackAudioSource");
        GameObject DeathGameObject = new GameObject("DeathAudioSource");
        GameObject JumpGameObject = new GameObject("JumpAudioSource");

        AssignParent(WalkGameObject);
        AssignParent(AttackGameObject);
        AssignParent(DeathGameObject);
        AssignParent(JumpGameObject);

        WalkSource = WalkGameObject.AddComponent<AudioSource>();
        AttackSource = AttackGameObject.AddComponent<AudioSource>();
        DeathSource = DeathGameObject.AddComponent<AudioSource>();
        JumpSource = DeathGameObject.AddComponent<AudioSource>();

        WalkSource.clip = WalkAudioClip;
        AttackSource.clip = AttackAudioClip;
        DeathSource.clip = DeathAudioClip;
        JumpSource.clip = JumpAudioClip;

        WalkSource.volume = VolumeLevel;
        AttackSource.volume = VolumeLevel;
        DeathSource.volume = VolumeLevel;
        JumpSource.volume = VolumeLevel;

        WalkSource.loop = LoopWalkAudio;
        AttackSource.loop = LoopAttackAudio;
        DeathSource.loop = LoopDeathAudio;
        JumpSource.loop = LoopJumpAudio;
    }

    //Just a helper function that assigns whatever object as a child of this gameObject
    void AssignParent(GameObject obj)
    {
        obj.transform.parent = transform;
    }
}
