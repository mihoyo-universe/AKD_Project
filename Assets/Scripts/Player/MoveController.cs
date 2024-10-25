using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float moveSpeed = 5f;       // Скорость движения

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Получение ввода клавиш для движения
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // Создаем вектор движения относительно направления персонажа
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move);
    }
}
