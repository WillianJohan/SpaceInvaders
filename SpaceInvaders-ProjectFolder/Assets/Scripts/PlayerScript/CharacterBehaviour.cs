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

    bool CanMove = false;
    Camera mainCamera;

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

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }

    void Update() => HandleMovimentationBehaviour();

    #endregion

    #region Player behaviour methods

    void HandleMovimentationBehaviour()
    {
        if (!CanMove)
            return;

        //movimentação do mouse
        bool isMouseMoving = (Input.GetAxis("Mouse X") != 0);
        if (isMouseMoving)
        {
            Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            MoveCharacterToPosition(newPosition);
            return;
        }

        //Movimentação pelo teclado
        float inputVelocity = HorizontalKeyboardInput();
        TranslateCharacter(inputVelocity);
    }

    void MoveCharacterToPosition(Vector3 position)
    {
        //Verifica se está dentro dos limites
        if (position.x >= rightMovimentLimit.position.x)
        {
            transform.position = new Vector3(rightMovimentLimit.position.x, transform.position.y, transform.position.z);
            return;
        }
        else if (position.x <= leftMovimentLimit.position.x)
        {
            transform.position = new Vector3(leftMovimentLimit.position.x, transform.position.y, transform.position.z);
            return;
        }

        transform.position = new Vector3(position.x, transform.position.y, transform.position.z);

    }

    void TranslateCharacter(float inputVelocity)
    {
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

    float HorizontalKeyboardInput()
    {
        if (Input.GetKey(key_Left) && !Input.GetKey(key_Right))
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

