﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhiteTeeth.Models;

namespace WhiteTeeth.Interfaces
{
	public interface IDentist
	{
        Task<IEnumerable<Dentist>> GetDentists();
        Task<Dentist> GetDentist(int id);
        Task AddDentist(Dentist dentist);
        Task SaveDentist(Dentist dentist);
        Task DeleteDentist(Dentist dentist);
        Task<Dentist> GetUser(string access_token);
    }
}

