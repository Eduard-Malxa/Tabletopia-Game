using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIButtons : MonoBehaviour
{
    [SerializeField]
    private FlatUIButtons flatUIButtons;

    [SerializeField]
    private CylinderUIButtons cylinderUIButtons;

    [SerializeField]
    protected MeshType meshType;

    [SerializeField]
    protected GameObject buttonParent;

    public void CloseAllButtonsParents()
    {
        flatUIButtons.buttonParent.SetActive(false);
        cylinderUIButtons.buttonParent.SetActive(false);
    }

    public virtual void OpenButton(MeshType value)
    {
        CloseAllButtonsParents();
        SetMeshType(value);
    }

    public void SetMeshType(MeshType value)
    {
        meshType = value;
    }

    public void RotateButton(Button button)
    {
        button.interactable = false;
        meshType.MeshTypeMovement.Rotate(button);
    }

    public void FlipButton(Button button)
    {
        button.interactable = false;
        meshType.MeshTypeMovement.Flip(button);
    }

    public void ResetRotationButton(Button button)
    {
        button.interactable = false;
        meshType.MeshTypeMovement.ResetRotation(button);
    }
}
