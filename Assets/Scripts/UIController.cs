using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI scoreText; // Campo para exibir a pontuação no UI (opcional)
    public static UIController Instance { get; private set; } // Singleton
    // Start is called before the first frame update
    private void Awake(){
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScoreUI(int score){
        if (scoreText != null){
            scoreText.text = score.ToString() ;
        }
    }
}
