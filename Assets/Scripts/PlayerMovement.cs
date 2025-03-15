using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private GameObject aimPivot;
    [SerializeField] private BallController ballController;
    public Transform ball;          // Arraste aqui o Transform da bola no Inspetor
    public float ballRotationSpeed = 100f;
    public float ballLaunchForce = 10f;
    private Transform playerTransform;
    private Transform aimPivotTransform;
    private float xRotation = 0f;
    private float yRotation = 0f;
    private float xAimRotation = 0f;
    private float yAimRotation = 0f;
    public bool isShootingMode = false; // Modo de Controle de Bola
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        aimPivotTransform = aimPivot.GetComponent<Transform>();
        Cursor.visible = false; // Esconde o cursor
        Cursor.lockState = CursorLockMode.Locked; // Trava o cursor no centro da tela
    }


    void Update()    {

        if (Input.GetMouseButtonDown(1))
            ChangeBetweenModes();

        if (isShootingMode){
            HandleAimingAndShooting();
            return;
        }
        HandleMouseLook();
        HandleMovement();
    }

    private void ChangeBetweenModes(){
        // Alterna entre os modos de andar e mirar o taco com o clique do botão direito do mouse
        isShootingMode = !isShootingMode;
        aimPivot.SetActive(isShootingMode);
    }

    void HandleMouseLook() {
        // Rotaciona a câmera baseado no movimento do mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xAimRotation += mouseX;
        yAimRotation -= mouseY; // Inverte... pq sim
        yAimRotation = Mathf.Clamp(yAimRotation, -90f, 90f); // Playtest indica que é melhor colocar um limite
        playerTransform.localRotation = Quaternion.Euler(yAimRotation, xAimRotation, 0f);
    }

    void HandleMovement()
    {
        // Pega as entradas do teclado para movimento
        float moveX = Input.GetAxis("Horizontal"); // A/D ou setas esquerda/direita para mover no eixo X
        float moveZ = Input.GetAxis("Vertical");   // W/S ou setas para cima/baixo para mover no eixo Z
        float moveY = 0f; // Inicia zerado
        if (Input.GetKey(KeyCode.LeftShift)) moveY = -1f;  // Move para baixo (Y negativo)
        if (Input.GetKey(KeyCode.Space)) moveY = 1f;      // Move para cima (Y positivo)

        // Calcula a direção do movimento baseado na orientação do jogador
        Vector3 moveDirection = playerTransform.right * moveX + playerTransform.forward * moveZ + playerTransform.up * moveY;
        playerTransform.position += moveDirection * moveSpeed * Time.deltaTime;
    }



    void HandleAimingAndShooting() {

        // Controle de mira
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation += mouseX;
        yRotation -= mouseY;
        aimPivotTransform.localRotation = Quaternion.Euler(yRotation, xRotation, 0f);

        // Controle de lançamento da bola com o clique esquerdo do mouse

        if (Input.GetMouseButton(0)) {
            ChargeLaunch();
        }
        if (Input.GetMouseButtonUp(0)) {
            LaunchBall();
        }

    }
    private void ChargeLaunch(){
        // Aumenta a força de lançamento da bola enquanto o botão esquerdo do mouse é pressionado
        ballLaunchForce += Time.deltaTime * 10f;
        ballLaunchForce = Mathf.Clamp(ballLaunchForce, 10f, 100f); // Limita a força de lançamento
    }

    private void LaunchBall(){
        // Garante que a bola tenha um Rigidbody.
        Rigidbody rigidbody = ball.GetComponent<Rigidbody>();

        if (rigidbody == null) {
            Debug.LogError("A bola não possui um Rigidbody anexado!");
            return;
        }
        rigidbody.AddForce(aimPivotTransform.forward * ballLaunchForce, ForceMode.Impulse);
        ChangeBetweenModes();
        ballController.StartBallMovement();
    }
}
