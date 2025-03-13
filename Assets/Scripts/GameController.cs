using UnityEngine;
using TMPro;
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; } // Singleton
    private int score = 0; // Variável para armazenar a pontuação
    [SerializeField] private TextMeshProUGUI scoreText; // Campo para exibir a pontuação no UI (opcional)

    // Método chamado pela caçapa para adicionar um ponto
    private void Awake(){
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Opcional: mantém o GameController entre as cenas

    }
    public void AddPoint(){
        score++;
        UpdateScoreUI(); // Atualiza a UI se estiver usando um Text
        Debug.Log("Ponto Adicionado! Pontuação: " + score);
    }

    // Método opcional para exibir a pontuação na UI
    private void UpdateScoreUI(){
        if (scoreText != null){
            scoreText.text = score.ToString() ;
        }
    }

}