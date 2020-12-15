using System;
using System.Collections.Generic;
using System.Linq;
using Michaelsoft.ContentManager.Server.DatabaseModels;
using Michaelsoft.ContentManager.Server.Exceptions;
using Michaelsoft.ContentManager.Server.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Michaelsoft.ContentManager.Server.Services
{
    public class ContentService
    {

        private readonly IMongoCollection<DbContent> _contents;

        private readonly DatabaseEncryptionService _encryptionService;

        public ContentService(IContentStoreDatabaseSettings settings,
                              DatabaseEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _contents = database.GetCollection<DbContent>(settings.ContentsCollectionName);
        }

        public List<DbContent> GetAll() => _contents.Find(content => true).ToList();

        private DbContent GetById(string id) => _contents.Find<DbContent>(content => content.Id == id).FirstOrDefault();

        public DbContent Create(DbContent content)
        {
            //TODO: Hash content and save hashes to index it
            content.Created = DateTime.Now;
            content.Updated = DateTime.Now;

            _contents.InsertOne(content);
            return content;
        }

        public void Delete(string id)
        {
            var content = GetById(id);
            if (content == null) throw new ContentNotFoundException();
            _contents.DeleteOne(c => c.Id == content.Id);
        }

        public DbContent Get(string id)
        {
            var content = GetById(id);
            if (content == null) throw new ContentNotFoundException();
            return content;
        }

        public void Update(string id,
                           DbContent newContent)
        {
            var content = GetById(id);
            if (content == null) throw new ContentNotFoundException();
            content.Updated = DateTime.Now;

            // TODO: Map data to content
            _contents.ReplaceOne(c => c.Id == content.Id, content);
        }

    }
}