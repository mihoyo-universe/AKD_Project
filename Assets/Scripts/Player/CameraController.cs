using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 2f; // Чувствительность мыши
    private float verticalRotation = 0f; // Вертикальный угол поворота
    protected Camera playerCamera;

    protected void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
    }
    void Update()
    {
        // Получение ввода мыши для вращения камеры
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Обновляем вертикальный угол поворота и ограничиваем его
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Ограничение по вертикали

        // Устанавливаем угол поворота для камеры
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        // Поворачиваем персонажа по оси Y
        transform.Rotate(Vector3.up * mouseX);
    }
}
