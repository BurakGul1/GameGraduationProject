using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TrampolineManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _isPressedButton;
    [SerializeField] private GameObject _trampoline;
    [SerializeField]private float _rotatePower;
    [SerializeField]private string _direction;
    void Update()
    {
        if (_isPressedButton)
        {
            if (_direction == "Left")
            {
                _trampoline.transform.Rotate(0, 0,  _rotatePower * Time.deltaTime , Space.Self);
            }
            else
            {
                _trampoline.transform.Rotate(0, 0,  -_rotatePower * Time.deltaTime , Space.Self);
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
