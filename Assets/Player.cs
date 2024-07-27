using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text ApplesTxt; // Текст о количестве яблок в руках
    public Text WarehouseTxt; // Текст о состоянии склада

    public int WarehouseMaxValue; // Максимальная вместимость склада
    public int ApplesMaxCanTake; // Максимально количество яблок вмещаемые в руках

    public float TakeTime;

    int warehouse; // Состояние склада (наличие)
    int apples; // Яблок в руках

    bool isTakeApples;

    private void Start()
    {
        TextShow();

        StartCoroutine(ApplesCounter());
    }

    IEnumerator ApplesCounter()
    {
        var timer = new WaitForSeconds(TakeTime);

        while (true) // Бесконечный цикл. 
        {
            yield return timer; // Обязательная строка! Без неё Unity зависнет. Пауза в цикле TakeTime секунд

            if (isTakeApples && apples < ApplesMaxCanTake) // Если берем яблоки и яблок взято меньше чем разрешено
            {
                apples++;
                TextShow();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Pointer pointer)) // Если мы вошли в триггер у которого есть скрипт Pointer
        {
            isTakeApples = true;
        }

        if (other.gameObject.TryGetComponent(out Warehouse _warehouse))
        {
            if (warehouse + apples > WarehouseMaxValue)
            {
                warehouse = WarehouseMaxValue;
            }
            else
            {
                warehouse += apples; // Склад плюсует в себя количество яблок
            }

            apples = 0; // Обнуляем яблоки в руках

            TextShow();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Pointer pointer)) // Если мы вышли из триггера у которого был скрипт с названием Pointer
        {
            isTakeApples = false;
        }
    }

    private void TextShow()
    {
        ApplesTxt.text = "Apples: " + apples + "/" + ApplesMaxCanTake;
        WarehouseTxt.text = "Warehouse: " + warehouse + "/" + WarehouseMaxValue;
    }
}
