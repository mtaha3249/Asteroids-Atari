using TMPro;
using UnityEngine;

public class PlayerScore : Comment, IGenericCallback
{
    [SerializeField] private GenericReference<int> _score;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Start()
    {
        // reset score when scene loaded
        _score.Value = 0;
        _scoreText.text = _score.Value.ToString();
    }

    public void OnEventRaisedCallback(params object[] param)
    {
        // update score
        _score.Value += (int) param[0];
        _scoreText.text = _score.Value.ToString();
    }
}
