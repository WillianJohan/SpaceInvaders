using System.Collections;
using UnityEngine;

public class AlienControlCenter : Singleton<AlienControlCenter>
{
    public AlienLineController[] alienLineControl;
	
	[SerializeField] float distanceMovement;
	[SerializeField] float movimentDelay = 0.2f;

	HorizontalMovimentDirection movimentDirection = HorizontalMovimentDirection.right;
	bool needTransition = false;
	bool canMove = false;
	bool isMovingY = false;
	bool isMovingX = false;

	enum HorizontalMovimentDirection { left = -1, right = 1 }

    #region Standard Unity Methods

    protected override void Awake()
    {
		base.Awake();

		AlienDetectionLimit.AlienReachedTheLimit += HandleTransition;
		AlienSpawner.OnStartSpawningAliens += HandleStartSpawningAliens;
		AlienSpawner.OnFinishedSpawningAliens += HandleFinishedSpawningAliens;
    }

    private void OnDestroy()
    {
		AlienDetectionLimit.AlienReachedTheLimit -= HandleTransition;
		AlienSpawner.OnStartSpawningAliens -= HandleStartSpawningAliens;
		AlienSpawner.OnFinishedSpawningAliens -= HandleFinishedSpawningAliens;
	}

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

		for (int i = alienLineControl.Length - 1; i >= 0; i--)
		{
			alienLineControl[i].SendMoveCommand(Vector3.right * distanceMovement * (int)movimentDirection);
			yield return new WaitForSeconds(movimentDelay);
		}

		isMovingX = false;
	}

	IEnumerator MoveAliensDown()
	{
		isMovingY = true;

		movimentDirection = (HorizontalMovimentDirection.right == movimentDirection) ?
			HorizontalMovimentDirection.left :
			HorizontalMovimentDirection.right;

		for (int i = 0; i <= alienLineControl.Length - 1; i++)
		{
			alienLineControl[i].SendMoveCommand(-Vector3.up * distanceMovement); 
			yield return new WaitForSeconds(movimentDelay);
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
		canMove = true;
    }

	#endregion

}
