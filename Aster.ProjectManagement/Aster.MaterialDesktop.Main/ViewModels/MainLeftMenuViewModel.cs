using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aster.MaterialDesktop.Main.ViewModels
{
    public class MainLeftMenuViewModel: BindableBase
    {
        public DelegateCommand<string> MenuExecuteDelegateCommand { get; private set; }

        private ObservableCollection<ModuleGroups> _moduleGroups ;
        public ObservableCollection<ModuleGroups> ModuleGroups
        {
            get { return _moduleGroups; }
            set { SetProperty(ref _moduleGroups, value); }
        }
        public MainLeftMenuViewModel()
        {
            MenuExecuteDelegateCommand = new DelegateCommand<string>(Execute);
            Init();
        }

        private void Execute(string parameter)
        {
            switch (parameter)
            {
                case "":

                    //通过pub Command 发送
                    break;



            }

        }
        private void Init()
        {
            _moduleGroups = new ObservableCollection<ModuleGroups>();
            ObservableCollection<Menu> menus = new ObservableCollection<Menu>();
            Menu menu1 = new Menu
            {
                MenuName = "菜单1",
                MenuCaption = "菜单1"
            };
            Menu menu2 = new Menu
            {
                MenuName = "菜单2",
                MenuCaption = "菜单2"
            };
            menus.Add(menu1);
            menus.Add(menu2);

            ModuleGroups moduleGroups1 = new ModuleGroups();
            moduleGroups1.GroupName = "模块1";
            moduleGroups1.GroupCaption = "模块1";
            moduleGroups1.Menus = menus;
            _moduleGroups.Add(moduleGroups1);
            ModuleGroups moduleGroups2 = new ModuleGroups();
            moduleGroups2.GroupName = "模块2";
            moduleGroups2.GroupCaption = "模块2";
            moduleGroups2.Menus = menus;
            _moduleGroups.Add(moduleGroups2);
        }
       


    }

    /// <summary>
    /// 模块组，大功能一个模块
    /// </summary>
    public class ModuleGroups : BindableBase
    {
        public string GroupName { get; set; }

        public string GroupCaption { get; set; }
        private ObservableCollection<Menu> _menus;
        public ObservableCollection<Menu> Menus
        {
            get { return _menus; }
            set { SetProperty(ref _menus, value); }
        }
    }

    /// <summary>
    /// 功能菜单
    /// </summary>
    public class Menu
    {
        public string MenuName { get; set; }

        public string MenuCaption { get; set; }


    }
}
