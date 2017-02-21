// Decompiled with JetBrains decompiler
// Type: CSharpRuntimeCameo.MainWindow
// Assembly: CSharpRuntimeCameo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EE9C72-FB3C-47D2-86EA-48DB053A5B3C
// Assembly location: C:\Users\user\Downloads\x86\x86\CSharpRuntimeCameo.exe

using Bosch.VideoSDK.AxCameoLib;
using Bosch.VideoSDK.CameoLib;
using Bosch.VideoSDK.Device;
using Bosch.VideoSDK.GCALib;
using Bosch.VideoSDK.Live;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Web;
using System.Windows.Forms;

namespace CSharpRuntimeCameo
{
  public class MainWindow : Form
  {
    private SettingsWindow settingsWindow = new SettingsWindow();
    private DeviceConnector m_deviceConnector = (DeviceConnector) new DeviceConnectorClass();
    private WebClient webClient = new WebClient();
    private MainWindow.State m_state;
    private DeviceProxy m_deviceProxy;
    private AxCameo m_axCameo;
    private Cameo m_cameo;
    private _IVideoInputVCAEvents_Event m_videoInputVCAEvents;
    private IContainer components;
    private TextBox TextBoxUrl;
    private Button ButtonConnect;
    private Panel PanelCameo;
    private ComboBox ComboBoxProgID;
    private PictureBox pictureBox1;
    private PictureBox pictureBox2;
    private Button button1;
    private Button button2;
    private Label label2;
    private Label label1;

    public MainWindow()
    {
      this.InitializeComponent();
    }

    public Bitmap cropThisRect(Bitmap b, Rectangle r)
    {
      Bitmap bitmap = new Bitmap(r.Width, r.Height);
      Graphics.FromImage((Image) bitmap).DrawImage((Image) b, -r.X, -r.Y);
      return bitmap;
    }

    private void MainWindow_Load(object sender, EventArgs e)
    {
      this.m_axCameo = new AxCameo();
      this.PanelCameo.Controls.Add((Control) this.m_axCameo);
      this.m_axCameo.Dock = DockStyle.Fill;
      this.m_cameo = (Cameo) this.m_axCameo.GetOcx();
      // ISSUE: method pointer
      this.m_deviceConnector.add_ConnectResult(new _IDeviceConnectorEvents_ConnectResultEventHandler((object) this, (UIntPtr) __methodptr(DeviceConnector_ConnectResult)));
      this.ComboBoxProgID.SelectedIndex = 0;
      this.UpdateGUI();
    }

