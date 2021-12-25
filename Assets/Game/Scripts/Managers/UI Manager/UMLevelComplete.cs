using System;
using System.Collections;
using UnityEngine;

public class UMLevelComplete : MonoBehaviour, IGenericCallback
{
    public LevelComplete _levelComplete;
    IEnumerator isCalled;

    IEnumerator GameEnd(GameObject gameEnd, float time, Action OnComplete = null)
    {
        yield return new WaitForSeconds(time);
        gameEnd.SetActive(true);
        if (OnComplete != null)
        {
            OnComplete.Invoke();
        }
    }

    public void OnEventRaisedCallback(params object[] param)
    {
        GameState state = (GameState) param[0];
        if (state == GameState.LevelComplete)
        {
            if (isCalled == null)
                isCalled = GameEnd(_levelComplete.Panel, _levelComplete.delay);
            StartCoroutine(isCalled);
        }
        else
            _levelComplete.Panel.SetActive(false);
    }
}