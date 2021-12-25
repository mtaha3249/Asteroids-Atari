using System;
using System.Collections;
using UnityEngine;

public class UMLevelFail : MonoBehaviour, IGenericCallback
{
    public LevelFail _levelFail;
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
        if (state == GameState.LevelFail)
        {
            if (isCalled == null)
                isCalled = GameEnd(_levelFail.Panel, _levelFail.delay);
            StartCoroutine(isCalled);
        }
        else
            _levelFail.Panel.SetActive(false);
    }
}