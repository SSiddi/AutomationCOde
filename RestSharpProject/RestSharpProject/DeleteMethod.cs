using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace RestSharpProject
{
    [TestClass]
    public class DeleteMethod
    {
        IRestClient Restclient;
        IRestRequest RestRequest;
        IRestResponse RestResponse;

        [TestMethod]
        public void DeleteOrder()
        {
            // Create order first so that it can be deleted later
            string url = "https://petstore.swagger.io/v2/store/order";
            int orderNumber = 2;
            Restclient = new RestClient();
            RestRequest = new RestRequest(url);

            JObject jObjectbody = new JObject();
            jObjectbody.Add("id", orderNumber);
            jObjectbody.Add("petId", "50");
            jObjectbody.Add("quantity", "10");
            jObjectbody.Add("shipDate", "2021-07-09T15:29:28.740Z");
            jObjectbody.Add("status", "placed");
            jObjectbody.Add("complete", true);

            RestRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            RestResponse = Restclient.Post(RestRequest);

            System.Console.WriteLine("Create order staus code:  " + RestResponse.StatusCode);
            System.Console.WriteLine("Create order response content: " + RestResponse.Content);

            // Delete above created order
            string deleteURL = "https://petstore.swagger.io/v2/store/order/{orderId}";
            RestRequest = new RestRequest(deleteURL);
            RestRequest.AddUrlSegment("orderId", orderNumber);
            RestResponse = Restclient.Delete(RestRequest);

            System.Console.WriteLine("Delete order staus code:  " + RestResponse.StatusCode);
            System.Console.WriteLine("Delete order response content: " + RestResponse.Content);
          
            Assert.AreEqual(200, (int)RestResponse.StatusCode, "Delete request failed !");
        }

        [TestMethod]
        public void DeleteNonExistingOrder()
        {
            string deleteURL = "https://petstore.swagger.io/v2/store/order/{orderId}";
            Restclient = new RestClient();
            RestRequest = new RestRequest(deleteURL);
            RestRequest.AddUrlSegment("orderId", "9900");
            RestResponse = Restclient.Delete(RestRequest);

            System.Console.WriteLine("Delete non existing order status: " + RestResponse.StatusCode);
            System.Console.WriteLine("Delete non existing order response content:  " + RestResponse.Content);

            Assert.AreEqual(404, (int)RestResponse.StatusCode, "Delete request should get fail for non existing order!");
        }
    }
}