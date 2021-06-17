using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Clips")]
    [SerializeField] List<AudioClip> PlayerLaserShootAudioClips = new List<AudioClip>();
    [SerializeField] List<AudioClip> AlienLaserShootAudioClips = new List<AudioClip>();
    [SerializeField] List<AudioClip> HitAudioClips = new List<AudioClip>();
    [SerializeField] List<AudioClip> ExplosionAudioClips = new List<AudioClip>();
    [SerializeField] List<AudioClip> AlienMovimentAudioClips = new List<AudioClip>();
    [SerializeField] List<AudioClip> PowerUpAudioClips = new List<AudioClip>();

    [Header("Audio Sources")]
    [SerializeField] AudioSource PlayerLaserShootSource;
    [SerializeField] AudioSource AlienLaserShootSource;
    [SerializeField] AudioSource HitSource;
    [SerializeField] AudioSource ExplosionSource;
    [SerializeField] AudioSource AlienMovimentSource;
    [SerializeField] AudioSource PowerUpSource;

    #region Standard Methods

    protected override void Awake()
    {
        base.Awake();

        ProjectileBehaviour.OnHit += HandleOnHit;
        AlienHealthHandler.OnAlienDie += HandleOnAlienDie;
        AlienControlCenter.OnBeginMoviment += HandleOnAlienBeginMovimet;
        AlienCombatBehaviour.OnShoot += HandleOnAlienShoot;
        PlayerCombatBehaviour.OnShoot += HandleOnPlayerShoot;

    }

    void OnDestroy()
    {
        ProjectileBehaviour.OnHit -= HandleOnHit;
        AlienHealthHandler.OnAlienDie -= HandleOnAlienDie;
        AlienControlCenter.OnBeginMoviment -= HandleOnAlienBeginMovimet;
        AlienCombatBehaviour.OnShoot -= HandleOnAlienShoot;
        PlayerCombatBehaviour.OnShoot -= HandleOnPlayerShoot;
    }

    #endregion

    #region Methods

    public void PlayRandomSound(AudioSource source, List<AudioClip> audioClipList)
    {
        int i = Random.Range(0, audioClipList.Count - 1);
        source.clip = audioClipList[i];
        source.Play();
    }

    void PlayPlayerShoot() => PlayRandomSound(PlayerLaserShootSource, PlayerLaserShootAudioClips);
    void PlayAlienShoot() => PlayRandomSound(AlienLaserShootSource, AlienLaserShootAudioClips);
    void PlayHit() => PlayRandomSound(HitSource, HitAudioClips);
    void PlayExplosion() => PlayRandomSound(ExplosionSource, ExplosionAudioClips);
    void PlayAlienMoviment() => PlayRandomSound(AlienMovimentSource, AlienMovimentAudioClips);
    void PlayPowerUp() => PlayRandomSound(PowerUpSource, PowerUpAudioClips);

    #endregion

    #region Handles

    void HandleOnAlienDie(AlienType type) => PlayExplosion();
    void HandleOnHit(Collider other) => PlayHit();
    void HandleOnPlayerShoot() => PlayPlayerShoot();
    void HandleOnAlienShoot() => PlayAlienShoot();
    void HandleOnAlienBeginMovimet() => PlayAlienMoviment();

    #endregion
}
