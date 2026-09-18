using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

public class GameOverWindowManager : MonoBehaviour
{
    private IHealthProvider _playerHealthProvider;
    private PlayerInputs _playerInputs;
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private string _defeatSoundName;
    [SerializeField] private string _winSoundName;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private int _requiredCountToWin;
    [SerializeField] private string _requiredIdToWin;
    [SerializeField] private GameObject _gameOverPanel;

    private bool _isMenuOpen = false;
    private bool _isPlayerDead = false;
    private void Awake()
    {
        _playerInputs = new PlayerInputs();
    }
    void Start()
    {
        _playerInputs.Player.Pause.Enable();
        _playerInputs.Player.Pause.performed += GameMenu;

        _playerInputs.Player.CheckToWin.Enable();
        _playerInputs.Player.CheckToWin.performed += WinHandle;

        _playerHealthProvider = GameObject.FindGameObjectWithTag("Player").GetComponent<IHealthProvider>();
        _playerHealthProvider.OnDied += PlayerDeathHandler;
        _playerHealthProvider.OnRevived += PlayerReviveHandler;

        _gameOverPanel.SetActive(false);
    }

    private void PlayerDeathHandler()
    {
        SoundManager.Instance.PlaySound(_defeatSoundName);
        _isMenuOpen = true;
        _isPlayerDead = true;
        _textMeshPro.text = "GAME OVER";
        GameManager.PlayerLiveStatusUpdate(true);
        _gameOverPanel.SetActive(true);
    }
    private void PlayerReviveHandler(float current, float max)
    {
        _isMenuOpen = false;
        _isPlayerDead = false;
        GameManager.PlayerLiveStatusUpdate(false);
        _gameOverPanel.SetActive(false);

    }
    private void GameMenu(InputAction.CallbackContext context)
    {
        if (_isPlayerDead) return;
        Debug.Log("[GameOver] Esc is pressed");
        _isMenuOpen = !_isMenuOpen;
        if (_isMenuOpen)
        {
            _textMeshPro.text = "GAME MENU";
            GameManager.GameStateUpdate(true);
            _gameOverPanel.SetActive(true);
        }
        else
        {
            GameManager.GameStateUpdate(false);
            _gameOverPanel.SetActive(false);
        }
    }
    private void WinMenu()
    {
        if (_isPlayerDead) return;

        SoundManager.Instance.PlaySound(_winSoundName);
        _isMenuOpen = true;
        _textMeshPro.text = "YOU WIN";
        GameManager.PlayerLiveStatusUpdate(true);
        _gameOverPanel.SetActive(true);
    }
    private void WinHandle(InputAction.CallbackContext context)
    {
        if (_inventory == null) return;

        if (_inventory.GetItemsCount(_requiredIdToWin) >= _requiredCountToWin)
        {
            WinMenu();
        }
    }

    public void ExitGame() => Application.Quit();
}
