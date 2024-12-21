using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectReturn : MonoBehaviour
{
    public Canvas LoadCanva;
    public Canvas selfCanva;
    public void Onclick()
    {
        selfCanva.gameObject.SetActive(false);
        LoadCanva.gameObject.SetActive(true);
    }
}
