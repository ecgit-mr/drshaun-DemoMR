using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObjectTransparent : MonoBehaviour
{
    public GameObject currentGameObject;
    public float alpha = 0.5f;//half transparency
    //Get current material
    private Material currentMat;

    public float CurrentAlpha { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetCurrentAlpha()
    {
        var meshRenderer = currentGameObject.GetComponent<Renderer>();
        currentMat = meshRenderer.material;
        ChangeAlpha(this.currentMat, 0.5f);

        ChangeAlpha(currentMat, this.CurrentAlpha);
    }

    void ChangeAlpha(Material mat, float alphaVal)
    { 
        this.CurrentAlpha = alphaVal;
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        mat.SetColor("_Color", newColor);
    }

    public void OnSliderValueChanged(SliderEventData eventData)
    {
        if(this.currentMat == null)
            return;
        this.ChangeAlpha(this.currentMat, eventData.NewValue);
    }
}
