using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [Header("Movement Delimitation")]
    [SerializeField] Transform leftMovimentLimit;
    [SerializeField] Transform rightMovimentLimit;

    [Header("Keyboard Input")]
    public KeyCode key_Left;
    public KeyCode key_Right;
    public KeyCode key_Shot;
    [SerializeField] float keyboardMovimentSensitivity;

    [Header("Mouse")]
    [SerializeField] float mouseSensitivity;

    [Header("Combat Variables")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileInitialPosition;
    [SerializeField] float shootDelay = 0.3f;
    [SerializeField] int maximumActiveProjectilesSimultaneously = 3;
    List<GameObject> bulletInstances = new List<GameObject>();

    bool CanMove = false;
    bool CanShot = false;

    #region Unity Standard Methods

    void Awake()
    {
        GameManager.StartGame += HandleInitGame;
        GameManager.StartingNewWave += HandleStartingNewWave;
    }

    void OnDestroy()
    {
        GameManager.StartGame -= HandleInitGame;
        GameManager.StartingNewWave -= HandleStartingNewWave;
    }

    void Update()
    {
        HandleShotCommand();
        HandleMovimentation();
    }

    #endregion

    #region Player behaviour methods

    void HandleShotCommand()
    {
        //Removes the projectiles that were destroyed
        for (int i = 0; i < bulletInstances.Count; i++)
            if (bulletInstances[i] == null)
                bulletInstances.RemoveAt(i);

        bool hasAmmo = maximumActiveProjectilesSimultaneously > bulletInstances.Count;
        bool wantToShoot = (Input.GetKey(key_Shot) || Input.GetMouseButton(0));
        if (wantToShoot && CanShot && hasAmmo)
            StartCoroutine(Shoot());
    }

    void HandleMovimentation()
    {
        if (!CanMove)
            return;

        float inputVelocity = GetHorizontalInputVelocity();
        if (inputVelocity == 0)
            return;
        else if (inputVelocity > 0 && transform.position.x >= rightMovimentLimit.position.x)
        {
            transform.position = new Vector3(rightMovimentLimit.position.x, transform.position.y, transform.position.z);
            return;
        }
        else if (inputVelocity < 0 && transform.position.x <= leftMovimentLimit.position.x)
        {
            transform.position = new Vector3(leftMovimentLimit.position.x, transform.position.y, transform.position.z);
            return;
        }

        transform.Translate(Vector3.right * inputVelocity * Time.deltaTime);
    }

    float GetHorizontalInputVelocity()
    {
        if (Input.GetAxis("Mouse X") != 0)
            return Input.GetAxis("Mouse X") * mouseSensitivity;
        else if (Input.GetKey(key_Left) && !Input.GetKey(key_Right))
            return -keyboardMovimentSensitivity;
        else if (Input.GetKey(key_Right) && !Input.GetKey(key_Left))
            return keyboardMovimentSensitivity;

        return 0;
    }

    IEnumerator Shoot()
    {
        CanShot = false;
        bulletInstances.Add(Instantiate(projectilePrefab, projectileInitialPosition.position, Quaternion.identity));
        yield return new WaitForSeconds(shootDelay);
        CanShot = true;
    }

    #endregion

    #region Handle events methods

    void HandleInitGame()
    {
        CanMove = true;
        CanShot = true;
    }

    void HandleStartingNewWave() 
    {
        CanMove = true;
        CanShot = false;
    }

    #endregion

}

