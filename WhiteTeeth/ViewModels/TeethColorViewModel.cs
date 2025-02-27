﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using WhiteTeeth.Interfaces;
using WhiteTeeth.Models;
using WhiteTeeth.Views;
using Xamarin.Forms;

namespace WhiteTeeth.ViewModels
{
	public class TeethColorViewModel : BaseViewModel
    {
        public ObservableCollection<TeethColor> teethColors;
        private readonly ITeethColor _teethColorService;
        
        public TeethColorViewModel(ITeethColor TeethColorService)
		{
            _teethColorService = TeethColorService;

            TeethColors = new ObservableCollection<TeethColor>();

            DeleteTeethColorCommand = new Command<TeethColor>(async b => await DeleteTeethColor(b));


        }

        private async Task DeleteTeethColor(TeethColor b)
        {
            int lastSlashIndex = b.client.LastIndexOf("/");
            string numberString = b.client.Substring(lastSlashIndex - 1, 1);
            await _teethColorService.DeleteTeethColor(b);

            await PopulateTeethColors(numberString);
        }
        
        public async Task PopulateTeethColors(string client_id)
        {
            try
            {
                TeethColors.Clear();
                var teethColors = await _teethColorService.GetTeethColorsByClient(client_id);
                
                foreach (var teethColor in teethColors)
                {
                    TeethColors.Add(teethColor);
                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public ObservableCollection<TeethColor> TeethColors
        {
            get => teethColors;
            set
            {
                teethColors = value;
                OnPropertyChanged(nameof(TeethColors));
            }
        }

        public ICommand DeleteTeethColorCommand { get; }


    }
}

