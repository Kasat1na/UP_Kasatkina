using System;
using System.Collections.Generic;
using System.Linq;
using MasterPol.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Avalonia.Controls;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;

namespace MasterPol.ViewModels
{
    public class AddViewModel : ViewModelBase
    {
        private Partner? _newPartner;
        public Partner? NewPartner
        {
            get => _newPartner;
            set => this.RaiseAndSetIfChanged(ref _newPartner, value);
        }

        private List<PartnerType> _partnerTypes;
        public List<PartnerType> PartnerTypes
        {
            get => _partnerTypes;
            set => this.RaiseAndSetIfChanged(ref _partnerTypes, value);
        }

        public AddViewModel()
        {
            LoadPartnerTypes();
            NewPartner = new Partner();
        }

        public AddViewModel(int Id)
        {
            LoadPartnerTypes();

            if (Id != 0)
            {
                NewPartner = MainWindowViewModel.myConnection.Partners
                    .Include(x => x.PartnerType)
                    .FirstOrDefault(x => x.Id == Id) ?? new Partner();
            }
            else
            {
                NewPartner = new Partner();
            }
        }

        private void LoadPartnerTypes()
        {
            PartnerTypes = MainWindowViewModel.myConnection.PartnerTypes.ToList();
        }

        public async void SavePartner()
        {
            if (NewPartner == null) return;

            // Проверка обязательных полей
            if (string.IsNullOrWhiteSpace(NewPartner.PartnerName))
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Поле 'Наименование' обязательно для заполнения.", ButtonEnum.Ok).ShowAsync();
                return; 
            }

            if (NewPartner.Rating == 0)
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Поле 'Рейтинг' обязательно для заполнения.", ButtonEnum.Ok).ShowAsync();
                return; 
            }

            if (string.IsNullOrWhiteSpace(NewPartner.LegalAddress))
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Поле 'Адрес' обязательно для заполнения.", ButtonEnum.Ok).ShowAsync();
                return; 
            }

            if (string.IsNullOrWhiteSpace(NewPartner.DirectorName))
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Поле 'ФИО директора' обязательно для заполнения.", ButtonEnum.Ok).ShowAsync();
                return; 
            }

            if (string.IsNullOrWhiteSpace(NewPartner.Phone))
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Поле 'Телефон' обязательно для заполнения.", ButtonEnum.Ok).ShowAsync();
                return; 
            }

            if (string.IsNullOrWhiteSpace(NewPartner.Email))
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Поле 'Email' обязательно для заполнения.", ButtonEnum.Ok).ShowAsync();
                return; 
            }

            // Сохраняем текущее состояние объекта
            var originalPartner = new Partner
            {
                Id = NewPartner.Id,
                PartnerName = NewPartner.PartnerName,
                Rating = NewPartner.Rating,
                LegalAddress = NewPartner.LegalAddress,
                DirectorName = NewPartner.DirectorName,
                Phone = NewPartner.Phone,
                Email = NewPartner.Email,
                PartnerType = NewPartner.PartnerType
            };

            ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Подтверждение", "Вы хотите сохранить изменения?", ButtonEnum.YesNo).ShowAsync();

            if (result == ButtonResult.Yes)
            {
                if (NewPartner.Id == 0)
                {
                    MainWindowViewModel.myConnection.Partners.Add(NewPartner);
                }
                MainWindowViewModel.myConnection.SaveChanges();
                await MessageBoxManager.GetMessageBoxStandard("Успех", "Изменения успешно сохранены.", ButtonEnum.Ok).ShowAsync();

                MainWindowViewModel.Instance.PageContent = new Show();
            }
            else if (result == ButtonResult.No)
            {
                var entry = MainWindowViewModel.myConnection.Entry(NewPartner);
                entry.CurrentValues.SetValues(originalPartner);

                MainWindowViewModel.Instance.PageContent = new Show();
            }
        }


        public void Exit()
        {
            MainWindowViewModel.Instance.PageContent = new Show();
        }
    }
}
