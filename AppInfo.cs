namespace PaymentVietQRCallAPI
{
    public class AppInfo
    {
        public string AppId { get; set; }
        public string AppLogo { get; set; }
        public string AppName { get; set; }
        public string BankName { get; set; }
        public int MonthlyInstall { get; set; }
        public string Deeplink { get; set; }
    }
    public class AppList
    {
        public List<AppInfo> Apps { get; set; }
    }
}
