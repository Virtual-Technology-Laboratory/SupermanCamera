/*
 * Copyright (c) 2014, Roger Lew (rogerlew.gmail.com)
 * Date: 5/28/2015
 * License: BSD (3-clause license)
 * 
 * The project described was supported by NSF award number IIA-1301792
 * from the NSF Idaho EPSCoR Program and by the National Science Foundation.
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace VTL.SupermanCamera
{
    public class SupermanCameraHUD : MonoBehaviour
    {
        public SupermanCamera supermanCam;

        Image Compass;
        Slider ZoomSlider;
        Text FOV;
        Text Altitude;

        GameObject Walking;
        GameObject Flying;

        void Start()
        {
            Compass = transform.Find("Compass").GetComponent<Image>();
            FOV = transform.Find("FOV").GetComponent<Text>();
            Altitude = transform.Find("Altitude").GetComponent<Text>();
            ZoomSlider = transform.Find("ZoomSlider").GetComponent<Slider>();
            Walking = transform.Find("Walking").gameObject;
            Flying = transform.Find("Flying").gameObject;
        }

        void Update()
        {
            Compass.transform.rotation = Quaternion.Euler(new Vector3(0, 0, supermanCam.headingAngle));
            ZoomSlider.value = 178 - (supermanCam.fieldOfView - 2);
            FOV.text = supermanCam.fieldOfView.ToString("0.0\u00B0");
            Altitude.text = supermanCam.altitude.ToString("0.0 m");


            if (supermanCam.controlMode == ControlMode.Fall ||
                supermanCam.controlMode == ControlMode.Walk)
            {
                Walking.SetActive(true);
                Flying.SetActive(false);
            }
            else
            {
                Walking.SetActive(false);
                Flying.SetActive(true);
            }
        }

        public void OnZoomSliderChange()
        {
            supermanCam.SetCameraFieldOfView(178 - ZoomSlider.value + 2);

        }
    }
}