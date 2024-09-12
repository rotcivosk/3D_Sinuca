using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements; // Necessário se você quiser mostrar a pontuação em um UI Text

public class GameController : MonoBehaviour
{
    private int score = 0; // Variável para armazenar a pontuação
    [SerializeField] private TextMeshProUGUI scoreText; // Campo para exibir a pontuação no UI (opcional)

    // Método chamado pela caçapa para adicionar um ponto
    public void AddPoint()
    {
        score++;
        UpdateScoreUI(); // Atualiza a UI se estiver usando um Text
        Debug.Log("Ponto Adicionado! Pontuação: " + score);
    }

    // Método opcional para exibir a pontuação na UI
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString() ;
        }
    }

    // Método opcional para resetar a pontuação (se necessário)
    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }

    // Retorna a pontuação atual (se precisar acessar em outro lugar)
    public int GetScore()
    {
        return score;
    }
}