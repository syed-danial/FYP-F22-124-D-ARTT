using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class blinkingText : MonoBehaviour
{
    private Text textComponent;
    public float blinkInterval = 0.5f;
    private bool isTextVisible = true;

    // Start is called before the first frame update
    void Start()
    {
     textComponent = GetComponent<Text>();
     StartBlinking();   
    }

    void StartBlinking()
    {
     StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            isTextVisible = !isTextVisible;
            textComponent.enabled = isTextVisible;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
