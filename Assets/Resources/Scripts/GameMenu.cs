using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMenu : MonoBehaviour
{
    public abstract void Open();
    public abstract void Close();

    public void SetMenuActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
