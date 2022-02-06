using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class APP_Global : MonoBehaviour
{
    // global properties
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
        foreach (var item in GameObject.FindObjectsOfType<GameObject>()) {
            item.SendMessage("UpdateGlobalsCache", this);
        }
    }
}
