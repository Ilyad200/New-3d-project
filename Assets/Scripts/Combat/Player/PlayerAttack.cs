using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private IHaveStats _stats;

    private PlayerInputs _input;
    private Collider[] _hitBuffer;   // Кэш для OverlapSphereNonAlloc
    private readonly HashSet<IDamageable> _processed = new HashSet<IDamageable>();

    [SerializeField] private Inventory _inventory;
    [SerializeField] private string _soundName;
    [SerializeField] private GameObject _damageArea;
    private SpriteRenderer _damageSprite;
    private float _showDamageAreaTime = 0.5f;

    private Animator _animator;

    private void Awake()
    {
        _input = new PlayerInputs();
        _hitBuffer = new Collider[16];

        _damageSprite = _damageArea.GetComponent<SpriteRenderer>();
        _damageSprite.color = new Color(1, 0, 0, 0);

        _animator = GetComponentInChildren<Animator>();

        GameManager.OnGamePaused += GameStateChanged;
        GameManager.OnInventoryUpdate += GameStateChanged;
        GameManager.OnPlayerLiveStatusUpdate += GameStateChanged;
    }
    private void Start()
    {
        _stats = GetComponent<IHaveStats>();
        _damageArea.transform.localScale = _stats.Get(StatType.AttackRange) * 2 * Vector3.one;
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

        _animator.SetTrigger("StartAttack");

        SoundManager.Instance.PlaySound(_soundName);

        int hitCount = Physics.OverlapSphereNonAlloc(
            transform.position,
            _stats.Get(StatType.AttackRange),
            _hitBuffer,
            ~0,
            QueryTriggerInteraction.Ignore
        );

        _processed.Clear();

        Debug.Log($"[PlayerAttack] attack start");
        Debug.Log($"[PlayerAttack] hit count {hitCount}");
        for (int i = 0; i < hitCount; i++)
        {
            Collider col = _hitBuffer[i];
            if (col.transform.IsChildOf(transform)) continue;

            if (col.TryGetComponent<IDamageable>(out var damageable))
            {
                if (!_processed.Add(damageable)) continue; // уже обработали этого получателя

                DamageContext context = new DamageContext(gameObject, col.gameObject);
                DamageType damageType = _inventory.Equipments[EquipmentSlot.RightArm]?.Data.DamageType ?? DamageType.Physical;
                context.AddComponent(gameObject, _inventory.Equipments[EquipmentSlot.RightArm]?.Stats[StatType.Attack] ?? _stats.Get(StatType.Attack), damageType);
                damageable.TakeDamage(context);
                Debug.Log($"[PlayerAttack] attack {i}. {col.GetEntityId()}");
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
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.orange;
        Gizmos.DrawWireSphere(transform.position, _stats.Get(StatType.AttackRange));
    }
}
