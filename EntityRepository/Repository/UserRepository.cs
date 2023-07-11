﻿using EntityRepository.Context;
using EntityRepository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityRepository.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>?> Get()
    {
        List<User> users = await _context.Users.ToListAsync();

        return users is not null ? users : null;
    }

    public async Task<User?> Get(Guid id)
    {
        User? user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

        return user is not null ? user : null;
    }

    public async Task<User> Create(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<bool> Update(User user)
    {
        User? requestedUser = await _context.Users.FindAsync(user.Id);

        if (requestedUser is not null)
        {
            requestedUser.Name = user.Name;

            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<bool> Delete(Guid id)
    {
        User? user = await _context.Users.FindAsync(id);

        if (user is null) 
        {
            return false;
        }

        var entityState = _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return entityState.State == EntityState.Detached ? true : false;
    }
}