using UnityEngine;

public class GarageDoor : MonoBehaviour
{
    public float openHeight = 5f; // ������, �� ������� ����������� ������
    public float openSpeed = 2f; // �������� �������� �����
    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpening = false; // ���� ��� ��������
    private bool isClosing = false; // ���� ��� ��������

    void Start()
    {
        // ������������� �������� �������
        closedPosition = transform.position;
        openPosition = closedPosition + new Vector3(0, openHeight, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) // ������� ������� O ��� ��������/��������
        {
            ToggleDoor();
        }

        // �������� �����
        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, openSpeed * Time.deltaTime);
            if (transform.position == openPosition)
                isOpening = false; // ��������� ����
        }
        else if (isClosing)
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, openSpeed * Time.deltaTime);
            if (transform.position == closedPosition)
                isClosing = false; // ��������� ����
        }
    }

    void ToggleDoor()
    {
        if (isOpening || transform.position == openPosition) // ���� ������ �������, �� ���������
        {
            isClosing = true;
        }
        else // ����� ���������
        {
            isOpening = true;
        }
    }
}