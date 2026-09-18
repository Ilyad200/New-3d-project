using System;
using UnityEngine;

public static class GameManager
{
    private static bool _isInventoryOpen = false;
    private static bool _isStatWindowOpen = false;
    private static bool _isPlayerDead = false;
    private static bool _isGamePaused = false;

    public static bool IsInventoryOpen => _isInventoryOpen;
    public static bool IsStatWindowOpen => _isStatWindowOpen;
    public static bool IsPlayerDead => _isPlayerDead;
    public static bool IsGamePaused => _isGamePaused;

    public static event Action<bool> OnInventoryUpdate;
    public static event Action<bool> OnStatWindowUpdate;
    public static event Action<bool> OnPlayerLiveStatusUpdate;
    public static event Action<bool> OnGamePaused;

    public static void InventoryUpdate(bool isInventoryOpen)
    {
        _isInventoryOpen = isInventoryOpen;
        OnInventoryUpdate?.Invoke(isInventoryOpen);
        ToggleCursorVisibility(isInventoryOpen);
    }
    public static void StatWindowUpdate(bool isStatWindowOpen)
    {
        _isStatWindowOpen = isStatWindowOpen;
        OnStatWindowUpdate?.Invoke(isStatWindowOpen);
        ToggleCursorVisibility(isStatWindowOpen);
    }
    public static void PlayerLiveStatusUpdate(bool playerIsDead)
    {
        _isPlayerDead = playerIsDead;
        OnPlayerLiveStatusUpdate?.Invoke(playerIsDead);
        ToggleCursorVisibility(playerIsDead);
    }
    public static void GameStateUpdate(bool isGamePaused)
    {
        _isGamePaused = isGamePaused;
        OnGamePaused?.Invoke(isGamePaused);
        ToggleCursorVisibility(isGamePaused);
    }
    private static void ToggleCursorVisibility(bool isVisible)
    {
        Debug.Log($"[GameManager] isVisible = {isVisible}");
        if (isVisible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Debug.Log($"[GameManager] cursor locked");
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log($"[GameManager] cursor state: {Cursor.lockState}");
        }
    }
}
