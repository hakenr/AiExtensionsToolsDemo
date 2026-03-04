using System.ComponentModel;
using Microsoft.Extensions.AI;

IChatClient client =
	new Azure.AI.OpenAI.AzureOpenAIClient(
			new Uri("https://hakenoai.openai.azure.com/"),
			new System.ClientModel.ApiKeyCredential("****API-KEY****"))
		.GetChatClient("gpt-4o")
		.AsIChatClient()

		.AsBuilder()
		.UseFunctionInvocation()
		.Build();

var options = new ChatOptions()
{
	Tools = [
		AIFunctionFactory.Create(GetCurrentTime),
		//AIFunctionFactory.Create(Multiply)
	],
};

var response = await client.GetResponseAsync("What time is it?", options);
//var response = await client.GetResponseAsync("How much is 9756 multiplied by 7136?", options);

Console.WriteLine(response.Text);


[Description("Gets the current date and time.")]
static string GetCurrentTime()
{
	return DateTime.Now.ToString();
}

[Description("Multiplies two integers and returns the result.")]
static int Multiply([Description("The first integer.")] int a, [Description("The second integer.")] int b)
{
	return a * b;
}