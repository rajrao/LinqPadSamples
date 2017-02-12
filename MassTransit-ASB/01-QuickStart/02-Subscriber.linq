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
			h.TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey", "xxxxxxxxxx", TokenScope.Namespace);
		});
	});

	bus.Start();

	string command = "S";

	while (string.Compare(command, "S", true) == 0)
	{
		var message = new RemoteExecutionContext()
		{
			OperationId = Guid.NewGuid(),
			MessageName = "Test",
			PrimaryEntityName = "TestEntity By Raj",
			OperationCreatedOn = DateTime.Now
		};
		bus.Publish<RemoteExecutionContext>(message);
		Console.WriteLine("\"S\" to send anothe message, any other key to quit");
		command = Console.ReadLine();
	}
	bus.Stop();
	Console.WriteLine("Done!");
	Console.ReadLine();
}