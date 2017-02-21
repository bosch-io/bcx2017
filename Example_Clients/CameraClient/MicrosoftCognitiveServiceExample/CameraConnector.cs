using Bosch.VideoSDK.AxCameoLib;
using Bosch.VideoSDK.CameoLib;
using Bosch.VideoSDK.Device;
using Bosch.VideoSDK.GCALib;
using Bosch.VideoSDK.Live;
using System;
using System.Windows.Forms;

namespace MicrosoftCognitiveServiceExample
{
    public class CameraConnector
    {
        private enum HRESULT : uint
        {
            S_OK = 0,
            E_NOTIMPL = 2147500033,
            E_FAIL = 2147500037,
            E_UNEXPECTED = 2147549183,
            E_INVALIDARG = 2147942487,
            IgnoreAndFixLater = 4294967295,
        }

        private enum State
        {
            Disconnected,
            Connecting,
            Connected,
            Disconnecting,
        }

        private DeviceConnector DeviceConnector = (DeviceConnector) new DeviceConnectorClass();

        private DeviceProxy DeviceProxy;

        private State ConnectionState;

        private AxCameo AxCameo;

        private Cameo Cameo;

        private _IVideoInputVCAEvents_Event VideoInputVCAEvents;

        private Button ConnectButton;

        private Panel CameoPanel;

        public CameraConnector (Button connectButton, Panel cameoPanel)
        {
            ConnectButton = connectButton;
            CameoPanel = cameoPanel;
        }



        public void ConnectToCamera(string ip, string CameraDriver)
        {
            AxCameo = new AxCameo();
            CameoPanel.Controls.Add(AxCameo);
            AxCameo.Dock = DockStyle.Fill;
            Cameo = (Cameo)AxCameo.GetOcx();
            AxCameo.VcaConfig.DisplayMode = VcaDisplayModes.vcmRenderVCD;
            DeviceConnector.ConnectResult += new Bosch.VideoSDK.GCALib._IDeviceConnectorEvents_ConnectResultEventHandler(DeviceConnector_ConnectResult);

            if (ConnectionState == State.Disconnected)
            {
                try
                {
                    ConnectionState = State.Connecting;
                    DeviceConnector.ConnectAsync(ip, CameraDriver);
                }
                catch (Exception ex)
                {
                    if (-1 != (int)CheckException(ex, "Failed to start asynchronous connection attempt to \"{0}\"", ip))
                    {
                        int num = (int)MessageBox.Show("Invalid IP address or progID! \n\nIP address:  " + ip + "\nProgID:\t  " + CameraDriver, "Invalid Argument");
                    }
                    ConnectionState = State.Disconnected;
                }
            }
            else if (ConnectionState == State.Connected)
            {
                ConnectionState = State.Disconnecting;
                DeviceProxy.Disconnect();
            }
            UpdateButtons();

        }

        private void DeviceProxy_ConnectionStateChanged(object eventSource, ConnectResultEnum state)
        {
            if (state == Bosch.VideoSDK.Device.ConnectResultEnum.creConnectionTerminated)
            {
                Cameo.SetVideoStream(null);
                DeviceProxy.ConnectionStateChanged -= new Bosch.VideoSDK.GCALib._IDeviceProxyEvents_ConnectionStateChangedEventHandler(DeviceProxy_ConnectionStateChanged);
                DeviceProxy = null;
                ConnectionState = State.Disconnected;                
            }
        }

