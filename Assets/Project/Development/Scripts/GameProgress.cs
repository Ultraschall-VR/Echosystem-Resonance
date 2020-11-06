using System;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public static GameProgress Instance;

    public bool LearnedTeleport = false;
    public bool LearnedGrab = false;
    public bool LearnedUncover = false;
    public bool LearnedBow = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (GameStateMachine.Instance.MenuOpen)
        {
            LearnedTeleport = false;
            LearnedGrab = false;
            LearnedUncover = false;
            LearnedBow = false;
        }
    }
}