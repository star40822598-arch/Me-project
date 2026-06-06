using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class ArduinoLampController : MonoBehaviour
{
    [Header("Arduino")]
    public string portName = "COM3";
    public int baudRate = 9600;

    [Header("Lights")]
    public Light yellowLamp;
    public Light greenLamp;

    private SerialPort serialPort;
    private Thread serialThread;

    private string receivedData = "";
    private bool running = true;
    private int lastPercent = -1;

    private bool yellowLampState = false;

    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        serialPort.ReadTimeout = 100;

        try
        {
            serialPort.Open();

            serialThread = new Thread(ReadSerial);
            serialThread.Start();
        }
        catch
        {
            Debug.LogError("Serial open failed");
        }
    }

    void ReadSerial()
    {
        while (running)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    receivedData = serialPort.ReadLine();
                }
            }
            catch { }
        }
    }

    void Update()
    {
        if (string.IsNullOrEmpty(receivedData))
            return;

        // 按鈕訊號
        if (receivedData.StartsWith("B:"))
        {
            yellowLampState = !yellowLampState;

            yellowLamp.enabled = yellowLampState;

            Debug.Log(
                "黃燈：" +
                (yellowLampState ? "開" : "關")
            );
        }

        // 可變電阻訊號
        if (receivedData.StartsWith("P:"))
        {
            string valueString = receivedData.Replace("P:", "");

            int potValue;

            if (int.TryParse(valueString, out potValue))
            {
                float intensity =
                    Mathf.Lerp(
                        0f,
                        5f,
                        potValue / 1023f
                    );

                greenLamp.intensity = intensity;

            }

            int percent =Mathf.RoundToInt(potValue / 1023f * 100f);

            if (percent != lastPercent)
            {
                Debug.Log("綠燈亮度：" + percent + "%");
                lastPercent = percent;
            }
        }

        receivedData = "";
    }

    private void OnApplicationQuit()
    {
        running = false;

        if (serialThread != null)
            serialThread.Join();

        if (serialPort != null && serialPort.IsOpen)
            serialPort.Close();
    }
}