using UnityEngine;
using System.Collections;

public enum GameState
{
    Ready = 0,
    Start = 1,
    GameOver = 2
}

public delegate void UpdateFunc(Entity entity);

static class Constants
{
    public const string userid = "userid";
    public const string username = "username";
    public const string password = "password";
}