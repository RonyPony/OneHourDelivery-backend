namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Configuration
{
    public class ApiConfiguration
    {
        public int AllowedClockSkewInMinutes { get; set; } = 5;

        public string SecurityKey { get; set; } = "NowIsTheTimeForAllGoodMenToComeToTheAideOfTheirCountry";
    }
}
