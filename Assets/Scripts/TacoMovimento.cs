using UnityEngine;

public class BastaoController : MonoBehaviour
{
    public float sensitivityX = 2.0f; // Sensibilidade para movimentação no eixo X
    public float sensitivityY = 2.0f; // Sensibilidade para movimentação no eixo Y
    public float chargeRate = 10f; // Taxa de carregamento da força da tacada
    public float maxShootForce = 100f; // Força máxima do tiro
    public float thrustSpeed = 10.0f; // Velocidade da estocada
    public float overshootDistance = 0.5f; // Distância extra além da posição inicial
    public Rigidbody cueRigidbody; // Rigidbody do taco
    public Collider cueCollider; // Collider do taco
    private Vector3 initialPosition; // Posição inicial do taco
    private PlayerMovement playerMovement; // Componente de controle do player
    private float currentShootForce = 0f; // Força de tiro atual
    private bool isThrusting = false; // Se está estocando
    private Vector3 cueRestPosition; // Posição de descanso do taco
    private bool hasHit = false; // Verifica se o taco já bateu na bola
    private bool isReturning = false; // Se o taco está retornando à posição inicial

    void Start()
    {
        // Armazena a posição inicial do taco
        initialPosition = transform.localPosition;
        // Acessa o componente PlayerMovement para verificar o estado
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        // O movimento do taco só é permitido se o botão direito do mouse estiver pressionado e o estado for de controle da arma
        if (playerMovement.isWeaponMode)
        {
            HandleWeaponMovement();
            HandleAimingAndShooting();
        }

        // Verifica se a posição Z do taco está abaixo de -2 e não está carregando uma estocada
        if (!Input.GetMouseButton(0) && transform.localPosition.z < -4f)
        {
            ResetCuePosition(); // Reseta a posição do taco
        }
    }

    void HandleAimingAndShooting()
    {
        // Libera o taco quando o botão é solto
        if (Input.GetMouseButtonUp(0) && !hasHit)
        {
            // Libera o taco para frente com a força acumulada
            ReleaseCue();
        }

        // Inicia o carregamento quando o botão for pressionado
        if (Input.GetMouseButtonDown(0))
        {
            cueCollider.enabled = true; // Ativa o colisor
            cueRestPosition = transform.localPosition; // Salva a posição de descanso do taco
            hasHit = false; // Reseta o estado de hit
            isReturning = false; // Reseta o estado de retorno
        }

        // Enquanto o botão está pressionado, carrega a força
        if (Input.GetMouseButton(0))
        {
            currentShootForce += chargeRate * Time.deltaTime; // Carrega a força
            currentShootForce = Mathf.Clamp(currentShootForce, 0, maxShootForce); // Limita a força ao máximo

            // Taco recua simbolizando o carregamento
            if (currentShootForce < maxShootForce)
            {
                transform.localPosition -= new Vector3(0, 0, 0.01f); // Movimenta o taco um pouco para trás
            }
        }
    }

    // Função que lida com a liberação do taco usando ForceMode.Impulse
    void ReleaseCue()
    {
        // Aplica força de impulso no taco usando o Rigidbody
        cueRigidbody.AddForce(transform.forward * currentShootForce, ForceMode.Impulse);

        // Inicia o movimento de estocada e indica que o taco está avançando
        isThrusting = true;
    }

    private void HandleWeaponMovement()
    {
        // Movimenta o taco baseado no movimento do mouse nos eixos X e Y
        float moveX = Input.GetAxis("Mouse X") * sensitivityX;
        float moveY = Input.GetAxis("Mouse Y") * sensitivityY;
        transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x + moveX, -1f, 1f),
            Mathf.Clamp(transform.localPosition.y + moveY, -1f, 1f),
            transform.localPosition.z
        );
    }

    void FixedUpdate()
    {
        // Se a estocada está ativa, move o taco rapidamente para frente
        if (isThrusting && !isReturning)
        {
            float overshootPositionZ = initialPosition.z + (overshootDistance * (1 + (currentShootForce / maxShootForce))); // Calcula a posição além da inicial
            transform.localPosition += new Vector3(0, 0, thrustSpeed * Time.deltaTime);

            // Se o taco passar da posição inicial com a distância extra, começa a retornar
            if (transform.localPosition.z >= overshootPositionZ)
            {
                isThrusting = false;
                isReturning = true; // Inicia o processo de retorno
            }
        }

        // Se o taco está retornando, move-o de volta à posição inicial
        if (isReturning)
        {
            // Move o taco para trás até sua posição inicial
            transform.localPosition = Vector3.Lerp(transform.localPosition, cueRestPosition, thrustSpeed * Time.deltaTime);

            // Quando o taco atingir a posição inicial, para o movimento e desativa o colisor
            if (Vector3.Distance(transform.localPosition, cueRestPosition) < 0.01f)
            {
                isReturning = false;
                cueCollider.enabled = false; // Desativa o colisor
                currentShootForce = 0f; // Reseta a força de tiro
                cueRigidbody.velocity = Vector3.zero; // Para a velocidade do taco
            }
        }
    }

    // Reseta a posição do taco caso ele fique abaixo de Z = -2
    private void ResetCuePosition()
    {
        Vector3 resetPosition = transform.localPosition;
        resetPosition.z = Mathf.Max(resetPosition.z, -2f); // Reseta o Z para no mínimo -2
        transform.localPosition = resetPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.CompareTag("WhiteBall")) //collision.gameObject.CompareTag("Ball") ||
        {

            hasHit = true;
            isThrusting = false; // Para a estocada ao acertar a bola
            isReturning = true;  // Inicia o processo de retorno

            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            // Aplica força à bola quando o taco colidir com ela
            if (ballRigidbody != null)
            {                   
                Vector3 shootDirection = collision.contacts[0].point - transform.position;
                shootDirection = shootDirection.normalized;

                ballRigidbody.AddForce(shootDirection * currentShootForce, ForceMode.Impulse);
            
            }
        }
    }
}
