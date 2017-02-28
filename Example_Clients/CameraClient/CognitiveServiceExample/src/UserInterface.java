import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.File;


public class UserInterface {

    public static boolean isConnected = false;

    public static String DEFAULT_IP;

    public static String SUBSCRIPTION_KEY_FOR_EMOTION;

    public static String SUBSCRIPTION_KEY_FOR_VISION;

    public static String PICTURE_PATH;

    public static Font FONT =new Font("Arial", Font.CENTER_BASELINE, 14);

    public static JButton analyzeButton;

    private static JButton connectButton;

    public static JButton automaticButton;

    public static boolean automaticActive = false;

    public static JTextField cameraURL;

    private JFrame mainFrame = new JFrame();

    public static JTextField subsciptionKey;

    public static JPanel cameoPanel;

    public static JLabel cameraShotImage;

    public static JLabel faceImage;

    public static JLabel cameraStreaming;

    public static JFormattedTextField resultsOfAnalysis;


    public UserInterface() {
        String ConfigFileName = "Resources\\MSCS.cfg";
        try {
            Configuration.ReadConfig(ConfigFileName);
        }
        catch (Exception ex) {
            ex.printStackTrace();
            System.out.println(("Configfile "
                    + (ConfigFileName + " not found")));
            System.out.println("Please define a configfile named MSCS.cfg to prefill Camera IP and Subscription Key");
        }


    }

    public void InitializeGUIComponents() {

        //
        //  URL Textbox
        //
        cameraURL = new JTextField();
        cameraURL.setText(DEFAULT_IP);
        cameraURL.setSize(150, 20);
        cameraURL.setFont(FONT);
        cameraURL.setLocation(150, 0);
        cameraURL.setName("Camera URL Textbox");

        //
        //  URL Label
        //
        JLabel urlLabel = new JLabel();
        urlLabel.setText("Camera IP : ");
        urlLabel.setSize(150, 20);
        urlLabel.setFont(FONT);
        urlLabel.setLocation(0, 0);
        urlLabel.setName("Camera URL Label");

        //
        //  Subscribtion Key Label
        //
        JLabel subsciptionKeyLabel = new JLabel();
        subsciptionKeyLabel.setText("Subscribtion Key : ");
        subsciptionKeyLabel.setSize(200, 20);
        subsciptionKeyLabel.setFont(FONT);
        subsciptionKeyLabel.setLocation(1250, 0);
        subsciptionKeyLabel.setName("Subscribtion Key Label");
        //
        //  Subscribtion Key Textbox
        //
        subsciptionKey = new JTextField();
        subsciptionKey.setText(SUBSCRIPTION_KEY_FOR_EMOTION);
        subsciptionKey.setSize(400, 20);
        subsciptionKey.setFont(FONT);
        subsciptionKey.setLocation(1450, 0);
        subsciptionKey.setName("Subscribtion Key Textbox");

        //
        //  BoschSmartEdgeDeviceLabel
        //
        JLabel boschSmartEdgeDevice = new JLabel();
        //  Construct an image object from a file in the local directory.
        //  ... This file must exist in the solution.
        String boschSmartEdgeImageFileName = "Resources\\SmartEdgeDevices.png";

        BufferedImage boschSmartEdgeImage = null;

        try {
            boschSmartEdgeImage = ImageIO.read(new File(boschSmartEdgeImageFileName));
        } catch (Exception ex) {
            ex.printStackTrace();
        }

        if ((boschSmartEdgeImage != null)) {
            ImageIcon icon = new ImageIcon(boschSmartEdgeImage);
            boschSmartEdgeDevice.setIcon(icon);
            boschSmartEdgeDevice.setName("CameraSnapshotBox");
        }

        //
        //  CameraStreamingBox
        //
        cameraStreaming = new JLabel();

        //
        //  CameraStreamingBox
        //
        cameraShotImage = new JLabel();

        //
        //  CameraStreamingBox
        //
        faceImage = new JLabel();


        //
        //  Connect Button
        //
        connectButton = new JButton();
        connectButton.setLocation(310, 0);
        connectButton.setSize(120, 30);
        connectButton.setText("Connect");
        connectButton.setFont(FONT);
        ConnectButtonListener connectButtonListener = new ConnectButtonListener();
        connectButton.addActionListener(connectButtonListener);

        //
        //  Analyze Button
        //
        analyzeButton = new JButton();
        analyzeButton.setLocation(1600, 30);
        analyzeButton.setSize(250, 100);
        analyzeButton.setText("Get face and analyze it");
        analyzeButton.setFont(FONT);
        analyzeButton.setEnabled(true);
        AnalyzeButtonListener analyzeButtonListener = new AnalyzeButtonListener();
        analyzeButton.addActionListener(analyzeButtonListener);

        //
        //  Automatic Button
        //
        automaticButton = new JButton();
        automaticButton.setLocation(1600, 400);
        automaticButton.setSize(250, 100);
        automaticButton.setText("Automatic disabled");
        automaticButton.setFont(FONT);
        automaticButton.setEnabled(false);
        AutomaticButtonListener automaticButtonListener = new AutomaticButtonListener();
        automaticButton.addActionListener(automaticButtonListener);

        //
        //  Result label
        //
        resultsOfAnalysis = new JFormattedTextField("");
        resultsOfAnalysis.setLocation(1600, 140);
        resultsOfAnalysis.setSize(300, 300);
        resultsOfAnalysis.setText("Results: \r\n" + (" x % " + ("happiness\r\n" + (" x % " + ("sadness\r\n" + (" x % " + ("neutral\r\n" + (" x % " + ("surprise\r\n" + (" x % " + ("anger\r\n" + (" x % " + ("contempt\r\n" + (" x % " + "fear\r\n\r\n"))))))))))))));
        resultsOfAnalysis.setFont(FONT);

        //
        //  MainWindow
        //

        GridBagLayout gridbag = new GridBagLayout();
        GridBagConstraints gridBagConstraints = new GridBagConstraints();

        mainFrame.setLayout(new GridLayout(4, 3));
        mainFrame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        mainFrame.setSize(1200, 800);



        mainFrame.add(urlLabel);
        mainFrame.add(boschSmartEdgeDevice);
        mainFrame.add(subsciptionKeyLabel);

        mainFrame.add(cameraURL);
        mainFrame.add(connectButton);
        mainFrame.add(subsciptionKey);

        mainFrame.add(cameraShotImage);
        mainFrame.add(cameraStreaming);
        mainFrame.add(faceImage);

        mainFrame.add(automaticButton);
        mainFrame.add(analyzeButton);
        mainFrame.add(resultsOfAnalysis);

        mainFrame.setName("Bosch Microsoft Analytic Sample");



        //Display the window.
        mainFrame.pack();
        mainFrame.setVisible(true);

    }




    public static BufferedImage cropImage(BufferedImage bufferedImage, Rectangle rectangle) {
        return bufferedImage.getSubimage(rectangle.x, rectangle.y, rectangle.width, rectangle.height);
    }

    public static boolean isConnected() {
        return isConnected;
    }

    public static void changeConnectionStatus()
    {
        if(isConnected)
        {
            isConnected = false;
            connectButton.setText("Connect");
        }
        else {
            isConnected = true;
            connectButton.setText("Disconnect");
        }
    }

}
