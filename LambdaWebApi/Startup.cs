using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace LambdaWebApi;

public class Startup
{
    private readonly IWebHostEnvironment _env;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        _env = env;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();

        if (_env.IsDevelopment())
        {
            services.AddTransient<IAmazonDynamoDB>(sp =>
            {
                var clientConfig = new AmazonDynamoDBConfig { ServiceURL = "http://localhost:8000", UseHttp = true, };
                return new AmazonDynamoDBClient("myaccesskey", "mysecretkey", clientConfig);
            });
        }
        else
        {
            services.AddAWSService<IAmazonDynamoDB>();
        }

        services.AddTransient<IDynamoDBContext, DynamoDBContext>((serviceProvider) =>
        {
            IAmazonDynamoDB amazonDynamoDBClient = serviceProvider.GetRequiredService<IAmazonDynamoDB>();
            DynamoDBContextConfig dynamoDBContextConfig = new DynamoDBContextConfig { SkipVersionCheck = true };
            return new DynamoDBContext(amazonDynamoDBClient, dynamoDBContextConfig);
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}