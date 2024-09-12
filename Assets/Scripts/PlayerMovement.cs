using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento do jogador
    public float mouseSensitivity = 100f; // Sensibilidade do mouse

    private Transform playerTransform;
    private float xRotation = 0f;
    private float yRotation = 0f;
    public bool isWeaponMode = false; // Estado para alternar entre movimento e controle da arma

    void Start()
    {
        playerTransform = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked; // Trava o cursor no centro da tela
    }

    void Update()
    {
        // Alterna entre os modos de andar e controlar a arma com o clique do botão direito do mouse
        if (Input.GetMouseButtonDown(1))
        {
            isWeaponMode = !isWeaponMode;
        }

        // Se não estiver no modo de controle da arma, o jogador pode se mover
        if (!isWeaponMode)
        {
            HandleMouseLook();
            HandleMovement();
        }
    }

    void HandleMouseLook()
    {
        // Rotaciona a câmera baseado no movimento do mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita a rotação no eixo X

        // Aplica a rotação no player (rotacionando em torno do eixo Y)
        playerTransform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    void HandleMovement()
    {
        // Pega as entradas do teclado para movimento
        float moveX = Input.GetAxis("Horizontal"); // A/D ou setas esquerda/direita para mover no eixo X
        float moveZ = Input.GetAxis("Vertical");   // W/S ou setas para cima/baixo para mover no eixo Z
        float moveY = 0f;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveY = -1f; // Move para baixo (Y negativo)
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            moveY = 1f;  // Move para cima (Y positivo)
        }

        // Calcula a direção do movimento baseado na orientação do jogador
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ + transform.up * moveY;
        
        // Move o jogador
        playerTransform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
