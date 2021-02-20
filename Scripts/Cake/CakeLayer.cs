using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class CakeLayer : MonoBehaviour
{
    [SerializeField] private int _clicksBeforeCooking; // клики до готовки
    [SerializeField] private ParticleSystem _cookingEffect; // добавляем эффект готовки к нашим слоям
    [SerializeField] private int _emitCount; // количество излучаемых частиц

    private SpriteRenderer _spriteRenderer; // спрайт нашего слоя
    private Color _layerColor; // цвет нашего слоя

    public int CookingProgress { get; private set; }
    public int ClicksBeforeCooking => _clicksBeforeCooking;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _layerColor = _spriteRenderer.color; // запоминаем цвет коржа, который был
        CreateGhostLayer(); // создаем цвет заготовки торта
    }

    public void IncreaseCookingProgress() // метод для увеличения прогресса выпечки торта при клике на единицу
    {
        if (_cookingEffect != null) // если для слоя существует эффект готовки
            _cookingEffect.Emit(_emitCount); // то включаем излучатель эффекта и указываем количество отображаемых частиц эффекта за клик

        CookingProgress++;
    }

    private void CreateGhostLayer()
    {
        _spriteRenderer.color = new Color(255, 255, 255, 60); // создаем белый полупрозрачный слой
    }

    public bool TryCookLayer() // метод "попытаться испечь слой торта"
    {
        if (_clicksBeforeCooking == CookingProgress) // если мы испекли слой
        {
            _spriteRenderer.color = _layerColor; // меняем цвет слоя у торта на готовый
            return true;
        }
        else
            return false;
    }
}
