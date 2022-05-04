using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;

namespace Bookstore
{
    public class Book
    {
        public int Id {get;set;}
        public string Title {get; set;}
        public string Description {get;set;}
        public string Author {get;set;}
    }

    public class CreateBook
    {
        public string Title {get; set;}
    }

    public class UpdateBook{
        public string Title {get; set;}
        public string Description {get;set;}
        public string Author {get;set;}
    }

    public static class BookstoreFunctions
    {
        public static readonly List<Book> Bookitems = new List<Book>();
	public static int genID = 0;
        [FunctionName("CreateBook")]
        public static async Task<IActionResult> CreateBook(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
    ILogger log)
        {
            log.LogInformation($"Getting all books from the database.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<CreateBook>(requestBody);

            var book = new Book() {Title = input.Title};
	    genID++;
	    book.Id=genID;
            Bookitems.Add(book);
            return new OkObjectResult(book);
        }

        [FunctionName("GetBooks")]
        public static async Task<IActionResult> GetBooks( 
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "book")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation($"Getting all the books");
            return new OkObjectResult(Bookitems);
        }

        [FunctionName("GetBookById")]
        public static async Task<IActionResult> GetBookById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "book/{id}")]HttpRequest req,
    ILogger log, int id)
        {
            var book = Bookitems.FirstOrDefault(b => b.Id ==id);
            if (book == null){
                return new NotFoundResult();
            }

            return new OkObjectResult(book);
        }

        [FunctionName("UpdateBook")]
        public static async Task<IActionResult> UpdateBook(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "book/{id}")]HttpRequest req,
    ILogger log, int id)
        {
            var book = Bookitems.FirstOrDefault(b => b.Id ==id);
            if (book == null){
                return new NotFoundResult();
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updatedBook = JsonConvert.DeserializeObject<UpdateBook>(requestBody);

            book.Title = updatedBook.Title;

            if (!string.IsNullOrEmpty(updatedBook.Description))
            {
                book.Description = updatedBook.Description;
            }

            if (!string.IsNullOrEmpty(updatedBook.Author))
            {
                book.Author = updatedBook.Author;
            }

            return new OkObjectResult(book);
        }

        [FunctionName("DeleteBook")]
        public static async Task<IActionResult> DeleteBook(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "book/{id}")]HttpRequest req,
    ILogger log, int id)
        {
            var book = Bookitems.FirstOrDefault(b => b.Id == id);
            if(book==null)
            {
                return new NotFoundResult();
            }
            Bookitems.Remove(book);
            return new OkResult();
        }
    }
}
