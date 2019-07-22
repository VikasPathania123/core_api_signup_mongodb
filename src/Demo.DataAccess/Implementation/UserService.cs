
namespace Demo.DataAccess.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Demo.Entities;
    using Demo.Infrastructure;
    using Demo.Services;
    using MongoDB.Driver;
    using System.Threading.Tasks;
    using Demo.Infrastructure.Core.Helper;
    using Demo.Infrastructure.Core.Helpers;

    public class UserService : HashPasswordUtility, IUserService
    {

        /// <summary>
        /// The mongo store settings.
        /// </summary>
        private readonly IMongoDbStoreSettings mongoStoreSettings;

        /// <summary>
        /// The mongo db client.
        /// </summary>
        private readonly IMongoClient mongoClient;

        /// <summary>
        /// The mongo db database.
        /// </summary>
        private readonly IMongoDatabase _mongoDatabase;

        public UserService(IMongoDbStoreSettings mongoDbStoreSettings)
        {
            this.mongoClient = new MongoClient(mongoDbStoreSettings.ConnectionString);
            _mongoDatabase = this.mongoClient.GetDatabase(mongoDbStoreSettings.DbName);
            this.mongoStoreSettings = mongoDbStoreSettings;
        }
        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var builder = Builders<User>.Filter;
            var preparedFilter = builder.Eq(x => x.Username, username);

            var user = await _mongoDatabase.GetCollection<User>("User").Find(preparedFilter).FirstOrDefaultAsync();

            // check if username exists
            if (user == null)
            {
                return null;
            }

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            // authentication successful
            return user;
        }

        public async Task<User> Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AppException("Password is required");
            }


            var builder = Builders<User>.Filter;
            var preparedFilter = builder.Eq(x => x.Username, user.Username);

            var _user = await _mongoDatabase.GetCollection<User>("User").Find(preparedFilter).FirstOrDefaultAsync();

            if (_user != null)
                throw new AppException("Username " + user.Username + " is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _mongoDatabase.GetCollection<User>("User").InsertOneAsync(user);

            return user;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(User user, string password = null)
        {
            throw new NotImplementedException();
        }
    }
}
