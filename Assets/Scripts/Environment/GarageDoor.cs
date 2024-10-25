using UnityEngine;

public class GarageDoor : MonoBehaviour
{
    public float openHeight = 5f; // Высота, на которую поднимаются ворота
    public float openSpeed = 2f; // Скорость открытия ворот
    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpening = false; // Флаг для открытия
    private bool isClosing = false; // Флаг для закрытия

    void Start()
    {
        // Устанавливаем закрытую позицию
        closedPosition = transform.position;
        openPosition = closedPosition + new Vector3(0, openHeight, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) // Нажатие клавиши O для открытия/закрытия
        {
            ToggleDoor();
        }

        // Движение ворот
        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, openSpeed * Time.deltaTime);
            if (transform.position == openPosition)
                isOpening = false; // Закрываем флаг
        }
        else if (isClosing)
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, openSpeed * Time.deltaTime);
            if (transform.position == closedPosition)
                isClosing = false; // Закрываем флаг
        }
    }

    void ToggleDoor()
    {
        if (isOpening || transform.position == openPosition) // Если ворота открыты, то закрываем
        {
            isClosing = true;
        }
        else // Иначе открываем
        {
            isOpening = true;
        }
    }
}