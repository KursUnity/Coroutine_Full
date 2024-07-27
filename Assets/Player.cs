using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text ApplesTxt; // ����� � ���������� ����� � �����
    public Text WarehouseTxt; // ����� � ��������� ������

    public int WarehouseMaxValue; // ������������ ����������� ������
    public int ApplesMaxCanTake; // ����������� ���������� ����� ��������� � �����

    public float TakeTime;

    int warehouse; // ��������� ������ (�������)
    int apples; // ����� � �����

    bool isTakeApples;

    private void Start()
    {
        TextShow();

        StartCoroutine(ApplesCounter());
    }

    IEnumerator ApplesCounter()
    {
        var timer = new WaitForSeconds(TakeTime);

        while (true) // ����������� ����. 
        {
            yield return timer; // ������������ ������! ��� �� Unity ��������. ����� � ����� TakeTime ������

            if (isTakeApples && apples < ApplesMaxCanTake) // ���� ����� ������ � ����� ����� ������ ��� ���������
            {
                apples++;
                TextShow();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Pointer pointer)) // ���� �� ����� � ������� � �������� ���� ������ Pointer
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
                warehouse += apples; // ����� ������� � ���� ���������� �����
            }

            apples = 0; // �������� ������ � �����

            TextShow();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Pointer pointer)) // ���� �� ����� �� �������� � �������� ��� ������ � ��������� Pointer
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
