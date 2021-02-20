using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cake : MonoBehaviour
{
    [SerializeField] private int _profit; // выгода (количество получаемых денег) с продажи этого торта

    private CakeLayer[] _layers; // массив из слоев
    private int _createdLayers; // сколько слоев уже готово

    public int Profit => _profit;
    public bool Done => _createdLayers == _layers.Length; // если число приготовленных слоев равно длине массива со слоями, то Done = true

    public event UnityAction CakeDone; // событие "торт готов"
    public event UnityAction<float, float> LayerCookingProgresses; // событие "прогресс готовки слоя торта"

    private void Start()
    {
        _layers = GetComponentsInChildren<CakeLayer>(); // заполним наш массив со слоями

        _createdLayers = 0; // обнуляем наш счетчик созданных слоев
    }

    public void OnClick() // когда игрок кликает по слою
    {
        if (Done == false) // если торт ещё не готов
        {
            if (TryBakeLayer()) // если мы пытаемся испечь торт
            {
                if (Done == true) // если торт готов
                {
                    CakeDone?.Invoke(); // вызываем событие "торт готов"
                }
            }
        }
    }

    private bool TryBakeLayer() // попытаться испечь слой
    {
        CakeLayer cakeLayer = _layers[_createdLayers]; // получаем нужный слой, с которым мы будем работать

        cakeLayer.IncreaseCookingProgress(); // увеличиваем прогресс приготовления на единицу
        LayerCookingProgresses?.Invoke(cakeLayer.CookingProgress, cakeLayer.ClicksBeforeCooking); // при увеличении прогресса передаем в событие значение прогресса кликов до готовки слоя и требуемое количество кликов

        if (cakeLayer.TryCookLayer()) // если кликов хватило, для того чтобы испечь слой
        {
            _createdLayers++; // увеличиваем количество готовых слоев на единицу
            return true; // возвращаем true
        }
        else 
        {
            return false; // иначе возвращаем false
        }
    }
}
