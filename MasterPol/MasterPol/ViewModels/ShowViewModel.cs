using System;
using System.Collections.Generic;
using System.Linq;
using MasterPol.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace MasterPol.ViewModels
{
    public class ShowViewModel : ViewModelBase
    {
        public List<Partner> _partnerList;
        public List<Partner> PartnerList { get => _partnerList; set => this.RaiseAndSetIfChanged(ref _partnerList, value); }
        public ShowViewModel()
        {
            Load();

        }
        private void Load()
        {
            PartnerList = MainWindowViewModel.myConnection.Partners.
            Include(x => x.PartnerType).
            Include(x => x.PartnersProducts).
            ThenInclude(x => x.Product).ToList();
        }
        public void ToAdd()
        {
            MainWindowViewModel.Instance.PageContent = new Add();
        }
        public void ToUpdate(int Id)
        {
            MainWindowViewModel.Instance.PageContent = new Add(Id);
        }
        public void ToSale(int Id)
        {
            MainWindowViewModel.Instance.PageContent = new Sale_history(Id);
        }
        public async void Delete(int Id)
        {
            var delete = MainWindowViewModel.myConnection.Partners.Include(x => x.PartnersProducts).FirstOrDefault(x => x.Id == Id);
            if (delete != null)
            {
                MainWindowViewModel.myConnection.PartnersProducts.RemoveRange(delete.PartnersProducts);
                MainWindowViewModel.myConnection.Partners.Remove(delete);
                await MainWindowViewModel.myConnection.SaveChangesAsync();
                Load();

            }
        }
    }
}