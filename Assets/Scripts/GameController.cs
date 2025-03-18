using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; } // Singleton
    private int score = 0; // Variável para armazenar a pontuação

    // Método chamado pela caçapa para adicionar um ponto
    private void Awake(){
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        Instance = this;
    }
    public void AddPoint(){
        score++;
        UIController.Instance.UpdateScoreUI(score); // Atualiza a UI se estiver usando um Text

    }

    // Método opcional para exibir a pontuação na UI


}