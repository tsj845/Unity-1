using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    bool onfloor = true;
    public bool control_while_in_air = true;
    float velup = 0.0f;
    float gravity = 0.02f;
    float initvup = 0.3f;
    bool cursor_shown = true;
    public float sensitivity = 2.5f;
    public float msense = 0.125f;
    void ToggleCursor () {
        cursor_shown = !cursor_shown;
        Cursor.lockState = cursor_shown ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = cursor_shown;
    }
    public void UpdateGlobalsCache (APP_Global global_vars) {
        // APP_Global global_vars = GameObject.FindGameObjectWithTag("SCRIPTING_GLOBAL").GetComponent<APP_Global>();
        gravity = global_vars.gravity;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.UpdateGlobalsCache(GameObject.FindGameObjectWithTag("SCRIPTING_GLOBAL").GetComponent<APP_Global>());
        this.ToggleCursor();
    }
    // Update is called once per frame
    void Update()
    {
        // allows control of velocity to be disabled while in air
        if (control_while_in_air || onfloor) {
            float strafe_amount = Input.GetAxisRaw("Horizontal") * msense;
            float aligned_amount = Input.GetAxisRaw("Vertical") * msense;
            Vector3 aligned_transform = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
            transform.Translate(strafe_amount, 0, 0);
            transform.position += (aligned_transform * aligned_amount);
        }

        if (Input.GetKey(KeyCode.Space) && onfloor) {
            this.Jump();
        }
        if (Input.GetKeyUp(KeyCode.M)) {
            this.ToggleCursor();
        }
        // only rotates camera when cursor is hidden
        if (!cursor_shown) {
            transform.eulerAngles += new Vector3(Input.GetAxisRaw("Mouse Y") * -sensitivity, Input.GetAxisRaw("Mouse X") * sensitivity, 0.0f);
        }
        // does logic for vertical movement
        if (!onfloor) {
            transform.position += new Vector3(0, velup, 0);
            // downwards acceleration
            velup -= gravity;
            // ensueres player doesn't go below floor
            if (transform.position.y < 0) {
                transform.position += new Vector3(0, 0-transform.position.y, 0);
                onfloor = true;
                velup = 0.0f;
            }
        }
    }
    void Jump () {
        if (!onfloor) {
            return;
        }
        onfloor = false;
        velup = initvup;
    }
}
