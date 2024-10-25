using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 2f; // ���������������� ����
    private float verticalRotation = 0f; // ������������ ���� ��������
    protected Camera playerCamera;

    protected void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
    }
    void Update()
    {
        // ��������� ����� ���� ��� �������� ������
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // ��������� ������������ ���� �������� � ������������ ���
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // ����������� �� ���������

        // ������������� ���� �������� ��� ������
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        // ������������ ��������� �� ��� Y
        transform.Rotate(Vector3.up * mouseX);
    }
}
