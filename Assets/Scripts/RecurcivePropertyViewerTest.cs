using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RecurcivePropertyViewerTest : MonoBehaviour
{
    void Start()
    {
        //Debug.Log(RecurcivePropertyViewer.ViewLogMain(this));
        Debug.Log(RecurcivePropertyViewer.ViewLogMain(gameObject));
        //Debug.Log(RecurcivePropertyViewer.ViewLogMain(transform));
        //Debug.Log(RecurcivePropertyViewer.ViewLogMain(tag));
        //Debug.Log(RecurcivePropertyViewer.ViewLogMain(name));
        //Debug.Log(RecurcivePropertyViewer.ViewLogMain(hideFlags));
        //Debug.Log(RecurcivePropertyViewer.ViewLogMain(gameObject.scene));
        //Debug.Log(RecurcivePropertyViewer.ViewLogMain(gameObject.scene.path));
    }
}