using OtpNet;
using System.Text;

namespace MedokStore.Application.Common.Services
{
    public class TestService : ITestService
    {


        public Totp totpAll { get; set; }
        Totp ITestService.CreateOPTCode(string key)
        {
            byte[] secretKey = Encoding.ASCII.GetBytes(key);
            var totp = new Totp(secretKey, mode: OtpHashMode.Sha256, step: 20, totpSize: 6);
            totpAll = totp;
            return totp;
        }
        bool ITestService.ConfirmEmail(Totp totp, string code)
        {

            long timeStepMatched;
            return totp.VerifyTotp(code, out timeStepMatched, window: null);
        }
    }
}
