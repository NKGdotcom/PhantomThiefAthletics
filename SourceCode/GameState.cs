using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }
    public State NowState {  get; private set; }
    public enum State
    {
        setObject,
        play,
        pause,
        result,
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    public void StateSetObject()
    {
        NowState = State.setObject;
    }
    public void StatePlay()
    {
        NowState = State.play;
    }
    public void StatePause()
    {
        NowState = State.pause;
    }
    public void StateResult()
    {
        NowState = State.result;
    }
}
