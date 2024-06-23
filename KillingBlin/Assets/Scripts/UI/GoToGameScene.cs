using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToGameScene : MonoBehaviour
{
    public enum Scene
    {
        OutGame = 0,
        InGame = 1,
    }

    [SerializeField] Button button;

    public void Awake()
    {
        button.onClick.AddListener(() => MoveToInGame());
    }

    public void MoveToInGame()
    {
        SceneManager.LoadScene((int)Scene.InGame);
    }
}
