using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MainMenu
{
    public GameObject Panel;
}

[Serializable]
public class Gameplay
{
    public string LevelText = "Level :";
    public Text levelNumber;
    public GameObject Panel;
}

[Serializable]
public class LevelComplete
{
    [Range(0, 3)] public float delay = 1.5f;
    public GameObject Panel;
}

[Serializable]
public class LevelFail
{
    [Range(0, 3)] public float delay = 1.5f;
    public GameObject Panel;
}