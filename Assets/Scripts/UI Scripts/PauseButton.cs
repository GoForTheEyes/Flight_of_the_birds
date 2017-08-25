using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

    private Image pauseImage;
    private Vector2 size;
    [SerializeField]
    private Sprite pause, resume;

	// Use this for initialization
	void Start () {
        pauseImage = GetComponent<Image>();
        pauseImage.sprite = pause;
        size = pauseImage.rectTransform.sizeDelta;

        
    }

    public void SwitchPauseButtonImage()
    {
        if (pauseImage.sprite == pause)
        {
            pauseImage.sprite = resume;
            pauseImage.rectTransform.sizeDelta = size;

        } else
        {
            pauseImage.sprite = pause;
            pauseImage.rectTransform.sizeDelta = size;
        }
    }
	
}
