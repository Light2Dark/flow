using UnityEngine;

public class OrbManager : MonoBehaviour
{
    public GameObject orbPrefab;
    public GameObject wallHolder;
    public Transform leftWall, topWall, bottomWall, rightWall;
    public float initialTime, timeReduction;
    public float volume = 0.8f;
    public event System.Action OnTimeRunOut;

    Vector3[] wallPositions =  new Vector3[4];
    Transform[] walls;
    GameObject orb;
    bool timerReset, timerIsRunning; // we are doing linear reduction in time
    int numOfOrbsCaptured;
    AudioSource OrbSound;

    float currentTimerTime; // use this to display timee!
    float timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        OrbSound = this.GetComponent<AudioSource>();
        // Find out left wall position, right wall position, position.x of orb will be btwn these positions
        // same process for horizontal walls but position.y. 
        // Instantiatee in update, as soon as it is destroyed, instantiate another one.

        walls = new Transform[4] {topWall, leftWall, bottomWall, rightWall};
        for (int i = 0; i < walls.Length; i++) {
            wallPositions[i] = walls[i].position;
        }
        SpawnOrb();

        currentTimerTime = initialTime;
    }

    void Update() {

        if (timerIsRunning) {

            if (timerReset) {
                initialTime -= timeReduction;
                currentTimerTime = initialTime;
                timerReset = false;
            }

            if (currentTimerTime > 0) {
                currentTimerTime -= Time.deltaTime;
                
            } else {
                if (OnTimeRunOut != null) {
                    OnTimeRunOut();
                }
            }

        }

        if (orb == null) {
            timerReset = true;
            numOfOrbsCaptured += 1;
            SpawnOrb(); // sets timerStart to true
        }
    }

    public void SpawnOrb() {
        Vector2 randomPoint = GetRandomSpawnPoint();
        Vector3 randomSpawnPoint = new Vector3(randomPoint.x, randomPoint.y, this.transform.position.z);
        orb = Instantiate(orbPrefab, randomSpawnPoint, Quaternion.identity);
        orb.transform.SetParent(this.GetComponent<Transform>());

        timerIsRunning = true;
    }

    public float GetCurrentTimerTime() {
        return currentTimerTime;
    }

    public int GetNumberOrbsCaptured() {
        return numOfOrbsCaptured;
    }

    public void PlayOrbCapturedSound() {
        OrbSound.volume = volume;
        OrbSound.Play();
    }
 
    Vector2 GetRandomSpawnPoint() {
        float wallThickness = walls[0].localScale.x;
        float xPosition = Random.Range(wallPositions[1].x + wallThickness, wallPositions[3].x - wallThickness); // min max, need to account for wall thickness
        float yPosition = Random.Range(wallPositions[0].y - wallThickness, wallPositions[2].y + wallThickness); // this has to be a method
        return new Vector2(xPosition, yPosition);
    }
}
