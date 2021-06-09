using System.Collections;
using UnityEngine;

public class AlienSpawner : Singleton<AlienSpawner>
{

	[SerializeField] AlienLineControl[] alienLines;
	[SerializeField] int amountOfAlienForEachLine = 11;
	[SerializeField] float spaceBetweenEnemies;

	void Start() => StartCoroutine(SpawnEnemieLines());

    IEnumerator SpawnEnemieLines()
    {
		AlienCombatBehaviour.IsEnabled = false; // TODO: Essa linha pode ser alterada futuramente

		for (int i = alienLines.Length - 1; i >= 0; i--)
		{
			for (int b = 0; b < amountOfAlienForEachLine; b++)
			{
				Transform LineTransform = alienLines[i].GetComponent<Transform>();
				Vector3 spawnPosition = new Vector3(LineTransform.position.x + (b * spaceBetweenEnemies), LineTransform.position.y, LineTransform.position.z);
				Instantiate(alienLines[i].AlienPrefab, spawnPosition, Quaternion.identity, alienLines[i].gameObject.transform);

				yield return new WaitForSeconds(0.02f);
			}
			yield return new WaitForSeconds(0.05f);
		}

		GetComponent<AlienControlCenter>().enabled = true;
		
		
		AlienCombatBehaviour.IsEnabled = true; // TODO: Essa linha pode ser alterada futuramente

		yield return null;
    }

}
