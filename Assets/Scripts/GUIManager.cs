﻿// <copyright file="GUIManager.cs" company="dyadica.co.uk">
// Copyright (c) 2010, 2014 All Right Reserved, http://www.dyadica.co.uk

// This source is subject to the dyadica.co.uk Permissive License.
// Please see the http://www.dyadica.co.uk/permissive-license file for more information.
// All other rights reserved.

// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// </copyright>

// <author>SJB</author>
// <email>SJB@dyadica.co.uk</email>
// <date>04.09.2013</date>
// <summary>A MonoBehaviour type class containing an example gui that can be used to 
// communicate with the UnitySerialPort.cs script</summary>

using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour 
{
    public static GUIManager Instance;

    private UnitySerialPort unitySerialPort;

    public GUISkin GUISkin;

    public string OutputString;

    private string PortOpenStatus;

    void Awake ()
    {
        Instance = this;
    }

	void Start () 
    {
        // Register reference to the UnitySerialPort. This
        // was defined in the scripts Awake function so we
        // know it is instantiated before this call.

        unitySerialPort = UnitySerialPort.Instance;
	}
	
	void Update () 
    {
        // Check to see if we have a serial port defined
        // and if not then return.

        if (unitySerialPort.SerialPort == null)
        {
            PortOpenStatus = "Open Port"; return;
        }

        // Check to see if the serial port is open or not
        // and then set the button text "PortOpenStatus" 
        // to reflect.

        switch (unitySerialPort.SerialPort.IsOpen)
        {
            case true: PortOpenStatus = "Close Port"; break;
            case false: PortOpenStatus = "Open Port"; break;
        }

	    // Here we have some sample usage scenarios that
        // demo the operation of the UnitySerialPort. In
        // order to use these you must first ensure that
        // the custom inputs are defined via:

        // Edit > Project Settings > Input

        //if (Input.GetButtonDown("SendData"))
        //{ unitySerialPort.SendSerialDataAsLine(OutputString); }
	}

    void OnGUI ()
    {
        // If we have defined a GUI Skin via the unity 
        // editor then apply it.

        if (GUISkin != null) { GUI.skin = GUISkin; }

        // Draw an area to hold the GUI content.

        GUILayout.BeginArea(new Rect(250, 500, 200, 200), "", GUI.skin.box);

        // Draw a button that can be used to open or
        // close the serial port.

       

        // Draw a title for the input textfield

        GUILayout.Label("Com Port");
        //GUILayout.TextField("Port :"+unitySerialPort.ComPort, GUILayout.Height(20));
        unitySerialPort.ComPort = GUILayout.TextField(unitySerialPort.ComPort,25);

       // GUILayout.Space(20);

        GUILayout.Label("Baud Rate");
        GUILayout.TextField(""+unitySerialPort.BaudRate, GUILayout.Height(20));
        

        if (GUILayout.Button(PortOpenStatus, GUILayout.Height(30)))
        {
            if (unitySerialPort.SerialPort == null)
            { unitySerialPort.OpenSerialPort(); return; }

            switch (unitySerialPort.SerialPort.IsOpen)
            {
                case true: unitySerialPort.CloseSerialPort(); break;
                case false: unitySerialPort.OpenSerialPort(); break;
            }
        }
        GUILayout.TextArea("tes :" + unitySerialPort.RawData, GUILayout.Height(60));

        // Draw a title for the output textfield

        //GUILayout.Label("Output string");

        //// Draw a textfield that can be used to define 
        //// data to be sent via the serial port

        //OutputString = GUILayout.TextField(OutputString, GUILayout.Height(20));

        //// Draw a button that can be used to send serial
        //// data from the unity environment.

        //if (GUILayout.Button("Send Data", GUILayout.Height(30)))
        //{
        //    if (unitySerialPort.SerialPort.IsOpen)
        //        unitySerialPort.SendSerialDataAsLine(OutputString);
        //}

        //// Thats it we are finished so lets close the area

        GUILayout.EndArea();
    }
}
