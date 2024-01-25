using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextLogic : MonoBehaviour
{

    private string[] displayText = {
        "one",
        "two",
        "three"
    };

    public TextMeshProUGUI canvasText;
    public SpriteRenderer textBackgroundImage;

    void Start()
    {
        
    }

    void Update()
    {
        if (canvasText.enabled && Input.GetKeyDown(KeyCode.Return) && SM.displayTextIterator > 0) {
            canvasText.enabled = false;
            textBackgroundImage.enabled = false;
        }
    }

    public void IntroTextDisplay() {
        //canvasText.text = displayText[SM.displayTextIterator];
        //canvasText.enabled = true;        
        //textBackgroundImage.enabled = true;
        SM.displayTextIterator = SM.displayTextIterator + 1;
    }

}
