using System.IO.Ports;
using System.IO;
using UnityEngine;

public class SerialReader : MonoBehaviour
{
    SerialPort serialPort;
    public string portName = "COM5"; // Change this to your Arduino's port
    public int baudRate = 9600;
    [SerializeField] bool valueChanged = false;
    [SerializeField] int lastButtonState = 1;
    [SerializeField] float changeAmount = 1;

    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open();
    }

    void Update()
    {
        if (serialPort.IsOpen)
        {
            string data = serialPort.ReadLine(); // Read data from the Arduino
            int buttonState = int.Parse(data.Trim()); // Convert the data to an integer
            
            Debug.Log("Button State: " + buttonState);

            // Example: Control a GameObject based on the button state
            if (buttonState != lastButtonState)
            {
                ButtonAction(buttonState);
            }

            lastButtonState = buttonState;
        }
    }

    void ButtonAction(int i)
    {
        if (i == 0)
            transform.position = new Vector3(0, 0, transform.position.z - changeAmount);
        else
            transform.position = new Vector3(0, 0, transform.position.z + changeAmount);
    }

    void OnDestroy()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}