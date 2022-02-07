using UnityEngine;
// using UnityEngine.UI;

public class APP_Global : MonoBehaviour
{
    // references
    private Canvas controls_menu;
    void Start () {
        controls_menu = GameObject.FindObjectOfType<Canvas>();
    }
    // this config
    private bool flush_enabled = true; /* controls if globals can be updated, used to disable updating when high frequency might
                                         be occuring such as in a menu */
    // global properties
    private float mouse_sensitivity_base = 2.5f;
    private float mouse_sensitivity_mult = 1f;
    public float mouse_sensitivity {
        get {return mouse_sensitivity_base * mouse_sensitivity_mult;}
    }
    private float gravity_base = 0.02f;
    private float gravity_mult = 1f;
    public float gravity {
        get {return gravity_base * gravity_mult;}
    }
    // base gravity setters
    public void SetGravityBase (float value) {
        gravity_base = value;
    }
    public void SetGravityBase (float value, bool flush) {
        gravity_base = value;
        if (flush) {
            this.UpdateGlobals();
        }
    }
    // gravity multiplier setters
    public void SetGravityMult (float value) {
        gravity_mult = value;
    }
    public void SetGravityMult (float value, bool flush) {
        gravity_mult = value;
        if (flush) {
            this.UpdateGlobals();
        }
    }
    // update globals
    void UpdateGlobals () {
        if (!flush_enabled) {
            return;
        }
        foreach (var item in GameObject.FindObjectsOfType<GameObject>()) {
            item.SendMessage("UpdateGlobalsCache", this, SendMessageOptions.DontRequireReceiver);
        }
    }
    // menu input stuff
    public void MenuMouseSensitivity (float value) {
        mouse_sensitivity_mult = value;
    }
    // menu controlling stuff
    public void OpenMenu () {
        flush_enabled = false;
        controls_menu.gameObject.SetActive(true);
    }
    public void CloseMenu () {
        controls_menu.gameObject.SetActive(false);
        flush_enabled = true;
        this.UpdateGlobals();
    }
}
