using System.Collections;
using UnityEngine;

public class AlienControlCenter : Singleton<AlienControlCenter>
{
    public AlienLineController[] alienLineControl;
	
	[SerializeField] float distanceMovement;
	[SerializeField] float movimentDelay = 0.2f;

	HorizontalMovimentDirection movimentDirection = HorizontalMovimentDirection.right;
	bool needTransition = false;
	bool isMovingY = false;
	bool isMovingX = false;

	enum HorizontalMovimentDirection { left = -1, right = 1 }

    #region Standard Unity Methods

    protected override void Awake()
    {
		base.Awake();

		// subscribe to transition
		AlienDetectionLimit.AlienReachedTheLimit += HandleTransition;
    }

    private void OnDestroy()
    {
		// unsubscribe to transition
		AlienDetectionLimit.AlienReachedTheLimit -= HandleTransition;
	}

    void Update()
    {
		if (isMovingX)
			return;

		if (!isMovingY && !needTransition)
			StartCoroutine(MoveAlienHorizontal());
		else if (!isMovingY && needTransition)
			StartCoroutine(MoveAliensDown());
    }

    #endregion

    #region Moviment Behaviour methods

    void HandleTransition() => needTransition = true;

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

		for (int i = alienLineControl.Length - 1; i >= 0; i--)
		{
			alienLineControl[i].SendMoveCommand(-Vector3.up * distanceMovement); 
			yield return new WaitForSeconds(movimentDelay);
		}
		
		needTransition = false;
		isMovingY = false;
	}

    #endregion
}
