1. Create an Azure Service Bus namespace. 
   a. Insert the URI in lines where it currently has: sb://anothertest.servicebus.windows.net/
2. Copy the shared access key.
   a. Insert the key where it currently has: xxxxxxxxxx
3. First run the consumer ([01-Consumer.linq](https://github.com/rajrao/LinqPadSamples/blob/master/MassTransit-ASB/01-QuickStart/01-Consumer.linq]))
4. Next run the publisher. ([02-Publisher.linq](https://github.com/rajrao/LinqPadSamples/blob/master/MassTransit-ASB/01-QuickStart/01-Consumer.linq])).

As you publish messages from the publisher (everytime you hit S and hit enter), the consumer will print the details of the message.

###Issues:
When you view the Topics blade in Azure Portal, and you click on the topic named: `"microsoft.xrm.sdk/remoteexecutioncontext"`, it should take you to the blade that shows you details for the selected topic. For some reason, only for the topics created by MT, this page, just keeps showing the loading spinner and it never completes. (It may possibly related to the fact that the MT topic has forwarding enabled?)

I am not a MT expert, just taking it for a spin and wondering why the Azure portal is having an issue showing additional details.
