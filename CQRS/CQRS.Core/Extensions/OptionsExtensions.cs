using Microsoft.Extensions.Configuration;

namespace CQRS.Core.Extensions
{
    public static class OptionsExtensions
    {
        public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string name = null) where TOptions : class, new()
        {
            if (string.IsNullOrEmpty(name))
                name = typeof(TOptions).Name;

            IConfigurationSection optionsConfig = configuration.GetSection(name);

            TOptions options = new TOptions();
            optionsConfig.Bind(options);

            return options;
        }
    }
}