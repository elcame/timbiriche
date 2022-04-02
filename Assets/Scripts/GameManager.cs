using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameState _GameState;
    

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _GameState = GameState.start;
    }

    
    public void UpdateGameState(GameState GameState)
    {
        _GameState = GameState;
    }

    public GameState GetGameState => _GameState;

    public void SwitchPlayer() { 
        _GameState = (_GameState == GameState.player1) ?  GameState.player2 :  GameState.player1;
    }

    public enum GameState
    {
        start,
        player1,
        player2,
        end
    }
}
