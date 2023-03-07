using OtpNet;

namespace MedokStore.Application.Common.Services
{
    public interface ITestService
    {
        public Totp totpAll { get; set; }
        Totp CreateOPTCode(string key);
        bool ConfirmEmail(Totp totp, string code);
    }
}
