using DevExpress.Mvvm.POCO;

namespace Aster.Desktop.Main.ViewModels
{
    public class MainViewModel
    {
        public static MainViewModel Create()
        {
            return ViewModelSource.Create(() => new MainViewModel());
        }
    }
}
