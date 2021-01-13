using System;
using System.Collections.Generic;
using System.Linq;
using Michaelsoft.ContentManager.Common.Extensions;
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

        private DbContent GetByHashes(string titleHash,
                                      string textContentHash,
                                      string htmlContentHash) =>
            _contents.Find<DbContent>(content => content.TitleHash == titleHash &&
                                                 content.TextContentHash == textContentHash &&
                                                 content.HtmlContentHash == htmlContentHash).FirstOrDefault();

        private bool CheckUrlFriendlyTitle(string urlFriendlyTitle) =>
            _contents.Find<DbContent>(content => content.UrlFriendlyTitle == urlFriendlyTitle).CountDocuments() > 0;

        public DbContent GetByUrlFriendlyTitle(string urlFriendlyTitle) =>
            _contents.Find<DbContent>(content => content.UrlFriendlyTitle == urlFriendlyTitle).FirstOrDefault();

        public DbContent Create(DbContent content)
        {
            var existingContent = GetByHashes(content.TitleHash, content.TextContentHash, content.HtmlContentHash);
            if (existingContent != null) return existingContent;

            content.UrlFriendlyTitle = content.Title.ToUrlFriendly();
            if (CheckUrlFriendlyTitle(content.UrlFriendlyTitle))
                content.UrlFriendlyTitle += "_" + StringHelper.RandomString(16, uppercase: false);

            content.TitleHash = content.Title.Sha1();
            content.HtmlContentHash = content.HtmlContent.Sha1();
            content.TextContentHash = content.TextContent.Sha1();
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

        public DbContent Update(string id,
                                DbContent newContent)
        {
            var content = GetById(id);
            if (content == null) throw new ContentNotFoundException();

            content.Updated = DateTime.Now;

            if (!newContent.Type.IsNullOrEmpty() && newContent.Type != content.Type)
            {
                content.Type = newContent.Type;
            }

            if (!newContent.Locale.IsNullOrEmpty() && newContent.Locale != content.Locale)
            {
                content.Locale = newContent.Locale;
            }
            
            if (newContent.Published != content.Published)
            {
                content.Published = newContent.Published;
            }

            if (!newContent.Title.IsNullOrEmpty() && newContent.Title != content.Title)
            {
                content.Title = newContent.Title;
                content.TitleHash = newContent.Title.Sha1();
                content.UrlFriendlyTitle = newContent.Title.ToUrlFriendly();
                if (CheckUrlFriendlyTitle(content.UrlFriendlyTitle))
                    content.UrlFriendlyTitle += "_" + StringHelper.RandomString(16, uppercase: false);
            }

            if (!newContent.Subtitle.IsNullOrEmpty() && newContent.Subtitle != content.Subtitle)
            {
                content.Subtitle = newContent.Subtitle;
            }

            if (!newContent.TextContent.IsNullOrEmpty() && newContent.TextContent != content.TextContent)
            {
                content.TextContent = newContent.TextContent;
                content.TextContentHash = newContent.TextContent.Sha1();
            }

            if (!newContent.HtmlContent.IsNullOrEmpty() && newContent.HtmlContent != content.HtmlContent)
            {
                content.HtmlContent = newContent.HtmlContent;
                content.HtmlContentHash = newContent.HtmlContent.Sha1();
            }

            _contents.ReplaceOne(c => c.Id == content.Id, content);
            return content;
        }

    }
}