using System.Drawing;
using System.Windows.Forms;
using System;
using System.IO;

namespace MicrosoftCognitiveServiceExample
{
    public class UserInterface : Form
    {
        public static string DEFAULT_IP;
        public static string SUBSCRIPTION_KEY_FOR_EMOTION;
        public static string SUBSCRIPTION_KEY_FOR_VISION;
        public static string PICTURE_PATH;

        public static MicrosoftCognitiveServiceRequests MicrosoftCognitiveServiceRequests = new MicrosoftCognitiveServiceRequests();        
        public static Button AnalyzeButton;
        private Button ConnectButton;
        public static Button AutomaticButton;
        public static bool AutomaticActive = false;
        public static TextBox CameraUrl;
        private ComboBox CameraDriverComboBox;
        public static TextBox SubsciptionKey;
        public static Panel CameoPanel;
        public static PictureBox CameraSnapshot;
        public static Label ResultsOfAnalysis;
        private CameraConnector CameraConnector;        

        public UserInterface()
        {
            string ConfigFileName = "..\\..\\..\\Resources\\MSCS.cfg";
            try
            {                
                Configuration.ReadConfig(ConfigFileName);
            }
            catch (Exception ex)
            {
               Console.Out.WriteLine(ex.StackTrace);

               Console.Out.WriteLine("Configfile " + ConfigFileName + " not found");
                
               Console.Out.WriteLine("Please define a configfile named MSCS.cfg to prefill Camera IP and Subscription Key");
            }

            this.InitializeGUIComponents();

            CameraConnector = new CameraConnector(ConnectButton, CameoPanel);
        }

