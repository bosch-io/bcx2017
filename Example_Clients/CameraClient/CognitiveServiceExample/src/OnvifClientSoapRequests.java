import javax.xml.namespace.QName;
import javax.xml.soap.*;
import javax.xml.transform.Source;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.stream.StreamResult;
import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.net.InetAddress;
import java.util.UUID;

/**
 * OnvifClientSoapRequests create a subscription to the camera and
 */
public class OnvifClientSoapRequests extends Thread {

    String ipOfCamera = UserInterface.cameraURL.getText();

    /**
     * Starting point for the SOAP Onvif Client
     */
    public void run() {

        while (UserInterface.isConnected()) {

            try {
                // Create SOAP Connection
                SOAPConnectionFactory soapConnectionFactory = SOAPConnectionFactory.newInstance();
                SOAPConnection soapConnection = soapConnectionFactory.createConnection();

                // Send SOAP Message to SOAP Server
                String url = "http://" + ipOfCamera + "/onvif/event_service";
                SOAPMessage soapResponse = soapConnection.call(createSOAPRequest(), url);

                // Process the SOAP Response
                printSOAPResponse(soapResponse);

                soapConnection.close();

                Thread.sleep(60000);

            } catch (Exception e) {
                System.err.println("Error occurred while sending SOAP Request to Server");
                e.printStackTrace();
            }

        }
    }


    private void filterEventFromSoapMessage(String soapMessage) {

        String event;
        String state;
        String timestamp;


    }

    private void addEventTime(String inputLine) {

        System.out.println(inputLine);
    }

    private void addEventName(String inputLine) {
        System.out.println(inputLine);
    }

    private void addEventState(String inputLine) {
        System.out.println(inputLine);

    }

    public SOAPMessage parseSoapMessageFromString(String xmlString) throws Exception {

        InputStream is = new ByteArrayInputStream(xmlString.getBytes());
        SOAPMessage request = MessageFactory.newInstance(SOAPConstants.SOAP_1_2_PROTOCOL).createMessage(null, is);

        return request;
    }



    private SOAPMessage createSOAPRequest() throws Exception {

        MessageFactory messageFactory = MessageFactory.newInstance(SOAPConstants.SOAP_1_2_PROTOCOL);
        SOAPMessage soapMessage = messageFactory.createMessage();
        SOAPPart soapPart = soapMessage.getSOAPPart();

        String serverURI = "http://www.w3.org/2005/08/addressing";

        // SOAP Envelope
        SOAPEnvelope envelope = soapPart.getEnvelope();
        envelope.addNamespaceDeclaration("a", serverURI);


        /*
        <s:Envelope xmlns:s="http://www.w3.org/2003/05/soap-envelope" xmlns:a="http://www.w3.org/2005/08/addressing">
	        <s:Header>
		        <a:Action s:mustUnderstand="1">http://docs.oasis-open.org/wsn/bw-2/NotificationProducer/SubscribeRequest</a:Action>
		        <a:MessageID>
		            urn:uuid:810096bf-8463-48de-92b3-45dd5b54f5a7
		        </a:MessageID>
		        <a:ReplyTo>
		            <a:Address>
		                http://www.w3.org/2005/08/addressing/anonymous
		             </a:Address>
		        </a:ReplyTo>
		        <a:To s:mustUnderstand="1">
		            http://192.168.1.198/onvif/event_service
		        </a:To>
	        </s:Header>
	        <s:Body xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
		        <Subscribe xmlns="http://docs.oasis-open.org/wsn/b-2">
			        <ConsumerReference>
				        <a:Address>
				            http://192.168.1.237:8085/subscription-2
				        </a:Address>
			        </ConsumerReference>
			        <InitialTerminationTime>
			            PT60S
			        </InitialTerminationTime>
		        </Subscribe>
	        </s:Body>
        </s:Envelope>
         */

        // SOAP Header

        SOAPHeader soapHeader = envelope.getHeader();

        SOAPHeaderElement soapHeaderElem = soapHeader.addHeaderElement(new QName(serverURI, "Action", "a"));
        soapHeaderElem.setMustUnderstand(true);
        soapHeaderElem.addTextNode("http://docs.oasis-open.org/wsn/bw-2/NotificationProducer/SubscribeRequest");

        SOAPHeaderElement soapHeaderElem2 = soapHeader.addHeaderElement(new QName(serverURI, "MessageID", "a"));
        UUID uuid = java.util.UUID.randomUUID();
        soapHeaderElem2.addTextNode("urn:uuid:" + uuid.toString());

        SOAPHeaderElement soapHeaderElem3 = soapHeader.addHeaderElement(new QName(serverURI, "ReplyTo", "a"));
        SOAPElement soapHeaderElem31 = soapHeaderElem3.addChildElement("Address", "a");
        soapHeaderElem31.addTextNode("http://www.w3.org/2005/08/addressing/anonymous");

        SOAPHeaderElement soapHeaderElem4 = soapHeader.addHeaderElement(new QName(serverURI, "To", "a"));
        soapHeaderElem4.setMustUnderstand(true);
        soapHeaderElem4.addTextNode("http://192.168.1.198/onvif/event_service");

        // SOAP Body
        SOAPBody soapBody = envelope.getBody();

        soapBody.addNamespaceDeclaration("xsi", "http://www.w3.org/2001/XMLSchema-instance");
        soapBody.addNamespaceDeclaration("xsd", "http://www.w3.org/2001/XMLSchema");

        SOAPElement soapBodySubscribeElement = soapBody.addChildElement("Subscribe", null, "http://docs.oasis-open.org/wsn/b-2");

        SOAPElement soapBodyConsumerReferenceElement = soapBodySubscribeElement.addChildElement("ConsumerReference");
        SOAPElement soapBodyAddressElement = soapBodyConsumerReferenceElement.addChildElement("Address", "a");
        soapBodyAddressElement.addTextNode("http://" + InetAddress.getLocalHost().getHostAddress() + ":" + CameraEventListener.LISTENING_PORT + "/subscription-2");
        SOAPElement soapBodyInitialTerminationTimeElement = soapBodySubscribeElement.addChildElement("InitialTerminationTime");
        soapBodyInitialTerminationTimeElement.addTextNode("PT60S");

        MimeHeaders headers = soapMessage.getMimeHeaders();
        headers.addHeader("SOAPAction", serverURI + "Subscription");

        soapMessage.saveChanges();

        /* Print the request message */
        System.out.print("Request SOAP Message = ");
        soapMessage.writeTo(System.out);
        System.out.println();

        return soapMessage;
    }

    /**
     * Method used to print the SOAP Response
     */
    private void printSOAPResponse(SOAPMessage soapResponse) throws Exception {
        TransformerFactory transformerFactory = TransformerFactory.newInstance();
        Transformer transformer = transformerFactory.newTransformer();
        Source sourceContent = soapResponse.getSOAPPart().getContent();
        System.out.println("\nResponse SOAP Message = ");
        StreamResult result = new StreamResult(System.out);
        transformer.transform(sourceContent, result);
    }

}

