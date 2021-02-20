using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingProgressBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fill; // получаем скорость заполнения _slider
    [SerializeField] private float _fillingSpeed; // скорость заполнения _slider
    [SerializeField] private Color _unfilledColor; // цвет незаполненной шкалы
    [SerializeField] private Color _filledColor; // цвет заполненной шкалы

    private float _targetProgress; // значение, к которому мы будем стремиться
    private Color _targetColor; // цвет, к которому мы будем стремиться

    private void Start()
    {
        ResetProgress(); // при старте игры сбрасываем значения слайдера
    }

    private void ResetProgress() // сбросить прогресс
    {
        _targetProgress = 0; // значение шкалы плавно переводим в 0
        _targetColor = _unfilledColor; // значение цвета заполненной шкалы плавно делаем равным цвету незаполненной шкалы
    }

    public void OnLayerCookingProgresses(float cookingProgress, float neededValue) // когда у слоя произошел прогресс готовки
    {
        _targetProgress = cookingProgress / neededValue; // чтобы найти _targetProgress, делим количество текущих кликов игрока на общее количество кликов, необходимое для приготовления слоя
        _targetColor = Color.Lerp(_unfilledColor, _filledColor, _targetProgress); // находим цвет на промежутке от 0 до 1 от _unfilledColor до _filledColor

        if (cookingProgress == neededValue) // когда прогресс слоя достигнут
            StartCoroutine(ResetProgressDelay()); // запускаем корутину с задержкой в пол секунды, для того, чтобы отобразить прогресс слоя
    }

    private void Update()
    {
        _slider.value = Mathf.Lerp(_slider.value, _targetProgress, _fillingSpeed); // через Lerp меняем значение нашего Slider на желаемое
        _fill.color = Color.Lerp(_fill.color, _targetColor, _fillingSpeed); // меняем значение цвета заполненной части Slider
    }

    private IEnumerator ResetProgressDelay()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

        yield return waitForSeconds;

        ResetProgress(); // после задержки, сбрасываем слой
    }
}