    private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.m_state != MainWindow.State.Connecting && this.m_state != MainWindow.State.Disconnecting)
        return;
      e.Cancel = true;
    }

    private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
      // ISSUE: method pointer
      this.m_deviceConnector.remove_ConnectResult(new _IDeviceConnectorEvents_ConnectResultEventHandler((object) this, (UIntPtr) __methodptr(DeviceConnector_ConnectResult)));
      if (this.m_state == MainWindow.State.Connected)
      {
        // ISSUE: method pointer
        this.m_deviceProxy.remove_ConnectionStateChanged(new _IDeviceProxyEvents_ConnectionStateChangedEventHandler((object) this, (UIntPtr) __methodptr(DeviceProxy_ConnectionStateChanged)));
        this.m_deviceProxy.Disconnect();
      }
      this.m_axCameo.Dispose();
    }

    private void ButtonConnect_Click(object sender, EventArgs e)
    {
      if (this.m_state == MainWindow.State.Disconnected)
      {
        try
        {
          this.m_state = MainWindow.State.Connecting;
          this.m_deviceConnector.ConnectAsync(this.TextBoxUrl.Text, this.ComboBoxProgID.Text);
        }
        catch (Exception ex)
        {
          if (-1 != (int) this.CheckException(ex, "Failed to start asynchronous connection attempt to \"{0}\"", (object) this.TextBoxUrl.Text))
          {
            int num = (int) MessageBox.Show("Invalid IP address or progID! \n\nIP address:  " + this.TextBoxUrl.Text + "\nProgID:\t  " + this.ComboBoxProgID.Text, "Invalid Argument");
          }
          this.m_state = MainWindow.State.Disconnected;
        }
      }
      else if (this.m_state == MainWindow.State.Connected)
      {
        this.m_state = MainWindow.State.Disconnecting;
        this.m_deviceProxy.Disconnect();
      }
      this.UpdateGUI();
    }

    private void DeviceConnector_ConnectResult(ConnectResultEnum connectResult, string url, DeviceProxy deviceProxy)
    {
      bool flag = false;
      if (connectResult == ConnectResultEnum.creInitialized && deviceProxy.VideoInputs.Count > 0)
      {
        flag = true;
        try
        {
          this.m_cameo.SetVideoStream((object) deviceProxy.VideoInputs[(object) 1].Stream);
          this.m_videoInputVCAEvents = deviceProxy.VideoInputs[(object) 1] as _IVideoInputVCAEvents_Event;
          if (this.m_videoInputVCAEvents != null)
          {
            // ISSUE: method pointer
            this.m_videoInputVCAEvents.add_MotionDetectorsStateChanged(new _IVideoInputVCAEvents_MotionDetectorsStateChangedEventHandler((object) this, (UIntPtr) __methodptr(VideoInputVCAEvents_MotionDetectorsStateChanged)));
          }
        }
        catch (Exception ex)
        {
          int num = (int) this.CheckException(ex, "Failed to render first video stream of {0}", (object) url);
          flag = false;
        }
      }
      if (flag)
      {
        this.m_deviceProxy = deviceProxy;
        // ISSUE: method pointer
        this.m_deviceProxy.add_ConnectionStateChanged(new _IDeviceProxyEvents_ConnectionStateChangedEventHandler((object) this, (UIntPtr) __methodptr(DeviceProxy_ConnectionStateChanged)));
        this.m_state = MainWindow.State.Connected;
      }
      else
      {
        if (deviceProxy != null)
          deviceProxy.Disconnect();
        this.m_state = MainWindow.State.Disconnected;
        int num = (int) MessageBox.Show("Failed to connect to \"" + url + "\".");
      }
      this.UpdateGUI();
    }

    private void VideoInputVCAEvents_MotionDetectorsStateChanged(VideoInput pEventSource, int ConfigId, int DetectorsState)
    {
      if (DetectorsState == 0)
        return;
      string file1 = this.settingsWindow.pathName + "\\BoschMS_" + DateTime.Now.ToString("dd_MM_yyyy-HH-mm-ss");
      if (Directory.Exists(this.settingsWindow.pathName))
      {
        if (this.settingsWindow.receiveAlarm1 || this.settingsWindow.receiveAlarm2 || (this.settingsWindow.receiveAlarm3 || this.settingsWindow.receiveAlarm4))
        {
          this.webClient.DownloadFile("http://" + this.TextBoxUrl.Text + "/snap.jpg", file1 + ".jpg");
          this.label1.Text = "Analyzing...";
        }
        if (DetectorsState % 2 > 0 && DetectorsState % 2 < 2 && this.settingsWindow.receiveAlarm1)
        {
          if (this.settingsWindow.rect1.Width > 0)
          {
            this.cropThisRect(new Bitmap(file1 + ".jpg"), this.settingsWindow.rect1).Save(file1 + "_crop.jpg");
            file1 += "_crop.jpg";
          }
          else
            file1 += ".jpg";
          if (this.settingsWindow.selectedAnalysis1 == "analysis")
            this.MakeAnalysisRequest(file1);
          if (this.settingsWindow.selectedAnalysis1 == "emotion")
            this.MakeEmotionRequest(file1);
        }
        if (DetectorsState % 4 > 1 && DetectorsState % 4 < 4 && this.settingsWindow.receiveAlarm2)
        {
          if (this.settingsWindow.rect2.Width > 0)
          {
            this.cropThisRect(new Bitmap(file1 + ".jpg"), this.settingsWindow.rect2).Save(file1 + "_crop.jpg");
            file1 += "_crop.jpg";
          }
          else
            file1 += ".jpg";
          if (this.settingsWindow.selectedAnalysis2 == "analysis")
            this.MakeAnalysisRequest(file1);
          if (this.settingsWindow.selectedAnalysis2 == "emotion")
            this.MakeEmotionRequest(file1);
        }
        if (DetectorsState % 8 > 3 && DetectorsState % 8 < 8 && this.settingsWindow.receiveAlarm3)
        {
          if (this.settingsWindow.rect3.Width > 0)
          {
            this.cropThisRect(new Bitmap(file1 + ".jpg"), this.settingsWindow.rect3).Save(file1 + "_crop.jpg");
            file1 += "_crop.jpg";
          }
          else
            file1 += ".jpg";
          if (this.settingsWindow.selectedAnalysis3 == "analysis")
            this.MakeAnalysisRequest(file1);
          if (this.settingsWindow.selectedAnalysis3 == "emotion")
            this.MakeEmotionRequest(file1);
        }
        if (DetectorsState <= 7 || DetectorsState >= 16 || !this.settingsWindow.receiveAlarm4)
          return;
        string file2;
        if (this.settingsWindow.rect4.Width > 0)
        {
          this.cropThisRect(new Bitmap(file1 + ".jpg"), this.settingsWindow.rect4).Save(file1 + "_crop.jpg");
          file2 = file1 + "_crop.jpg";
        }
        else
          file2 = file1 + ".jpg";
        if (this.settingsWindow.selectedAnalysis4 == "analysis")
          this.MakeAnalysisRequest(file2);
        if (!(this.settingsWindow.selectedAnalysis4 == "emotion"))
          return;
        this.MakeEmotionRequest(file2);
      }
      else
        this.label1.Text = "Pathname provided in Settings does not exist. Please provide a valid path.";
    }

    private async void MakeEmotionRequest(string file)
    {
      HttpClient client = new HttpClient();
      NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
      client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "c8cc8078277649beb8b267cca7683d3d");
      queryString["visualFeatures"] = "Categories";
      string uri = "https://api.projectoxford.ai/emotion/v1.0/recognize";
      byte[] byteData = System.IO.File.ReadAllBytes(file);
      HttpResponseMessage response;
      using (ByteArrayContent byteArrayContent = new ByteArrayContent(byteData))
      {
        byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        response = (HttpResponseMessage) await client.PostAsync(uri, (HttpContent) byteArrayContent);
      }
      List<MainWindow.EmotionFace> emotionfaceList = JsonConvert.DeserializeObject<List<MainWindow.EmotionFace>>(response.Content.ReadAsStringAsync().get_Result());
      if (emotionfaceList != null)
      {
        this.label1.Text = "";
        for (int index = 0; index < emotionfaceList.Count; ++index)
        {
          Label label1_1 = this.label1;
          string str1 = label1_1.Text + "Person " + (object) (index + 1) + " is feeling... \r\n";
          label1_1.Text = str1;
          Label label1_2 = this.label1;
          string str2 = label1_2.Text + string.Format("{0:P1}", (object) emotionfaceList[index].scores.happiness) + " happiness\r\n";
          label1_2.Text = str2;
          Label label1_3 = this.label1;
          string str3 = label1_3.Text + string.Format("{0:P1}", (object) emotionfaceList[index].scores.sadness) + " sadness\r\n";
          label1_3.Text = str3;
          Label label1_4 = this.label1;
          string str4 = label1_4.Text + string.Format("{0:P1}", (object) emotionfaceList[index].scores.neutral) + " neutral\r\n";
          label1_4.Text = str4;
          Label label1_5 = this.label1;
          string str5 = label1_5.Text + string.Format("{0:P1}", (object) emotionfaceList[index].scores.surprise) + " surprise\r\n";
          label1_5.Text = str5;
          Label label1_6 = this.label1;
          string str6 = label1_6.Text + string.Format("{0:P1}", (object) emotionfaceList[index].scores.anger) + " anger\r\n";
          label1_6.Text = str6;
          Label label1_7 = this.label1;
          string str7 = label1_7.Text + string.Format("{0:P1}", (object) emotionfaceList[index].scores.contempt) + " contempt\r\n";
          label1_7.Text = str7;
          Label label1_8 = this.label1;
          string str8 = label1_8.Text + string.Format("{0:P1}", (object) emotionfaceList[index].scores.fear) + " fear\r\n\r\n";
          label1_8.Text = str8;
        }
      }
      else
        this.label1.Text += "No faces detected.";
      this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox2.Image = (Image) new Bitmap(file);
    }

    private async void MakeAnalysisRequest(string file)
    {
      HttpClient client = new HttpClient();
      NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
      client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "292efa769e804678853998b841646150");
      queryString["visualFeatures"] = "Tags,Description,Faces";
      string uri = "https://api.projectoxford.ai/vision/v1.0/analyze?" + (object) queryString;
      byte[] byteData = System.IO.File.ReadAllBytes(file);
      HttpResponseMessage response;
      using (ByteArrayContent byteArrayContent = new ByteArrayContent(byteData))
      {
        byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        response = (HttpResponseMessage) await client.PostAsync(uri, (HttpContent) byteArrayContent);
      }
      Console.WriteLine(response.Content.ReadAsStringAsync().get_Result());
      MainWindow.AnalysisResponse analysisResponse = JsonConvert.DeserializeObject<MainWindow.AnalysisResponse>(response.Content.ReadAsStringAsync().get_Result());
      if (analysisResponse.tags != null)
      {
        this.label1.Text = "Here's what I see: \r\n";
        for (int index = 0; index < analysisResponse.tags.Count; ++index)
        {
          Label label1 = this.label1;
          string str = label1.Text + (object) (index + 1) + ". " + analysisResponse.tags[index].name + " [" + string.Format("{0:P1}", (object) analysisResponse.tags[index].confidence) + "]\r\n";
          label1.Text = str;
        }
      }
      if (analysisResponse.description != null)
      {
        this.label1.Text += "\r\n";
        for (int index = 0; index < analysisResponse.description.captions.Count; ++index)
        {
          Label label1 = this.label1;
          string str = label1.Text + "I think this is... " + analysisResponse.description.captions[index].text + ". [" + string.Format("{0:P1}", (object) analysisResponse.description.captions[index].confidence) + "]\r\n";
          label1.Text = str;
        }
      }
      if (analysisResponse.faces != null)
      {
        this.label1.Text += "\r\n";
        for (int index = 0; index < analysisResponse.faces.Count; ++index)
        {
          Label label1 = this.label1;
          string str = label1.Text + "People in this photo: " + (object) analysisResponse.faces[index].age + " year old " + analysisResponse.faces[index].gender;
          label1.Text = str;
        }
      }
      this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox2.Image = (Image) new Bitmap(file);
    }

    private void DeviceProxy_ConnectionStateChanged(object eventSource, ConnectResultEnum state)
    {
      if (state != ConnectResultEnum.creConnectionTerminated)
        return;
      this.m_cameo.SetVideoStream((object) null);
      // ISSUE: method pointer
      this.m_deviceProxy.remove_ConnectionStateChanged(new _IDeviceProxyEvents_ConnectionStateChangedEventHandler((object) this, (UIntPtr) __methodptr(DeviceProxy_ConnectionStateChanged)));
      this.m_deviceProxy = (DeviceProxy) null;
      this.m_state = MainWindow.State.Disconnected;
      this.UpdateGUI();
    }

    private void TextBoxUrl_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Return || !this.ButtonConnect.Enabled)
        return;
      this.ButtonConnect_Click((object) this, EventArgs.Empty);
    }

    private void TextBoxUrl_TextChanged(object sender, EventArgs e)
    {
      this.UpdateGUI();
    }

    private void UpdateGUI()
    {
      if (this.m_state == MainWindow.State.Disconnected)
      {
        this.ButtonConnect.Text = "Connect";
        this.ButtonConnect.Enabled = this.TextBoxUrl.Text.Length > 0;
      }
      else if (this.m_state == MainWindow.State.Connecting)
      {
        this.ButtonConnect.Text = "Connecting";
        this.ButtonConnect.Enabled = false;
      }
      else if (this.m_state == MainWindow.State.Connected)
      {
        this.ButtonConnect.Text = "Disconnect";
        this.ButtonConnect.Enabled = true;
      }
      else
      {
        this.ButtonConnect.Text = "Disconnecting";
        this.ButtonConnect.Enabled = false;
      }
    }

    private MainWindow.HRESULT CheckException(Exception ex, string format, params object[] args)
    {
      string str = string.Format(format, args) + ": " + ex.Message;
      if (System.Type.op_Equality(ex.GetType(), typeof (COMException)))
      {
        switch ((uint) ((ExternalException) ex).ErrorCode)
        {
          case 2147500037:
            return MainWindow.HRESULT.E_FAIL;
          case 2147549183:
            return MainWindow.HRESULT.E_UNEXPECTED;
        }
      }
      else
      {
        if (System.Type.op_Equality(ex.GetType(), typeof (NotImplementedException)))
          return MainWindow.HRESULT.E_NOTIMPL;
        if (System.Type.op_Equality(ex.GetType(), typeof (ArgumentException)))
          return MainWindow.HRESULT.E_INVALIDARG;
      }
      if (MessageBox.Show(str + "\n\nTerminate application?", "Unexpected Exception", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
      {
        Process.GetCurrentProcess().Kill();
        throw ex;
      }
      return MainWindow.HRESULT.IgnoreAndFixLater;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.settingsWindow.Show();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.MakeEmotionRequest("C:\\Users\\HJE1SGP\\Desktop\\photos\\now0.jpg");
    }

    private void label2_Click(object sender, EventArgs e)
    {
    }

    private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
    }

    private void toolStripStatusLabel2_Click(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.TextBoxUrl = new TextBox();
      this.ButtonConnect = new Button();
      this.PanelCameo = new Panel();
      this.pictureBox1 = new PictureBox();
      this.ComboBoxProgID = new ComboBox();
      this.pictureBox2 = new PictureBox();
      this.button1 = new Button();
      this.button2 = new Button();
      this.label2 = new Label();
      this.label1 = new Label();
      this.PanelCameo.SuspendLayout();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      this.SuspendLayout();
      this.TextBoxUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.TextBoxUrl.Location = new Point(12, 12);
      this.TextBoxUrl.Name = "TextBoxUrl";
      this.TextBoxUrl.Size = new Size(665, 20);
      this.TextBoxUrl.TabIndex = 0;
      this.TextBoxUrl.TextChanged += new EventHandler(this.TextBoxUrl_TextChanged);
      this.TextBoxUrl.KeyDown += new KeyEventHandler(this.TextBoxUrl_KeyDown);
      this.ButtonConnect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.ButtonConnect.Location = new Point(839, 9);
      this.ButtonConnect.Name = "ButtonConnect";
      this.ButtonConnect.Size = new Size(75, 23);
      this.ButtonConnect.TabIndex = 1;
      this.ButtonConnect.Text = "Connect";
      this.ButtonConnect.UseVisualStyleBackColor = true;
      this.ButtonConnect.Click += new EventHandler(this.ButtonConnect_Click);
      this.PanelCameo.Controls.Add((Control) this.pictureBox1);
      this.PanelCameo.Location = new Point(12, 38);
      this.PanelCameo.Name = "PanelCameo";
      this.PanelCameo.Size = new Size(263, 148);
      this.PanelCameo.TabIndex = 2;
      this.pictureBox1.Location = new Point(729, 203);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(100, 50);
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.ComboBoxProgID.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.ComboBoxProgID.FormattingEnabled = true;
      this.ComboBoxProgID.Items.AddRange(new object[7]
      {
        (object) "GCA.VIP.DeviceProxy",
        (object) "GCA.ONVIF.DeviceProxy",
        (object) "GCA.RTSP.DeviceProxy",
        (object) "GCA.File.DeviceProxy",
        (object) "GCA.Divar600.DeviceProxy",
        (object) "GCA.Divar700.DeviceProxy",
        (object) "GCA.DiBos.DeviceProxy"
      });
      this.ComboBoxProgID.Location = new Point(683, 12);
      this.ComboBoxProgID.Name = "ComboBoxProgID";
      this.ComboBoxProgID.Size = new Size(150, 21);
      this.ComboBoxProgID.TabIndex = 3;
      this.pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pictureBox2.Location = new Point(12, 192);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new Size(729, 423);
      this.pictureBox2.TabIndex = 5;
      this.pictureBox2.TabStop = false;
      this.button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.button1.Location = new Point(920, 9);
      this.button1.Name = "button1";
      this.button1.Size = new Size(74, 23);
      this.button1.TabIndex = 6;
      this.button1.Text = "Settings";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.button2.Location = new Point(707, 141);
      this.button2.Name = "button2";
      this.button2.Size = new Size(86, 28);
      this.button2.TabIndex = 7;
      this.button2.Text = "button2";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.label2.Font = new Font("Segoe UI", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label2.Location = new Point(311, 99);
      this.label2.Name = "label2";
      this.label2.Size = new Size(656, 30);
      this.label2.TabIndex = 8;
      this.label2.Text = "Bosch integrated with Microsoft Cognitive Services";
      this.label2.TextAlign = ContentAlignment.TopCenter;
      this.label2.Click += new EventHandler(this.label2_Click);
      this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.label1.Font = new Font("Segoe UI", 14f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1.Location = new Point(770, 192);
      this.label1.Name = "label1";
      this.label1.Size = new Size(224, 423);
      this.label1.TabIndex = 9;
      this.label1.Text = "1. Connect to a device\r\n2. Make sure device is sending IVA alarms\r\n3. Check settings to receive IVA alarms";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(1000, 627);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.pictureBox2);
      this.Controls.Add((Control) this.ComboBoxProgID);
      this.Controls.Add((Control) this.PanelCameo);
      this.Controls.Add((Control) this.ButtonConnect);
      this.Controls.Add((Control) this.TextBoxUrl);
      this.Name = "MainWindow";
      this.Text = "Bosch Microsoft Analytic Sample";
      this.FormClosing += new FormClosingEventHandler(this.MainWindow_FormClosing);
      this.FormClosed += new FormClosedEventHandler(this.MainWindow_FormClosed);
      this.Load += new EventHandler(this.MainWindow_Load);
      this.PanelCameo.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      ((ISupportInitialize) this.pictureBox2).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

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

    public class AnalysisResponse
    {
      public List<MainWindow.AnalysisResponse.Tags> tags;
      public MainWindow.AnalysisResponse.Description description;
      public List<MainWindow.AnalysisResponse.Face> faces;

      public class Description
      {
        public string[] tags;
        public List<MainWindow.AnalysisResponse.Description.Captions> captions;

        public class Captions
        {
          public string text;
          public float confidence;
        }
      }

      public class Tags
      {
        public string name;
        public float confidence;
      }

      public class Face
      {
        public int age;
        public string gender;
        public MainWindow.FaceRectangle faceRectangle;
      }
    }

    public class FaceRectangle
    {
      public int left;
      public int top;
      public int width;
      public int height;
    }

    public class EmotionFace
    {
      public MainWindow.FaceRectangle faceRectangle;
      public MainWindow.EmotionFace.Scores scores;

      public class Scores
      {
        public float anger;
        public float contempt;
        public float disgust;
        public float fear;
        public float happiness;
        public float neutral;
        public float sadness;
        public float surprise;
      }
    }
  }
}
