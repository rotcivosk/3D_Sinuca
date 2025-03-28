using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] private Transform whiteBall; // Referência à bola branca
    [SerializeField] private float offsetDistance = 2f; // Distância do jogador em relação à bola branca

    void Update()
    {
        // Verifica se a tecla F foi pressionada
        if (Input.GetKeyDown(KeyCode.F))
        {
            TeleportBehindWhiteBall();
        }
    }

    private void TeleportBehindWhiteBall()    {
        if (whiteBall != null)
        {
            Vector3 forwardDirection = transform.forward;
            Vector3 targetPosition = whiteBall.position - forwardDirection * offsetDistance;
            transform.position = targetPosition;
            transform.LookAt(whiteBall);
        }        else        {
            Debug.LogWarning("A referência à bola branca não foi definida.");
        }
    }
}
