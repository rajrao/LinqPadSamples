<Query Kind="Program">
  <NuGetReference>MassTransit</NuGetReference>
  <NuGetReference>MassTransit.AzureServiceBus</NuGetReference>
  <NuGetReference>Microsoft.CrmSdk.CoreAssemblies</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>WindowsAzure.ServiceBus</NuGetReference>
  <Namespace>MassTransit</Namespace>
  <Namespace>MassTransit.AzureServiceBusTransport</Namespace>
  <Namespace>Microsoft.ServiceBus</Namespace>
  <Namespace>Microsoft.Xrm.Sdk</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var bus = Bus.Factory.CreateUsingAzureServiceBus(cfg =>
	{
		var host = cfg.Host(new Uri("sb://anothertest.servicebus.windows.net/"), h =>
		{
			h.OperationTimeout = TimeSpan.FromSeconds(30);
			//replace with your shared access key
			h.TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey","xxxxxxxxxx",TokenScope.Namespace);
		});

		cfg.ReceiveEndpoint(host, "MtPubSubExample_TestSubscriber", e => e.Consumer<RemoteExecutionContextConsumer>());
	});

	bus.Start();

	Console.ReadLine();
	
	bus.Stop();
		
	Console.ReadLine();
}

class RemoteExecutionContextConsumer : IConsumer<RemoteExecutionContext>
{
	public Task Consume(ConsumeContext<RemoteExecutionContext> context)
	{
		Console.Write($"OperationId:  {context.Message.OperationId}");
		Console.Write($"  MessageName: {context.Message.MessageName}");
		Console.Write($"  PrimaryEntityName: {context.Message.PrimaryEntityName}");
		Console.Write($"  OperationCreatedOn: {context.Message.OperationCreatedOn}");
		Console.Write($"  PROCESSED: " + DateTime.Now);
		Console.WriteLine(" (" + System.Threading.Thread.CurrentThread.ManagedThreadId + ")");
		
		Task.Delay(10000).Wait();
		Console.WriteLine("Consumption completed");
		return Task.FromResult(0);
	}
}