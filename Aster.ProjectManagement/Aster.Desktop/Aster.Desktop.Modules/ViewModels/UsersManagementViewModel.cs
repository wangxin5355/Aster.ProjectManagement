using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using Aster.Desktop.Common;
using System;
using Aster.Entity.Entities;
using Aster.Common.Data.Core.Repositories;
using Aster.Common.Data.Repositories;
using Aster.Common.Data.Core.Sessions;
using Aster.Common.Data.Sql;
using Aster.Common.Data.Implementor;
using Aster.Common.Data.Core.Configuration;

namespace Aster.Desktop.Modules.ViewModels
{

    public class UsersManagementViewModel : IDocumentModule, ISupportState<UsersManagementViewModel.Info>
    {
        public virtual string Caption { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string Content { get; set; }

        public static UsersManagementViewModel Create(string caption, string content)
        {
            return ViewModelSource.Create(() => new UsersManagementViewModel()
            {
                Caption = caption,
                Content = content
            });
        
        }
        protected UsersManagementViewModel() {

        }

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