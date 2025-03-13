/*using UnityEngine;

public class ShootBall : MonoBehaviour
{
    [SerializeField] private float maxShootForce = 50f; // Força máxima aplicada à bola
    [SerializeField] private float chargeRate = 10f; // Velocidade com que a força aumenta ao segurar o clique
    [SerializeField] private Transform ball; // Referência à bola
    [SerializeField] private Transform cueRestPosition; // Posição inicial do taco
    [SerializeField] private Transform cueAimPosition; // Posição de mira do taco
    [SerializeField] private Collider cueCollider; // O colisor do taco
    [SerializeField] private AudioClip shootSound; // Som a ser reproduzido na tacada
    [SerializeField] private AudioSource audioSource; // Fonte de áudio para reproduzir o som da tacada
    private float currentShootForce = 0f; // Força atual carregada
    private bool isAiming = false; // Para controlar a fase de mira

    void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        // Se o botão direito do mouse for pressionado, entrar na fase de mira
        if (Input.GetMouseButtonDown(1))
        {
            isAiming = true;
            transform.position = cueAimPosition.position; // Move o taco para a posição de mira
            cueCollider.enabled = true; // Ativa o colisor do taco
        }

        // Se o botão direito do mouse for solto, sair da fase de mira
        if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
            transform.position = cueRestPosition.position; // Retorna o taco à posição inicial
            cueCollider.enabled = false; // Desativa o colisor do taco
            currentShootForce = 0f; // Reseta a força
        }

        // Se o jogador está mirando e pressiona o botão esquerdo do mouse, carregar a força
        if (isAiming && Input.GetMouseButton(0))
        {
            currentShootForce += chargeRate * Time.deltaTime;
            currentShootForce = Mathf.Clamp(currentShootForce, 0, maxShootForce); // Limita a força ao valor máximo
        }

        // Quando o botão esquerdo é solto, dispara a bola
        if (isAiming && Input.GetMouseButtonUp(0))
        {
            Shoot();
            currentShootForce = 0f; // Reseta a força após o tiro
        }
    }

    private void Shoot()
    {
        // Verifica se a bola está atribuída e possui um Rigidbody
        if (ball != null && ball.GetComponent<Rigidbody>() != null)
        {
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();

            // Calcula a direção em que o taco está apontando
            Vector3 shootDirection = (ball.position - transform.position).normalized;

            // Aplica a força acumulada à bola
            ballRigidbody.AddForce(shootDirection * currentShootForce, ForceMode.Impulse);

            SoundEffectPlay();
        }
    }

    private void SoundEffectPlay()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
*/