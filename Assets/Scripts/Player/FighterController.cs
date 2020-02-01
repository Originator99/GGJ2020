using UnityEngine;
using System.Collections;

public class FighterController : MonoBehaviour {

    [SerializeField]
    private int maxSpeed = 200;

    [SerializeField]
    private float accelerator = 5000;

    [SerializeField]
    private int turnSpeed;

    private Transform tr;
    private Rigidbody rb;

    private float ntruePitch;
    private float ntrueYaw;
    private float trueYaw;
    private float truePitch;

    private bool can_controll_player;

    void Start () {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        GameEvents.OnEventAction += HandlePlayerEvents;

    }

    private void OnDestroy() {
        GameEvents.OnEventAction -= HandlePlayerEvents;
    }

    private void HandlePlayerEvents(EVENT_TYPE type, System.Object data = null) {
        if (type == EVENT_TYPE.GAME_START) {
            Cursor.lockState = CursorLockMode.Locked;
            can_controll_player = true;
        }
        if (type == EVENT_TYPE.GAME_OVER) {
            Cursor.lockState = CursorLockMode.None;
            can_controll_player = false;
        }
    }


    void Update () {
        if (can_controll_player) {
            truePitch = -Input.GetAxis("Mouse Y");
            trueYaw = Input.GetAxis("Mouse X");

            ntrueYaw = Mathf.Lerp(ntrueYaw, trueYaw, Time.deltaTime * 4);
            ntruePitch = Mathf.Lerp(ntruePitch, truePitch, Time.deltaTime * 4);

            if (Input.GetKey(KeyCode.LeftShift)) {
                maxSpeed = 1000;
                accelerator = 10000;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift)) {
                maxSpeed = 200;
                accelerator = 5000;
            }
        }
    }

    void FixedUpdate() {
        if (can_controll_player) {
            float accel = 0;
            float moveX = 0;
            float moveY = 0;
            float currentSpeed = GetComponent<Rigidbody>().velocity.magnitude;

            if (Input.GetKey(KeyCode.W)) accel = accelerator;
            else if (Input.GetKey(KeyCode.S)) accel = -accelerator;

            Quaternion newRot = Quaternion.Euler(tr.eulerAngles.x, tr.eulerAngles.y, 0);
            if (Input.GetKey(KeyCode.A)) {
                moveX = -5000;
                newRot = Quaternion.Euler(tr.eulerAngles.x, tr.eulerAngles.y, 90);
            } else if (Input.GetKey(KeyCode.D)) {
                moveX = 5000;
                newRot = Quaternion.Euler(tr.eulerAngles.x, tr.eulerAngles.y, -90);
            }

            tr.rotation = Quaternion.Slerp(tr.rotation, newRot, Time.deltaTime * turnSpeed);

            if (Input.GetKey(KeyCode.Space)) {
                moveY = 2000;
                Cursor.lockState = CursorLockMode.Locked;
            } else if (Input.GetKey(KeyCode.LeftControl)) {
                moveY = -2000;
            }


            Quaternion rot = Quaternion.Euler(tr.eulerAngles.x, tr.eulerAngles.y, 0);
            rb.AddForce(rot * Vector3.forward * accel);
            rb.AddForce(rot * Vector3.right * moveX);
            rb.AddForce(rot * Vector3.up * moveY);
            tr.Rotate(ntruePitch, ntrueYaw, 0);

            if (currentSpeed > maxSpeed) {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }
}
