using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using UrlShortner.Api.Entities;

namespace UrlShortner.Api.Repository
{


    public class DbRepository : IRepository
    {
        private readonly IMongoCollection<LongUrl> dbCollection;
        private readonly FilterDefinitionBuilder<LongUrl> filterBuilder = Builders<LongUrl>.Filter;

        public DbRepository(IMongoDatabase database)
        {
            dbCollection = database.GetCollection<LongUrl>("longUrls");
        }

        public async Task CreateAsync(LongUrl longUrl, string baseUrl)
        {
            var x = System.DateTime.UtcNow;
            if (longUrl == null)
            {
                throw new ArgumentNullException(nameof(LongUrl));
            }
            // await dbCollection.InsertOneAsync(longUrl);
            longUrl.id = Guid.NewGuid();
            bool isKeyAvailable = false;

            for (int shift = 0; shift <= 10 && !isKeyAvailable; shift++)
            {
                var key = extractKeyFromGuid(longUrl.id, shift++); 
                var lu = await dbCollection.Find<LongUrl>(filterBuilder.Eq(lu => lu.shortUrlId, key)).FirstOrDefaultAsync();
                if(lu == null){
                    longUrl.shortUrlId = key;
                    longUrl.shortUrl = baseUrl + key;
                    await dbCollection.InsertOneAsync(longUrl);
                    isKeyAvailable = true;
                }
            }
            var y = System.DateTime.UtcNow;
            System.Console.WriteLine($"Time taken: {y - x}");
        }
 
        public async Task<string> GetLongUrl(string key){
            var longUrl = await dbCollection.Find<LongUrl>(filterBuilder.Eq(lu => lu.shortUrlId, key)).FirstOrDefaultAsync();
            return longUrl?.longUrl;
        }
        private string extractKeyFromGuid(Guid id, int shift){
            byte[] key = new byte[6];
            var idAr = id.ToByteArray();
            for (int i = 0; i < 6; i++)
            {
                key[i] = idAr[shift + i];
            }
            var keyString = Convert.ToBase64String(key);
            return keyString;
        }
    }
}