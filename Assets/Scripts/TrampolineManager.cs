using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TrampolineManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _isPressedButton;
    [SerializeField] private GameObject trampoline;
    [SerializeField]private float rotatePower;
    [SerializeField]private string _direction;
    void Update()
    {
        if (_isPressedButton)
        {
            if (_direction == "Left")
            {
                trampoline.transform.Rotate(0, 0,  rotatePower * Time.deltaTime , Space.Self);
            }
            else
            {
                trampoline.transform.Rotate(0, 0,  -rotatePower * Time.deltaTime , Space.Self);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressedButton = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressedButton = false;
    }
}
