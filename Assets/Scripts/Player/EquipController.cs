using UnityEngine;

public class EquipController : MonoBehaviour
{
    private string currentObject;
    private GameObject equippedItem; // Ссылка на объект в руках
    private bool handsFull = false; // Переменная для проверки, держит ли персонаж предмет

    private Transform player; // Ссылка на объект игрока (персонажа)
    public float throwForce = 10f; // Сила броска

    private void Start()
    {
        player = this.transform; // Устанавливаем ссылку на объект игрока
    }

    public void EquipItem(string item)
    {
        // Если руки заняты, бросаем текущий предмет
        if (handsFull)
        {
            ThrowEquippedItem();
        }

        currentObject = item;

        // Находим объект на сцене
        equippedItem = GameObject.Find(currentObject);
        if (equippedItem != null)
        {
            handsFull = true; // Устанавливаем, что руки заняты

            // Привязываем предмет к игроку
            equippedItem.transform.position = player.position + new Vector3(-0.585f, 0.233f, 0.5f); // Устанавливаем начальную позицию
            equippedItem.transform.SetParent(player); // Привязываем предмет к персонажу
            equippedItem.transform.localRotation = Quaternion.identity; // Сбрасываем вращение

            // Сохраняем оригинальный масштаб
            equippedItem.transform.localScale = Vector3.one; // Убедимся, что масштаб равен 1
        }
        else
        {
            Debug.LogWarning("Объект " + currentObject + " не найден на сцене.");
        }
    }

    void Update()
    {
        // Если у персонажа есть предмет, обновляем его позицию
        if (handsFull && equippedItem != null)
        {
            equippedItem.transform.localPosition = new Vector3(-0.585f, 0.233f, 0.5f); // Используем локальные координаты
        }

        // Проверка нажатия клавиши "Q" для выброса предмета
        if (Input.GetKeyDown(KeyCode.Q) && handsFull)
        {
            ThrowEquippedItem();
        }
    }

    private void ThrowEquippedItem()
    {
        // Удаляем предмет из рук и бросаем его вперед
        equippedItem.transform.SetParent(null); // Убираем предмет из родителя
        Rigidbody rb = equippedItem.GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = equippedItem.AddComponent<Rigidbody>(); // Добавляем Rigidbody, если его нет
        }

        // Устанавливаем позицию броска
        equippedItem.transform.position += player.forward; // Бросаем предмет вперед относительно игрока
        rb.velocity = player.forward * throwForce; // Устанавливаем скорость для броска

        // Сохраняем оригинальный масштаб после броска
        equippedItem.transform.localScale = Vector3.one;

        handsFull = false; // Освобождаем руки
        equippedItem = null; // Очищаем ссылку на экипированный предмет
    }

    public string GetCurrentItem()
    {
        return currentObject;
    }
}