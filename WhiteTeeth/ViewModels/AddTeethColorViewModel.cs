﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using WhiteTeeth.Interfaces;
using WhiteTeeth.Models;
using Xamarin.Forms;

namespace WhiteTeeth.ViewModels
{
    public class AddTeethColorViewModel : BaseViewModel
    {

        public readonly ITeethColor _TeethColorService;
        private int teethcolor_id;
        private string color;
        private DateTime date;
        private string image;
        private byte[] byteImage;
        private string client;

        public AddTeethColorViewModel(ITeethColor teethColorService)
        {
            _TeethColorService = teethColorService;
            SaveTeethColorCommand = new Command(async () => await SaveTeethcolor());

        }

        private async Task SaveTeethcolor()
        {
           
            try
            {
                var content = new MultipartFormDataContent();
                content.Add(new ByteArrayContent(byteImage), "image", "image.jpg");
                content.Add(new StringContent(color), "color");
                content.Add(new StringContent($"http://whitesite.fly.dev/client/{client}/"), "client");
                var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                content.Add(new StringContent(currentDate), "date");

                await _TeethColorService.AddTeethColor(content);

                NavigateToHome?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                NavigateToHome?.Invoke(this, EventArgs.Empty);
            }
        }
        

        public string Color
        {
            get => color;
            set
            {
                color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public string Client
        {
            get => client;
            set
            {
                client = value;
                OnPropertyChanged(nameof(Client));
            }
        }

        public string _Image
        {
            get => image;
            set
            {
                image = value;
                OnPropertyChanged(nameof(_Image));
            }
        }

        public byte[] ByteImage
        {
            get => byteImage;
            set
            {
                byteImage = value;
                OnPropertyChanged(nameof(ByteImage));
            }
        }

        public ICommand SaveTeethColorCommand { get; }
        public event EventHandler<EventArgs> NavigateToHome;
    }

}