using Aster.Framework.Common.Data.Core;
using Aster.Framework.Common.Data.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aster.Entity.Entities
{
    [TableName("t_employee")]
    public class Employee:IEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// 身份证名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdCardNo { get; set; }

        /// <summary>
        /// 身份证地址
        /// </summary>
        public string Addr { get; set; }

        /// <summary>
        /// 居住地址
        /// </summary>
        public string LiveAddr { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>

        public long Phone { get; set; }

        /// <summary>
        /// 默认时薪
        /// </summary>
        public double DefaultPrice { get; set; }

        /// <summary>
        /// 信用等级
        /// </summary>
        public int CreditLevel { get; set; }

        /// <summary>
        /// 干活努力程度
        /// </summary>
        public int DiligentLevel { get; set; }

        /// <summary>
        /// 身份证正面
        /// </summary>
        public string IdCardPictureUp { get; set; }

        /// <summary>
        /// 身份证反面
        /// </summary>
        public string IdCardPictureDown { get; set; }

    }
}
