using UnityEngine;
public class BallController : MonoBehaviour
{
    [SerializeField] private float stopThreshold = 0.1f; // Velocidade mínima para parar a bola
    private bool isStopped = true; // Flag para verificar se a bola está parada
    [SerializeField] private Transform ballPosition; // Posição inicial da bola

    void Update()    {
        if (!isStopped && GetComponent<Rigidbody>().velocity.magnitude < stopThreshold)
            StopBall();
    }

    public void StartBallMovement(){
        isStopped = false;
    }
    private void StopBall()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        isStopped = true;
        ResetBallRotation();
    }

    private void ResetBallRotation(){
        ballPosition.rotation = Quaternion.identity;
    }
}
