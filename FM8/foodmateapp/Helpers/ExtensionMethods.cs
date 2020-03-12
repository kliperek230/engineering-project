using System.Collections.Generic;
using System.Linq;
using foodmateapp.Model;

namespace foodmateapp.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<Users> WithoutPasswords(this IEnumerable<Users> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static Users WithoutPassword(this Users users)
        {
            users.Pswd = null;
            return users;
        }
    }
}
