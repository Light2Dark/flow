using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject orbHolder;
    OrbManager orbManager;
    Text timerText;
    
    // How do I reference the orbs. Events?
    void Start() {
        timerText = GetComponent<Text>();
        orbManager = orbHolder.GetComponent<OrbManager>();
    }

    void Update() {
        float currentTimerTime = orbManager.GetCurrentTimerTime();
        currentTimerTime = Mathf.Round(currentTimerTime * 10) / 10;
        
        timerText.text = currentTimerTime.ToString();
    }
}
