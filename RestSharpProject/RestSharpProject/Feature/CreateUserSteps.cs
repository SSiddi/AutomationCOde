using TechTalk.SpecFlow;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace RestSharpProject.Feature
{
    [Binding]
    public class CreateUserSteps
    {
        private const string url = "https://petstore.swagger.io/v2/user";
        IRestClient Restclient = new RestClient();
        IRestRequest RestRequest = new RestRequest(url);
        IRestResponse RestResponse;
        JObject jObjectbody;

        [Given(@"I input name,lastname, emailid, password")]
        public void GivenIInputNameLastnameEmailidPassword()
        {
            jObjectbody = new JObject();
            jObjectbody.Add("username", "Adda");
            jObjectbody.Add("firstName", "Ada");
            jObjectbody.Add("lastName", "Maria");
            jObjectbody.Add("email", "Ada.maria@gmail.com");
            jObjectbody.Add("password", "test@123");
            jObjectbody.Add("phone", "02233212");
            jObjectbody.Add("userStatus", 0);
        }
        
        [When(@"I create the user")]
        public void WhenICreateTheUser()
        {
            RestRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            RestResponse = Restclient.Post(RestRequest);
        }
        
        [Then(@"user is created")]
        public void ThenUserIsCreated()
        {
            System.Console.WriteLine("Create new user status code: " + RestResponse.StatusCode);
            System.Console.WriteLine("Create new user response content: " + RestResponse.Content);

            Assert.AreEqual(200, (int)RestResponse.StatusCode, "Post request failed !");
        }
    }
}
