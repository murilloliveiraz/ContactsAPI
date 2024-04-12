﻿using ContatosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContatosAPI.Data
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options): base(options) 
        {
            
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