        private void DeviceConnector_ConnectResult(ConnectResultEnum connectResult, string url, DeviceProxy deviceProxy)
        {
            bool flag = false;
            if (connectResult == ConnectResultEnum.creInitialized && deviceProxy.VideoInputs.Count > 0)
            {
                flag = true;
                try
                {
                    Cameo.SetVideoStream(deviceProxy.VideoInputs[1].Stream);
                    VideoInputVCAEvents = deviceProxy.VideoInputs[1] as _IVideoInputVCAEvents_Event;
                    AxCameo.VcaConfig.DisplayMode = VcaDisplayModes.vcmRenderVCD;
                    if (VideoInputVCAEvents != null)
                    {                        
                        VideoInputVCAEvents.MotionDetectorsStateChanged += new _IVideoInputVCAEvents_MotionDetectorsStateChangedEventHandler(VideoInputVCAEvents_MotionDetectorsStateChanged);                        
                    }
                }
                catch (Exception ex)
                {
                    int num = (int)this.CheckException(ex, "Failed to render first video stream of {0}", (object)url);
                    flag = false;
                }
            }
            if (flag)
            {
                DeviceProxy = deviceProxy;
                DeviceProxy.ConnectionStateChanged += new _IDeviceProxyEvents_ConnectionStateChangedEventHandler(DeviceProxy_ConnectionStateChanged);                
                ConnectionState = State.Connected;                
            }
            else
            {
                if (deviceProxy != null)
                {
                    deviceProxy.Disconnect();
                    ConnectionState = State.Disconnected;
                    int num = (int)MessageBox.Show("Failed to connect to \"" + url + "\".");
                }
            }
            UpdateButtons();

        }

        private void VideoInputVCAEvents_MotionDetectorsStateChanged(VideoInput pEventSource, int ConfigId, int DetectorsState)
        {
            // Motion detected
            if (UserInterface.AutomaticActive)
            {
                string PictureFileName = UserInterface.PICTURE_PATH + "\\SnapShot_" + DateTime.Now.ToString("dd_MM_yyyy-HH-mm-ss") + ".jpg";
                string DownloadURL = "http://" + UserInterface.CameraUrl.Text + "/snap.jpg";
                UserInterface.MicrosoftCognitiveServiceRequests.Analyze(PictureFileName, DownloadURL, UserInterface.SubsciptionKey.Text);
            }
            
        }

        private HRESULT CheckException(Exception ex, string format, params object[] args)
        {
            string message = string.Format(format, args) + ": " + ex.Message;
            if (ex.GetType() == typeof(System.Runtime.InteropServices.COMException))
            {
                uint errorCode = (uint)((System.Runtime.InteropServices.COMException)ex).ErrorCode;
                if (errorCode == (uint)HRESULT.E_FAIL)
                    return HRESULT.E_FAIL;
                else if (errorCode == (uint)HRESULT.E_UNEXPECTED)
                    return HRESULT.E_UNEXPECTED;
            }
            else if (ex.GetType() == typeof(System.NotImplementedException))
                return HRESULT.E_NOTIMPL;
            else if (ex.GetType() == typeof(System.ArgumentException))
                return HRESULT.E_INVALIDARG;

            if (MessageBox.Show(message + "\n\nTerminate application?", "Unexpected Exception", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                throw ex;
            }
            else
                return HRESULT.IgnoreAndFixLater;
        }

        public void Disconnect()
        {
            // disconnect from camera            
            DeviceConnector.ConnectResult -= new Bosch.VideoSDK.GCALib._IDeviceConnectorEvents_ConnectResultEventHandler(DeviceConnector_ConnectResult);
            if (ConnectionState == State.Connected)
            {               
                DeviceProxy.ConnectionStateChanged -= new Bosch.VideoSDK.GCALib._IDeviceProxyEvents_ConnectionStateChangedEventHandler(DeviceProxy_ConnectionStateChanged);
                DeviceProxy.Disconnect();
                ConnectionState = State.Disconnected;
            }
            
            AxCameo.Dispose();
            
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            if (ConnectionState == State.Disconnected)
            {
                ConnectButton.Text = "Connect";
                ConnectButton.Enabled = true;
                UserInterface.AutomaticButton.Enabled = false;
                UserInterface.AnalyzeButton.Enabled = false;
            }
            else if (ConnectionState == State.Connecting)
            {
                ConnectButton.Text = "Connecting";
                ConnectButton.Enabled = false;
            }
            else if (ConnectionState == State.Connected)
            {
                ConnectButton.Text = "Disconnect";
                ConnectButton.Enabled = true;
                UserInterface.AutomaticButton.Enabled = true;
                if (UserInterface.AutomaticActive)
                {
                    UserInterface.AnalyzeButton.Enabled = false;
                }
                else
                {
                    UserInterface.AnalyzeButton.Enabled = true;
                }
                
            }
            else
            {
                ConnectButton.Text = "Disconnecting";
                ConnectButton.Enabled = false;
            }
        }
    }
}
