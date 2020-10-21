using AlertToCareApi.Models;
using System.Linq;
using Xunit;

namespace AlertToCareUnitTest
{
    public class VitalsMonitoringUnitTest
    {
        [Fact]
        public void WhenCheckVitalsIsGivenCorrectLogItReturnsAString()
        {
            var apiClassVitalsMonitoring = new AlertToCareApi.Utilities.VitalsMonitoring();
            //var _context = new AlertToCareApi.ConfigDbContext();
            //VitalsLogs Log = _context.VitalsLogs.ToList().FirstOrDefault();

            VitalsLogs log = new VitalsLogs();
            log.VitalsLogId = 1;
            log.PatientId = 22;
            log.Spo2Rate = 95;
            log.RespRate = 7;
            log.BpmRate = 78;

            string ans = apiClassVitalsMonitoring.CheckVitals(log);
            string[] arr = ans.Split(",").ToArray();

            Assert.True(ans.Length > 1);
            Assert.True(arr.Length == 5);
        }


        [Fact]
        public void WhenCheckVitalsIsGivenLogThatDoesNotExistInDatabaseItReturnsAnError()
        {
            var apiClassVitalsMonitoring = new AlertToCareApi.Utilities.VitalsMonitoring();
            VitalsLogs log = new VitalsLogs();
            log.VitalsLogId = 1;
            log.PatientId = 90;
            log.Spo2Rate = 95;
            log.RespRate = 7;
            log.BpmRate = 78;

            string ans = apiClassVitalsMonitoring.CheckVitals(log);
            string[] arr = ans.Split(',');

            Assert.True(ans.Length > 1);
            bool result = ans.Contains("Error");
            Assert.True(result);

            Assert.False(arr.Length == 5);

        }
    }
}
