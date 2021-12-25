using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUIHandler : Comment, IGenericCallback
{
    [SerializeField] private GenericReference<int> _maxLives;
    [SerializeField] private GameObject _lifePrefab;
    [SerializeField] private GameEvent _gameEvent;
    private Stack<Image> _hearts = new Stack<Image>();

    private void Start()
    {
        // spawn hearts for the max live
        // did to make dynamic UI
        for (int i = 0; i < _maxLives.Value; i++)
        {
            Image _image = Instantiate(_lifePrefab, transform).gameObject.GetComponent<Image>();
            _image.color = Color.red;
            _hearts.Push(_image);
        }
    }

    public void OnEventRaisedCallback(params object[] param)
    {
        // get latest heart and change color
        _hearts.Pop().color = Color.white;
        if (_hearts.Count <= 0)
            _gameEvent.Raise(GameState.LevelFail);
    }
}