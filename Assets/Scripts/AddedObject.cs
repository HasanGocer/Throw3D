using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AddedObject : MonoSingleton<AddedObject>
{
    public void StartSlalom(ObjectTouch objectTouch)
    {
        objectTouch.ItemDown();
        objectTouch.WinFunc();
        objectTouch.WrongObjectFunc(objectTouch.gameObject);
    }
}
