using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseController : CameraController
{
    public Texture2D cursorTexture;     // Текстура курсора
    public Text cursorItemText;
    public GameObject allItemText; // Объект со всеми текстами в ItemCamera

    private string currentObjectName; // Для хранения текущего объекта
    public EquipController equipController; // Ссылка на EquipController

    void Start()
    {
        base.Start();
        Cursor.lockState = CursorLockMode.Locked; // Скрыть курсор и заблокировать его в центре
    }

    private void Update()
    {
        // Проверка, наведён ли курсор на объект
        CheckMouseOverObject();

        // Проверка нажатия клавиши "E"
        if (Input.GetKeyDown(KeyCode.E) && !string.IsNullOrEmpty(currentObjectName))
        {
            // Передаем информацию о выделенном предмете в EquipController
            equipController.EquipItem(currentObjectName);
        }
    }

    private Dictionary<string, string> itemDescriptions = new Dictionary<string, string>
    {
        { "Pan", "Горячая сковорода" },
        { "GasCan", "Канистра бензина" },
        { "Dumbell1", "Тяжелая гиря" },
        { "Bottle2Purple", "Добрый кола" },
        { "Briefcase", "Чемодан" },
        { "Medkit", "Аптечка" },
        { "Money", "Деньги" },
        { "Plunger", "Вантуз" },
        { "Pot", "Большая Кастрюля" }
    };

    void CheckMouseOverObject()
    {
        // Проверяем, находится ли курсор внутри видимого окна
        if (Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width &&
            Input.mousePosition.y >= 0 && Input.mousePosition.y <= Screen.height)
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Получаем имя объекта, на который наведён курсор
                string objectName = hit.collider.gameObject.name;

                // Проверяем, есть ли описание для данного объекта
                if (itemDescriptions.TryGetValue(objectName, out string description))
                {
                    allItemText.SetActive(true);
                    cursorItemText.text = description; // Устанавливаем текст
                    currentObjectName = objectName; // Сохраняем текущее имя объекта
                }
                else
                {
                    cursorItemText.text = ""; // Очищаем текст, если объекта нет в словаре
                    allItemText.SetActive(false);
                    currentObjectName = null; // Сбрасываем текущее имя объекта
                }
            }
            else
            {
                cursorItemText.text = ""; // Очищаем текст, если ничего не наведено
                allItemText.SetActive(false);
                currentObjectName = null; // Сбрасываем текущее имя объекта
            }
        }
    }

    void OnGUI()
    {
        // Отрисовка курсора в центре экрана
        float cursorWidth = cursorTexture.width;
        float cursorHeight = cursorTexture.height;
        float xMin = (Screen.width / 2) - (cursorWidth / 2);
        float yMin = (Screen.height / 2) - (cursorHeight / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, cursorWidth, cursorHeight), cursorTexture);
    }
}