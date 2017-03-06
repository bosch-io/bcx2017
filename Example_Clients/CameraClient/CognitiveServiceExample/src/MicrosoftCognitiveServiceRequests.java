import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.client.utils.URIBuilder;
import org.apache.http.entity.ByteArrayEntity;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.HttpClients;
import org.apache.http.util.EntityUtils;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;

import java.io.File;
import java.net.URI;
import java.nio.file.Files;

/**
 * MicrosoftCognitiveServiceRequests which holds the required method for Microsoft cognitive service
 */

public class MicrosoftCognitiveServiceRequests {


    public FaceRectangle Analyze(File picture, String subsciptionKey) {

        FaceRectangle faceRectangle = new FaceRectangle();

        try {
            String cognitiveServiceResponse = this.SendEmotionRequest(picture, subsciptionKey);

            JSONParser parser = new JSONParser();

            JSONArray emotionfaceList = (JSONArray)parser.parse(cognitiveServiceResponse);

            if ((emotionfaceList != null)) {
                if ((emotionfaceList.size() == 0)) {
                    UserInterface.resultsOfAnalysis.setText("No faces detected. \r\n");
                }
                else {
                    for (int index = 0; (index < emotionfaceList.size()); index++) {

                        JSONObject emotionface = (JSONObject)emotionfaceList.get(index);

                        JSONObject face = (JSONObject)emotionface.get("faceRectangle");

                        faceRectangle.top = (Long) face.get("top");
                        faceRectangle.left = (Long) face.get("left");
                        faceRectangle.width = (Long) face.get("width");
                        faceRectangle.height = (Long) face.get("height");

                        JSONObject emotionScores = (JSONObject)emotionface.get("scores");

                        Scores scores = new Scores();
                        scores.happiness = (Double)emotionScores.get("happiness");
                        scores.sadness = (Double)emotionScores.get("sadness");
                        scores.neutral = (Double)emotionScores.get("neutral");
                        scores.surprise = (Double)emotionScores.get("surprise");
                        scores.anger = (Double)emotionScores.get("anger");
                        scores.contempt = (Double)emotionScores.get("contempt");
                        scores.fear = (Double)emotionScores.get("fear");

                        UserInterface.resultsOfAnalysis.setText("Emotion results: \r\n"
                                + String.format("%.2f",scores.happiness*100) + " % happiness\r\n"
                                + String.format("%.2f",scores.sadness*100) + " % sadness\r\n"
                                + String.format("%.2f",scores.neutral*100) + " % neutral\r\n"
                                + String.format("%.2f",scores.surprise*100) + " % surprise\r\n"
                                + String.format("%.2f", scores.anger*100) + " % anger\r\n"
                                + String.format("%.2f", scores.contempt*100) + " % contempt\r\n"
                                + String.format("%.2f", scores.fear*100) + " % fear\r\n\r\n");

                    }

                }

            }

        } catch (Exception ex) {
            ex.printStackTrace();
        }

        return faceRectangle;

    }

    public String SendEmotionRequest(File picture, String subscriptionKey) {

        HttpClient httpclient = HttpClients.createDefault();

        String analysis = "";

        try {
            URIBuilder builder = new URIBuilder("https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize");

            URI uri = builder.build();
            HttpPost request = new HttpPost(uri);

            byte[] binaryImageData = Files.readAllBytes(picture.toPath());

            request.setHeader("Content-Type", "application/octet-stream");
            request.setHeader("Ocp-Apim-Subscription-Key", subscriptionKey);

            request.setEntity(new ByteArrayEntity(binaryImageData));

            HttpResponse response = httpclient.execute(request);

            // System.out.println(response.toString());

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