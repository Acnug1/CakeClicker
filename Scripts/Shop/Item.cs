using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _profit;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    private CakeShopItem _cakeItem;

    public event UnityAction<CakeShopItem, Item> SellButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnSellButtonClick); // несколько обработчиков на одно событие выполняются в зависимости от порядка подписки на него 
        _sellButton.onClick.AddListener(CheckCakeState); // (в данном случае, сначала будет обрабатываться продажа, затем проверка)
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnSellButtonClick);
        _sellButton.onClick.RemoveListener(CheckCakeState);
    }

    private void OnSellButtonClick()
    {
        if (!_cakeItem.IsBuy) // если товар не продан
            SellButtonClick?.Invoke(_cakeItem, this); // вызываем обработку события нажатия на кнопку покупки (после продажи торта отписываемся от данного события)
    }

    private void CheckCakeState()
    {
        if (_cakeItem.IsBuy) // если товар продан
        {
            _sellButton.interactable = false; // отключаем кнопку
            _label.text = "Sell!";
        }
    }

    public void SetCake(CakeShopItem cakeItem)
    {
        _cakeItem = cakeItem;
        RenderCake(cakeItem);
    }

    private void RenderCake(CakeShopItem cakeItem)
    {
        _label.text = cakeItem.Label;
        _price.text = cakeItem.Price.ToString();
        _profit.text = cakeItem.CakeProfit.ToString();
        _icon.sprite = cakeItem.Icon;
    }
}
