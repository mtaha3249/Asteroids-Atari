using UnityEngine;

[RequireComponent(typeof(CheckViewBounds))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, IGenericCallback
{
    [Header("Movement"), SerializeField]
    private GenericReference<float> _sensitivity;

    [SerializeField]
    private GenericReference<float> _moveSpeed;

    [SerializeField]
    private GenericReference<float> _turnSpeed;

    [Header("Debug"), SerializeField, ReadOnly]
    private bool _canMove;

    private Rigidbody _body;
    private CheckViewBounds _checkViewBounds;

    private void Start()
    {
        _body = GetComponent<Rigidbody>();
        _checkViewBounds = GetComponent<CheckViewBounds>();
    }

    public void OnEventRaisedCallback(params object[] param)
    {
        GameState _state = (GameState) param[0];
        switch (_state)
        {
            case GameState.MainMenu:
                _canMove = false;
                break;
            case GameState.Gameplay:
                _canMove = true;
                break;
            case GameState.LevelComplete:
                _canMove = false;
                break;
            case GameState.LevelFail:
                _body.velocity = Vector3.zero;
                _body.angularVelocity = Vector3.zero;
                _canMove = false;
                break;
            case GameState.Reset:
                _body.velocity = Vector3.zero;
                _body.angularVelocity = Vector3.zero;
                _canMove = false;
                break;
        }
    }

    /// <summary>
    /// Move Player base on the given Input
    /// Top Down game so _input variable only use X and Z Axis, so arrange input accordingly
    /// </summary>
    /// <param name="_input">Axis Input</param>
    public void MovePlayer(Vector3 _input)
    {
        if (!_canMove)
            return;

        _checkViewBounds.CheckPosition();

        _body.velocity = transform.forward * _input.z * _moveSpeed.Value * _body.mass * _sensitivity.Value *
                         Time.fixedDeltaTime;
        _body.angularVelocity = transform.up * _input.x * _turnSpeed.Value * _body.mass * _sensitivity.Value *
                                Time.fixedDeltaTime;
    }
}