import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.image.BufferedImage;
import java.io.InputStream;
import java.net.URL;

public class CameraConnector extends Thread {

    public void run(){
        connect();
    }

    public void connect() {

        String url = "http://" + UserInterface.DEFAULT_IP + "/snap.jpg";

        try {
            InputStream in = new URL(url).openStream();

            BufferedImage image = ImageIO.read(in);

            if ((image != null)) {
                ImageIcon icon = new ImageIcon(image);
                UserInterface.cameraSnapshot.setIcon(icon);
            }

        } catch (Exception e) {
            e.printStackTrace();
        }


    }
}

