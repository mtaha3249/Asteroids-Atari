using UnityEngine;

public class UMMainMenu : MonoBehaviour, IGenericCallback
{
    public MainMenu _mainMenu;

    public void OnEventRaisedCallback(params object[] param)
    {
        GameState state = (GameState) param[0];
        
        if (state == GameState.MainMenu)
            _mainMenu.Panel.SetActive(true);
        else
            _mainMenu.Panel.SetActive(false);
    }
}