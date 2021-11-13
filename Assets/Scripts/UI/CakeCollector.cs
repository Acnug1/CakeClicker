using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]

public class CakeCollector : MonoBehaviour
{
    [SerializeField] private CakePlace _cakePlace;
    [SerializeField] private Button _collectButton;

    private Cake _targetCake; // торт, который мы будем хранить
    private CanvasGroup _canvasGroup;

    public event UnityAction<Cake> CakeCollected; // событие "торт собран"

    private void OnEnable()
    {
        _cakePlace.CakeReadyForCollection += OnCakeReadyForColletion; 
    }

    private void OnDisable()
    {
        _cakePlace.CakeReadyForCollection -= OnCakeReadyForColletion;
        _collectButton.onClick.RemoveListener(CollectCake);
    }

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        ClosePanel(); // по умолчанию отключаем панель продажи торта
    }

    private void OnCakeReadyForColletion(Cake cake)
    {
        _targetCake = cake; // находим наш торт для сбора
        OpenPanel(); // открываем панель
    }

    private void CollectCake() // метод "собрать торт"
    {
        CakeCollected?.Invoke(_targetCake); // вызываем обработчик события "торт собран"
        ClosePanel(); // скрываем панель
    }

    private void OpenPanel()
    {
        _canvasGroup.alpha = 1;
        _collectButton.onClick.AddListener(CollectCake);
    }

    private void ClosePanel()
    {
        _canvasGroup.alpha = 0;
        _collectButton.onClick.RemoveListener(CollectCake);
    }
}
