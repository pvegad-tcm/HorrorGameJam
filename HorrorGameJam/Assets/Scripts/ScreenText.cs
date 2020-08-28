using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenText : MonoBehaviour
{
    public List<string> computerTexts;
    public TextMeshProUGUI textMesh;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextText()
    {
        index++;
        textMesh.text = computerTexts[index];
    }
}
