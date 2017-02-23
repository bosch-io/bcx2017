import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

/**
 * Created by DaveKing on 19.02.2017.
 */
public class ConnectButtonListener implements ActionListener {

    CameraConnector cameraConnector = new CameraConnector();

    @Override
    public void actionPerformed(ActionEvent e) {

        if (!UserInterface.isConnected()) {

            cameraConnector.setIsConnected(true);

            Thread connection = new Thread(cameraConnector);

            connection.start();

            UserInterface.changeConnectionStatus();
        }
        else {
            cameraConnector.setIsConnected(false);

            UserInterface.changeConnectionStatus();
        }
    }
}

