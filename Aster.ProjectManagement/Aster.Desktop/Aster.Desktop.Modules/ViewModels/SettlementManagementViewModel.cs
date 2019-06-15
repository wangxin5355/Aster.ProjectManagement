using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using Aster.Desktop.Common;
using System;
namespace Aster.Desktop.Modules.ViewModels
{

    public class SettlementManagementViewModel : IDocumentModule, ISupportState<SettlementManagementViewModel.Info>
    {

        public virtual string Caption { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string Content { get; set; }

        public static SettlementManagementViewModel Create(string caption, string content)
        {
            return ViewModelSource.Create(() => new SettlementManagementViewModel()
            {
                Caption = caption,
                Content = content
            });
        }
        protected SettlementManagementViewModel() { }

        #region Serialization
        [Serializable]
        public class Info
        {
            public string Content { get; set; }
            public string Caption { get; set; }
        }
        Info ISupportState<Info>.SaveState()
        {
            return new Info()
            {
                Content = this.Content,
                Caption = this.Caption,
            };
        }
        void ISupportState<Info>.RestoreState(Info state)
        {
            this.Content = state.Content;
            this.Caption = state.Caption;
        }
        #endregion
    }
}