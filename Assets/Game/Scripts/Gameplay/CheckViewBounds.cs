using UnityEngine;
using UnityEngine.Events;

public class CheckViewBounds : Comment
{
    [Header("Screen"), SerializeField]
    private bool canUpdatePosition;

    [SerializeField, Range(0, 300)]
    private int cushionX = 100;

    [SerializeField, Range(0, 300)]
    private int cushionY = 100;

    public UnityEvent OutOfBound;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    /// <summary>
    /// Check Position if out of camera bound
    /// Visible on the other side
    /// </summary>
    public void CheckPosition()
    {
        Vector3 _screenPosition = _camera.WorldToScreenPoint(transform.position);
        if (IsRight(_screenPosition))
        {
            OutOfBound.Invoke();
            if (canUpdatePosition)
                transform.position =
                    _camera.ScreenToWorldPoint(new Vector3(0 + cushionX, _screenPosition.y, _screenPosition.z));
        }
        else if (IsLeft(_screenPosition))
        {
            OutOfBound.Invoke();
            if (canUpdatePosition)
                transform.position =
                    _camera.ScreenToWorldPoint(new Vector3(Screen.width - cushionX, _screenPosition.y,
                        _screenPosition.z));
        }
        else if (IsUp(_screenPosition))
        {
            OutOfBound.Invoke();
            if (canUpdatePosition)
                transform.position =
                    _camera.ScreenToWorldPoint(new Vector3(_screenPosition.x, 0 + cushionY, _screenPosition.z));
        }
        else if (IsBottom(_screenPosition))
        {
            OutOfBound.Invoke();
            if (canUpdatePosition)
                transform.position =
                    _camera.ScreenToWorldPoint(new Vector3(_screenPosition.x, Screen.height - cushionY,
                        _screenPosition.z));
        }
    }

    /// <summary>
    /// If I exceeded Right side
    /// </summary>
    /// <param name="_screenPos">current screen position</param>
    /// <returns></returns>
    bool IsRight(Vector3 _screenPos) => _screenPos.x >= Screen.width + cushionX;

    /// <summary>
    /// If I exceeded Left side
    /// </summary>
    /// <param name="_screenPos">current screen position</param>
    /// <returns></returns>
    bool IsLeft(Vector3 _screenPos) => _screenPos.x <= 0 - cushionX;

    /// <summary>
    /// If I exceeded Top
    /// </summary>
    /// <param name="_screenPos">current screen position</param>
    /// <returns></returns>
    bool IsUp(Vector3 _screenPos) => _screenPos.y >= Screen.height + cushionY;

    /// <summary>
    /// If I exceeded Bottom
    /// </summary>
    /// <param name="_screenPos">current screen position</param>
    /// <returns></returns>
    bool IsBottom(Vector3 _screenPos) => _screenPos.y <= 0 - cushionY;
}