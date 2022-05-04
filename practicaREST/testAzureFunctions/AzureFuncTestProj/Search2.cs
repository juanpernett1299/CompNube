using System;
using System.IO;
using System.Collections.Generic;  
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFuncTestProj
{
    public static class Search
    {
        [FunctionName("Search")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

        	string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(queryByName(name)))
                : new BadRequestObjectResult("Please pass a name on the query string");	
            
        }

        public class Person
	    {
	    	private string name, surname;

			public string Name { get { return name; } }
			public string Surname { get { return surname; } }

	    	public Person(string name, string surname){
	    		this.name = name;
	    		this.surname = surname;
	    	}	
		}

        /// <summary>
        /// Returns a list of people, for debug purposes
        /// </summary>
		private static List<Person> buildPersonList()
		{
	    	List<Person> list = new List<Person>();
	    	list.Add(new Person("Lucas", "Monastirsky"));
	    	list.Add(new Person("Sofia", "Vazquez"));
	    	list.Add(new Person("Ricardo", "Gutierrez"));
	    	list.Add(new Person("Lucio", "Flores"));
	    	return list;
		}

        /// <summary>
        /// Returns all people with a first name that contains the argument name
        /// </summary>
		private static List<Person> queryByName(string name)
		{
			List<Person> all_people = buildPersonList();
			List<Person> list = new List<Person>();
			all_people.ForEach(delegate(Person person){
				if(person.Name.ToLower().Contains(name))
					list.Add(person); });
			return list;
		}

    }
    
}
