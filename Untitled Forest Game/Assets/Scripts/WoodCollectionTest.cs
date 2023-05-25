using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCollectionTest : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text wood;
    private int twig;

    void Start()
    {
        this.wood.text = "Wood";
    }

    void Update()
    {

    }
}