using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRegion : MonoBehaviour
{
    public enum PlayerPhase
    {
        platforming,
        painting,
        gameOver
    }
    public PlayerPhase currentPlayerPhase = PlayerPhase.platforming;
}
