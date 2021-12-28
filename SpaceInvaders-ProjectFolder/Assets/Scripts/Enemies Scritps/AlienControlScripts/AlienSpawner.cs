using System;
using System.Collections;
using UnityEngine;

public class AlienSpawner : Singleton<AlienSpawner>
{
	[SerializeField] AlienControlCenter controlCenter;
	[SerializeField] int enemiesForLine = 11;
	[SerializeField] float horizontalSpace;
	[SerializeField] float verticalSpace;
	[SerializeField] float spawnVelocity = 0.03f;

	public static event Action OnStartSpawningAliens;
	public static event Action OnFinishedSpawningAliens;

	public void SpawnAliens(Transform offsetPosition) => StartCoroutine("SpawnEnemieLines", offsetPosition);

	IEnumerator SpawnEnemieLines(Transform offesetPosition)
    {
		OnStartSpawningAliens?.Invoke();

		for (int y = 0; y <= controlCenter.alienLineControl.Length -1 ; y++)
		{
			for (int x = 0; x < enemiesForLine; x++)
			{
				Vector3 spawnPosition = new Vector3(
					offesetPosition.position.x + (x * horizontalSpace),
					offesetPosition.position.y + (y * verticalSpace),
					offesetPosition.position.z);

				GameObject alienInstance = Instantiate(controlCenter.alienLineControl[y].AlienPrefab, spawnPosition, Quaternion.identity, this.transform);
				controlCenter.alienLineControl[y].AddAlien(alienInstance);

				yield return new WaitForSeconds(spawnVelocity);
			}
			yield return new WaitForSeconds(spawnVelocity);
		}

		OnFinishedSpawningAliens?.Invoke();

		yield return null;
    }
}
