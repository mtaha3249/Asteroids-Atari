using ControlFreak2;
using UnityEngine;

[RequireComponent(typeof(PlayerWeaponController))]
[RequireComponent(typeof(PlayerController))]
public class InputController : MonoBehaviour, IGenericCallback
{
    [Header("Input Axis"), SerializeField]
    private string _hAxis = "Horizontal";

    [SerializeField]
    private string _vAxis = "Vertical";

    [SerializeField]
    private string _shootAxis = "Fire1";

    [Header("Debug"), SerializeField, ReadOnly]
    private Vector3 _input;

    [SerializeField, ReadOnly]
    private bool _canShoot;

    [SerializeField, ReadOnly]
    private bool _getInput;

    private PlayerController _playerController;
    private PlayerWeaponController _playerWeaponController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _playerWeaponController = GetComponent<PlayerWeaponController>();
    }

    public void OnEventRaisedCallback(params object[] param)
    {
        GameState _state = (GameState) param[0];
        switch (_state)
        {
            case GameState.MainMenu:
                _getInput = false;
                break;
            case GameState.Gameplay:
                _getInput = true;
                break;
            case GameState.LevelComplete:
                _getInput = false;
                break;
            case GameState.LevelFail:
                _getInput = false;
                break;
            case GameState.Reset:
                _getInput = false;
                break;
        }
    }

    private void Update()
    {
        if (!_getInput)
            return;

        // get value from input system
        _input = (Vector3.right * CF2Input.GetAxis(_hAxis)) + (Vector3.forward * CF2Input.GetAxis(_vAxis));
        _canShoot = CF2Input.GetButton(_shootAxis);

        // player shoot
        if (_canShoot)
            _playerWeaponController.Shoot();
    }

    private void FixedUpdate()
    {
        if (!_getInput)
            return;

        _playerController.MovePlayer(_input);
    }
}