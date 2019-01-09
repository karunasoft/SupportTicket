using System.Collections.Generic;
using ST.SharedInterfacesLib;
using ST.SharedUserEntitiesLib;


namespace ST.UserServiceLib
{
    public class UserService<TUsersRepo> :
        IUserService<ISTUsersRepo> where TUsersRepo: ISTUsersRepo
    {
        private readonly ISTUsersRepo _usersRepo;
        private readonly string _jwtSecret;

        public UserService(ISTEnvironment stEnvironment, TUsersRepo usersRepo)
        {
            _usersRepo = usersRepo;
            _jwtSecret = stEnvironment.JWTSecret;
        }

        public User Authenticate(string username, string password)
        {
            var user = _usersRepo.GetUserMatching(username, password);

            // return null if user not found
            if (user == null)
                return null;

            var payload = new Dictionary<string, object>()
            {
                { "claim1", 0 },
                { "claim2", "claim2-value" }
            };

            user.Token = JwtCore.JsonWebToken
                .Encode(payload, _jwtSecret, JwtCore.JwtHashAlgorithm.HS256);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            // return users without passwords
            return _usersRepo.GetAllUsers();
        }

        public User SignUp(User user)
        {
            return _usersRepo.SignUp(user);
        }

        public User Get(int id)
        {
            return _usersRepo.Get(id);
        }
    }
}