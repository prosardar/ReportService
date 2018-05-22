using Report.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Report.BL
{
    public class EmpCodeResolver : IEmpCodeResolver
    {
        public async Task<string> GetCode(string inn)
        {
            var client = new HttpClient();
            return await client.GetStringAsync("http://buh.local/api/inn/" + inn);
        }
    }
}
