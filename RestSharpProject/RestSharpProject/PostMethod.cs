using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace RestSharpProject
{
    [TestClass]
    public class PostMethod
    {
       [TestMethod]
        public void PostAPICreateUser()
        {
            string url = "https://petstore.swagger.io/v2/user";
            IRestClient Restclient = new RestClient();
            IRestRequest RestRequest = new RestRequest(url);
            
            JObject jObjectbody = new JObject();
            jObjectbody.Add("username", "Adda");
            jObjectbody.Add("firstName", "Ada");
            jObjectbody.Add("lastName", "Maria");
            jObjectbody.Add("email", "Ada.maria@gmail.com");
            jObjectbody.Add("password", "test@123");
            jObjectbody.Add("phone","02233212");
            jObjectbody.Add("userStatus", 0);

            RestRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            IRestResponse RestResponse = Restclient.Post(RestRequest);
            
            System.Console.WriteLine("Create new user status code: " +RestResponse.StatusCode);
            System.Console.WriteLine("Create new user response content: " +RestResponse.Content);

            Assert.AreEqual(200, (int)RestResponse.StatusCode, "Post request failed !");

        }



        [TestMethod]
        public void PostAPICreateUser_ForInvalidContentType()
        {
            string url = "https://petstore.swagger.io/v2/user";
            IRestClient Restclient = new RestClient();
            IRestRequest RestRequest = new RestRequest(url);

            JObject jObjectbody = new JObject();
            jObjectbody.Add("username", "Mohan");
            jObjectbody.Add("firstName", "Mohan");
            jObjectbody.Add("lastName", "Singh");
            jObjectbody.Add("email", "MohanSingh@gmail.com");
            jObjectbody.Add("password", "Mohan@123");
            jObjectbody.Add("phone", "02233212");
            jObjectbody.Add("userStatus", 0);

            RestRequest.AddParameter("text/html", jObjectbody, ParameterType.RequestBody);
            IRestResponse RestResponse = Restclient.Post(RestRequest);

            System.Console.WriteLine("Create new user with invalid content type status: " +RestResponse.StatusCode);

            Assert.AreEqual(415, (int)RestResponse.StatusCode, "Post request did not fail for invalid content type");

        }
    }
}
