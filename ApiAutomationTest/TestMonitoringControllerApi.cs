using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Net;
using AlertToCareApi.Models;

namespace ApiAutomationTest
{
    [TestClass]
    public class TestMonitoringControllerApi
    {
        private readonly string baseUrl = "https://localhost:44368/api/monitoring/";

        #region Test Main Functions
        [TestMethod]
        public void TestGetAlarmForAllPatients()
        {
            string url = baseUrl + "HealthStatus";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void TestGetAlarmForParticularPatient()
        {
            string url = baseUrl + "HealthStatus/6";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void TestGetAlarmForParticularPatientWhoseMonitoringStatusIsOff()
        {
            string url = baseUrl + "HealthStatus/11";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [TestMethod]
        public void TestSetAlarmOffForParticularPatient()
        {
            string url = baseUrl + "SetAlarmOff/2";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void TestSetAlarmOffForAnInvalidPatientId()
        {
            string url = baseUrl + "SetAlarmOff/134";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [TestMethod]
        public void TestSetAlarmOnForParticularPatient()
        {
            string url = baseUrl + "SetAlarmOn/2";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void TestSetAlarmOnForAnInvalidPatientId()
        {
            string url = baseUrl + "SetAlarmOn/134";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        #endregion

        #region Test Manipulation Functions
        [TestMethod]
        public void TestGetAllVitalInfo()
        {
            string url = baseUrl + "Vitals";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void TestAddNewVitalInfo()
        {
            string url = baseUrl + "Vitals";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            VitalsLogs vitalLogs = new VitalsLogs { VitalsLogId = 201, PatientId = 1, BpmRate = 30.0, RespRate = 85.0, Spo2Rate = 90.0 };
            request.AddJsonBody(vitalLogs);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void TestUpdateVitalInfoAtParticularVitalLogId()
        {
            string url = baseUrl + "Vitals/1";
            var client = new RestClient(url);
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            VitalsLogs vitalLogs = new VitalsLogs { VitalsLogId = 1, PatientId = 1, BpmRate = 36.0, RespRate = 85.0, Spo2Rate = 90.0 };
            request.AddJsonBody(vitalLogs);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void TestDeleteVitalInfoAtParticularVitalLogId()
        {
            string url = baseUrl + "Vitals/201";
            var client = new RestClient(url);
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        #endregion
    }
}

