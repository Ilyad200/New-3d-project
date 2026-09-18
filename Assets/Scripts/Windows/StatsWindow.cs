using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class StatsWindow : MonoBehaviour
{
    private PlayerInputs _inputs;
    private bool _isWindowOpen = false;
    [SerializeField] private List<TextMeshProUGUI> _statsTexts;
    private void Awake()
    {
        _inputs = new PlayerInputs();
        _inputs.Player.CheckStats.performed += OpenStatWindow;
        _inputs.Player.CheckStats.Enable();

        GameManager.OnGamePaused += ChangeWindowStatus;
        GameManager.OnPlayerLiveStatusUpdate += ChangeWindowStatus;
        GameManager.OnInventoryUpdate += ChangeWindowStatus;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OpenStatWindow(InputAction.CallbackContext context)
    {
        _isWindowOpen = !_isWindowOpen;
        GameManager.StatWindowUpdate(_isWindowOpen);
        var playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        gameObject.SetActive(_isWindowOpen);

        if (_statsTexts == null || !_isWindowOpen) return;

        _statsTexts[0].text = playerStats.Stats[StatType.Attack].ToString();
        _statsTexts[1].text = playerStats.Stats[StatType.Defense].ToString();
        _statsTexts[2].text = playerStats.Stats[StatType.Health].ToString();
        _statsTexts[3].text = playerStats.Stats[StatType.Speed].ToString();
        _statsTexts[4].text = playerStats.Stats[StatType.Regeneration].ToString();
    }
    private void ChangeWindowStatus(bool isClose)
    {
        if (isClose)
        {
            _isWindowOpen = false;
            _inputs.Player.CheckStats.Disable();
            gameObject.SetActive(false);
        }
        else _inputs.Player.CheckStats.Enable();
    }
    private void OnDestroy()
    {
        _inputs.Player.CheckStats.performed -= OpenStatWindow;
        _inputs.Player.CheckStats.Disable();

        GameManager.OnGamePaused -= ChangeWindowStatus;
        GameManager.OnPlayerLiveStatusUpdate -= ChangeWindowStatus;
        GameManager.OnInventoryUpdate -= ChangeWindowStatus;
    }
}
