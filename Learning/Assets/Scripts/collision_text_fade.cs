using UnityEngine;
using UnityEngine.UI;

public class collision_text_fade : MonoBehaviour
{
    public float fade_val = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fade_val >= 0) {
            this.GetComponent<Text>().color = new Color(0f, 0f, 0f, fade_val);
            fade_val -= 0.01f;
        }
    }
}
