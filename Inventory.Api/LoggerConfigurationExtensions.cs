using Serilog;
using Serilog.Settings.Configuration;
using Serilog.Templates;

namespace Inventory.Api;

public static class LoggerConfigurationExtensions
{
    private const string LogTemplate = 
        """
        [{@t:yyyy,MMM dd HH:mm:ss} - {@l:u}]{#if SourceContext is not null} - {SourceContext}{#end}
        {@m:lj}
        {#if @x is not null} @x
        {#end}
        """;

    public static void SetupLogger(this WebApplicationBuilder applicationBuilder)
    {
        applicationBuilder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration, new ConfigurationReaderOptions
            {
                SectionName = "Serilog"
            });
           
            var expressionTemplate = new ExpressionTemplate(LogTemplate);

            configuration.WriteTo.Console(expressionTemplate);
        });
    }
}