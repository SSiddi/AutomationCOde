using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace RestSharpProject
{
    [TestClass]
    public class GetMethod
    {
        [TestMethod]
        public void GetPetAPI()
        {
            // Create new pet
            string createPetURL = "https://petstore.swagger.io/v2/pet";
            IRestRequest RestRequest;
            IRestClient Restclient;
            IRestResponse RestResponse;
            int petCode = 2;

            Restclient = new RestClient();
            RestRequest = new RestRequest(createPetURL);

            JObject jObjectbody = new JObject();
            jObjectbody.Add("id", petCode);
            jObjectbody.Add("name", "Dozo");
            jObjectbody.Add("status", "Available");
      
            RestRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            RestResponse = Restclient.Post(RestRequest);

            System.Console.WriteLine("Post response code: " + RestResponse.StatusCode);
            System.Console.WriteLine("Post response : " + RestResponse.Content);

            var jObject = JObject.Parse(RestResponse.Content);
            string petName = jObject.GetValue("name").ToString();

            string fetchURL = "https://petstore.swagger.io/v2/pet/{petId}";
            Restclient = new RestClient();
            RestRequest = new RestRequest(fetchURL);
            RestRequest.AddUrlSegment("petId", petCode);

            RestResponse = Restclient.Get(RestRequest);

            System.Console.WriteLine("Get Status code: " + RestResponse.StatusCode);
            System.Console.WriteLine("Get response: " +RestResponse.Content);

            var jObjectGet = JObject.Parse(RestResponse.Content);
            string fetchedPetName = jObjectGet.GetValue("name").ToString();

            Assert.AreEqual(200, (int)RestResponse.StatusCode, "Get request failed !");
            Assert.AreEqual(petName,fetchedPetName, "Pet name fetched does not match with the created pet");
        }


        [TestMethod]
        public void GetInventoryDetailsAPI()
        {
            string url = "https://petstore.swagger.io/v2/store/inventory";
            IRestClient Restclient = new RestClient();
            IRestRequest RestRequest = new RestRequest(url);
            IRestResponse RestResponse = Restclient.Get(RestRequest);

            System.Console.WriteLine("Get inventory status code:  " + RestResponse.StatusCode);
            System.Console.WriteLine("Get inventory status content:  " + RestResponse.Content);

            Assert.AreEqual(200, (int)RestResponse.StatusCode, "Get request failed !");
        }

        [TestMethod]
        public void GetUserForInvalidUser()
        {
            string url = "https://petstore.swagger.io/v2/user/{username}";
            IRestClient Restclient = new RestClient();
            IRestRequest RestRequest = new RestRequest(url);
            RestRequest.AddUrlSegment("username", "@#$%%");
            IRestResponse RestResponse = Restclient.Get(RestRequest);

            System.Console.WriteLine("Status code for invalid user:  " + RestResponse.StatusCode);
            System.Console.WriteLine(RestResponse.Content);

            Assert.AreEqual(404, (int)RestResponse.StatusCode, "Get request should fail for invalid username");
        }

    }
}
