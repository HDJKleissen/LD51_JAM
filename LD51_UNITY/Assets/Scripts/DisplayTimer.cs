using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTimer : MonoBehaviour
{
    Timer timer;
    [SerializeField] TextMeshProUGUI overheadText;
    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayOverheadText();
    }

    void DisplayOverheadText()
    {
        overheadText.text = $"{timer.TimeLeft.ToString("0.00")}";
    }
}
