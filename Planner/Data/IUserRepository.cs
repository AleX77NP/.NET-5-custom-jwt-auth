using System;
using System.Collections;
using Planner.Models;

namespace Planner.Data
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetByEmail(string email);
        User GetById(int id);
    }
}
