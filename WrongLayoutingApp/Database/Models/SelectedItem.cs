using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WrongLayoutingApp.DatabaseModels
{
    public class SelectedItem : INotifyPropertyChanged
    {
        public int Id { get; set; }
        
        public int ItemId { get; set; }




        private Item _item = new Item();
        
        public Item Item
        {
            get => _item;
            set
            {
                if (_item != value)
                {
                    _item = value;
                    ItemId = _item.Id;

                    NotifyPropertyChanged(nameof(Item));
                }
            }
        }

        private bool _isSelected = false;
        public bool IsSelected 
        { 
            get => _isSelected;
            set 
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    NotifyPropertyChanged(nameof(IsSelected));
                }
            }
        }

        private int _min = 0;
        public int Min
        {
            get => _min;
            set
            {
                _min = value;
                NotifyPropertyChanged(nameof(IsValid));
            }
        }

        private int _max = 0;
        public int Max
        {
            get => _max;
            set
            {
                _max = value;
                NotifyPropertyChanged(nameof(IsValid));
            }
        }

        private int _optimalAmount = 0;
        public int OptimalAmount
        {
            get => _optimalAmount;
            set
            {
                _optimalAmount = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsValid { get => Max >= Min; }



        #region PropertyChanged Event

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion                
    }
}
