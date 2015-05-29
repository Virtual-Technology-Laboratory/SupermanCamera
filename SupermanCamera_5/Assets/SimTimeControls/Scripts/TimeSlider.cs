/*
 * Copyright (c) 2014, Roger Lew (rogerlew.gmail.com)
 * Date: 2/5/2015
 * License: BSD (3-clause license)
 * 
 * The project described was supported by NSF award number IIA-1301792
 * from the NSF Idaho EPSCoR Program and by the National Science Foundation.
 * 
 */

#define NOTDEBUG

using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

namespace VTL.SimTimeControls
{
	public class TimeSlider : MonoBehaviour
	{
	    public bool IsPlaying = true;
	    public float PlaySpeed = 1f;
	    public string StartTimeStr = "10/01/2010 00:00";
	    public float TimeDeltaHours = 1;
	    

	    // readonly properties
	    private DateTime StartTimeProperty;
	    public DateTime StartTime
	    {
	        get { return StartTimeProperty; }
	        set { }
	    }

	    private DateTime SimTimeProperty;
	    public DateTime SimTime
	    {
	        get { return SimTimeProperty; }
	        set { }
	    }

	    private Int64 SimStepProperty;
	    public Int64 SimStep
	    {
	        get { return SimStepProperty; }
	        set {  }
	    }

	    // private attibutes for tracking simulation time relative to real time
	    private TimeSpan TimeDelta;
	    private bool LastPlayState;
	    private DateTime LastUpdate;

		private Slider slider;

		// Use this for initialization
		void Start () 
		{
			slider = gameObject.GetComponent<Slider>();

	        StartTimeProperty = Convert.ToDateTime(StartTimeStr);
	        TimeDelta = new TimeSpan(0, 0, 0, 0, (int)(TimeDeltaHours*3600000));
			SimTimeProperty = StartTime;
			LastUpdate = DateTime.UtcNow;

	        PlaySpeed = 1f;
		}
		
		// Update is called once per frame
		void Update () {
			if (IsPlaying) {
				if (LastPlayState)
				{
	                slider.value += (float)(DateTime.UtcNow - LastUpdate).TotalSeconds * PlaySpeed;
				}
				LastUpdate = DateTime.UtcNow;
				LastPlayState = true;

	            SimStepProperty = (Int64)Math.Floor(slider.value);
				SimTimeProperty = StartTime + TimeSpan.FromTicks( (Int64)((double)TimeDelta.Ticks * slider.value));
			}
			else 
			{
				LastPlayState = false;
			}

	#if DEBUG
	        Debug.Log(String.Format("{0}, {1}, {2}, {3}, {4}", IsPlaying, Time.time, slider.value, SimStep, SimTime));
	#endif

		}
		
		void TogglePlaying() {
	        IsPlaying = !IsPlaying;
		}
	}
}