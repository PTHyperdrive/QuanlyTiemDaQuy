using CommunityToolkit.Mvvm.Messaging.Messages;
using QuanLyTiemDaQuy.Core.Models;

namespace QuanLyTiemDaQuy.Maui.Messages
{
    public class CustomerSelectedMessage : ValueChangedMessage<Customer>
    {
        public CustomerSelectedMessage(Customer customer) : base(customer)
        {
        }
    }
}
