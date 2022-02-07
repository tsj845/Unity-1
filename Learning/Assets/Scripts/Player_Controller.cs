using UnityEngine;
// using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    // references
    private APP_Global global_vars;
    // properties
    bool onfloor = true;
    public bool control_while_in_air = true;
    bool menu_open = false;
    float velup = 0.0f;
    float gravity = 0.02f;
    float initvup = 0.3f;
    bool cursor_shown = true;
    public float sensitivity = 2.5f;
    public float msense = 0.125f;
    void ToggleCursor () {
        cursor_shown = !cursor_shown;
        this.SetCursor();
    }
    void SetCursor () {
        Cursor.lockState = cursor_shown ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = cursor_shown;
    }
    public void UpdateGlobalsCache (APP_Global global_vars_v) {
        // APP_Global global_vars = GameObject.FindGameObjectWithTag("SCRIPTING_GLOBAL").GetComponent<APP_Global>();
        gravity = global_vars_v.gravity;
        sensitivity = global_vars_v.mouse_sensitivity;
    }
    void RegenerateRefs () {
        global_vars = GameObject.FindGameObjectWithTag("SCRIPTING_GLOBAL").GetComponent<APP_Global>();
    }
    // Start is called before the first frame update
    void Start()
    {
        this.RegenerateRefs();
        this.UpdateGlobalsCache(global_vars);
        this.ToggleCursor();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P)) {
            menu_open = !menu_open;
            cursor_shown = menu_open;
            this.SetCursor();
            // this.ToggleCursor();
            if (menu_open) {
                global_vars.OpenMenu();
            } else {
                global_vars.CloseMenu();
                this.RegenerateRefs();
            }
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            // Debug.Log(sensitivity);
            // Debug.Log(menu_open + " " + cursor_shown.ToString() + " " + Cursor.visible.ToString() + " " + Cursor.lockState.ToString());
        }
        if (Input.GetKeyUp(KeyCode.M)) {
            this.ToggleCursor();
        }
        if (menu_open) {
            return;
        }
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
