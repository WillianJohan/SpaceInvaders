// Original Code from: https://gist.github.com/ftvs/5822103


using UnityEngine;

public class CameraShake : MonoBehaviour
{
	[SerializeField] Transform camTransform;
	[SerializeField] float shakeDuration = 0.1f;
	[SerializeField] float shakeAmount = 0.7f;
	[SerializeField] float decreaseFactor = 1.0f;

	Vector3 originalPos = Vector3.zero;
	float currentShakeTime = 0;
	bool doShake = false;


	void Awake()		=> PlayerHealthHandler.OnPlayerHit += HandleOnPlayerHit;
	void OnDestroy()	=> PlayerHealthHandler.OnPlayerHit -= HandleOnPlayerHit;
	
	void Start()		=> originalPos = camTransform.localPosition;


	void Update()
	{
		if (!doShake)
			return;

		if (currentShakeTime > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			currentShakeTime -= decreaseFactor * Time.deltaTime;
			return;
		}
	
		//Reset camera position
		currentShakeTime = 0f;
		camTransform.localPosition = originalPos;
		doShake = false;
	}


	void HandleOnPlayerHit()
	{
		currentShakeTime = shakeDuration;
		doShake = true;
	}

}