using UnityEngine;

public class MSenseSlider : MonoBehaviour
{
    GameObject globals;
    void Start () {
        globals = GameObject.FindGameObjectWithTag("SCRIPTING_GLOBAL");
    }
    void OnValueChanged (float value) {
        Debug.Log("base input detected");
        globals.SendMessage("MenuMouseSensitivity", value);
    }
}
