using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class MeshTypeMovement : MonoBehaviour
{
    [SerializeField]
    private MeshType meshType;

    [SerializeField]
    private Collider meshTypeCollider;

    [SerializeField]
    private Rigidbody meshTypeRigidBody;

    [SerializeField]
    private Renderer meshTypeRenderer;

    private Vector3 worldPosition;

    private Vector3 screenPostion;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Color selectedColor;

    [SerializeField]
    private Color deSelelctedColor;

    [SerializeField]
    private float folowSpeed;

    [SerializeField]
    private bool moveAccess;

    private float waitForMoveTime;

    [SerializeField]
    private float timeDelay;

    [SerializeField]
    private int selectedTimesCount;

    [SerializeField]
    private float rotationSpeed;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            screenPostion = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(screenPostion);

            if (Physics.Raycast(ray, out RaycastHit hitdata, 100, groundLayer))
            {
                if (moveAccess)
                {
                    if (waitForMoveTime >= timeDelay)
                    {
                        worldPosition = hitdata.point;
                        var newPosition = new Vector3(worldPosition.x, transform.position.y, worldPosition.z);
                        transform.position = Vector3.Lerp(transform.position, newPosition, folowSpeed * Time.deltaTime);
                    }

                    if (waitForMoveTime < timeDelay)
                    {
                        waitForMoveTime += Time.deltaTime;
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            screenPostion = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(screenPostion);

            if (Physics.Raycast(ray, out RaycastHit hitdata))
            {
                Collider collider = hitdata.collider;

                if (collider == meshTypeCollider)
                {
                    moveAccess = true;
                    meshTypeRenderer.material.color = selectedColor;

                    if (selectedTimesCount == 0)
                    {
                        meshType.UIButtons.OpenButton(meshType);
                        ShakeOnSelect();
                        selectedTimesCount++;
                    }
                }
                else
                {
                    if (hitdata.collider.gameObject.TryGetComponent(out Ground ground) && selectedTimesCount > 0)
                    {
                        meshType.UIButtons.CloseAllButtonsParents();
                    }

                    moveAccess = false;
                    meshTypeRenderer.material.color = deSelelctedColor;
                    selectedTimesCount = 0;
                }
            }
        }
    }

    private void OnMouseUp()
    {
        waitForMoveTime = 0;
    }

    private void ShakeOnSelect()
    {
        transform.DOPunchScale(Vector3.one / 3, 0.25f, 1, 1);
    }

    public void ResetRotation(Button button)
    {
        RotateWithElevetion(button, new Vector3(0, 0, 0));
    }

    public void Rotate(Button button)
    {
        Sequence secuence = DOTween.Sequence();
        Tween rotation = transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z),
                                            rotationSpeed, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        secuence.Append(rotation).OnComplete(() => button.interactable = true);
    }

    public void Flip(Button button)
    {
        RotateWithElevetion(button, new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 90));
    }

    public void RotateWithElevetion(Button button, Vector3 vector3)
    {
        Sequence secuence = DOTween.Sequence();
        Tween moveUp = transform.DOLocalMove(new Vector3(transform.localPosition.x, transform.localPosition.y + 3, transform.localPosition.z),
                                                         rotationSpeed);

        Tween rotation = transform.DORotate(vector3,
                                            rotationSpeed, RotateMode.FastBeyond360).SetEase(Ease.Linear);

        secuence.AppendCallback(() => IsKinematicRidiBody(true))
                .Append(moveUp)
                .Join(rotation)
                .AppendCallback(() => IsKinematicRidiBody(false))
                .OnComplete(() => button.interactable = true);
    }

    private void IsKinematicRidiBody(bool value)
    {
        meshTypeRigidBody.isKinematic = value;
    }
}
