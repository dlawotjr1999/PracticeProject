using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUITemp : MonoBehaviour
{
    public float Cooldown = 3f;
    public Image img;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        if(fadeAway)
        {
            for(float i = Cooldown; i >= 0; i-= Time.deltaTime)
            {
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        else
        {
            for (float i = 0f; i <= Cooldown; i += Time.deltaTime)
            {
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}
