using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using Aster.Desktop.Common;
using System;
using Aster.Entity.Entities;
using Aster.Framework.Common.Data.Core.Repositories;
using Aster.Framework.Common.Data.Repositories;
using Aster.Framework.Common.Data.Core.Sessions;
using Aster.Framework.Common.Data.Sql;
using Aster.Framework.Common.Data.Implementor;
using Aster.Framework.Common.Data.Core.Configuration;

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