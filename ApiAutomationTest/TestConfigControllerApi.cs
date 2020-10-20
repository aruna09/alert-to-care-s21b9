using AlertToCareApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Net;

namespace ApiAutomationTest
{
    [TestClass]
    public class TestConfigControllerApi
    {
        private readonly string baseUrl = "https://localhost:44368/api/config/";

        #region Test Main Functions
        [TestMethod]
        public void TestBedsInEachIcuApi()
        {
            string url = baseUrl + "BedsInEachIcu";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void TestBedsInEachIcuApiForIncorrectUrl()
        {
            string url = baseUrl + "BedsInEachIcus";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void TestBedIdentificationApi()
        {
            string url = baseUrl + "Beds/1";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void TestBedIdentificationApiForInvalidBedId()
        {
            string url = baseUrl + "Beds/125";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [TestMethod]
        public void TestBedIdentificationApiForIncorrectUrl()
        {
            string url = baseUrl + "Bed/1";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
        [TestMethod]
        public void TestLayoutInfoApi()
        {
            string url = baseUrl + "Layouts";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void TestLayoutInfoApiForIncorrectUrl()
        {
            string url = baseUrl + "Layout";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
        #endregion

        #region Test Manipulation Functions
        [TestMethod]
        public void TestGetAllBedInfoApi()
        {
            string url = baseUrl + "Beds";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void TestGetAllIcuInfoApi()
        {
            string url = baseUrl + "Icu";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void TestAddBedInfoWithExistingBedId()
        {
            string url = baseUrl + "Beds";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            Beds bedInfo = new Beds { BedId = 1, IcuNo = 3, OccupancyStatus = true };
            request.AddJsonBody(bedInfo);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
        [TestMethod]
        public void TestAddBedInfoApiWithInvalidIcuNo()
        {
            string url = baseUrl + "Beds";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            Beds bedInfo = new Beds { BedId = 1, IcuNo = 291, OccupancyStatus = true };
            request.AddJsonBody(bedInfo);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [TestMethod]
        public void TestAddBedInfoApiWhenGivenIcuIsFull()
        {
            string url = baseUrl + "Beds";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            Beds bedInfo = new Beds { BedId = 11, IcuNo = 10, OccupancyStatus = true };
            request.AddJsonBody(bedInfo);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [TestMethod]
        public void TestAddIcuInfoApiWithExistingIcuNo()
        {
            string url = baseUrl + "Icu";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            Icu icuInfo = new Icu { IcuNo = 1, FloorNo = 3, LayoutId = 4 };
            request.AddJsonBody(icuInfo);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
        [TestMethod]
        public void TestAddNewIcuInfoApiWithInvalidLayoutId()
        {
            string url = baseUrl + "Icu";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            Icu icuInfo = new Icu { IcuNo = 1, FloorNo = 3, LayoutId = 5 };
            request.AddJsonBody(icuInfo);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        #endregion
    }
}
