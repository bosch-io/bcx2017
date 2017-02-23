import javax.imageio.ImageIO;
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
 * Created by DaveKing on 19.02.2017.
 */
public class AnalyzeButtonListener implements ActionListener {


    @Override
    public void actionPerformed(ActionEvent e) {
        try {

            String url = "http://" + UserInterface.DEFAULT_IP + "/snap.jpg";

            InputStream in = new URL(url).openStream();

            String timeStamp = new SimpleDateFormat("HH-mm-ss-dd-MM-yyyy").format(new Date());

            File picture = new File(UserInterface.PICTURE_PATH + File.separator + timeStamp + ".jpg");

            Files.copy(in, Paths.get(picture.getAbsolutePath()));

            BufferedImage image = ImageIO.read(picture);

            MicrosoftCognitiveServiceRequests microsoftCognitiveServiceRequests = new MicrosoftCognitiveServiceRequests();

            microsoftCognitiveServiceRequests.Analyze(picture, UserInterface.SUBSCRIPTION_KEY_FOR_EMOTION);

        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }


}
