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
    [SerializeField] List<AudioClip> NewWaveAudioClips = new List<AudioClip>();
    [SerializeField] List<AudioClip> SpawnElementAudioClips = new List<AudioClip>();
    [SerializeField] List<AudioClip> PlayerHitAudioClips = new List<AudioClip>();
    [SerializeField] List<AudioClip> EndGameAudioClips = new List<AudioClip>();

    [Header("Audio Sources")]
    [SerializeField] AudioSource PlayerLaserShootSource;
    [SerializeField] AudioSource AlienLaserShootSource;
    [SerializeField] AudioSource ProjectileHitSource;
    [SerializeField] AudioSource ExplosionSource;
    [SerializeField] AudioSource AlienMovimentSource;
    [SerializeField] AudioSource NewWaveSource;
    [SerializeField] AudioSource PlayerHitSource;
    [SerializeField] AudioSource SpawnElementSource;
    [SerializeField] AudioSource EndGameSource;

    #region Standard Methods

    protected override void Awake()
    {
        base.Awake();

        ProjectileBehaviour.OnHit += HandleOnHit;
        AlienHealthHandler.OnAlienDie += HandleOnAlienDie;
        AlienControlCenter.OnBeginMoviment += HandleOnAlienBeginMovimet;
        AlienCombatBehaviour.OnShoot += HandleOnAlienShoot;
        PlayerCombatBehaviour.OnShoot += HandleOnPlayerShoot;
        EndGameManager.EndGame += HandleOnEndGame;
        PlayerHealthHandler.OnPlayerHit += HandleOnPlayerHit;
        GameManager.StartingNewWave += HandleOnNewWave;

        //SpawnElementSource;
    }

    void OnDestroy()
    {
        ProjectileBehaviour.OnHit -= HandleOnHit;
        AlienHealthHandler.OnAlienDie -= HandleOnAlienDie;
        AlienControlCenter.OnBeginMoviment -= HandleOnAlienBeginMovimet;
        AlienCombatBehaviour.OnShoot -= HandleOnAlienShoot;
        PlayerCombatBehaviour.OnShoot -= HandleOnPlayerShoot;
        EndGameManager.EndGame -= HandleOnEndGame;
        PlayerHealthHandler.OnPlayerHit -= HandleOnPlayerHit;
        GameManager.StartingNewWave -= HandleOnNewWave;

        //SpawnElementSource;
    }

    #endregion

    #region Methods

    public void PlayRandomSound(AudioSource source, List<AudioClip> audioClipList)
    {
        if (audioClipList.Count == 0 || source == null)
            return;

        int i = Random.Range(0, audioClipList.Count - 1);
        source.clip = audioClipList[i];
        source.Play();
    }

    void PlayPlayerShoot() => PlayRandomSound(PlayerLaserShootSource, PlayerLaserShootAudioClips);
    void PlayAlienShoot() => PlayRandomSound(AlienLaserShootSource, AlienLaserShootAudioClips);
    void PlayHit() => PlayRandomSound(ProjectileHitSource, HitAudioClips);
    void PlayExplosion() => PlayRandomSound(ExplosionSource, ExplosionAudioClips);
    void PlayAlienMoviment() => PlayRandomSound(AlienMovimentSource, AlienMovimentAudioClips);
    void NewWaveSound() => PlayRandomSound(NewWaveSource, NewWaveAudioClips);
    void PlaySpawnSound() => PlayRandomSound(SpawnElementSource, SpawnElementAudioClips);
    void PlayEndGameSound() => PlayRandomSound(EndGameSource, EndGameAudioClips);
    void PlayPlayerHitSound() => PlayRandomSound(PlayerHitSource, PlayerHitAudioClips);
    void PlayNewWaveSound() => PlayRandomSound(NewWaveSource, NewWaveAudioClips);

    #endregion

    #region Handles

    void HandleOnAlienDie(AlienType type) => PlayExplosion();
    void HandleOnHit(Collider other) => PlayHit();
    void HandleOnPlayerShoot() => PlayPlayerShoot();
    void HandleOnAlienShoot() => PlayAlienShoot();
    void HandleOnAlienBeginMovimet() => PlayAlienMoviment();
    void HandleOnSpawnElement() => PlaySpawnSound();
    void HandleOnEndGame() => PlayEndGameSound();
    void HandleOnPlayerHit() => PlayPlayerHitSound();
    void HandleOnNewWave() => PlayNewWaveSound();

    #endregion
}
