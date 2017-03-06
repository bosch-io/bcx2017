import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.File;

/**
 * UserInterface class defines the GUI of the CongnitveServiceExample
 */

public class UserInterface {

    public static boolean isConnected = false;

    public static String DEFAULT_IP;

    public static String SUBSCRIPTION_KEY_FOR_EMOTION;

    public static String SUBSCRIPTION_KEY_FOR_VISION;

    public static String PICTURE_PATH;

    public static Font FONT =new Font("Arial", Font.CENTER_BASELINE, 14);

    public static JButton analyzeButton;

    private static JButton connectButton;

    public static JFormattedTextField eventField;

    public static JTextField cameraURL;

    private JFrame mainFrame = new JFrame();

    public static JTextField subsciptionKey;

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

        // GUI elements

        //
        //  URL Textbox
        //
        cameraURL = new JTextField();
        cameraURL.setText(DEFAULT_IP);
        cameraURL.setName("Camera URL Textbox");

        //
        //  URL Label
        //
        JLabel urlLabel = new JLabel();
        urlLabel.setText("Camera IP : ");
        urlLabel.setName("Camera URL Label");

        //
        //  Subscribtion Key Label
        //
        JLabel subsciptionKeyLabel = new JLabel();
        subsciptionKeyLabel.setText("Subscribtion Key : ");
        subsciptionKeyLabel.setName("Subscribtion Key Label");
        //
        //  Subscribtion Key Textbox
        //
        subsciptionKey = new JTextField();
        subsciptionKey.setText(SUBSCRIPTION_KEY_FOR_EMOTION);
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
        connectButton.setText("Connect");
        ConnectButtonListener connectButtonListener = new ConnectButtonListener();
        connectButton.addActionListener(connectButtonListener);

        //
        //  Analyze Button
        //
        analyzeButton = new JButton();
        analyzeButton.setText("Get face and analyze it");
        analyzeButton.setEnabled(false);
        AnalyzeButtonListener analyzeButtonListener = new AnalyzeButtonListener();
        analyzeButton.addActionListener(analyzeButtonListener);

        //
        //  Event Textfield Button
        //
        eventField = new JFormattedTextField("");
        eventField.setColumns(5);

        //
        //  Result label
        //
        resultsOfAnalysis = new JFormattedTextField("");
        resultsOfAnalysis.setText("Emotion results: \r\n" + (" x % " + ("happiness\r\n" + (" x % " + ("sadness\r\n" + (" x % " + ("neutral\r\n" + (" x % " + ("surprise\r\n" + (" x % " + ("anger\r\n" + (" x % " + ("contempt\r\n" + (" x % " + "fear\r\n\r\n"))))))))))))));
        resultsOfAnalysis.setFont(FONT);

        //
        //  MainWindow
        //
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

        mainFrame.add(eventField);
        mainFrame.add(analyzeButton);
        mainFrame.add(resultsOfAnalysis);

        mainFrame.setName("Bosch Microsoft Analytic Sample");



        //Display the window.
        mainFrame.pack();
        mainFrame.setVisible(true);

    }

    // cropImage extract from the given rectangle the subimage from the given image
    public static BufferedImage cropImage(BufferedImage bufferedImage, Rectangle rectangle) {
        if (rectangle.height == 0 || rectangle.width == 0 )
        {
            return bufferedImage;
        }
        else {
            return bufferedImage.getSubimage(rectangle.x, rectangle.y, rectangle.width, rectangle.height);
        }
    }

    // gives the status of the connection to the camera
    public static boolean isConnected() {
        return isConnected;
    }

    // called when the connection status change to update all buttons and the status
    public static void changeConnectionStatus()
    {
        if(isConnected)
        {
            isConnected = false;
            connectButton.setText("Connect");
            analyzeButton.setEnabled(false);
        }
        else {
            isConnected = true;
            connectButton.setText("Disconnect");
            analyzeButton.setEnabled(true);
        }
    }

}
