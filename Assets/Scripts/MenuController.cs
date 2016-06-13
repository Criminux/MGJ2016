using UnityEngine;
using System.Collections;
using System;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    Transform Play;
    [SerializeField]
    Transform Highscore;
    [SerializeField]
    Transform Options;
    [SerializeField]
    Transform Quit;

    [SerializeField]
    float activeDistance;
    float passiveDistance;

    int selectedButton;
    private float previousSelection;

    void Start()
    {
        passiveDistance = Play.transform.localPosition.y;
        SetButtonPosition(Play, activeDistance);
    }

    void Update()
    {
        float playerSelection = Input.GetAxis("VerticalControllerMovement");
        //print(playerSelection);
        if (playerSelection < -0.2f && previousSelection > -0.2f)
        {
            selectedButton++;
            if (selectedButton > 3)
                selectedButton = 0;
            UpdateSelection();
        }
        if (playerSelection > 0.2f && previousSelection < 0.2f)
        {
            selectedButton--;
            if (selectedButton < 0)
                selectedButton = 3;
            UpdateSelection();
        }
        previousSelection = playerSelection;
    }

    private void UpdateSelection()
    {
        SetButtonPosition(Play, passiveDistance);
        SetButtonPosition(Highscore, passiveDistance);
        SetButtonPosition(Options, passiveDistance);
        SetButtonPosition(Quit, passiveDistance);

        switch (selectedButton)
        {
            case 0:
                SetButtonPosition(Play, activeDistance);
                break;
            case 1:
                SetButtonPosition(Highscore, activeDistance);
                break;
            case 2:
                SetButtonPosition(Options, activeDistance);
                break;
            case 3:
                SetButtonPosition(Quit, activeDistance);
                break;
        }
    }

    private void SetButtonPosition(Transform button, float distance)
    {
        button.transform.localPosition = new Vector3(button.transform.localPosition.x, distance, button.transform.localPosition.z);
    }
}
