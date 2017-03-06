import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.image.BufferedImage;
import java.io.InputStream;
import java.net.URL;

/**
 * CameraConnector handles the connection to the camera and download the pictures from camera
 */

public class CameraConnector extends Thread {

    private boolean isConnected = false;

    public void setIsConnected(boolean isConnected) {
        this.isConnected = isConnected;
    }

    public void run(){
        while(isConnected)
        {
            try {
                getPictureFromCamera();
            }
            catch (Exception ex)
            {
                ex.printStackTrace();
            }

        }

    }

    public void getPictureFromCamera() {

        String url = "http://" + UserInterface.cameraURL.getText() + "/snap.jpg";

        try {
            InputStream in = new URL(url).openStream();

            BufferedImage image = ImageIO.read(in);

            if ((image != null)) {
                ImageIcon icon = new ImageIcon(image);
                UserInterface.cameraStreaming.setIcon(icon);
            }

        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}

