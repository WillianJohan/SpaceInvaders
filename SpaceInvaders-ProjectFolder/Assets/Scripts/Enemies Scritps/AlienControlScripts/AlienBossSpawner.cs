using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBossSpawner : MonoBehaviour
{
	[Header("Boss Configuration")]
	[SerializeField] GameObject AlienBossPrefab;
	[SerializeField] Transform spawnPosition;
	[SerializeField] int minBossSpawnTime = 15;
	[SerializeField] int maxBossSpawnTime = 45;
	bool CanSpawn = false;
	Coroutine bossSpawnCoroutine = null;

	#region Unity Standard's Methods.

	private void Awake()
	{
		AlienSpawner.OnStartSpawningAliens += HandleOnStartSpawnAliens;
		AlienSpawner.OnFinishedSpawningAliens += HandleOnStopSpawnAliens;
		AlienBossBehaviour.OnBossDie += HandleOnBossDie;
	}
	private void OnDestroy()
	{
		AlienSpawner.OnStartSpawningAliens -= HandleOnStartSpawnAliens;
		AlienSpawner.OnFinishedSpawningAliens -= HandleOnStopSpawnAliens;
		AlienBossBehaviour.OnBossDie -= HandleOnBossDie;
	}

    #endregion

    public void ConfigureSpawnBossPosition(Transform offsetPosition)
		=> this.spawnPosition = offsetPosition;
		
	void ConfigureAndStartCoroutine()
    {
		if (!AlienBossPrefab || !CanSpawn)
			return;

		bossSpawnCoroutine = StartCoroutine("SpawnBossCoroutine", spawnPosition);
	}
	
	IEnumerator SpawnBossCoroutine(Transform offsetPosition)
	{
		int spawnTime = UnityEngine.Random.Range(minBossSpawnTime, maxBossSpawnTime);
		yield return new WaitForSeconds(spawnTime);
		Instantiate(AlienBossPrefab, offsetPosition.position, Quaternion.identity, this.transform);
	}

	#region HandleEvents

	void HandleOnStartSpawnAliens()
	{
		CanSpawn = false;
		if (bossSpawnCoroutine != null)
		{
			StopCoroutine(bossSpawnCoroutine);
			bossSpawnCoroutine = null;
		}
	}

	void HandleOnStopSpawnAliens()
	{
		CanSpawn = true;
		ConfigureAndStartCoroutine();
	}

	void HandleOnBossDie()
		=> ConfigureAndStartCoroutine();

	#endregion

}
