using UnityEngine;

public class PocketCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider colliderObject) {
        if (!colliderObject.CompareTag("Ball")) return;
        PocketBall(colliderObject);
    }

    private void PocketBall(Collider colliderObject) {
        Debug.Log("Bola encaçapada!");
        GameController.Instance.AddPoint();
        colliderObject.gameObject.SetActive(false);
    }
}