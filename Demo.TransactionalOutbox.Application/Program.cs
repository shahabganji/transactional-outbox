using Demo.TransactionalOutbox.Application;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(Startup.ConfigureServices)
    .Build();


await host.RunAsync();
