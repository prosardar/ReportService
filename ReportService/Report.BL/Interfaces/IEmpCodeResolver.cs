using System.Threading.Tasks;

namespace Report.BL
{
    public interface IEmpCodeResolver
    {
        Task<string> GetCode(string inn);
    }
}