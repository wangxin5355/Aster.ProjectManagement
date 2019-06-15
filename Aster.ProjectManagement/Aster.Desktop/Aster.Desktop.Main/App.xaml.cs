using DevExpress.Mvvm;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using Aster.Desktop.Common;
using Aster.Desktop.Main.Properties;
using Aster.Desktop.Main.ViewModels;
using Aster.Desktop.Main.Views;
using Aster.Desktop.Modules.ViewModels;
using Aster.Desktop.Modules.Views;
using System.ComponentModel;
using System.Windows;
using AppModules = Aster.Desktop.Common.Modules;

namespace Aster.Desktop.Main
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ApplicationThemeHelper.UpdateApplicationThemeName();
            new Bootstrapper().Run();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            ApplicationThemeHelper.SaveApplicationThemeName();
            base.OnExit(e);
        }
    }
    public class Bootstrapper
    {
        const string StateVersion = "1.0";
        public virtual void Run()
        {
            ConfigureTypeLocators();
            RegisterModules();
            if (!RestoreState())
                InjectModules();
            ConfigureNavigation();
            ShowMainWindow();
        }

        protected IModuleManager Manager { get { return ModuleManager.DefaultManager; } }
        protected virtual void ConfigureTypeLocators()
        {
            var mainAssembly = typeof(MainViewModel).Assembly;
            var modulesAssembly = typeof(ProjectsManagementViewModel).Assembly;
            var assemblies = new[] { mainAssembly, modulesAssembly };
            ViewModelLocator.Default = new ViewModelLocator(assemblies);
            ViewLocator.Default = new ViewLocator(assemblies);
        }
        protected virtual void RegisterModules()
        {
            Manager.Register(Regions.MainWindow, new Module(AppModules.Main, MainViewModel.Create, typeof(MainView)));
            //配置导航项
            Manager.Register(Regions.Navigation_ProjectsManagement, new Module(AppModules.ProjectsManagement, () => new NavigationItem("项目管理")));

            Manager.Register(Regions.Navigation_UsesManagement, new Module(AppModules.UsersManagement, () => new NavigationItem("人员管理")));

            Manager.Register(Regions.Navigation_UsesManagement, new Module(AppModules.OutsourcingGroupManagement, () => new NavigationItem("外包组")));

            Manager.Register(Regions.Navigation_SettlementManagement, new Module(AppModules.SettlementManagement, () => new NavigationItem("结算管理")));

            Manager.Register(Regions.Documents, new Module(AppModules.ProjectsManagement, () => ProjectsManagementViewModel.Create("项目管理", "项目管理模块"), typeof(ProjectsManagementView)));
            Manager.Register(Regions.Documents, new Module(AppModules.UsersManagement, () => UsersManagementViewModel.Create("人员管理", "人员管理模块"), typeof(UsersManagementView)));
            Manager.Register(Regions.Documents, new Module(AppModules.OutsourcingGroupManagement, () => UsersManagementViewModel.Create("外包组管理", "人员管理外包组管理"), typeof(UsersManagementView)));
            Manager.Register(Regions.Documents, new Module(AppModules.SettlementManagement, () => SettlementManagementViewModel.Create("结算管理", "结算管理模块"), typeof(SettlementManagementView)));
        }
        protected virtual bool RestoreState()
        {
#if !DEBUG
            if (Settings.Default.StateVersion != StateVersion) return false;
            return Manager.Restore(Settings.Default.LogicalState, Settings.Default.VisualState);
#else
            return false;
#endif
        }
        protected virtual void InjectModules()
        {
            Manager.Inject(Regions.MainWindow, AppModules.Main);
            Manager.Inject(Regions.Navigation_ProjectsManagement, AppModules.ProjectsManagement);
            Manager.Inject(Regions.Navigation_UsesManagement, AppModules.UsersManagement);
            Manager.Inject(Regions.Navigation_UsesManagement, AppModules.OutsourcingGroupManagement);
            Manager.Inject(Regions.Navigation_SettlementManagement, AppModules.SettlementManagement);
        }
        protected virtual void ConfigureNavigation()
        {
            Manager.GetEvents(Regions.Navigation_ProjectsManagement).Navigation += OnNavigation;
            Manager.GetEvents(Regions.Navigation_UsesManagement).Navigation += OnNavigation1;
            Manager.GetEvents(Regions.Navigation_SettlementManagement).Navigation += OnNavigation2;
            Manager.GetEvents(Regions.Documents).Navigation += OnDocumentsNavigation;
        }
        protected virtual void ShowMainWindow()
        {
            App.Current.MainWindow = new MainWindow();
            App.Current.MainWindow.Show();
            App.Current.MainWindow.Closing += OnClosing;
        }
        void OnNavigation(object sender, NavigationEventArgs e)//项目管理导航事件
        {
            if (e.NewViewModelKey == null) return;
            Manager.InjectOrNavigate(Regions.Documents, e.NewViewModelKey);
        }
        void OnNavigation1(object sender, NavigationEventArgs e)//项目管理导航事件
        {
            if (e.NewViewModelKey == null) return;
            Manager.InjectOrNavigate(Regions.Documents, e.NewViewModelKey);
        }
        void OnNavigation2(object sender, NavigationEventArgs e)//项目管理导航事件
        {
            if (e.NewViewModelKey == null) return;
            Manager.InjectOrNavigate(Regions.Documents, e.NewViewModelKey);
        }


        void OnDocumentsNavigation(object sender, NavigationEventArgs e)
        {
            Manager.Navigate(Regions.Navigation_ProjectsManagement, e.NewViewModelKey);
        }



        void OnClosing(object sender, CancelEventArgs e)
        {
            string logicalState;
            string visualState;
            Manager.Save(out logicalState, out visualState);
            Settings.Default.StateVersion = StateVersion;
            Settings.Default.LogicalState = logicalState;
            Settings.Default.VisualState = visualState;
            Settings.Default.Save();
        }
    }
}