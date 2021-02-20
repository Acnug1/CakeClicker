using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<CakeShopItem> _cakes; // список товаров ScriptableObject (тортов) для продажи
    [SerializeField] private Player _player;
    [SerializeField] private Item _template; // шаблон итема
    [SerializeField] private Transform _itemContainer; // Transform родителя нашего Item (content в ScrollView)

    private void Start()
    {
        for (int i = 0; i < _cakes.Count; i++) // перебираем список тортов
        {
            AddItem(_cakes[i]); // добавляем товары и передаем в качестве параметров список ScriptableObject тортов
        }
    }

    private void AddItem(CakeShopItem cakeItem) // добавить шаблон товара в магазин
    {
        Item item = Instantiate(_template, _itemContainer); // создаем новый item (товар) в контенте
        InitializeItem(item, cakeItem); // после добавление товара мы должны его инициализировать (заполнить данными)
    }

    private void InitializeItem(Item item, CakeShopItem cakeItem) // инициализация итема (шаблона товара)
    {
        item.SetCake(cakeItem); // устанавливаем вместо шаблона товара конкретный тортик
        item.SellButtonClick += OnSellButtonClick; // подписываемся под обработчик событий OnSellButtonClick
        item.name = _template.name + (_itemContainer.childCount); // к имени шаблона добавляем порядковый номер количества объектов в данный момент в контейнере content
    }

    private void OnSellButtonClick(CakeShopItem cakeItem, Item item) // обработчик нажатия на кнопку покупки
    {
        TrySellCake(cakeItem, item); // пытаемся купить товар
    }

    private void TrySellCake(CakeShopItem cakeItem, Item item)
    {
        if (_player.CheckSolvency(cakeItem.Price)) // если игрок может позволить себе данный торт
        {
            _player.BuyCake(cakeItem); // покупаем торт
            cakeItem.Buy(); // переводим флаг покупки у торта в true
            UnsubscribeItem(item); // отписываемся от события OnSellButtonClick
        }    
    }

    private void UnsubscribeItem(Item item) // если игрок уже купил этот торт, то больше его купить он не сможет, так как мы отписались от события
    {
        item.SellButtonClick -= OnSellButtonClick;
    }
}
