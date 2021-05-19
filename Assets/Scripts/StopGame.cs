using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGame : MonoBehaviour
{
    public void PauseGame(bool state)
    {
        Time.timeScale = state ? 0 : 1;
    }
}
