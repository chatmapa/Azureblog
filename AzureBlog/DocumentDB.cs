using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using Microsoft.Azure.Documents.Client;

using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace AzureBlog
{
    public static class DocumentDb
    {
        private static readonly string _dataBase = "blogDB";//"chathurablog.documents.azure.com";
        private static readonly string _imageCollection = "images";
        private static readonly string _commentsCollection = "Comments";

        private static readonly DocumentClient _client;
        static DocumentDb()
        {
            var url = ConfigurationManager.AppSettings["documentdb:url"];
            var key = ConfigurationManager.AppSettings["documentdb:key"];
            _client = new DocumentClient(new Uri(url), key);
        }

        public static async void SetUp()
        {
            await _client.OpenAsync();
            Database database = _client.CreateDatabaseQuery().Where(db => db.Id == _dataBase).ToArray().FirstOrDefault();
            if (database == null)
            {
                database = await _client.CreateDatabaseAsync(
                    new Database { Id = _dataBase });
            }
            DocumentCollection commentInfo = new DocumentCollection
            {
                Id = _commentsCollection,
                IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 })
            };

            await _client.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri(_dataBase), commentInfo);
        }

        public static IList<Comment> Initialize()
        {
            //get all comments and display
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            IQueryable<Comment> commentQuery = _client.CreateDocumentQuery<Comment>(
                    UriFactory.CreateDocumentCollectionUri(_dataBase, _commentsCollection), queryOptions)
                    .Where(c => c.UserInfo.UserId == "chathura");

            return commentQuery.ToList();

            // The query is executed synchronously here, but can also be executed asynchronously via the IDocumentQuery<T> interface
            //foreach (Comment comment in commentQuery)
            //{
            //    Console.WriteLine("\tRead {0}", family);
            //}

           
            
            //// Now execute the same query via direct SQL
            //IQueryable<Comment> commentQueryInSql = _client.CreateDocumentQuery<Comment>(
            //        UriFactory.CreateDocumentCollectionUri(_dataBase, _commentsCollection),
            //        "SELECT * FROM Comment WHERE Comment.UserInfo.UserId = 'chathura'",
            //        queryOptions);

            //Console.WriteLine("Running direct SQL query...");
            //foreach (Comment comment in commentQuery)
            //{
            //    Console.WriteLine("\tRead {0}", family);
            //}
        }

 

 

        public static async Task CreateDocument(string text)
        {
            //https://azure.microsoft.com/en-us/documentation/articles/documentdb-get-started-quickstart/
            //http://chathurablog.azurewebsites.net
            //work on update document - reply  
            //searches
            //user profile - sql
            //user images
            //try
            //{
            var comment = new Comment
                {
                    Id = Guid.NewGuid().ToString().ToLower(),
                    //Epoch = DateTime.UtcNow.Ticks,
                    //Name  = blogName,
                    Text = text,
                    Replies = new List<Reply>(),
                    Tags = new List<string>(),
                    UserInfo = new UserInfo {UserId = "chathura",CreatedOn = DateTime.UtcNow } 
                };


                await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_dataBase, _commentsCollection), comment);

 
            //}
            //catch (DocumentClientException de)
            //{
            //    // If the document collection does not exist, create a new collection
            //    //if (de.StatusCode == HttpStatusCode.NotFound)
            //    //{
            //    //    DocumentCollection collectionInfo = new DocumentCollection
            //    //    {
            //    //        Id = _commentsCollection,
            //    //        IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) {Precision = -1})
            //    //    };

            //    //    // Configure collections for maximum query flexibility including string range queries.

            //    //    // Here we create a collection with 400 RU/s.
            //    //    await _client.CreateDocumentCollectionAsync(
            //    //        UriFactory.CreateDatabaseUri(_dataBase),
            //    //        collectionInfo,
            //    //        new RequestOptions { OfferThroughput = 400 });

            //    //    //this.WriteToConsoleAndPromptToContinue("Created {0}", collectionName);
            //    //}
            //    //else
            //    //{
            //        throw;
            //    //}
            //}
        }

    }

    public class Comment
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString().ToLower();
        //public string Name { get; set; }
        public string Text { get; set; }
        public List<string> Tags { get; set; }
        public UserInfo UserInfo { get; set; }
        public List<Reply> Replies { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        //public long Epoch { get; set; }
    }

    public class Reply
    {
        public string Id { get; set; } = Guid.NewGuid().ToString().ToLower();
        public string Text { get; set; }
        public UserInfo UserInfo { get; set; }
    }

    public class UserInfo
    {
        public string UserId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}