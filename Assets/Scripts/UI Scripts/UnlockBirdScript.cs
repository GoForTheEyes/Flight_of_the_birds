using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnlockBirdScript : MonoBehaviour {

    [SerializeField]
    private Sprite[] unlockedBirds;

    [SerializeField]
    private Image unlockedImage;

    [SerializeField]
    private Text text1, text2;

    public void UnlockGreen ()
    {
        text1.color = Color.green;
        text2.color = Color.green;
        unlockedImage.sprite = unlockedBirds[0];
    }

    public void UnlockRed()
    {
        text1.color = Color.red;
        text2.color = Color.red;
        unlockedImage.sprite = unlockedBirds[1];
    }

}
