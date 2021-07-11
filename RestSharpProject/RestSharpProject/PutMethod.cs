using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace RestSharpProject
{
    [TestClass]
    public class PutMethod
    {
        [TestMethod]
        public void PutMethodForUpdateUser()
        {
            string url = "https://petstore.swagger.io/v2/user/{username}";
            IRestClient Restclient = new RestClient();
            IRestRequest RestRequest = new RestRequest(url);

            RestRequest.AddUrlSegment("username", "efwefw");

            JObject jObjectbody = new JObject();
            jObjectbody.Add("username", "Supriya");
            jObjectbody.Add("firstName", "Supriya");
            jObjectbody.Add("lastName", "Singh");
            jObjectbody.Add("email", "SupriyaSingh@gmail.com");
            jObjectbody.Add("password", "Supriya@123");
            jObjectbody.Add("phone", "02233212");
            jObjectbody.Add("userStatus", 0);

            RestRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            IRestResponse RestResponse = Restclient.Put(RestRequest);

            System.Console.WriteLine(RestResponse.StatusCode);
            System.Console.WriteLine(RestResponse.Content);
            Assert.AreEqual(200, (int)RestResponse.StatusCode, "Put failed");
        }

        [TestMethod]
        public void PutMethodWhenUserNameIsNotProvided()

        {
            string url = "https://petstore.swagger.io/v2/user/{username}";
            IRestClient Restclient = new RestClient();
            IRestRequest RestRequest = new RestRequest(url);

            RestRequest.AddUrlSegment("username", "   ");

            JObject jObjectbody = new JObject();
            jObjectbody.Add("username", "Supriya");
            jObjectbody.Add("firstName", "Supriya");
            jObjectbody.Add("lastName", "Singh");
            jObjectbody.Add("email", "SupriyaSingh@gmail.com");
            jObjectbody.Add("password", "Supriya@123");
            jObjectbody.Add("phone", "02233212");
            jObjectbody.Add("userStatus", 0);

            RestRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            IRestResponse RestResponse = Restclient.Put(RestRequest);

            System.Console.WriteLine(RestResponse.StatusCode);
            System.Console.WriteLine(RestResponse.Content);
            Assert.AreEqual(405, (int)RestResponse.StatusCode, "Put method should fail incase of no username");
        }
    }
}
