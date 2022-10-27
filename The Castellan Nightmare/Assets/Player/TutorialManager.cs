using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private RectTransform spacebar;
    [SerializeField] private RectTransform mouseClick;

    private void OnEnable()
    {
        PlayerInput.playerInteract += PlayerInteract;
        PlayerInput.playerUpgrade += PlayerUpgrade;
    }

    private void OnDisable()
    {
        PlayerInput.playerInteract -= PlayerInteract;
        PlayerInput.playerUpgrade -= PlayerUpgrade;
    }

    private void PlayerInteract(Vector2 obj)
    {
        RemovePrompt(TutorialPrompts.Interact);
    }

    private void PlayerUpgrade(Vector2 obj)
    {
        RemovePrompt(TutorialPrompts.Upgrade);
    }

    public void ShowPrompt(TutorialPrompts prompt)
    {
        switch (prompt)
        {
            case TutorialPrompts.Interact:
                spacebar.gameObject.SetActive(true);
                break;
            case TutorialPrompts.Upgrade:
                mouseClick.gameObject.SetActive(true);
                break;
        }
    }

    public void RemovePrompt(TutorialPrompts prompt)
    {
        switch (prompt)
        {
            case TutorialPrompts.Interact:
                spacebar.gameObject.SetActive(false);
                break;
            case TutorialPrompts.Upgrade:
                mouseClick.gameObject.SetActive(false);
                break;
        }
    }

    public void RemovePrompts()
    {
        spacebar.gameObject.SetActive(false);
        mouseClick.gameObject.SetActive(false);
    }
}

public enum TutorialPrompts
{
    Interact,
    Upgrade,
}
