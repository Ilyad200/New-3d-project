using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] protected GameObject _parent;


    protected IHealthProvider _healthProvider;
    protected float _maxWidth;
    [SerializeField] protected RectTransform _fillRect;
    [SerializeField] protected RectTransform _backFillRect;
    [SerializeField] protected Image _image;
    [SerializeField] protected TextMeshProUGUI _healthText;
    protected float visualHP;
    protected float lerpSpeed = 2f;

    protected Coroutine animationCoroutine;

    protected virtual void Awake()
    {
        if (_parent == null) _parent = transform.parent.gameObject;

        _healthProvider = _parent.GetComponentInParent<IHealthProvider>();

        _maxWidth = _fillRect.rect.width;
    }
    protected virtual void Start()
    {
        visualHP = _healthProvider.MaxHP;
    }

    protected void OnEnable()
    {
        _healthProvider.OnDamaged += Damaging;
        _healthProvider.OnHealed += Healing;
        _healthProvider.OnMaxHealthChanged += HealthChange;
        _healthProvider.OnRevived += Revived;
    }
    protected void OnDisable()
    {
        _healthProvider.OnDamaged -= Damaging;
        _healthProvider.OnHealed -= Healing;
        _healthProvider.OnMaxHealthChanged -= HealthChange;
        _healthProvider.OnRevived -= Revived;
    }

    protected void Revived(float current, float max)
    {
        if (animationCoroutine != null) StopCoroutine(animationCoroutine);
        visualHP = current;
        float percent = current / max;
        _fillRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _maxWidth * percent);
        _backFillRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _maxWidth * percent);

        _healthText.text = $"{Mathf.CeilToInt(current)}/{max}";
    }

    protected void HealthChange(float current, float max)
    {
        float percent = current / max;
        if (current <= visualHP)
        {
            _fillRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _maxWidth * percent);
        }
        else
        {
            _backFillRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _maxWidth * percent);
        }

        _healthText.text = $"{Mathf.CeilToInt(current)}/{max}";

        if (animationCoroutine != null) StopCoroutine(animationCoroutine);
        animationCoroutine = StartCoroutine(SmoothDepletion(current, max));
    }
    protected void Healing(HealEventArgs args)
    {
        _image.color = new Color(0, 0.8f, 0);
        HealthChange(args.CurrentHP, args.MaxHP);
    }
    protected void Damaging(DamageEventArgs args)
    {
        _image.color = Color.darkRed;
        HealthChange(args.CurrentHP, args.MaxHP);
    }

    protected IEnumerator SmoothDepletion(float targetHP, float max)
    {
        bool isHealing = targetHP > visualHP;

        float startHP = visualHP;

        float t = 0f;
        while (t < 1f)
        {
            visualHP = Mathf.Lerp(startHP, targetHP, t);
            t += Time.deltaTime * lerpSpeed;
            UpdateView(isHealing, max);
            yield return null;
        }
        visualHP = targetHP;
        UpdateView(isHealing, max);
    }
    protected void UpdateView(bool isHealing, float max)
    {
        float percent = visualHP / max;

        if (isHealing)
        {
            _fillRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _maxWidth * percent);
        }
        else
        {
            _backFillRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _maxWidth * percent);
        }
    }
}
