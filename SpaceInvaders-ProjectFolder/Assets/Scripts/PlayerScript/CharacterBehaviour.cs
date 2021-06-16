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

    [Header("Movimentation Sensitivity")]
    [SerializeField] float keyboardMovimentSensitivity;
    [SerializeField] float mouseSensitivity;

    bool CanMove = false;

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

    void Update() => HandleMovimentationBehaviour();

    #endregion

    #region Player behaviour methods

    
    //TODO: Colocar o player na posição do mouse (dentro das limitações)
    void HandleMovimentationBehaviour()
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

    #endregion

    #region Handle events methods

    void HandleInitGame()           => CanMove = true;
    void HandleStartingNewWave()    => CanMove = true;

    #endregion

}

