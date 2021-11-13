using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeDispenser : MonoBehaviour // класс "раздатчик тортов"
{
    [SerializeField] private CakeCollector _cakeCollector; // выдача нового торта осуществляется после сбора готового торта
    [SerializeField] private CakePlace _cakePlace; // место, куда мы будем выдавать торт
    [SerializeField] private List<Cake> _cakeTemplates; // список для хранения шаблонов тортов
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _cakeCollector.CakeCollected += OnCakeCollected;
        _player.CakeBought += OnCakeBought; // подписываемся на обработчик события "торт куплен" 
    }

    private void OnDisable()
    {
        _cakeCollector.CakeCollected -= OnCakeCollected;
        _player.CakeBought -= OnCakeBought;
    }

    private void Start()
    {
        DispenceCake(); // выдаем торт при старте игры
    }

    private void DispenceCake() // метод "выдать торт"
    {
        int randomNumber = Random.Range(0, _cakeTemplates.Count); // рандомное число, под которым мы выберем торт
        Cake randomCake = _cakeTemplates[randomNumber]; // выбираем рандомный торт
        _cakePlace.SetCake(randomCake); // устанавливаем его на подставку
    }

    private void OnCakeCollected(Cake cake) // обработчик события "торт собран"
    {
        _cakePlace.RemoveCake(cake); // удаляем собранный торт
        DispenceCake(); // выдаем новый торт
    }

    private void OnCakeBought(Cake cake)
    {
        _cakeTemplates.Add(cake); // добавляем новый торт в список наших тортов на выдачу
    }
}
