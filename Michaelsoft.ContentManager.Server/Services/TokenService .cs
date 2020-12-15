using System;
using Michaelsoft.ContentManager.Server.DatabaseModels;
using Michaelsoft.ContentManager.Server.Exceptions;
using Michaelsoft.ContentManager.Server.Settings;
using MongoDB.Driver;

namespace Michaelsoft.ContentManager.Server.Services
{
    public class TokenService
    {

        private readonly IMongoCollection<DbToken> _tokens;

        public TokenService(ITokenStoreDatabaseSettings settings,
                            DatabaseEncryptionService encryptionService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _tokens = database.GetCollection<DbToken>(settings.TokensCollectionName);
            var indexKeysDefinition = Builders<DbToken>.IndexKeys.Ascending(t => t.ExpireAt);
            _tokens.Indexes.CreateOne
                (
                 new CreateIndexModel<DbToken>(indexKeysDefinition,
                                               new CreateIndexOptions {ExpireAfter = TimeSpan.Zero})
                );
        }

        private DbToken GetById(string id) => _tokens.Find<DbToken>(token => token.Id == id).FirstOrDefault();

        private DbToken GetByTypeAndAuthor(string type,
                                           string author) =>
            _tokens.Find<DbToken>(t => t.Type == type && t.Author == author).FirstOrDefault();

        private DbToken GetByTypeAndValue(string type,
                                          string value) =>
            _tokens.Find<DbToken>(t => t.Type == type && t.Value == value).FirstOrDefault();

        public DbToken Create(string type,
                              string value,
                              string author,
                              int ttlSeconds)
        {
            var token = new DbToken
            {
                Author = author,
                Type = type,
                Value = value,
                Created = DateTime.Now,
                ExpireAt = DateTime.Now.AddSeconds(ttlSeconds)
            };
            var existing = GetByTypeAndAuthor(type, author);
            if (existing != null) return existing;
            _tokens.InsertOne(token);
            return token;
        }

        public DbToken GetTokenByTypeAuthorAndValue(string type,
                                                    string author,
                                                    string value)
        {
            var token = GetByTypeAndAuthor(type, author);
            if (token == null) throw new TokenNotFoundException();
            if (!value.Equals(token.Value)) throw new InvalidTokenException();
            return token;
        }

        public DbToken GetTokenByTypeAndValue(string type,
                                              string value)
        {
            var token = GetByTypeAndValue(type, value);
            if (token == null) throw new TokenNotFoundException();
            return token;
        }

        public void Delete(string id)
        {
            var token = GetById(id);
            if (token == null) throw new TokenNotFoundException();
            _tokens.DeleteOne(u => u.Id == token.Id);
        }

    }
}