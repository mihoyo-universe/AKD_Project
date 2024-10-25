using UnityEngine;

public class EquipController : MonoBehaviour
{
    private string currentObject;
    private GameObject equippedItem; // ������ �� ������ � �����
    private bool handsFull = false; // ���������� ��� ��������, ������ �� �������� �������

    private Transform player; // ������ �� ������ ������ (���������)
    public float throwForce = 10f; // ���� ������

    private void Start()
    {
        player = this.transform; // ������������� ������ �� ������ ������
    }

    public void EquipItem(string item)
    {
        // ���� ���� ������, ������� ������� �������
        if (handsFull)
        {
            ThrowEquippedItem();
        }

        currentObject = item;

        // ������� ������ �� �����
        equippedItem = GameObject.Find(currentObject);
        if (equippedItem != null)
        {
            handsFull = true; // �������������, ��� ���� ������

            // ����������� ������� � ������
            equippedItem.transform.position = player.position + new Vector3(-0.585f, 0.233f, 0.5f); // ������������� ��������� �������
            equippedItem.transform.SetParent(player); // ����������� ������� � ���������
            equippedItem.transform.localRotation = Quaternion.identity; // ���������� ��������

            // ��������� ������������ �������
            equippedItem.transform.localScale = Vector3.one; // ��������, ��� ������� ����� 1
        }
        else
        {
            Debug.LogWarning("������ " + currentObject + " �� ������ �� �����.");
        }
    }

    void Update()
    {
        // ���� � ��������� ���� �������, ��������� ��� �������
        if (handsFull && equippedItem != null)
        {
            equippedItem.transform.localPosition = new Vector3(-0.585f, 0.233f, 0.5f); // ���������� ��������� ����������
        }

        // �������� ������� ������� "Q" ��� ������� ��������
        if (Input.GetKeyDown(KeyCode.Q) && handsFull)
        {
            ThrowEquippedItem();
        }
    }

    private void ThrowEquippedItem()
    {
        // ������� ������� �� ��� � ������� ��� ������
        equippedItem.transform.SetParent(null); // ������� ������� �� ��������
        Rigidbody rb = equippedItem.GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = equippedItem.AddComponent<Rigidbody>(); // ��������� Rigidbody, ���� ��� ���
        }

        // ������������� ������� ������
        equippedItem.transform.position += player.forward; // ������� ������� ������ ������������ ������
        rb.velocity = player.forward * throwForce; // ������������� �������� ��� ������

        // ��������� ������������ ������� ����� ������
        equippedItem.transform.localScale = Vector3.one;

        handsFull = false; // ����������� ����
        equippedItem = null; // ������� ������ �� ������������� �������
    }

    public string GetCurrentItem()
    {
        return currentObject;
    }
}