using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CakePlace : MonoBehaviour
{
    [SerializeField] private ClickerZone _clickerZone;
    [SerializeField] private CookingProgressBar _cookingProgressBar;

    private Cake _cake;

    public event UnityAction<Cake> CakeReadyForCollection; // событие "торт готов к сбору"

    public void SetCake(Cake cake)
    {
        _cake = Instantiate(cake, transform); // и создаем новый
        _cake.CakeDone += OnCakeDone; // подписка под событие "торт готов"
        _cake.LayerCookingProgresses += _cookingProgressBar.OnLayerCookingProgresses; // при создании торта на подставке подписываемся на обработчик события "OnLayerCookingProgresses"
        _clickerZone.Click += _cake.OnClick; // при создании торта подписываемся на обработчик события _cake.OnClick
    }

    public void RemoveCake(Cake cake)
    {
        _cake.CakeDone -= OnCakeDone; // отписка события "торт готов"
        _clickerZone.Click -= _cake.OnClick; // при удалении торта отписываемся от обработчика события _cake.OnClick
        _cake.LayerCookingProgresses -= _cookingProgressBar.OnLayerCookingProgresses; // при удалении торта с подставки отписываемся от обработчика события "OnLayerCookingProgresses"
        Destroy(cake.gameObject);
    }

    private void OnCakeDone() // обработчик события "торт готов"
    {
        CakeReadyForCollection?.Invoke(_cake); // вызываем событие "торт готов к сбору" и передаем сам торт
    }
}
