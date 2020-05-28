# confy

### Easily Run your applications by using the same externalized config source locally.

```
public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(builder => builder.AddUserSecrets<Program>()) // Configure secrets if present
                .ConfigureAppConfiguration((context, builder) =>
                {
                    if (context.HostingEnvironment.IsDevelopment())
                        builder.AddGitSource(source =>
                        {
                            source.Url = "<<YOUR GIT REPO HERE>>";
                            source.AppName = "<<YOUR GIT REPO APP NAME>>";
                            source.Branch = "<<BRANCH TO CLONE FROM GIT>>";
                            source.AuthTokenEnvironmentVariableName = "<<YOUR GIT'S AUTHTOKEN>>";
                            source.UserNameEnvironmentVariableName = "<<YOUR GIT USERNAME>>";
                            source.CloneOptions = options =>
                            {
                                options.CloneSubDir = "<<SUBDIR TO CLONE TO>>"; // Default directory is platform specific temp dir prefixed with subdir
                                options.AlwaysCloneOnStart = true; // Always clone regardless of whether subdir exists or not, use when running tests
                            };
                        });
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

```

This sample code is with .net core 3.1, currently previous versions are not supported 
