using Newtonsoft.Json;
using Report.Model.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Report.BL
{
    public class EmpSalaryService : IEmpSalaryService
    {
        public int Salary(EmployeeReportBlock employee)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://salary.local/api/empcode/"+employee.Inn);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new { employee.BuhCode });
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var reader = new System.IO.StreamReader(httpResponse.GetResponseStream(), true);
            string responseText = reader.ReadToEnd();
            return (int)Decimal.Parse(responseText);
        }
    }
}
