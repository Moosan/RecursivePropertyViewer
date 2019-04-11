using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RecurcivePropertyViewerTest : MonoBehaviour
{
    void Start()
    {
        Debug.Log(RecurcivePropertyViewer.ViewLogMain(this));
    }
}