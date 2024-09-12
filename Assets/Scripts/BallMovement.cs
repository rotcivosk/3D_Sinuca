using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour

{
    [SerializeField]
    private float speed = 10f; // Velocidade da bola, ajustável pelo Inspector

    [SerializeField]
    private Transform aimArrow; // Referência à seta indicadora

    [SerializeField]
    private float rotationSpeed = 100f; // Velocidade de rotação da seta

    private Rigidbody rb;

    void Start()
    {
        // Obtém o Rigidbody do objeto
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Controle da rotação da seta
        RotateArrow();

        // Verifica se a tecla Espaço foi pressionada
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void RotateArrow()
    {
        // Obtém a entrada do jogador (setas esquerda/direita para girar no eixo Y, cima/baixo para girar no eixo X)
        float horizontalInput = Input.GetAxis("Horizontal"); // Esquerda/Direita
        float verticalInput = Input.GetAxis("Vertical"); // Cima/Baixo

        // Rotaciona a seta no eixo Y (esquerda/direita)
        aimArrow.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime, Space.World);

        // Rotaciona a seta no eixo X (cima/baixo)
        aimArrow.Rotate(Vector3.right, -verticalInput * rotationSpeed * Time.deltaTime, Space.Self);
    }

    void Shoot()
    {
        // A direção é a frente (forward) da seta
        Vector3 direction = aimArrow.forward;

        // Define a velocidade da bola na direção da seta
        rb.velocity = direction * speed;
    }
}