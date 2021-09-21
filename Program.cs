using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using SewLib;
using System.IO.Ports;





class Testing
{

    SewDevice myDevice;
    SerialPort comPort;
    public Testing()
    {

        comPort = new SerialPort("COM4", 115200);


        myDevice = new SewDevice(ref comPort);

        myDevice.OnSamplesReceived += new ReceivedSamplesHandler(ReceiveData);

    }

    private void btstart_Click()
    {
        eErrorClassOpcodes reply = myDevice.StartStreaming();
        Console.WriteLine(reply);
        if (reply == eErrorClassOpcodes.Ok)
        {
            // ok device is connected and streaming is started
        }
        else
        {
            // check which error occurred and do something
        }

    }
    //private void btstop_click(object sender, eventargs e)
    private void btstop_Click()
    {
        eErrorClassOpcodes reply = myDevice.StopStreaming();
        if (reply == eErrorClassOpcodes.Ok)
        {

            // ok device is connected and streaming is started
        }
        else
        {
            // check which error occurred and do something
        }

    }

    private void close_port()
    {
        comPort.Close();
    }

    private void open_port()
    {
        comPort.Open();
    }
    // used to receive the data
    public void ReceiveData(object sender, SewDeviceEventArgs e)


    {

        foreach (SewChannelStream channelstream in e.samplesList)
        {
            switch (channelstream.channel)
            {
                case eChannelNumb.AccX:
                    foreach (SewSample sample in channelstream.samples)
                    {

                        Console.WriteLine("AccX is:" + sample.sample);
                    }
                    break;
                case eChannelNumb.MagX:
                    foreach (SewSample sample in channelstream.samples)
                    {
                        Console.WriteLine("MagX is:" + sample.sample);
                    }
                    break;


                //
                // add here all the channel to be monitored
                //
                default:
                    break;
            }
        }
    }

    static void Main(string[] args)
    {

        Testing acquire = new Testing();

        Console.WriteLine("Starting");
        acquire.btstart_Click();




        Console.ReadLine();

        acquire.btstop_Click();
        //acquire.comPort.Close();
        Console.WriteLine("Finished");
    }


}
