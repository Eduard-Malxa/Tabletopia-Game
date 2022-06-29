using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CylinderUIButtons : UIButtons
{
    public override void OpenButton(MeshType value)
    {
        base.OpenButton(value);
        buttonParent.SetActive(true);
    }

    private void Update()
    {
        if (meshType != null)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                meshType.MeshTypeMovement.enabled = false;
            }
            else
            {
                meshType.MeshTypeMovement.enabled = true;
            }
        }
    }
}
