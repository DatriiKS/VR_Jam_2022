using AimIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SpawnableEnemy : MonoBehaviour
{
    public enum EnemyState { Spawning, Alive, Dead }
    public AudioSource audio;
    public AudioClip[] searchClips;
    public AudioClip[] angryClips;
    public AudioClip[] spawnClips;
    public AudioClip[] biteClips;
    public int killValue = 1;
    public Transform playerTransform;
    // How often should this agent change it's destination?
    public UnityEvent<SpawnableEnemy> onKillEnemy = new UnityEvent<SpawnableEnemy>();
    public UnityEvent onAttackStart = new UnityEvent();
    public UnityEvent onAttackEnd = new UnityEvent();
    float timer = 1f;
    public NavMeshAgent agent;
    Animator anim;
    bool seenPlayer = false;
    public bool atPlayer = false;
    public float audioTimer = 0;
    public float audioTimerMin = 10;
    public float audioTimerMax = 60;
    public float remainingDistance;
    public GameManager gameManager;
    public EnemyState state = EnemyState.Spawning;
    public Transform vfxTransform;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioTimer = Random.Range(audioTimerMin, audioTimerMax);
        anim = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        playerTransform = gameManager.player.rig.CameraFloorOffsetObject.transform;
        if (agent.enabled)
            agent.SetDestination(playerTransform.position);
        if (spawnClips.Length > 0)
        {
            gameManager.audioManager.sfxAudio.PlayOneShot(AudioManager.GetRandomClip(spawnClips));
        }
        if (vfxTransform != null)
        {
            vfxTransform.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        UpdateAudio();
        UpdateAnimator();
        UpdateNavMeshAgent();

    }

    public void UpdateAudio()
    {
        // Play some audio if we need to
        if (audio != null)
        {
            if (audioTimer <= 0)
            {
                if (seenPlayer)
                {
                    PlayAngrySound();
                }
                else
                {
                    PlaySearchSound();
                }
                audioTimer = Random.Range(audioTimerMin, audioTimerMax);
            }
        }
        audioTimer -= Time.deltaTime;
    }

    public void UpdateAnimator()
    {
        if (anim != null && anim.enabled)
        {
            anim.SetFloat("Speed", agent.velocity.magnitude);
            anim.SetBool("AtPlayer", atPlayer);
            anim.SetBool("PlayerAlive", gameManager.player.state == Player.PlayerState.Alive);
        }

    }
    public void UpdateNavMeshAgent()
    {
        if (agent.enabled)
        {
            remainingDistance = Vector3.Distance(transform.position, gameManager.player.GetPlayerTransform().position);

            if (remainingDistance <= agent.stoppingDistance)
            {
                atPlayer = true;
            }
            else
            {
                atPlayer = false;
            }

            if (!atPlayer)
            {
                agent.SetDestination(gameManager.player.GetPlayerTransform().position);
            }

        }
    }

    public void Kill()
    {
        Debug.Log("Enemy is getting killed!");
        gameManager.AddScore(killValue);
        onKillEnemy.Invoke(this);
        Destroy(gameObject);
    }

    public void PlaySearchSound()
    {
        if (searchClips.Length > 0)
            audio.PlayOneShot(AudioManager.GetRandomClip(searchClips));
    }
    public void PlayAngrySound()
    {
        if (angryClips.Length > 0)
            audio.PlayOneShot(AudioManager.GetRandomClip(angryClips));
    }

    public void SetSeenPlayer(GameObject playerObject)
    {
        seenPlayer = true;
        if (TryGetComponent<AimIKBehaviour>(out var aik))
        {
            aik.Target = gameManager.player.GetComponentInChildren<Camera>().transform;
        }
    }

    public void Attack()
    {
        AttackStart();
    }
    public void AttackStart()
    {
        onAttackStart.Invoke();
        audio.PlayOneShot(AudioManager.GetRandomClip(biteClips));

    }
    public void AttackEnd()
    {
        onAttackEnd.Invoke();
    }

    public void RightFootStep()
    {
        audio.PlayOneShot(gameManager.audioManager.footStepClips[0]);

    }


    public void LeftFootStep()
    {
        if (gameManager.audioManager.footStepClips.Length > 0)
            audio.PlayOneShot(gameManager.audioManager.footStepClips[1]);
    }

    public void BiteStart()
    {
        if (gameManager.audioManager.footStepClips.Length > 0)
            audio.PlayOneShot(AudioManager.GetRandomClip(biteClips));
    }

    public void StandStart()
    {
        if (agent)
            agent.enabled = false;
    }

    public void StandEnd() { 
        if(agent) {
            agent.enabled = true;
            agent.SetDestination(playerTransform.position);
        }
    }

    public void PlayWithAudioManager()
    {
        gameManager.audioManager.PlayGhostDieSound();
    }

    public void ReparentEffect()
    {
        if(vfxTransform != null)
        {
            gameManager.effectManager.ReparentEffect(vfxTransform);
        }
    }

}
