using System;
using System.Collections.Generic;
using System.Linq;
using MasterPol.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace MasterPol.ViewModels
{
	public class Sale_historyViewModel : ViewModelBase
    {
        private List<PartnersProduct> _partnersProducts;
        public List<PartnersProduct> PartnersProducts
        {
            get => _partnersProducts;
            set => this.RaiseAndSetIfChanged(ref _partnersProducts, value);
        }

        public Sale_historyViewModel(int partnerId)
        {
            if (partnerId != 0)
            {
                PartnersProducts = MainWindowViewModel.myConnection.PartnersProducts
                    .Include(x => x.Product)
                    .Include(x => x.Partner)
                    .Where(x => x.Partner.Id == partnerId)
                    .ToList();
            }
            else
            {
                PartnersProducts = new List<PartnersProduct>();
            }
        }
        public void Exit()
        {
            MainWindowViewModel.Instance.PageContent = new Show();
        }


    }
}