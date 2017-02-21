using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Drawing;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace MicrosoftCognitiveServiceExample
{

    public class MicrosoftCognitiveServiceRequests     {

        private WebClient WebClient = new WebClient();

        public async void Analyze(string PictureFileName, string DownloadURL, string SubsciptionKey)
        {
            WebClient.DownloadFile(DownloadURL, PictureFileName);

            Bitmap mainbitmap = new Bitmap(Image.FromFile(PictureFileName));

            try
            {
                HttpResponseMessage cognitiveServiceResponse = await SendEmotionRequest(PictureFileName, SubsciptionKey);

                List<EmotionFace> emotionfaceList = JsonConvert.DeserializeObject<List<EmotionFace>>(await cognitiveServiceResponse.Content.ReadAsStringAsync());

                if (emotionfaceList != null)
                {
                    if (emotionfaceList.Count == 0)
                    {
                        UserInterface.ResultsOfAnalysis.Text = "No faces detected. \r\n";
                    }
                    else
                    {
                        for (int index = 0; index < emotionfaceList.Count; index++)
                        {
                            UserInterface.CameraSnapshot.SizeMode = PictureBoxSizeMode.CenterImage;
                            UserInterface.CameraSnapshot.Image = UserInterface.cropThisRect(mainbitmap, new Rectangle(emotionfaceList[index].faceRectangle.left, emotionfaceList[index].faceRectangle.top, emotionfaceList[index].faceRectangle.width, emotionfaceList[index].faceRectangle.height));

                            UserInterface.ResultsOfAnalysis.Text = "Results: \r\n"
                            + string.Format("{0:P1}", emotionfaceList[index].scores.happiness) + " happiness\r\n"
                            + string.Format("{0:P1}", emotionfaceList[index].scores.sadness) + " sadness\r\n"
                            + string.Format("{0:P1}", emotionfaceList[index].scores.neutral) + " neutral\r\n"
                            + string.Format("{0:P1}", emotionfaceList[index].scores.surprise) + " surprise\r\n"
                            + string.Format("{0:P1}", emotionfaceList[index].scores.anger) + " anger\r\n"
                            + string.Format("{0:P1}", emotionfaceList[index].scores.contempt) + " contempt\r\n"
                            + string.Format("{0:P1}", emotionfaceList[index].scores.fear) + " fear\r\n\r\n";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public async Task<HttpResponseMessage> SendEmotionRequest(string file, string subscriptionKey)
        {
            HttpClient client = new HttpClient();
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);

            // request URL
            string SERVICE_ENDPOINT = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize";

            // Request header
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameter
            queryString["visualFeatures"] = "Categories";

            byte[] byteData = System.IO.File.ReadAllBytes(file);
            HttpResponseMessage response;

            // Request body / contentremote 
            using (ByteArrayContent byteArrayContent = new ByteArrayContent(byteData))
            {
                byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(SERVICE_ENDPOINT, byteArrayContent);
            }

            return response;

        }

        public async Task<HttpResponseMessage> SendAnalysisRequest(string file, string subscriptionKey)
        {
            HttpClient client = new HttpClient();
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            queryString["visualFeatures"] = "Tags,Description,Faces";
            string uri = "https://api.projectoxford.ai/vision/v1.0/analyze?" + (object)queryString;
            byte[] byteData = System.IO.File.ReadAllBytes(file);
            HttpResponseMessage response;
            using (ByteArrayContent byteArrayContent = new ByteArrayContent(byteData))
            {
                byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, byteArrayContent);
            }

            return response;

        }

        
    }
}
