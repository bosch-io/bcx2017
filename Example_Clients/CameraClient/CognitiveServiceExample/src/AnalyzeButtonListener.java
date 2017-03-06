import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.InputStream;
import java.net.URL;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.text.SimpleDateFormat;
import java.util.Date;

/**
 * AnalyzeButtonListener reacts when the analyze button is clicked
 */
public class AnalyzeButtonListener implements ActionListener {


    @Override
    public void actionPerformed(ActionEvent e) {
        try {

            String url = "http://" + UserInterface.cameraURL.getText() + "/snap.jpg";

            InputStream in = new URL(url).openStream();

            String timeStamp = new SimpleDateFormat("HH-mm-ss-dd-MM-yyyy").format(new Date());

            File picture = new File(UserInterface.PICTURE_PATH + File.separator + timeStamp + ".jpg");

            Files.copy(in, Paths.get(picture.getAbsolutePath()));

            BufferedImage image = ImageIO.read(picture);

            if ((image != null)) {
                ImageIcon icon = new ImageIcon(image);
                UserInterface.cameraShotImage.setIcon(icon);
            }

            MicrosoftCognitiveServiceRequests microsoftCognitiveServiceRequests = new MicrosoftCognitiveServiceRequests();

            FaceRectangle faceRectangle = microsoftCognitiveServiceRequests.Analyze(picture, UserInterface.SUBSCRIPTION_KEY_FOR_EMOTION);

            Image faceImage = UserInterface.cropImage(image, new Rectangle(Math.toIntExact(faceRectangle.left), Math.toIntExact(faceRectangle.top), Math.toIntExact(faceRectangle.width), Math.toIntExact(faceRectangle.height)));

            if ((faceImage != null)) {
                ImageIcon icon = new ImageIcon(faceImage);
                UserInterface.faceImage.setIcon(icon);
            }

        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }


}
