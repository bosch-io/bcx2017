import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.client.utils.URIBuilder;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.HttpClients;
import org.apache.http.util.EntityUtils;

import javax.imageio.ImageIO;
import java.awt.image.BufferedImage;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.net.URI;

public class MicrosoftCognitiveServiceRequests {


    public void Analyze(File picture, String subsciptionKey) {

        try {
            String cognitiveServiceResponse = this.SendEmotionRequest(picture, subsciptionKey);

            System.out.println(cognitiveServiceResponse);
            /*

            List<EmotionFace> emotionfaceList = ((JsonConvert.DeserializeObject
                    < (List < EmotionFace))
                    + cognitiveServiceResponse.Content.ReadAsStringAsync());
            if ((emotionfaceList != null)) {
                if ((emotionfaceList.Count == 0)) {
                    UserInterface.resultsOfAnalysis.Text = "No faces detected. \r\n";
                }
                else {
                    for (int index = 0; (index < emotionfaceList.Count); index++) {
                        UserInterface.CameraSnapshot.SizeMode = PictureBoxSizeMode.CenterImage;
                        UserInterface.CameraSnapshot.Image = UserInterface.cropThisRect(mainbitmap, new Rectangle(emotionfaceList[index].faceRectangle.left, emotionfaceList[index].faceRectangle.top, emotionfaceList[index].faceRectangle.width, emotionfaceList[index].faceRectangle.height));
                        UserInterface.resultsOfAnalysis.Text = ("Results: \r\n"
                                + (String.Format("{0:P1}", emotionfaceList[index].scores.happiness) + (" happiness\r\n"
                                + (String.Format("{0:P1}", emotionfaceList[index].scores.sadness) + (" sadness\r\n"
                                + (String.Format("{0:P1}", emotionfaceList[index].scores.neutral) + (" neutral\r\n"
                                + (String.Format("{0:P1}", emotionfaceList[index].scores.surprise) + (" surprise\r\n"
                                + (String.Format("{0:P1}", emotionfaceList[index].scores.anger) + (" anger\r\n"
                                + (String.Format("{0:P1}", emotionfaceList[index].scores.contempt) + (" contempt\r\n"
                                + (String.Format("{0:P1}", emotionfaceList[index].scores.fear) + " fear\r\n\r\n"))))))))))))));
                    }

                }

            }
*/
        } catch (Exception ex) {
            ex.printStackTrace();
        }

    }

    public String SendEmotionRequest(File picture, String subscriptionKey) {

        HttpClient httpclient = HttpClients.createDefault();

        String analysis = "";

        try {
            URIBuilder builder = new URIBuilder("https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize");

            URI uri = builder.build();
            HttpPost request = new HttpPost(uri);

            boolean useJson = true;
            StringEntity reqEntity;

            if (useJson)
            {
                request.setHeader("Content-Type", "application/json");
                request.setHeader("Ocp-Apim-Subscription-Key", subscriptionKey);

                String bildURL = "\"http://tutorials.ipp.boschsecurity.com/downloads/IPP/Facedetection/Bild1.jpg\"";

                // Request body
                reqEntity = new StringEntity("{\"url\":" + bildURL + "}");
            }
            else{
                request.setHeader("Content-Type", "application/octet-stream");
                request.setHeader("Ocp-Apim-Subscription-Key", subscriptionKey);

                ByteArrayOutputStream outputStream = new ByteArrayOutputStream();

                // lokal gespeichertes Bild
                picture = new File("Resources\\Bild1.jpg");

                BufferedImage imgForAnalysis = ImageIO.read(picture);

                ImageIO.write(imgForAnalysis, "jpg", outputStream);

                // transfer the file byte-by-byte to the response object

                // funktioniert nicht :(
                reqEntity = new StringEntity(outputStream.toString());

            }

            request.setEntity(reqEntity);

            HttpResponse response = httpclient.execute(request);

            System.out.println(response.toString());

            HttpEntity entity = response.getEntity();

            if (entity != null) {
                analysis = EntityUtils.toString(entity);
            }

        } catch (Exception e) {
            System.out.println(e.getMessage());
        }

        return analysis;
    }

    public String SendAnalysisRequest(String file, String subscriptionKey) {

        HttpClient httpclient = HttpClients.createDefault();

        String analysis = "";

        try {
            URIBuilder builder = new URIBuilder("https://westus.api.cognitive.microsoft.com/vision/v1.0/analyze");

            builder.setParameter("visualFeatures", "Categories");
            builder.setParameter("details", "{string}");
            builder.setParameter("language", "en");

            URI uri = builder.build();
            HttpPost request = new HttpPost(uri);
            request.setHeader("Content-Type", "application/json");
            request.setHeader("Ocp-Apim-Subscription-Key", "{subscription key}");


            // Request body
            StringEntity reqEntity = new StringEntity("{body}");
            request.setEntity(reqEntity);

            HttpResponse response = httpclient.execute(request);
            HttpEntity entity = response.getEntity();

            if (entity != null) {
                analysis = EntityUtils.toString(entity);
            }

        } catch (Exception e) {
            System.out.println(e.getMessage());
        }

        return analysis;
    }
}