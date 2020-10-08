using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    int numOrbsCaptured;

    public GameObject orb;
    public GameObject gameOverScreen, duringGameScreen;
    public OrbManager orbManager;
    public TextMeshProUGUI orbsCollectedTxt;

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    // Start is called before the first frame update
    void Start()
    {
        orb.GetComponent<OrbManager>().OnTimeRunOut += OnGameOver;
    }

    void OnGameOver() {
        gameOverScreen.SetActive(true);
        duringGameScreen.SetActive(false);
        numOrbsCaptured = orbManager.GetNumberOrbsCaptured();
        
        // function to return a diff. string based on numOrbsCaptured
        string text = CreateTextOrbs(numOrbsCaptured);
        orbsCollectedTxt.text = text;
    }

    private string CreateTextOrbs(int orbsCaptured) {
        if (orbsCaptured <= 5) {
            return orbsCaptured.ToString() + " orb(s) only, " + "I recommend playing with your eyes open";
        } else if (orbsCaptured <= 10) {
            return "Pft, only noobs get " + orbsCaptured.ToString() + " orbs";
        } else if (orbsCaptured <= 15) {
            return "Even my grandma could get " + orbsCaptured.ToString() + " orbs" ;
        } else {
            return "Maybe you're decent..";
        }
    }
}
