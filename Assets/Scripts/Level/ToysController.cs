using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToysController : MonoBehaviour
{
    private List<string> activeToysIds = new List<string>();

    public List<string> ActiveToysIds { get { return activeToysIds; } }
}