        private void InitializeGUIComponents()
        {
            // 
            // URL Label
            // 
            Label URLLabel = new System.Windows.Forms.Label();
            URLLabel.Text = "Camera IP : ";
            URLLabel.Size = new System.Drawing.Size(150, 20);
            URLLabel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            URLLabel.Location = new System.Drawing.Point(0, 0);
            URLLabel.Name = "Camera URL Label";

            // 
            // URL Textbox
            // 
            CameraUrl = new System.Windows.Forms.TextBox();
            CameraUrl.Text = DEFAULT_IP;
            CameraUrl.Size = new System.Drawing.Size(150, 20);
            CameraUrl.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            CameraUrl.Location = new System.Drawing.Point(150, 0);
            CameraUrl.Name = "Camera URL Textbox";

            // 
            // Camera Driver Label
            // 
            Label CameraDriverLabel = new System.Windows.Forms.Label();
            CameraDriverLabel.Text = "Camera Driver : ";
            CameraDriverLabel.Size = new System.Drawing.Size(150, 30);
            CameraDriverLabel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            CameraDriverLabel.Location = new System.Drawing.Point(450, 0);
            CameraDriverLabel.Name = "Camera Driver Label";

            // 
            // ComboBoxProgID
            // 
            CameraDriverComboBox = new System.Windows.Forms.ComboBox();
            CameraDriverComboBox.Name = "Camera Driver";
            CameraDriverComboBox.Items.AddRange(new object[] {
            "GCA.VIP.DeviceProxy",
            "GCA.ONVIF.DeviceProxy",
            "GCA.RTSP.DeviceProxy"});
            CameraDriverComboBox.Location = new System.Drawing.Point(600, 0);
            CameraDriverComboBox.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            CameraDriverComboBox.Size = new System.Drawing.Size(250, 30);
            CameraDriverComboBox.SelectedIndex = 0;


            // 
            // Subscribtion Key Label
            // 
            Label SubsciptionKeyLabel = new System.Windows.Forms.Label();
            SubsciptionKeyLabel.Text = "Subscribtion Key : ";
            SubsciptionKeyLabel.Size = new System.Drawing.Size(200, 20);
            SubsciptionKeyLabel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            SubsciptionKeyLabel.Location = new System.Drawing.Point(1250, 0);
            SubsciptionKeyLabel.Name = "Subscribtion Key Label";

            // 
            // Subscribtion Key Textbox
            // 
            SubsciptionKey = new System.Windows.Forms.TextBox();
            SubsciptionKey.Text = SUBSCRIPTION_KEY_FOR_EMOTION;
            SubsciptionKey.Size = new System.Drawing.Size(400, 20);
            SubsciptionKey.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            SubsciptionKey.Location = new System.Drawing.Point(1450, 0);
            SubsciptionKey.Name = "Subscribtion Key Textbox";

            // 
            // CameraSnapshotBox
            //         
            CameraSnapshot = new System.Windows.Forms.PictureBox();

            // Construct an image object from a file in the local directory.
            // ... This file must exist in the solution.
            string ImageFileName = "..\\..\\..\\Resources\\SmartEdgeDevices.png"; 
            Image image = new Bitmap(Image.FromFile(ImageFileName), new Size(800, 450));
            // Set the PictureBox image property to this image.
            // ... Then, adjust its height and width properties.
            if (image != null)
            {
                CameraSnapshot.Image = image;
                CameraSnapshot.Location = new System.Drawing.Point(800, 30);
                CameraSnapshot.Name = "CameraSnapshotBox";
                CameraSnapshot.SizeMode = PictureBoxSizeMode.AutoSize;
            }

            // 
            // Cameo Panel
            // 
            CameoPanel = new System.Windows.Forms.Panel();
            CameoPanel.Location = new System.Drawing.Point(0, 30);
            CameoPanel.Name = "Cameo";
            CameoPanel.Size = new System.Drawing.Size(800, 450);

            //
            // Connect Button
            //

            ConnectButton = new System.Windows.Forms.Button();
            ConnectButton.Location = new System.Drawing.Point(310, 00);
            ConnectButton.Size = new Size(120, 30);
            ConnectButton.Text = "Connect";
            ConnectButton.Click += new EventHandler(ConnectButtonClick);
            ConnectButton.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            //
            // Analyze Button
            //

            AnalyzeButton = new System.Windows.Forms.Button();
            AnalyzeButton.Location = new System.Drawing.Point(1600, 30);
            AnalyzeButton.Size = new Size(250, 100);
            AnalyzeButton.Text = "Get face and analyze it";
            AnalyzeButton.Click += new EventHandler(AnalyzeButtonClick);
            AnalyzeButton.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            AnalyzeButton.Enabled = false;

            //
            // Automatic Button
            //

            AutomaticButton = new System.Windows.Forms.Button();
            AutomaticButton.Location = new System.Drawing.Point(1600, 400);
            AutomaticButton.Size = new Size(250, 100);
            AutomaticButton.Text = "Automatic disabled";
            AutomaticButton.Click += new EventHandler(AutomaticButtonClick);
            AutomaticButton.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            AutomaticButton.Enabled = false;


            // 
            // Result label
            //       
            ResultsOfAnalysis = new System.Windows.Forms.Label();
            ResultsOfAnalysis.Location = new System.Drawing.Point(1600, 140);
            ResultsOfAnalysis.Size = new Size(300, 300);
            ResultsOfAnalysis.Text = "Results: \r\n" + " x % " + "happiness\r\n" + " x % " + "sadness\r\n" + " x % " + "neutral\r\n" + " x % " + "surprise\r\n" + " x % " + "anger\r\n" + " x % " + "contempt\r\n" + " x % " + "fear\r\n\r\n";
            ResultsOfAnalysis.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            // 
            // MainWindow
            // 

            BackColor = SystemColors.Control;
            ClientSize = new Size(1850, 500);
            Controls.Add(URLLabel);
            Controls.Add(CameraUrl);
            Controls.Add(ConnectButton);
            Controls.Add(CameraDriverLabel);
            Controls.Add(CameraDriverComboBox);
            Controls.Add(SubsciptionKeyLabel);
            Controls.Add(SubsciptionKey);
            Controls.Add(CameoPanel);
            Controls.Add(CameraSnapshot);
            Controls.Add(AnalyzeButton);
            Controls.Add(AutomaticButton);
            Controls.Add(ResultsOfAnalysis);
            Name = "MainWindow";
            Text = "Bosch Microsoft Analytic Sample";
        }

        private void AnalyzeButtonClick(object sender, EventArgs e)
        {
            string PictureFileName = PICTURE_PATH + "\\SnapShot_" + DateTime.Now.ToString("dd_MM_yyyy-HH-mm-ss") + ".jpg";
            string DownloadURL = "http://" + CameraUrl.Text + "/snap.jpg";
            MicrosoftCognitiveServiceRequests.Analyze(PictureFileName, DownloadURL, SubsciptionKey.Text);

        }

        private void AutomaticButtonClick(object sender, EventArgs e)
        {
            if (AutomaticButton.Text.Equals("Automatic disabled"))
            {
                AutomaticButton.Text = "Automatic activated";
                AutomaticActive = true;
                AnalyzeButton.Enabled = false;
            }
            else
            {
                AutomaticButton.Text = "Automatic disabled";
                AutomaticActive = false;
                AnalyzeButton.Enabled = true;
            }

        }

        private void ConnectButtonClick(object sender, EventArgs e)
        {           

            if (ConnectButton.Text.Equals("Connect"))
            {
                CameraConnector.ConnectToCamera(CameraUrl.Text, CameraDriverComboBox.SelectedItem.ToString());
            }
            else
            {
                CameraConnector.Disconnect();
            }
            

        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            CameraConnector.Disconnect();
        }
        public static Bitmap cropThisRect(Bitmap b, Rectangle r)
        {
            return b.Clone(r, b.PixelFormat);
        }
    }
}
