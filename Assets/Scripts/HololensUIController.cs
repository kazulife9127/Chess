using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HololensUIController : MonoBehaviour
{
    public GameObject boardObject;
    [SerializeField] private Behaviour constraintManager;
    [SerializeField] private Behaviour objectManipulator;
    [SerializeField] private Behaviour moveAxisConstraint;
    [SerializeField] private Behaviour fixdRotationToUserConstraint;
    [SerializeField] private Behaviour minMaxScaleConstraint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PositionAndScaleToggle()
    {
        constraintManager.enabled = false;
        objectManipulator.enabled = false;
        moveAxisConstraint.enabled = false;
        fixdRotationToUserConstraint.enabled = false;
        minMaxScaleConstraint.enabled = false;
    }
}
