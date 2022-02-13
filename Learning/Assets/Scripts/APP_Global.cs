using UnityEngine;
using UnityEngine.UI;

public class APP_Global : MonoBehaviour
{
    // optimization properties
    private int cmid = -1;
    // references
    private GameObject controls_menu;
    private GameObject options_menu;
    private GameObject main_menu;
    private GameObject collision_text;
    void Start () {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        Text[] collision_texts = canvas.GetComponentsInChildren<Text>();
        foreach (var text in collision_texts) {
            if (text.gameObject.name == "Collision Text") {
                collision_text = text.gameObject;
                break;
            }
        }
        CanvasGroup[] menus = canvas.GetComponentsInChildren<CanvasGroup>();
        foreach (var group in menus) {
            if (group.gameObject.name == "Controls Menu") {
                controls_menu = group.gameObject;
            } else if (group.gameObject.name == "Options Menu") {
                options_menu = group.gameObject;
            } else if (group.gameObject.name == "Main Menu") {
                main_menu = group.gameObject;
            }
            group.gameObject.SetActive(false);
        }
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
    // helper function
    private void CloseActiveMenu () {
        switch (cmid) {
            case 0:
                main_menu.gameObject.SetActive(false);
                break;
            case 1:
                controls_menu.gameObject.SetActive(false);
                break;
            case 2:
                options_menu.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    // sub menu stuff
    public void SubMenuEntry (int mid) {
        if (mid > 2 || mid == cmid) {
            return;
        }
        this.CloseActiveMenu();
        switch (mid) {
            case -1:
                main_menu.gameObject.SetActive(true);
                this.CloseMenu();
                break;
            case 0:
                main_menu.SetActive(true);
                break;
            case 1:
                controls_menu.SetActive(true);
                break;
            case 2:
                options_menu.SetActive(true);
                break;
            default:
                break;
        }
        cmid = mid;
    }
    public bool BackMenu () {
        if (main_menu.gameObject.activeSelf) {
            this.SubMenuEntry(-1);
            return false;
        } else {
            this.SubMenuEntry(0);
            return true;
        }
    }
    public void Collision () {
        collision_text.GetComponent<collision_text_fade>().fade_val = 1f;
        // GameObject text = Resources.Load<GameObject>("CollisionTextPrefab.prefab");
    }
    // menu controlling stuff
    public void OpenMenu () {
        cmid = 0;
        flush_enabled = false;
        main_menu.gameObject.SetActive(true);
    }
    public void CloseMenu () {
        main_menu.gameObject.SetActive(false);
        flush_enabled = true;
        this.UpdateGlobals();
    }
}
