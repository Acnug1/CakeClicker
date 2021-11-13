using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WalletDisplay : MonoBehaviour
{
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private TMP_Text _cakeDisplay;

    private void OnEnable()
    {
        _playerWallet.CakeBalanceChanged += OnCakeBalanceChanged;
    }

    private void OnDisable()
    {
        _playerWallet.CakeBalanceChanged -= OnCakeBalanceChanged;
    }

    private void OnCakeBalanceChanged(int cakeBalance)
    {
        _cakeDisplay.text = cakeBalance.ToString();
    }
}
