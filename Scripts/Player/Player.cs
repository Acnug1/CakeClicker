using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerWallet))]

public class Player : MonoBehaviour
{
    [SerializeField] private CakeCollector _cakeCollector; // сборщик тортов

    private PlayerWallet _playerWallet;

    public event UnityAction<Cake> CakeBought; // событие "покупка торта"

    private void OnEnable()
    {
        _cakeCollector.CakeCollected += OnCakeCollected;
    }

    private void OnDisable()
    {
        _cakeCollector.CakeCollected -= OnCakeCollected;
    }

    private void Start()
    {
        _playerWallet = GetComponent<PlayerWallet>();
    }

    private void OnCakeCollected(Cake cake)
    {
        _playerWallet.AddCakeProfit(cake.Profit); // пополняем кошелек игрока на определенную сумму (стоимость торта)
    }

    public bool CheckSolvency(int price) // проверяем платежеспособность игрока (возвращаем true или false)
    {
        return _playerWallet.BakedCakes >= price; // если баланс игрока больше или равен стоимости товара
    }

    public void BuyCake(CakeShopItem cakeItem) // покупаем торт
    {
        _playerWallet.WithdrawCakes(cakeItem.Price); // вызываем метод "изъять деньги за торт" и передаем стоимость торта
        CakeBought?.Invoke(cakeItem.Cake); // вызываем событие "покупка торта" и передаем его в диспенсер
    }
}
