import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

/**
 * ConnectButtonListener is the listener which reacts when the getPictureFromCamera button is clicked
 */

public class ConnectButtonListener implements ActionListener {

    CameraConnector cameraConnector = new CameraConnector();

    OnvifClientSoapRequests onvifClientSoapRequests = new OnvifClientSoapRequests();

    CameraEventListener cameraEventListener = new CameraEventListener();

    @Override
    public void actionPerformed(ActionEvent e) {

        if (!UserInterface.isConnected()) {

            cameraConnector.setIsConnected(true);

            Thread connection = new Thread(cameraConnector);

            connection.start();

            Thread eventSubscription = new Thread(onvifClientSoapRequests);

            eventSubscription.start();

            Thread eventListener = new Thread(cameraEventListener);

            eventListener.start();

            UserInterface.changeConnectionStatus();
        }
        else {
            cameraConnector.setIsConnected(false);

            UserInterface.changeConnectionStatus();
        }
    }
}

