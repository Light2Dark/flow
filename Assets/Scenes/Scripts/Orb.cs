using UnityEngine;

public class Orb : MonoBehaviour
{
    Transform OrbHolder;

    void Start() {
        OrbHolder = FindObjectOfType<OrbManager>().transform;
        this.transform.SetParent(OrbHolder);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            OrbHolder.GetComponent<OrbManager>().PlayOrbCapturedSound();
            Destroy(this.gameObject);
        }
    }
}
