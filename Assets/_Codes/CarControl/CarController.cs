using System;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public enum ControlMode
    {
        Keyboard,
        Buttons
    }

    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public GameObject wheelEffectObj;
        public ParticleSystem smokeParticle;
        public Axel axel;
    }

    public ControlMode control;

    public float maxAcceleration = 80.0f;
    public float brakeAcceleration = 50.0f;
    public float turnSensitivity = 1.5f;
    public float maxSteerAngle = 35.0f;

    public Vector3 _centerOfMass = new Vector3(0, -0.9f, 0); // Lowered center of mass

    public List<Wheel> wheels;

    private float moveInput;
    private float steerInput;
    private float currentAcceleration;
    private float currentSteerAngle;

    private Rigidbody carRb;
    private CarLights carLights;

    void Start()
    {
        carRb = GetComponent<Rigidbody>(); 
        carRb.centerOfMass = _centerOfMass;
        carRb.collisionDetectionMode = CollisionDetectionMode.Continuous; 

        carLights = GetComponent<CarLights>();

        foreach (var wheel in wheels)
        {
            WheelFrictionCurve forwardFriction = wheel.wheelCollider.forwardFriction;
            forwardFriction.extremumSlip = 0.2f; 
            forwardFriction.extremumValue = 2.0f; 
            forwardFriction.asymptoteSlip = 0.5f;
            forwardFriction.asymptoteValue = 1.5f; 
            forwardFriction.stiffness = 3.0f; 
            wheel.wheelCollider.forwardFriction = forwardFriction;

            WheelFrictionCurve sidewaysFriction = wheel.wheelCollider.sidewaysFriction;
            sidewaysFriction.extremumSlip = 0.2f; 
            sidewaysFriction.extremumValue = 2.0f; 
            sidewaysFriction.asymptoteSlip = 0.5f; 
            sidewaysFriction.asymptoteValue = 1.5f;
            sidewaysFriction.stiffness = 3.0f; 
            wheel.wheelCollider.sidewaysFriction = sidewaysFriction;

            JointSpring suspensionSpring = wheel.wheelCollider.suspensionSpring;
            suspensionSpring.spring = 10000f;
            suspensionSpring.damper = 4500f;
            suspensionSpring.targetPosition = 0.5f; 
            wheel.wheelCollider.suspensionSpring = suspensionSpring;

            wheel.wheelCollider.suspensionDistance = 0.4f; 
        }
    }

    void Update()
    {
        GetInputs();
        AnimateWheels();
        WheelEffects();
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    public void MoveInput(float input)
    {
        moveInput = input;
    }

    public void SteerInput(float input)
    {
        steerInput = input;
    }

    void GetInputs()
    {
        if (control == ControlMode.Keyboard)
        {
            moveInput = Input.GetAxis("Vertical");
            steerInput = Input.GetAxis("Horizontal");
        }
    }

    void Move()
    {
        float adjustedAcceleration = maxAcceleration;

        if (Mathf.Abs(carRb.velocity.magnitude) < 10f) 
        {
            adjustedAcceleration *= 1.5f; 
        }

        if (moveInput < 0) 
        {
            adjustedAcceleration *= 0.7f; 
        }

        currentAcceleration = Mathf.Lerp(currentAcceleration, moveInput * adjustedAcceleration * 800, Time.deltaTime * 5);
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = currentAcceleration * Time.deltaTime;
        }
    }

    void Steer()
    {
        currentSteerAngle = Mathf.Lerp(currentSteerAngle, steerInput * turnSensitivity * maxSteerAngle, Time.deltaTime * 5);
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                wheel.wheelCollider.steerAngle = currentSteerAngle;
            }
        }
    }

    void Brake()
    {
        bool isBraking = Input.GetKey(KeyCode.Space) || moveInput == 0;
        float brakeForce = isBraking ? brakeAcceleration * 500 : 0;

        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.brakeTorque = brakeForce * Time.deltaTime;
        }

        carLights.isBackLightOn = isBraking;
        carLights.OperateBackLights();
    }

    void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }

    void WheelEffects()
    {
        foreach (var wheel in wheels)
        {
            var dirtParticleMainSettings = wheel.smokeParticle.main;

            if (Input.GetKey(KeyCode.Space) && wheel.axel == Axel.Rear && wheel.wheelCollider.isGrounded && carRb.velocity.magnitude >= 10.0f)
            {
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
                wheel.smokeParticle.Emit(1);
            }
            else
            {
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
            }
        }
    }
}
