using AlertToCareApi.Models;
using System;
using System.Linq;
using Xunit;

namespace AlertToCareUnitTest
{
    public class VitalsMonitoringUnitTest
    {
        [Fact]
        public void WhenCheckVitalsIsGivenCorrectLogItReturnsAString()
        {
            var ApiClassVitalsMonitoring = new AlertToCareApi.Utilities.VitalsMonitoring();
            //var _context = new AlertToCareApi.ConfigDbContext();
            //VitalsLogs Log = _context.VitalsLogs.ToList().FirstOrDefault();

            var Log = new VitalsLogs();
            Log.VitalsLogId = 1;
            Log.PatientId = 22;
            Log.Spo2Rate = 95;
            Log.RespRate = 7;
            Log.BpmRate = 78;

            string ans = ApiClassVitalsMonitoring.CheckVitals(Log);
            string[] arr = ans.Split(",").ToArray();

            Assert.True(ans.Length > 1);
            Assert.True(arr.Length == 5);
        }


        [Fact]
        public void WhenCheckVitalsIsGivenLogThatDoesNotExistInDatabaseItReturnsAnError()
        {
            var ApiClassVitalsMonitoring = new AlertToCareApi.Utilities.VitalsMonitoring();
            var Log = new VitalsLogs();
            Log.VitalsLogId = 1;
            Log.PatientId = 90;
            Log.Spo2Rate = 95;
            Log.RespRate = 7;
            Log.BpmRate = 78;

            string ans = ApiClassVitalsMonitoring.CheckVitals(Log);
            string[] arr = ans.Split(',');

            Assert.True(ans.Length > 1);
            Assert.True(ans.Contains("Error") == true);

            Assert.False(arr.Length == 5);

        }
    }
}
