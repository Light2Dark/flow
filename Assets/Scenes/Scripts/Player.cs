using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed, rotateSpeed, brakePercentOutOfVelocity;
    public FixedJoystick joystick;
    public float maxVelocity;
    public float reduceVelocity;
    public AudioClip orbPickupSound, wallHitSound;
    Rigidbody2D rb;
    
    AudioSource soundEffects;
    bool flow, slow;    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        soundEffects = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // rotating player
        Vector2 directionJoystick = joystick.Direction;
        if (directionJoystick != Vector2.zero) {

            float angle = Mathf.Atan2(directionJoystick.y, directionJoystick.x) * Mathf.Rad2Deg - 90;
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, angle, Time.fixedDeltaTime * rotateSpeed));
        }

        // moving player
        Vector2 currentVelocity = rb.GetPointVelocity(rb.transform.position);
        if (currentVelocity.magnitude > maxVelocity) {
            rb.velocity = new Vector2(maxVelocity - reduceVelocity, maxVelocity - reduceVelocity);
        }
        if (flow) {
            if (currentVelocity.magnitude < maxVelocity) {
                rb.AddForce(transform.up * speed, ForceMode2D.Force);
            }
        } else if (slow) {
            rb.velocity = rb.velocity * brakePercentOutOfVelocity * Time.fixedDeltaTime;
            rb.angularVelocity = rb.angularVelocity * brakePercentOutOfVelocity * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if (other.transform.tag == "Wall") {
            soundEffects.clip = wallHitSound;
            soundEffects.volume = 0.65f;
            soundEffects.Play();
        }
    }

    public void ToggleFlow() {
        flow = !flow;
    }

    public void ToggleSlow() {
        slow = !slow;
    }


}
