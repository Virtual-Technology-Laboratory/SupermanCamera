/*
 * Copyright (c) 2014, Roger Lew (rogerlew.gmail.com)
 * Date: 5/12/2015 - 5/28/2015
 * License: BSD (3-clause license)
 * 
 * The project described was supported by NSF award number IIA-1301792
 * from the NSF Idaho EPSCoR Program and by the National Science Foundation.
 * 
 */

using UnityEngine;
using System.Collections;

namespace VTL.SupermanCamera
{
    public enum ControlMode { Walk, Fly, Fall, TakeOff };

    public class SupermanCamera : MonoBehaviour
    {
        // WASDQE Panning
        public float minPanSpeed = 0.1f;    // Starting panning speed
        public float maxPanSpeed = 1000f;   // Max panning speed
        public float panTimeConstant = 10f; // Time to reach max panning speed

        public float maxWalkPanSpeed = 10f;   // Max panning speed
        public float walkPanTimeConstant = 2f; // Time to reach max panning speed

        // Mouse right-down rotation
        public float rotateSpeed = 10; // mouse down rotation speed about x and y axes
        public float zoomSpeed = 2;    // zoom speed

        public float walkTransitionElevation = 50; // m

        float panT = 0;
        public float panSpeed = 10;

        public Vector3 panTranslation;
        bool wKeyDown = false;
        bool aKeyDown = false;
        bool sKeyDown = false;
        bool dKeyDown = false;
        bool qKeyDown = false;
        bool eKeyDown = false;
        bool spaceKeyDown = false;

        Vector3 lastMousePosition;

        public float altitude = -1;
        public float headingAngle = -1;

        const float RAD2DEG = 180f / Mathf.PI;
        const float GRAVITY = 9.8f;

        public ControlMode controlMode;

        new Camera camera;
        public float fieldOfView;
        new Rigidbody rigidbody;
        new ConstantForce constantForce;

        public float gravity;

        void Start()
        {
            camera = GetComponent<Camera>();
            rigidbody = GetComponent<Rigidbody>();
            constantForce = GetComponent<ConstantForce>();
        }


        void Update()
        {
            //
            // WASDQE Panning

            // read key inputs
            wKeyDown = Input.GetKey(KeyCode.W);
            aKeyDown = Input.GetKey(KeyCode.A);
            sKeyDown = Input.GetKey(KeyCode.S);
            dKeyDown = Input.GetKey(KeyCode.D);
            qKeyDown = Input.GetKey(KeyCode.Q);
            eKeyDown = Input.GetKey(KeyCode.E);
            spaceKeyDown = Input.GetKey(KeyCode.Space);

            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, Vector3.down, out hitInfo))
            {
                altitude = hitInfo.distance;

                if (controlMode == ControlMode.Fly)
                    gravity = 0;
                else if (controlMode == ControlMode.Walk || controlMode == ControlMode.Fall)
                    gravity = GRAVITY;
                else if (controlMode == ControlMode.TakeOff)
                    gravity = Mathf.Lerp(0, GRAVITY, Mathf.Clamp01(1 - altitude / walkTransitionElevation));
            }
            else
            {
                gravity = 0;
            }
            constantForce.force = gravity * Vector3.down;

            if (altitude > walkTransitionElevation)
            {
                controlMode = ControlMode.Fly;
            }
            else if (eKeyDown)
            {
                if (controlMode == ControlMode.Walk)
                {
                    panT = 0;
                    panSpeed = minPanSpeed;

                }
                controlMode = ControlMode.TakeOff;
            }
            else if (altitude > 4f)
                controlMode = ControlMode.Fall;
            else
                controlMode = ControlMode.Walk;


            if (controlMode == ControlMode.Fly ||
                controlMode == ControlMode.TakeOff)
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            else
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation |
                                        RigidbodyConstraints.FreezePositionX |
                                        RigidbodyConstraints.FreezePositionZ;

            headingAngle = Mathf.Atan2(transform.forward.x,
                                       transform.forward.z) * RAD2DEG;

            // determine panTranslation
            panTranslation = Vector3.zero;
            if (dKeyDown && !aKeyDown)
                panTranslation += Vector3.right * Time.deltaTime * panSpeed;
            else if (aKeyDown && !dKeyDown)
                panTranslation += Vector3.left * Time.deltaTime * panSpeed;

            if (wKeyDown && !sKeyDown)
                panTranslation += Vector3.forward * Time.deltaTime * panSpeed;
            else if (sKeyDown && !wKeyDown)
                panTranslation += Vector3.back * Time.deltaTime * panSpeed;


            if (controlMode == ControlMode.Fly || controlMode == ControlMode.Fall)
            {
                if (qKeyDown && !eKeyDown)
                    panTranslation += Vector3.down * Time.deltaTime * panSpeed;
                else if (eKeyDown && !qKeyDown)
                    panTranslation += Vector3.up * Time.deltaTime * panSpeed;
            }
            else
            {
                if (eKeyDown && !qKeyDown)
                    panTranslation += Vector3.up * Mathf.Max(0.8f, Time.deltaTime * panSpeed);

            }

            transform.Translate(panTranslation, Space.Self);

            // Update panSpeed
            if (wKeyDown || aKeyDown || sKeyDown ||
                dKeyDown || qKeyDown || eKeyDown)
            {
                if (!spaceKeyDown)
                {
                    if (controlMode == ControlMode.Fly ||
                         controlMode == ControlMode.Fall ||
                         controlMode == ControlMode.TakeOff)
                    {
                        panT += Time.deltaTime / panTimeConstant;
                        panSpeed = Mathf.Lerp(minPanSpeed, maxPanSpeed, panT * panT);
                    }
                    else
                    {
                        panT += Time.deltaTime / walkPanTimeConstant;
                        panSpeed = Mathf.Lerp(minPanSpeed, maxWalkPanSpeed, panT);
                    }
                }
            }
            else
            {
                panT = 0;
                panSpeed = minPanSpeed;
            }

            //
            // Mouse Rotation
            if (Input.GetMouseButton(1))
            {
                // if the game window is separate from the editor window and the editor 
                // window is active then you go to right-click on the game window the 
                // rotation jumps if  we don't ignore the mouseDelta for that frame.
                Vector3 mouseDelta;
                if (lastMousePosition.x >= 0 &&
                    lastMousePosition.y >= 0 &&
                    lastMousePosition.x <= Screen.width &&
                    lastMousePosition.y <= Screen.height)
                    mouseDelta = Input.mousePosition - lastMousePosition;
                else
                    mouseDelta = Vector3.zero;

                var fovNormalizedRotationSpeed = rotateSpeed / 60f * fieldOfView;

                var rotation = Vector3.up * Time.deltaTime * fovNormalizedRotationSpeed * mouseDelta.x;
                rotation += Vector3.left * Time.deltaTime * fovNormalizedRotationSpeed * mouseDelta.y;
                transform.Rotate(rotation, Space.Self);

                // Make sure z rotation stays locked
                rotation = transform.rotation.eulerAngles;
                rotation.z = 0;
                transform.rotation = Quaternion.Euler(rotation);
            }

            lastMousePosition = Input.mousePosition;

            //
            // Mouse Zoom
            fieldOfView = Mathf.Clamp(camera.fieldOfView - Input.mouseScrollDelta.y * zoomSpeed, 2, 180);
            camera.fieldOfView = fieldOfView;
        }

        public void SetCameraFieldOfView(float newFOV)
        {
            fieldOfView = camera.fieldOfView = newFOV;
        }
    }
}