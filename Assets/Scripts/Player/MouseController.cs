using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseController : CameraController
{
    public Texture2D cursorTexture;     // �������� �������
    public Text cursorItemText;
    public GameObject allItemText; // ������ �� ����� �������� � ItemCamera

    private string currentObjectName; // ��� �������� �������� �������
    public EquipController equipController; // ������ �� EquipController

    void Start()
    {
        base.Start();
        Cursor.lockState = CursorLockMode.Locked; // ������ ������ � ������������� ��� � ������
    }

    private void Update()
    {
        // ��������, ������ �� ������ �� ������
        CheckMouseOverObject();

        // �������� ������� ������� "E"
        if (Input.GetKeyDown(KeyCode.E) && !string.IsNullOrEmpty(currentObjectName))
        {
            // �������� ���������� � ���������� �������� � EquipController
            equipController.EquipItem(currentObjectName);
        }
    }

    private Dictionary<string, string> itemDescriptions = new Dictionary<string, string>
    {
        { "Pan", "������� ���������" },
        { "GasCan", "�������� �������" },
        { "Dumbell1", "������� ����" },
        { "Bottle2Purple", "������ ����" },
        { "Briefcase", "�������" },
        { "Medkit", "�������" },
        { "Money", "������" },
        { "Plunger", "������" },
        { "Pot", "������� ��������" }
    };

    void CheckMouseOverObject()
    {
        // ���������, ��������� �� ������ ������ �������� ����
        if (Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width &&
            Input.mousePosition.y >= 0 && Input.mousePosition.y <= Screen.height)
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // �������� ��� �������, �� ������� ������ ������
                string objectName = hit.collider.gameObject.name;

                // ���������, ���� �� �������� ��� ������� �������
                if (itemDescriptions.TryGetValue(objectName, out string description))
                {
                    allItemText.SetActive(true);
                    cursorItemText.text = description; // ������������� �����
                    currentObjectName = objectName; // ��������� ������� ��� �������
                }
                else
                {
                    cursorItemText.text = ""; // ������� �����, ���� ������� ��� � �������
                    allItemText.SetActive(false);
                    currentObjectName = null; // ���������� ������� ��� �������
                }
            }
            else
            {
                cursorItemText.text = ""; // ������� �����, ���� ������ �� ��������
                allItemText.SetActive(false);
                currentObjectName = null; // ���������� ������� ��� �������
            }
        }
    }

    void OnGUI()
    {
        // ��������� ������� � ������ ������
        float cursorWidth = cursorTexture.width;
        float cursorHeight = cursorTexture.height;
        float xMin = (Screen.width / 2) - (cursorWidth / 2);
        float yMin = (Screen.height / 2) - (cursorHeight / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, cursorWidth, cursorHeight), cursorTexture);
    }
}