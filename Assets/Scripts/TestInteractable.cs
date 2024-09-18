using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : Interactable
{
    public override void OnFocus()
    {
        print("Looking At : " + gameObject.name);
    }

    public override void OnInteract()
    {
        print("Interacted With : " + gameObject.name);
    }

    public override void OnLoseFocus()
    {
        print("Stopped Looking At : " + gameObject.name);
    }
}
