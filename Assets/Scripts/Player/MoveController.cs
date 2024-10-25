using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float moveSpeed = 5f;       // �������� ��������

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // ��������� ����� ������ ��� ��������
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // ������� ������ �������� ������������ ����������� ���������
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move);
    }
}
