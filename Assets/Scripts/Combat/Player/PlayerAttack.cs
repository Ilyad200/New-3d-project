using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private Dictionary<StatType, float> _stats;

    private PlayerInputs _input;
    private Collider[] _hitBuffer;   // Кэш для OverlapSphereNonAlloc

    [SerializeField] private Inventory _inventory;
    [SerializeField] private string _soundName;
    [SerializeField] private GameObject _damageArea;
    private SpriteRenderer _damageSprite;
    private float _showDamageAreaTime = 0.5f;

    private void Awake()
    {
        _input = new PlayerInputs();
        _hitBuffer = new Collider[16];

        _damageSprite = _damageArea.GetComponent<SpriteRenderer>();
        _damageSprite.color = new Color(1, 0, 0, 0);

        GameManager.OnGamePaused += GameStateChanged;
        GameManager.OnInventoryUpdate += GameStateChanged;
        GameManager.OnPlayerLiveStatusUpdate += GameStateChanged;
    }
    private void Start()
    {
        _stats = GetComponent<PlayerStats>().Stats;
        _damageArea.transform.localScale = _stats[StatType.AttackRange] * 2 * Vector3.one;
    }
    private void OnEnable()
    {
        _input.Player.Attack.performed += OnAttackPressed;
        _input.Player.Enable();
    }
    private void OnDisable()
    {
        _input.Player.Attack.performed -= OnAttackPressed;
        _input.Player.Disable();
    }

    private void OnAttackPressed(InputAction.CallbackContext ctx)
    {
        StopAllCoroutines();
        StartCoroutine(ShowDamageArea());

        SoundManager.Instance.PlaySound(_soundName);

        int hitCount = Physics.OverlapSphereNonAlloc(
            transform.position,
            _stats[StatType.AttackRange],
            _hitBuffer
        );

        for (int i = 0; i < hitCount; i++)
        {
            if (_hitBuffer[i].transform == transform) continue;

            if (_hitBuffer[i].TryGetComponent<IDamageable>(out var damageable))
            {
                float finalDamage = _stats[StatType.Attack];
                damageable.TakeDamage(new DamageContext(gameObject, finalDamage, _hitBuffer[i].transform));
            }
        }
    }
    private IEnumerator ShowDamageArea()
    {
        float elapsed = 0f;
        while (elapsed < _showDamageAreaTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _showDamageAreaTime);

            _damageSprite.color = Color.Lerp(Color.red, new Color(1, 0, 0, 0), t);

            yield return null;
        }

        _damageSprite.color = new Color(1, 0, 0, 0);
    }
    private void GameStateChanged(bool lockAttack)
    {
        if (lockAttack)
            _input.Player.Disable();
        else
            _input.Player.Enable();
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = new Color(1, 0, 0, 0.8f);
    //    Gizmos.DrawWireSphere(transform.position, _stats[StatType.AttackRange]);
    //}

    private void OnDestroy()
    {
        GameManager.OnGamePaused -= GameStateChanged;
        GameManager.OnInventoryUpdate -= GameStateChanged;
        GameManager.OnPlayerLiveStatusUpdate -= GameStateChanged;
    }
}
