using System.Collections;
using UnityEngine;

public class AlienControlCenter : Singleton<AlienControlCenter>
{
    public AlienLineController[] alienLineControl;
	
	[SerializeField] float minDistanceOfMovement;
	[SerializeField] float maxDistanceOfMovement;
	[SerializeField] float minFrequencyMoviment = 0.1f;

	HorizontalMovimentDirection movimentDirection = HorizontalMovimentDirection.right;
	bool needTransition = false;
	bool canMove = false;
	bool isMovingY = false;
	bool isMovingX = false;
	float aliensLengh = 0;

	enum HorizontalMovimentDirection { left = -1, right = 1 }
	GameManager gameManager;

    #region Standard Unity Methods

    protected override void Awake()
    {
		base.Awake();

		SideBarriers.AlienReachedTheLimit += HandleTransition;
		AlienSpawner.OnStartSpawningAliens += HandleStartSpawningAliens;
		AlienSpawner.OnFinishedSpawningAliens += HandleFinishedSpawningAliens;
    }

    private void OnDestroy()
    {
		SideBarriers.AlienReachedTheLimit -= HandleTransition;
		AlienSpawner.OnStartSpawningAliens -= HandleStartSpawningAliens;
		AlienSpawner.OnFinishedSpawningAliens -= HandleFinishedSpawningAliens;
	}

	void Start() => gameManager = FindObjectOfType<GameManager>();

    void Update() => MovimentBehaviour();

	#endregion

	#region Moviment Behaviour methods

	void MovimentBehaviour()
    {
		if (!canMove)
			return;

		if (isMovingX)
			return;

		if (!isMovingY && !needTransition)
			StartCoroutine(MoveAlienHorizontal());
		else if (!isMovingY && needTransition)
			StartCoroutine(MoveAliensDown());
	}

    IEnumerator MoveAlienHorizontal()
	{
		isMovingX = true;

		float x = (GameManager.Instance.AliensAlive / aliensLengh);
		float a = minDistanceOfMovement - maxDistanceOfMovement;
		float c = maxDistanceOfMovement;

		float distance = a * x + c;

		for (int i = alienLineControl.Length - 1; i >= 0; i--)
		{
			alienLineControl[i].MoveLine(Vector3.right * distance * (int)movimentDirection);

			float waitSeconds = (aliensLengh != 0) ?
				minFrequencyMoviment * (gameManager.AliensAlive / aliensLengh) :
				minFrequencyMoviment;

			yield return new WaitForSeconds(waitSeconds);
		}

		isMovingX = false;
	}

	IEnumerator MoveAliensDown()
	{
		isMovingY = true;

		movimentDirection = (HorizontalMovimentDirection.right == movimentDirection) ?
			HorizontalMovimentDirection.left :
			HorizontalMovimentDirection.right;

		float x = (gameManager.AliensAlive / aliensLengh);
		float a = minDistanceOfMovement - maxDistanceOfMovement;
		float c = maxDistanceOfMovement;

		float distance = (a * x + c) * 2;

		for (int i = 0; i <= alienLineControl.Length - 1; i++)
		{
			alienLineControl[i].MoveLine(-Vector3.up * distance);
			
			float waitSeconds = (aliensLengh != 0) ? 
				minFrequencyMoviment * (gameManager.AliensAlive / aliensLengh) : 
				minFrequencyMoviment;

			yield return new WaitForSeconds(waitSeconds);
		}
		
		needTransition = false;
		isMovingY = false;
	}

	#endregion

	#region Event Handlers

	void HandleTransition() => needTransition = true;

	void HandleStartSpawningAliens()
    {
		StopAllCoroutines();

		canMove = false;
		isMovingX = false;
		isMovingY = false;
		needTransition = false;
		movimentDirection = HorizontalMovimentDirection.right;
	}

	void HandleFinishedSpawningAliens()
    {
		aliensLengh = gameManager.AliensAlive;
		canMove = true;
    }

	#endregion

}
