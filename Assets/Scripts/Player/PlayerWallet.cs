using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWallet : MonoBehaviour
{
    private int _bakedCakes; // баланс за выпеченные торты

    public int BakedCakes => _bakedCakes;

    public event UnityAction<int> CakeBalanceChanged; // событие по изменению баланса денег на экране

    private void Start()
    {
        CakeBalanceChanged?.Invoke(_bakedCakes);
    }

    public void AddCakeProfit(int amount) // метод "добавить деньги за торт"
    {
        _bakedCakes += amount;
        CakeBalanceChanged?.Invoke(_bakedCakes);
    }

    public void WithdrawCakes(int amount) // метод "списать деньги за торт"
    {
        _bakedCakes -= amount;
        CakeBalanceChanged?.Invoke(_bakedCakes);
    }
}
