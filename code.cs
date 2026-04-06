var client = new HttpClient();
var response = await client.GetStringAsync("https://www.google.com");
Console.WriteLine(response);