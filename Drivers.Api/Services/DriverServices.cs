using Drivers.Api.Configurations;
using Drivers.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Drivers.Api.Services;
//AAAAAA
public class DriverServices
{
    private readonly IMongoCollection<Driver> _driversCollection;
    public DriverServices(
        IOptions<DatabaseSettings> databaseSettings)
        {
            //INICIZALIZAR MI CLIENTE DE MONGO DB
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            //CONECTAR A LA BASE DE DATOS DE MONGO DB
            var mongoDB=
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _driversCollection=
            mongoDB.GetCollection<Driver>
            (databaseSettings.Value.CollectionName);
        }

       public async Task<List<Driver>>GetAsync()=>
    await _driversCollection.Find(_=> true).ToListAsync();

        public async Task<Driver> GetDriverId(string id)
        {
            return await _driversCollection.FindAsync(new BsonDocument
            {{"_id", new ObjectId(id)}}).Result.FirstAsync();
        }

        public async Task InsertDriver(Driver driver)
        {
            await _driversCollection.InsertOneAsync(driver);
        }


        public async Task UpdateDriver(Driver driver)
        {
             var filter = Builders<Driver>.Filter.Eq(s=>s.Id, driver.Id);
            await _driversCollection.ReplaceOneAsync(filter, driver);
        }





         public async Task DeleteDriver(string id)
        {
            var filter = Builders<Driver>.Filter.Eq(s=>s.Id, id);
            await _driversCollection.DeleteOneAsync(filter);
        }






}