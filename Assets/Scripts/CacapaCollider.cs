using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacapaCollider : MonoBehaviour
{
    [SerializeField] private GameController gameController; // Referência ao GameController
    [SerializeField] private string ballTag = "Ball"; // Tag usada para identificar as bolas

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou no trigger é uma bola
        if (other.CompareTag(ballTag))
        {
            // Adiciona um ponto no GameController
            gameController.AddPoint();

            // Desativa a bola que entrou na caçapa
            other.gameObject.SetActive(false);
        }
    }
}