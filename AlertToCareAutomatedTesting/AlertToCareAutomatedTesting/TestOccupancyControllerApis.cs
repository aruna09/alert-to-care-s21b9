using AlertToCareAutomatedTesting.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Net;

namespace AlertToCareAutomatedTesting
{
    [TestClass]
    public class TestOccupancyControllerApis
    {
        private readonly string baseUrl = "https://localhost:44368/api/occupancy/";

        #region Test Main Functions
        [TestMethod]
        public void TestAvailableBedsApi()
        {
            string url = baseUrl + "AvailableBeds";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void TestAvailableBedsApiWithIncorrectUrl()
        {
            string url = baseUrl + "AvailableBed";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
        [TestMethod]
        public void TestBedOccupancyStatusApi()
        {
            string url = baseUrl + "Status/1";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void TestBedOccupancyStatusApiWithIncorrectUrl()
        {
            string url = baseUrl + "Stats/1";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
        [TestMethod]
        public void TestBedOccupancyStatusApiWithInvalidBedId()
        {
            string url = baseUrl + "Status/121";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void TestAddPatientInfoApi()
        {
            string url = baseUrl + "PatientInfo";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            Patients patientInfo = new Patients { PatientId = 27, PatientName = "Nikita Kumari", Age = 23, ContactNo = "9826376268" };
            request.AddJsonBody(patientInfo);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void TestAddPatientInfoApiWithExistingPatientId()
        {
            string url = baseUrl + "PatientInfo";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            Patients patientInfo = new Patients { PatientId = 1, PatientName = "Nikita Kumari", Age = 23, ContactNo = "9826376268" };
            request.AddJsonBody(patientInfo);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
        [TestMethod]
        public void TestAddPatientInfoWithInvalidContactNo()
        {
            string url = baseUrl + "PatientInfo";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            Patients patientInfo = new Patients { PatientId = 1, PatientName = "Nikita Kumari", Age = 23, ContactNo = "982637668" };
            request.AddJsonBody(patientInfo);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [TestMethod]
        public void TestAddPatientInfoWithInvalidName()
        {
            string url = baseUrl + "PatientInfo";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            Patients patientInfo = new Patients { PatientId = 1, PatientName = "Nikita@. Kumari", Age = 23, ContactNo = "9826375668" };
            request.AddJsonBody(patientInfo);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [TestMethod]
        public void TestDeletingPatientInfo()
        {
            string url = baseUrl + "PatientInfo/27";
            var client = new RestClient(url);
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void TestDeletingPatientInfoWithInvalidPatientId()
        {
            string url = baseUrl + "PatientInfo/123";
            var client = new RestClient(url);
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        #endregion

        #region Test Manipulation Functions
        [TestMethod]
        public void TestGetAllPatientInfoApi()
        {
            string url = baseUrl + "PatientInfo";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void TestGetParticularPatientInfoApi()
        {
            string url = baseUrl + "PatientInfo/2";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void TestGetParticularPatientInfoApiWithInvalidPatientId()
        {
            string url = baseUrl + "PatientInfo/134";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        #endregion

    }
}
